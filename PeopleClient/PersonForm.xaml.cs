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

namespace PeopleClient
{
    /// <summary>
    /// Interaction logic for PersonForm.xaml
    /// </summary>
    public partial class PersonForm : Window
    {
        PersonData service = new PersonData();
        People person;
        public PersonForm()
        {
            InitializeComponent();
        }
        public PersonForm(People person)
        {
            InitializeComponent();
            this.person = person;
            LoadPerson();
            addButton.Visibility = Visibility.Collapsed;
            updateButton.Visibility = Visibility.Visible;
        }

        private void LoadPerson()
        {
            nameInput.Text = this.person.Name;
            SportInput.IsChecked = this.person.Sports;
            CVVInput.Text = this.person.CVV.ToString();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                People person = CreatePersonFromInputFields();
                People newPerson = service.Add(person);
                if (newPerson.Id != 0)
                {
                    MessageBox.Show("Sikeres felvétel");
                    nameInput.Text = "";
                    SportInput.IsChecked = false;
                    CVVInput.Text = "";
                }
                else
                {
                    MessageBox.Show("Hiba történt a felvétel során!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                People person = CreatePersonFromInputFields();
                People updated = service.Update(this.person.Id, person);
                if (updated.Id != 0)
                {
                    MessageBox.Show("Sikeres módosítás!");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Hiba történt a módosítás során!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private People CreatePersonFromInputFields()
        {
            string name = nameInput.Text.Trim();
            bool sport = (bool)SportInput.IsChecked;
            string CVV = CVVInput.Text.Trim();

            if (string.IsNullOrEmpty(name))
            {
                throw new Exception("Név kitöltése kötelező!");
            }

            if (string.IsNullOrEmpty(CVV))
            {
                throw new Exception("CVV kitöltése kötelező!");
            }

            if (!int.TryParse(CVV, out int cvv))
            {
                throw new Exception("A CVV csak szám lehet!");
            }

            if (cvv > 1000)
            {
                throw new Exception("A CVV nem lehet 999 fölött!");
            }

            People person = new People();
            person.Name = name;
            person.Sports = sport;
            person.CVV = cvv;
            return person;
        }
    }
}
