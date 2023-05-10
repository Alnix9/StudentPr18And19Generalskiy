using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace StudentPr18Generalskiy
{
    /// <summary>
    /// Логика взаимодействия для EditRecord.xaml
    /// </summary>
    public partial class EditRecord : Window
    {
        public EditRecord()
        {
            InitializeComponent();
            Height += 25;
        }
        StudentsEntities db = StudentsEntities.GetContext();
        ChoiceOfDiscipline p1 = new ChoiceOfDiscipline();
        private void Button_Edit(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (tbSecondName.Text.Length == 0) errors.AppendLine("Введите фамилию");
            if (tbFirstName.Text.Length == 0) errors.AppendLine("Введите имя");
            if (tbThirdName.Text.Length == 0) errors.AppendLine("Введите отчество");
            if (tbNumberOfBook.Text.Length == 0) errors.AppendLine("Введите номер зачётной книжки");
            if (CBHostel.Text.Length == 0) errors.AppendLine("Введите живёт студент в общежитие или нет");
            if (tbIndexOfGroup.Text.Length == 0) errors.AppendLine("Введите индекс группы");
            if (CBGeography.Text.Length == 0) errors.AppendLine("Введите 0 или 1");
            if (CBMathematics.Text.Length == 0) errors.AppendLine("Введите 0 или 1");
            if (CBBiology.Text.Length == 0) errors.AppendLine("Введите 0 или 1");
            if (CBPhysics.Text.Length == 0) errors.AppendLine("Введите 0 или 1");
            if (CBChemistry.Text.Length == 0) errors.AppendLine("Введите 0 или 1");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            p1.Фамилия = tbSecondName.Text;
            p1.Имя = tbFirstName.Text;
            p1.Отчество = tbThirdName.Text;
            p1.Номер_зачётной_книжки = Convert.ToInt32(tbNumberOfBook.Text);
            p1.Общежитие = CBHostel.Text;
            p1.Индекс_группы = Convert.ToInt32(tbIndexOfGroup.Text);
            p1.География = Convert.ToInt32(CBGeography.Text);
            p1.Математика = Convert.ToInt32(CBMathematics.Text);
            p1.Биология = Convert.ToInt32(CBBiology.Text);
            p1.Физика = Convert.ToInt32(CBPhysics.Text);
            p1.Химия = Convert.ToInt32(CBChemistry.Text);

            try
            {
                db.SaveChanges();
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            p1 = db.ChoiceOfDisciplines.Find(Data.Id);
            tbSecondName.Text = p1.Фамилия;
            tbFirstName.Text = p1.Имя;
            tbThirdName.Text = p1.Отчество;
            tbNumberOfBook.Text = Convert.ToString(p1.Номер_зачётной_книжки);
            CBHostel.Text = p1.Общежитие;
            tbIndexOfGroup.Text = Convert.ToString(p1.Индекс_группы);
            CBGeography.Text = Convert.ToString(p1.География);
            CBMathematics.Text = Convert.ToString(p1.Математика);
            CBBiology.Text = Convert.ToString(p1.Биология);
            CBPhysics.Text = Convert.ToString(p1.Физика);
            CBChemistry.Text = Convert.ToString(p1.Химия);
        }

        private void Button_Cancel(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
