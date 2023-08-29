using HandyControl.Controls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
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
using System.Windows.Controls.Primitives;

namespace Hi7.UsercontrolHi7
{
    /// <summary>
    /// Interaction logic for UserControlDatebox.xaml
    /// </summary>
    public class MonthItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public partial class UserControlDatebox : UserControl
    {
        public static List<string> autocompleteListDay;
        public static List<string> autocompleteListYear;
        public static List<MonthItem> autocompleteListMonth;
        public static string selectedMonthIndex;
        public static string hi7Date;
        public static string hi7day,hi7month,hi7year;
        public string datenow;

        //กำหนดสี
        public static readonly DependencyProperty TextBoxColorProperty = DependencyProperty.Register("TextBoxColor", typeof(Brush), typeof(UserControlDatebox), new PropertyMetadata(new SolidColorBrush(Color.FromArgb(0xFF, 0xED, 0xED, 0xED))));

        public Brush TextBoxColor
        {
            get { return (Brush)GetValue(TextBoxColorProperty); }
            set { SetValue(TextBoxColorProperty, value); }
        }
        public static readonly DependencyProperty TextBoxBorderBrushProperty =
        DependencyProperty.Register("TextBoxBorderBrush", typeof(Brush), typeof(UserControlDatebox), new PropertyMetadata(new SolidColorBrush(Color.FromArgb(0xFF, 0x88, 0x29, 0xEC))));

        public Brush TextBoxBorderBrush
        {
            get { return (Brush)GetValue(TextBoxBorderBrushProperty); }
            set { SetValue(TextBoxBorderBrushProperty, value); }
        }
        public static readonly DependencyProperty TextBoxBorderThicknessProperty =
        DependencyProperty.Register("TextBoxBorderThickness", typeof(Thickness), typeof(UserControlDatebox), new PropertyMetadata(new Thickness(0, 0, 0, 2)));

        public Thickness TextBoxBorderThickness
        {
            get { return (Thickness)GetValue(TextBoxBorderThicknessProperty); }
            set { SetValue(TextBoxBorderThicknessProperty, value); }
        }
        //Text
        public static DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(UserControlDatebox), new PropertyMetadata("00/00/0000"));

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public UserControlDatebox()
        {
           
            InitializeComponent();

            autocompleteListDay = GenerateAutocompleteListDay();
            autocompleteListMonth = GenerateAutocompleteListMonth();
            autocompleteListYear = GenerateAutocompleteListYear();
                 
            
            Closebtnday();
            Closebtnmont();
            Closebtnyear();
            txtDateThai.Text = "00/00/0000";
        }
        void GetdatetoDateThai()
        {
            string d = searchTextBoxDay.Text;
            string m = GetMonthCode(searchTextBoxMonth.Text);
            string y = searchTextBoxYear.Text;
            txtDateThai.Text = d + "/" + m + "/" + y;
        }
        public void ResetDate()
        {
            searchTextBoxDay.Text = null;
            searchTextBoxMonth.Text = null;
            searchTextBoxYear.Text = null;
        }
        private string GetMonthName(string monthCode)
        {
            // สร้างคู่ key-value สำหรับรหัสเดือนและชื่อเดือน
            Dictionary<string, string> monthNames = new Dictionary<string, string>
        {
            { "00",""},
            { "01", "ม.ค." },
            { "02", "ก.พ." },
            { "03", "มี.ค." },
            { "04", "เม.ย." },
            { "05", "พ.ค." },
            { "06", "มิ.ย." },
            { "07", "ก.ค." },
            { "08", "ส.ค." },
            { "09", "ก.ย." },
            { "10", "ต.ค." },
            { "11", "พ.ย." },
            { "12", "ธ.ค." }
        };

            // ค้นหาชื่อเดือนจากรหัสเดือนที่กำหนด
            if (monthNames.ContainsKey(monthCode))
            {
                return monthNames[monthCode];
            }

            return string.Empty;
        }
        private string GetMonthCode(string monthName)
        {
            // สร้างคู่ key-value สำหรับรหัสเดือนและชื่อเดือน
            Dictionary<string, string> monthCode = new Dictionary<string, string>
        {
            { "ม.ค.","01" },
            { "ก.พ.","02" },
            { "มี.ค.","03" },
            { "เม.ย.","04" },
            { "พ.ค.","05" },
            { "มิ.ย.","06" },
            { "ก.ค.","07" },
            { "ส.ค.","08" },
            { "ก.ย.","09" },
            { "ต.ค.","10" },
            { "พ.ย.","11" },
            { "ธ.ค.","12" }
        };
            // ค้นหาชื่อเดือนจากรหัสเดือนที่กำหนด
            if (monthCode.ContainsKey(monthName))
            {
                return monthCode[monthName];
            }

            return string.Empty;
        }
        private List<string> GenerateAutocompleteListDay()
        {
            List<string> list = new List<string>();
            for (int i = 0; i <= 31; i++)
            {
                list.Add(i.ToString("D2"));
            }
            return list;
        }

