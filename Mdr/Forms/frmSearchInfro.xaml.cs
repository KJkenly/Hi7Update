using HandyControl.Controls;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Mdr.Forms
{
    /// <summary>
    /// Interaction logic for frmSearchInfro.xaml
    /// </summary>
    public partial class frmSearchInfro : System.Windows.Window
    {
        public frmSearchInfro()
        {
            InitializeComponent();
        }
        private void OpenAutoSuggestionBox()
        {
            try
            {
                // Enable.  
                //this.autoListPopup.Visibility = Visibility.Visible;
                //this.autoListPopup.IsOpen = true;
                this.autoList.Visibility = Visibility.Visible;
            }
            catch (Exception ex)
            {
                // Info.  
                System.Windows.MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Console.Write(ex);
            }
        }

        private void CloseAutoSuggestionBox()
        {
            try
            {
                this.autoList.Visibility = Visibility.Collapsed;
            }
            catch (Exception ex)
            {
                // Info.  
                System.Windows.MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Console.Write(ex);
            }
        }
        private void ClickClose(object sender, RoutedEventArgs e)
        {
            this.Close();
            frmMdr.statusOpenfroms = "Y";
        }

        private void ClickMin(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;

        }

        //กำหนดค่า dt เป็น DataTable
        DataTable dt = new System.Data.DataTable();
        void getSearch()
        {
            try
            {
                dt = HI7.Class.HIUility.getSearchInfor(HI7.Class.HIUility._TXTSEARCH);
                System.Data.DataView view = new System.Data.DataView(dt);
                System.Data.DataTable selected = view.ToTable("Selected", false);
                this.autoList.ItemsSource = selected.DefaultView;

            }
            catch (Exception ex)
            {

            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Hi7.Class.APIConnect.getConfgXML();
            //Focus
            Keyboard.Focus(this.autoTextBox);
            //Show
            autoTextBox.Text = HI7.Class.HIUility._TXTSEARCH;
            autoTextBox.Select(HI7.Class.HIUility._TXTSEARCH.Length, 0);
            getSearch();


        }


        private void autoTextBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            string x = autoTextBox.ToString().Substring(1, 1);
            string cCkhLength = autoTextBox.Text;
            int n;
            bool isNumeric = int.TryParse(x, out n);
            if (cCkhLength.Length == 1)
            {
                try
                {
                    //Enable.
                    this.OpenAutoSuggestionBox();
                    System.Data.DataView view = new System.Data.DataView(dt);
                    DataTable selected = view.ToTable("Selected", false).Select("fname like '" + this.autoTextBox.Text + "%'").CopyToDataTable();
                    this.autoList.ItemsSource = selected.DefaultView;
                    //moveDown();
                }
                catch (Exception ex)
                {
                    //Info.
                    HI7.Class.HIUility._TXTSEARCH = autoTextBox.Text;
                    getSearch();
                }
            }
            else
            {
                if (isNumeric)
                {
                    try
                    {

                        //Enable.
                        this.OpenAutoSuggestionBox();

                        System.Data.DataView view = new System.Data.DataView(dt);
                        DataTable selected = view.ToTable("Selected", false).Select("pop_id like '" + this.autoTextBox.Text + "%'").CopyToDataTable();
                        this.autoList.ItemsSource = selected.DefaultView;
                        //moveDown();
                    }
                    catch (Exception ex)
                    {
                        //Info.
                        HI7.Class.HIUility._TXTSEARCH = autoTextBox.Text;
                        getSearch();
                    }

                }
                else
                {
                    try
                    {

                        //Enable.
                        this.OpenAutoSuggestionBox();

                        System.Data.DataView view = new System.Data.DataView(dt);
                        DataTable selected = view.ToTable("Selected", false).Select("fullname like '" + this.autoTextBox.Text + "%'").CopyToDataTable();
                        this.autoList.ItemsSource = selected.DefaultView;
                        //moveDown();
                    }
                    catch (Exception ex)
                    {
                        //Info.
                        HI7.Class.HIUility._TXTSEARCH = autoTextBox.Text;
                        getSearch();
                    }
                }

            }


        }

        private void moveUp()
        {

            //เก็บค่า Record - 1 จะได้ค่า Array เพื่อใช้แสดงใน Grid
            int aRecno = autoList.Items.Count - 1;

            if (autoList.SelectedIndex <= 0)
            {
                this.autoList.SelectedIndex = aRecno * 0;
                object item = autoList.Items[aRecno * 0];
                autoList.SelectedItem = item;
                Keyboard.Focus(this.autoList);
                this.autoList.ScrollIntoView(item);

            }
            else
            {
                this.autoList.SelectedIndex = autoList.SelectedIndex - 1;
                object item = autoList.Items[autoList.SelectedIndex];
                autoList.SelectedItem = item;
                Keyboard.Focus(this.autoList);
                this.autoList.ScrollIntoView(item);
            }


        }
        private void moveDown()
        {
            //เก็บค่า Record - 1 จะได้ค่า Array เพื่อใช้แสดงใน Grid
            int aRecno = autoList.Items.Count - 1;
            //ต้องน้อยกว่า 0 ถึงจะเลื่อนลงได้
            if (autoList.SelectedIndex < 0)
            {
                this.autoList.SelectedIndex = aRecno * 0;
                object item = autoList.Items[aRecno * 0];
                autoList.SelectedItem = item;
                Keyboard.Focus(this.autoList);
                this.autoList.ScrollIntoView(item);

            }
            else
            {
                this.autoList.SelectedIndex = autoList.SelectedIndex + 1;
                object item = autoList.Items[autoList.SelectedIndex];
                autoList.SelectedItem = item;
                Keyboard.Focus(this.autoList);
                this.autoList.ScrollIntoView(item);
            }
        }

        Int16 gindex = 0;
        private void autoList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key.Equals(Key.Up))
            {
                Keyboard.Focus(this.autoList);
                moveUp();
            }
            if (e.Key.Equals(Key.Down))
            {
                Keyboard.Focus(this.autoList);
                moveDown();
            }
            e.Handled = true;
        }

        private void autoTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Down)
            {
                Keyboard.Focus(this.autoList);
                moveDown();
            }
        }

        private void autoTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Up || e.Key == Key.Down)
            {
                autoList.Focus();
            }
        }

        private void autoList_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                try
                {
                    DataRowView dataRow = (DataRowView)autoList.SelectedItem;
                    string hn = dataRow.Row.ItemArray[0].ToString();
                    if (hn != "")
                    {
                        //MessageBox.Show(cellValue);
                        Mdr.Forms.frmMdr.searchhn = hn;
                        this.Close();
                    }
                    else
                    {
                        Growl.Warning("autoList_KeyUp : มีค่าว่าง");
                    }
                }
                catch (Exception ex)
                {
                    Growl.Error(ex.Message);
                }


            }
        }
        private void autoList_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                DataRowView dataRow = (DataRowView)autoList.SelectedItem;
                string hn = dataRow.Row.ItemArray[0].ToString();
                if (hn != "")
                {
                    //MessageBox.Show(cellValue);
                    Mdr.Forms.frmMdr.searchhn = hn;
                    this.Close();
                }
                else
                {
                    Growl.Warning("autoList_KeyUp : มีค่าว่าง");
                }
            }
            catch (Exception ex)
            {
                Growl.Error(ex.Message);
            }
        }

        //private void autoTextBox_KeyDown(object sender, KeyEventArgs e)
        //{
        //    try
        //    {
        //        if (e.Key == Key.Enter)
        //        {

        //        }
        //    }
        //    catch (Exception ex) {
        //        Growl.Error(ex.Message);
        //    }

        //}
    }
}

