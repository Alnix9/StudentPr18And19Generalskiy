using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace StudentPr18Generalskiy
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Height += 25;
        }
        StudentsEntities db = StudentsEntities.GetContext();
        ChoiceOfDiscipline p1 = new ChoiceOfDiscipline();

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Login user = new Login();
            user.ShowDialog();
            if (Data.Login == false) Close();
            if (Data.Right == "Админ     ") ;
            if(Data.Right == "Гость     ")
            {
                b1.IsEnabled = false;
                b2.IsEnabled = false;
                b3.IsEnabled = false;
                b4.IsEnabled = false;
                b5.IsEnabled = false;
                b6.IsEnabled = false;
                b7.IsEnabled = false;
                b8.IsEnabled = false;
            }
            if(Data.Right == "Клиент")
            {
                b1.IsEnabled = false;
                b2.IsEnabled = false;
                b3.IsEnabled = false;
                b4.IsEnabled = false;
                b5.IsEnabled = false;
                b6.IsEnabled = false;
                b7.IsEnabled = false;
                b8.IsEnabled = false;
                MenuBt1.IsEnabled = false;
                MenuBt2.IsEnabled = false;
                MenuBt3.IsEnabled = false;  
                MenuBt4.IsEnabled = false;
            }
            else { }

            Title = Data.Familia + " " + Data.Name + " " + Data.Otchestvo + " - " + Data.Right;
            db.ChoiceOfDisciplines.Load();
            DataGridStudents.ItemsSource = db.ChoiceOfDisciplines.Local.ToBindingList();
        }
        private void AddRecord(object sender, RoutedEventArgs e)
        {
            AddRecord f = new AddRecord();
            f.ShowDialog();
            DataGridStudents.Focus();
        }

        private void EditRecord(object sender, RoutedEventArgs e)
        {
            int IndexRow = DataGridStudents.SelectedIndex;
            if (IndexRow != -1)
            {
                ChoiceOfDiscipline row = (ChoiceOfDiscipline)DataGridStudents.Items[IndexRow];
                Data.Id = Convert.ToInt32(row.Номер_зачётной_книжки);
                EditRecord f = new EditRecord();
                f.ShowDialog();
                DataGridStudents.Items.Refresh();
                DataGridStudents.Focus();
            }
        }

        private void ViewRecord(object sender, RoutedEventArgs e)
        {
            var gen = db.Database.SqlQuery<StudentsEntities>("SELECT * FROM Students WHERE Общежитие = 'Да' ");
            DataGridStudents.ItemsSource = gen.ToList();
        }

        private void DeleteRecord(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result;
            result = MessageBox.Show("Удалить запись?", "Удаление записи",
            MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    ChoiceOfDiscipline row = (ChoiceOfDiscipline)DataGridStudents.SelectedItems[0];
                    db.ChoiceOfDisciplines.Remove(row);
                    db.SaveChanges();
                }
                catch (ArgumentOutOfRangeException)
                {
                    MessageBox.Show("Выберите запись");
                }
            }
        }
        List<ChoiceOfDiscipline> _choiceOfDisciplines;
        private void Find(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < DataGridStudents.Items.Count; i++)
            {
                var row = (ChoiceOfDiscipline)DataGridStudents.Items[i];
                string findContent = row.Фамилия;
                try
                {
                    if (findContent != null && findContent.Contains(tbFindText.Text))
                    {
                        object item = DataGridStudents.Items[i];
                        DataGridStudents.SelectedItem = item;
                        DataGridStudents.ScrollIntoView(item);
                        DataGridStudents.Focus();
                        break;
                    }
                }
                catch
                {
                }
            }
        }
        private void Filtered(object sender, RoutedEventArgs e)
        {
            _choiceOfDisciplines = db.ChoiceOfDisciplines.ToList();
            var filtered = _choiceOfDisciplines.Where(_ассортимент_Обуви => _ассортимент_Обуви.Имя == tbFiltered.Text);
            DataGridStudents.ItemsSource = filtered;
        }


        private void LiveInHostel(object sender, RoutedEventArgs e)
        {
            var Общежитие = from p1 in db.ChoiceOfDisciplines
                               where p1.Общежитие.StartsWith("Д")
                               select p1;
            DataGridStudents.ItemsSource = Общежитие.ToList();
        }

        private void Geography(object sender, RoutedEventArgs e)
        {
            var География = from p1 in db.ChoiceOfDisciplines
                            where p1.География == 1
                            select p1;
            DataGridStudents.ItemsSource = География.ToList();
        }
        private void Update(object sender, RoutedEventArgs e)
        {
            db.Database.ExecuteSqlCommand("UPDATE ChoiceOfDiscipline SET Химия=1 WHERE Химия=0");
            db.ChangeTracker.Entries().ToList().ForEach(p1 => p1.Reload());
        }
        private void DeleteLast(object sender, RoutedEventArgs e)
        {
            int Last = db.Database.ExecuteSqlCommand("DELETE FROM ChoiceOfDiscipline WHERE [Номер зачётной книжки]=240");
            db.ChangeTracker.Entries().ToList().ForEach(p1 => p1.Reload());
        }

        private void CancelFiltered(object sender, RoutedEventArgs e)
        {
            _choiceOfDisciplines = db.ChoiceOfDisciplines.ToList();
            DataGridStudents.ItemsSource = _choiceOfDisciplines;
        }

        private void About_Program(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("1. Разработать интерфейс для доступа и управления однотабличной базой данных. " +
                "2. Создать меню." +
                "3. Использовать кнопки для функций Добавить, Изменить, Просмотр, Удалить." +
                "4. Реализовать функции добавить, изменить, просмотр с помощью окна – бланка." +
                "5. Реализовать функцию удалить текущую запись." +
                "6. SQL Запросы к базе данных, выбор параметров запроса:a. Запрос на выборку LINQ (2-5 штуки)b. Запрос на обновление (1-2 штуки) c. Запрос на удаление (1-2 штуки)" +
                "7. Исключения" +
                "8. Заполненная БД 15 -20 записей");
        }
    }
}