        private List<MonthItem> GenerateAutocompleteListMonth()
        {
            List<MonthItem> list = new List<MonthItem>();
            //DateTimeFormatInfo dtfi = CultureInfo.CurrentCulture.DateTimeFormat;
            list.Add(new MonthItem { Id = 1, Name = "ม.ค." });
            list.Add(new MonthItem { Id = 2, Name = "ก.พ." });
            list.Add(new MonthItem { Id = 3, Name = "มี.ค." });
            list.Add(new MonthItem { Id = 4, Name = "เม.ย." });
            list.Add(new MonthItem { Id = 5, Name = "พ.ค." });
            list.Add(new MonthItem { Id = 6, Name = "มิ.ย." });
            list.Add(new MonthItem { Id = 7, Name = "ก.ค." });
            list.Add(new MonthItem { Id = 8, Name = "ส.ค." });
            list.Add(new MonthItem { Id = 9, Name = "ก.ย." });
            list.Add(new MonthItem { Id = 10, Name = "ต.ค." });
            list.Add(new MonthItem { Id = 11, Name = "พ.ย." });
            list.Add(new MonthItem { Id = 12, Name = "ธ.ค." });
            //for (int i = 1; i <= 12; i++)
            //{
            //    string monthName = dtfi.GetAbbreviatedMonthName(i);
            //    list.Add(new MonthItem { Id = i, Name = monthName });
            //}
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
        private void searchTextBoxDay_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = searchTextBoxDay.Text;
            List<string> filteredList = autocompleteListDay
                .Where(item => item.Contains(searchText))
                .ToList();

            if (filteredList.Count > 0)
            {
                autoCompleteListBoxDay.ItemsSource = filteredList;
                popupDay.IsOpen = true;
            }
            else
            {
                popupDay.IsOpen = false;
                searchTextBoxDay.Text = "";
                CheckTextboMonth();
            }
        }
        private void searchTextBoxDay_KeyUp(object sender, KeyEventArgs e)

