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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Mdr.UserControl
{
    /// <summary>
    /// Interaction logic for UserControlDateHi7.xaml
    /// </summary>
    public partial class UserControlDateHi7
    {
        private List<string> autocompleteListDay;
        private List<string> autocompleteListMonth;
        private List<string> autocompleteListYear;
        public UserControlDateHi7()
        {
            InitializeComponent();
            autocompleteListDay = GenerateAutocompleteListDay();
            autocompleteListMonth = GenerateAutocompleteListMonth();
            autocompleteListYear = GenerateAutocompleteListYear();
        }
        private List<string> GenerateAutocompleteListDay()
        {
            List<string> list = new List<string>();
            for (int i = 1; i <= 31; i++)
            {
                list.Add(i.ToString("D2"));
            }
            return list;
        }
        private List<string> GenerateAutocompleteListMonth()
        {
            List<string> list = new List<string>();
            for (int i = 1; i <= 12; i++)
            {
                list.Add(i.ToString("D2"));
            }
            return list;
        }
        private List<string> GenerateAutocompleteListYear()
        {
            List<string> list = new List<string>();
            for (int i = 2400; i <= 2600; i++)
            {
                list.Add(i.ToString());
            }
            return list;
        }
        //วัน
        private void searchTextBoxDay_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = searchTextBoxDay.Text;
            List<string> filteredList = autocompleteListDay
                .Where(item => item.StartsWith(searchText))
                .ToList();

            if (filteredList.Count > 0)
            {
                autoCompleteListBoxDay.ItemsSource = filteredList;
                autoCompleteListBoxDay.Visibility = Visibility.Visible;
            }
            else
            {
                autoCompleteListBoxDay.Visibility = Visibility.Collapsed;
            }
        }

        private void searchTextBoxDay_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                // Perform search or selection logic here
                string selectedText = searchTextBoxDay.Text;
                // ...
            }
        }

        private void autoCompleteListBoxDay_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (autoCompleteListBoxDay.SelectedItem != null)
            {
                searchTextBoxDay.Text = autoCompleteListBoxDay.SelectedItem.ToString();
                autoCompleteListBoxDay.Visibility = Visibility.Collapsed;
                searchTextBoxDay.Focus();
                searchTextBoxDay.SelectAll();
            }
        }
        //เดือน
        private void searchTextBoxMonth_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                // Perform search or selection logic here
                string selectedText = searchTextBoxMonth.Text;
                // ...
            }
        }

        private void searchTextBoxMonth_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = searchTextBoxMonth.Text;
            List<string> filteredList = autocompleteListMonth
                .Where(item => item.StartsWith(searchText))
                .ToList();

            if (filteredList.Count > 0)
            {
                autoCompleteListBoxMonth.ItemsSource = filteredList;
                autoCompleteListBoxMonth.Visibility = Visibility.Visible;
            }
            else
            {
                autoCompleteListBoxMonth.Visibility = Visibility.Collapsed;
            }
        }

        private void autoCompleteListBoxMonth_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (autoCompleteListBoxMonth.SelectedItem != null)
            {
                searchTextBoxMonth.Text = autoCompleteListBoxMonth.SelectedItem.ToString();
                autoCompleteListBoxMonth.Visibility = Visibility.Collapsed;
                searchTextBoxMonth.Focus();
                searchTextBoxMonth.SelectAll();
            }
        }
        //ปี
        private void searchTextBoxYear_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                // Perform search or selection logic here
                string selectedText = searchTextBoxYear.Text;
                // ...
            }
        }

        private void searchTextBoxYear_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = searchTextBoxYear.Text;
            List<string> filteredList = autocompleteListYear
                .Where(item => item.StartsWith(searchText))
                .ToList();

            if (filteredList.Count > 0)
            {
                autoCompleteListBoxYear.ItemsSource = filteredList;
                autoCompleteListBoxYear.Visibility = Visibility.Visible;
            }
            else
            {
                autoCompleteListBoxYear.Visibility = Visibility.Collapsed;
            }
        }

        private void autoCompleteListBoxYear_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (autoCompleteListBoxYear.SelectedItem != null)
            {
                searchTextBoxYear.Text = autoCompleteListBoxYear.SelectedItem.ToString();
                autoCompleteListBoxYear.Visibility = Visibility.Collapsed;
                searchTextBoxYear.Focus();
                searchTextBoxYear.SelectAll();
            }
        }
    }
}