        {
            if (e.Key == Key.Enter)
            {

                if (autoCompleteListBoxDay.SelectedIndex >= 0)
                {
                    searchTextBoxDay.Text = autoCompleteListBoxDay.SelectedItem.ToString();
                    CheckTextboMonth();
                    searchTextBoxMonth.Focus();
                    popupDay.IsOpen = false;
                    Closebtnday();
                }
                else
                {

                    if (searchTextBoxDay.Text.Length == 2)
                    {
                        searchTextBoxDay.Text = searchTextBoxDay.Text;
                        CheckTextboMonth();
                        searchTextBoxMonth.Focus();
                        popupDay.IsOpen = false;
                        Closebtnday();
                        return;
                    }
                    else if (searchTextBoxDay.Text.Length == 1)
                    {
                        if (searchTextBoxDay.Text == "0")
                        {
                            searchTextBoxDay.Text = "00";
                            CheckTextboMonth();
                            searchTextBoxMonth.Focus();
                            popupDay.IsOpen = false;
                            Closebtnday();
                            return;
                        }
                        searchTextBoxDay.Text = "0" + searchTextBoxDay.Text;
                        CheckTextboMonth();
                        searchTextBoxMonth.Focus();
                        popupDay.IsOpen = false;
                        Closebtnday();
                        return;
                    }
                    CheckTextboMonth();

                }

            }
            else if (e.Key == Key.Down)
            {
                
                if (autoCompleteListBoxDay.Items.Count > 0)
                {
                    int selectedIndex =0;
                    
                    selectedIndex = autoCompleteListBoxDay.SelectedIndex;
                    if (selectedIndex < autoCompleteListBoxDay.Items.Count - 1)
                    {
                        autoCompleteListBoxDay.SelectedIndex = selectedIndex + 1;
                        autoCompleteListBoxDay.ScrollIntoView(autoCompleteListBoxDay.SelectedItem);
                    }
                }
                e.Handled = true;
                
            }
            else if (e.Key == Key.Up)
            {
                if (autoCompleteListBoxDay.Items.Count > 0)
                {
                    int selectedIndex = 0;
                    selectedIndex = autoCompleteListBoxDay.SelectedIndex;
                    if (selectedIndex > 0)
                    {
                        autoCompleteListBoxDay.SelectedIndex = selectedIndex - 1;
                        autoCompleteListBoxDay.ScrollIntoView(autoCompleteListBoxDay.SelectedItem);
                    }
                }
                e.Handled = true;
            }
        }



        private void autoCompleteListBoxDay_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (autoCompleteListBoxDay.SelectedItem != null)
            {
                //Closebtnday();
                CheckTextboMonth();
            }
        }
        void CheckTextboxdmy()
        {
            string d = searchTextBoxDay.Text;
            string m = searchTextBoxMonth.Text;
            string y = searchTextBoxYear.Text;

            if (string.IsNullOrEmpty(d))
            {
                d = "00";
            }

            if (string.IsNullOrEmpty(m))
            {
                m = "00";
            }

            if (string.IsNullOrEmpty(y))
            {
                y = "0000";
            }

            txtDateThai.Text = d + "/" + m + "/" + y;
        }
        void CheckTextboMonth()
        {
            string d = "";
            string m = "";
            string y = "";
            d = searchTextBoxDay.Text;
            m = searchTextBoxMonth.Text;
            y = searchTextBoxYear.Text;
            if (string.IsNullOrEmpty(d))
            {
                d = "00";
            }

            if (string.IsNullOrEmpty(m))
            {
                m = "00";
            }
            else {
                m = GetMonthCode(m);
            }

            if (string.IsNullOrEmpty(y))
            {
                y = "0000";
            }

            txtDateThai.Text = d + "/" + m + "/" + y;
            hi7Date = txtDateThai.Text;
            //System.Windows.MessageBox.Show(txtDateThai.Text);
        }
        // เดือน
        private void searchTextBoxMonth_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {

                if (autoCompleteListBoxMonth.SelectedIndex >= 0)
                {
                    searchTextBoxMonth.Text = autoCompleteListBoxMonth.SelectedItem.ToString();
                }
                try
                {
                    if (string.IsNullOrEmpty(searchTextBoxMonth.Text))
                    {
                        CheckTextboMonth();
                        return;
                    }
                    string selectedMonthName = autoCompleteListBoxMonth.SelectedItem.ToString();
                    if (string.IsNullOrEmpty(selectedMonthName))
                    {
                        CheckTextboMonth();
                        return;
                    }
                    // ค้นหา MonthItem ที่ตรงกับชื่อเดือนที่ถูกเลือก
                    MonthItem selectedMonthItem = autocompleteListMonth.FirstOrDefault(item => item.Name == selectedMonthName);

                    if (selectedMonthItem != null)
                    {
                        // ทำสิ่งที่คุณต้องการด้วย selectedMonthItem.Id
                        selectedMonthIndex = selectedMonthItem.Id.ToString();
                        if (selectedMonthIndex.Length == 1) // ตรวจสอบว่าเป็นเลขหลักเดียว
                        {
                            selectedMonthIndex = "0" + selectedMonthIndex; // เติม 0 นำหน้าเลข
                        }
                        // ...
                    }
                }
                catch
                {
                    searchTextBoxMonth.Text = "";
                    CheckTextboMonth();
                    return;
                }
                Closebtnmont();
                //string index = autoCompleteListBoxMonth.SelectedItem.ToString();
                CheckTextboMonth();
                searchTextBoxMonth.SelectAll();
                searchTextBoxYear.Focus();
                popupMonth.IsOpen = false;
            }
            else if (e.Key == Key.Down)
            {
                if (autoCompleteListBoxMonth.Items.Count > 0)
                {
                    int selectedIndex = autoCompleteListBoxMonth.SelectedIndex;
                    if (selectedIndex < autoCompleteListBoxMonth.Items.Count - 1)
                    {
                        autoCompleteListBoxMonth.SelectedIndex = selectedIndex + 1;
                        autoCompleteListBoxMonth.ScrollIntoView(autoCompleteListBoxMonth.SelectedItem);
                    }
                }
                e.Handled = true;
            }
            else if (e.Key == Key.Up)
            {
                if (autoCompleteListBoxMonth.Items.Count > 0)
                {
                    int selectedIndex = autoCompleteListBoxMonth.SelectedIndex;
                    if (selectedIndex > 0)
                    {
                        autoCompleteListBoxMonth.SelectedIndex = selectedIndex - 1;
                        autoCompleteListBoxMonth.ScrollIntoView(autoCompleteListBoxMonth.SelectedItem);
                    }
                }
                e.Handled = true;
            }
        }

        private void searchTextBoxMonth_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = searchTextBoxMonth.Text;
            List<string> filteredList = autocompleteListMonth
                .Where(item => item.Name.Contains(searchText))
                .Select(item => item.Name)
                .ToList();

            if (filteredList.Count > 0)
            {
                autoCompleteListBoxMonth.ItemsSource = filteredList;
                popupMonth.IsOpen = true;
                Closebtnmont();
            }
            else
            {
                popupMonth.IsOpen = false;
                searchTextBoxMonth.Text = "";
                CheckTextboMonth();
                Closebtnmont();
            }
        }

        private void autoCompleteListBoxMonth_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (autoCompleteListBoxMonth.SelectedItem != null)
            {
                //Closebtnmont();
                CheckTextboMonth();
            }
        }
        // ปี
        private void searchTextBoxYear_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (autoCompleteListBoxYear.SelectedIndex >= 0)
                {
                    searchTextBoxYear.Text = autoCompleteListBoxYear.SelectedItem.ToString();
                    CheckTextboMonth();
                    popupYear.IsOpen = false;
                    Closebtnyear();
                }
                else
                {
                    if (string.IsNullOrEmpty(searchTextBoxYear.Text))
                    {
                        CheckTextboMonth();
                        popupYear.IsOpen = false;
                        Closebtnyear();
                    }
                    else
                    {
                        CheckTextboMonth();
                        popupYear.IsOpen = false;
                        Closebtnyear();
                    }


                }

            }
            else if (e.Key == Key.Down)
            {
                if (autoCompleteListBoxYear.Items.Count > 0)
                {
                    int selectedIndex = autoCompleteListBoxYear.SelectedIndex;
                    if (selectedIndex < autoCompleteListBoxYear.Items.Count - 1)
                    {
                        autoCompleteListBoxYear.SelectedIndex = selectedIndex + 1;
                        autoCompleteListBoxYear.ScrollIntoView(autoCompleteListBoxYear.SelectedItem);
                    }
                }
                e.Handled = true;
            }
            else if (e.Key == Key.Up)
            {
                if (autoCompleteListBoxYear.Items.Count > 0)
                {
                    int selectedIndex = autoCompleteListBoxYear.SelectedIndex;
                    if (selectedIndex > 0)
                    {
                        autoCompleteListBoxYear.SelectedIndex = selectedIndex - 1;
                        autoCompleteListBoxYear.ScrollIntoView(autoCompleteListBoxYear.SelectedItem);
                    }
                }
                e.Handled = true;
            }
        }
        private void searchTextBoxYear_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = searchTextBoxYear.Text;
            List<string> filteredList = autocompleteListYear
                .Where(item => item.Contains(searchText))
                .ToList();

            if (filteredList.Count > 0)
            {
                autoCompleteListBoxYear.ItemsSource = filteredList;
                popupYear.IsOpen = true;
            }
            else
            {
                popupYear.IsOpen = false;
                searchTextBoxYear.Text = "";
                CheckTextboMonth();
            }
        }

        private void autoCompleteListBoxYear_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (autoCompleteListBoxYear.SelectedItem != null)
            {
                //Closebtnyear();
                CheckTextboMonth();
            }
        }

        private void btnCkickDay_Click(object sender, RoutedEventArgs e)
        {
            
            string searchText = searchTextBoxDay.Text;
            List<string> filteredList = autocompleteListDay
                .Where(item => item.StartsWith(searchText))
                .ToList();
            if (string.IsNullOrEmpty(searchTextBoxDay.Text))
            {
                autoCompleteListBoxDay.ItemsSource = filteredList;
                popupDay.IsOpen = true;
            }
            else
            {
                popupDay.IsOpen = true;
            }
        }

        private void btnCkickMonth_Click(object sender, RoutedEventArgs e)
        {
            string searchText = searchTextBoxMonth.Text;
            List<MonthItem> filteredList = autocompleteListMonth
                .Where(item => item.Name.StartsWith(searchText))
                .ToList();

            if (string.IsNullOrEmpty(searchText))
            {
                autoCompleteListBoxMonth.ItemsSource = autocompleteListMonth.Select(item => item.Name);
                popupMonth.IsOpen = true;
            }
            else
            {
                autoCompleteListBoxMonth.ItemsSource = filteredList.Select(item => item.Name);
                popupMonth.IsOpen = true;
            }
        }

        private void btnCkickYear_Click(object sender, RoutedEventArgs e)
        {
            string searchText = searchTextBoxYear.Text;
            List<string> filteredList = autocompleteListYear
                .Where(item => item.StartsWith(searchText))
                .ToList();
            if (string.IsNullOrEmpty(searchTextBoxYear.Text))
            {
                autoCompleteListBoxYear.ItemsSource = filteredList;
                popupYear.IsOpen = true;
            }
            else
            {
                autoCompleteListBoxYear.ItemsSource = filteredList;
                popupYear.IsOpen = true;
            }
        }

        private void btnClearCkickDay_Click(object sender, RoutedEventArgs e)
        {
            searchTextBoxDay.Text = null;
            CheckTextboMonth();
            Openbtnday();
            popupDay.IsOpen = false;
            searchTextBoxDay.Focus();
        }

        private void btnClearCkickMonth_Click(object sender, RoutedEventArgs e)
        {
            searchTextBoxMonth.Text = null;
            CheckTextboMonth();
            Openbtnmont();
            popupMonth.IsOpen = false;
            searchTextBoxMonth.Focus();
        }

        private void btnClearCkickYear_Click(object sender, RoutedEventArgs e)
        {
            searchTextBoxYear.Text = null;
            CheckTextboMonth();
            Openbtnyear();
            popupYear.IsOpen = false;
            searchTextBoxYear.Focus();

        }
        //ปิดลูกศร เปิดกากบาท วัน
        void Closebtnday()
        {
            btnCkickDay.Visibility = Visibility.Collapsed;
            btnClearCkickDay.Visibility = Visibility.Visible;
        }
        void Openbtnday()
        {
            btnCkickDay.Visibility = Visibility.Visible;
            btnClearCkickDay.Visibility = Visibility.Collapsed;
        }
        //ปิดลูกศร เปิดกากบาท เดือน
        void Closebtnmont()
        {
            btnCkickMonth.Visibility = Visibility.Collapsed;
            btnClearCkickMonth.Visibility = Visibility.Visible;
        }
        void Openbtnmont()
        {
            btnCkickMonth.Visibility = Visibility.Visible;
            btnClearCkickMonth.Visibility = Visibility.Collapsed;
        }
        //ปิดลูกศร เปิดกากบาท ปี
        void Closebtnyear()
        {
            btnCkickYear.Visibility = Visibility.Collapsed;
            btnClearCkickYear.Visibility = Visibility.Visible;
        }
        void Openbtnyear()
        {
            btnCkickYear.Visibility = Visibility.Visible;
            btnClearCkickYear.Visibility = Visibility.Collapsed;
        }
        private void autoCompleteListBoxDay_MouseLeftButtonUp_1(object sender, MouseButtonEventArgs e)
        {
            // ตรวจสอบว่ามีรายการที่ถูกเลือกหรือไม่
            //System.Windows.MessageBox.Show(searchTextBoxDay.Text);
            if (autoCompleteListBoxDay.SelectedItem != null)
            {
                // ดึงข้อมูลที่ถูกเลือกจาก ListBox
                string selectedData = autoCompleteListBoxDay.SelectedItem.ToString();

                // แสดง MessageBox แสดงข้อมูลที่ถูกเลือก
                searchTextBoxDay.Text = selectedData;
                CheckTextboMonth();
                popupDay.IsOpen = false;
                Closebtnday();
            }
        }

        private void autoCompleteListBoxMonth_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            // ตรวจสอบว่ามีรายการที่ถูกเลือกหรือไม่
            if (autoCompleteListBoxMonth.SelectedItem != null)
            {
                string selectedData = autoCompleteListBoxMonth.SelectedItem.ToString();
                searchTextBoxMonth.Text = selectedData;
                CheckTextboMonth();
                popupMonth.IsOpen = false;
                Closebtnmont();
            }
        }
        private void btndate_Click(object sender, RoutedEventArgs e)
        {
            popupDatetime.IsOpen = true;
            popupMonth.IsOpen = false;
            popupYear.IsOpen = false;
            popupDay.IsOpen = false;
            if (string.IsNullOrEmpty(txtDateThai.Text))
            {
                Openbtnday();
                Openbtnmont();
                Openbtnyear();
            }
            else
            {
                if (txtDateThai.Text == "00/00/0000")
                {
                    Openbtnday();
                    Openbtnmont();
                    Openbtnyear();
                }
                else
                {
                    Closebtnday();
                    Closebtnmont();
                    Closebtnyear();
                }

            }

        }
        private void txtDateThai_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtDateThai.Focus();
        }
        private void btndateClear_Click(object sender, RoutedEventArgs e)
        {
            txtDateThai.Text = "00/00/0000";
            searchTextBoxDay.Text = string.Empty;
            searchTextBoxMonth.Text= string.Empty;
            searchTextBoxYear.Text= string.Empty;
            Openbtnday();
            Openbtnmont();
            Openbtnyear();
        }
        private void autoCompleteListBoxYear_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (autoCompleteListBoxYear.SelectedItem != null)
            {
                // ดึงข้อมูลที่ถูกเลือกจาก ListBox
                string selectedData = autoCompleteListBoxYear.SelectedItem.ToString();

                // แสดง MessageBox แสดงข้อมูลที่ถูกเลือก
                searchTextBoxYear.Text = selectedData;
                CheckTextboMonth();
                popupYear.IsOpen = false;
                Closebtnyear();
            }
        }

    }
}
