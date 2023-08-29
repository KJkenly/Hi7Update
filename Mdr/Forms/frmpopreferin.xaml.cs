using HandyControl.Controls;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace Mdr.Forms
{
    /// <summary>
    /// Interaction logic for frmpopreferin.xaml
    /// </summary>
    public partial class frmpopreferin : System.Windows.Window
    {
        public frmpopreferin()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                Hi7.Class.APIConnect.getConfgXML();

                //Focus
                autoTextBox.Focus();
                autoTextBox.Text = HI7.Class.HIUility._TXTSEARCHPTTYPE;
                autoTextBox.Select(HI7.Class.HIUility._TXTSEARCHPTTYPE.Length, 0);
                getPttype();
            }
            catch (Exception ex)
            {
                Growl.Error(ex.Message);
            }

        }
        private void moveallposition(object sender, MouseButtonEventArgs e)
        {

        }

        //กำหนดค่า dt เป็น DataTable
        DataTable dt = new System.Data.DataTable();
        void getPttype()
        {
            try
            {
                dt = HI7.Class.HIUility.getlookupPttype(HI7.Class.HIUility._TXTSEARCHPTTYPE);
                System.Data.DataView view = new System.Data.DataView(dt);
                System.Data.DataTable selected = view.ToTable("Selected", false);
                this.autoList.ItemsSource = selected.DefaultView;
            }
            catch (Exception ex)
            {

            }
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
        }

        private void ClickMin(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;

        }

        private void autoTextBox_SelectionChanged(object sender, RoutedEventArgs e)

        {
            try
            {

                //Enable.
                this.OpenAutoSuggestionBox();
                string autotextbox = autoTextBox.Text;
                if (autotextbox == "")
                {
                    HI7.Class.HIUility._TXTSEARCHPTTYPE = autoTextBox.Text;
                    getPttype();
                }
                else
                {
                    System.Data.DataView view = new System.Data.DataView(dt);
                    DataTable selected = view.ToTable("Selected", false).Select("cln like '" + this.autoTextBox.Text + "%'").CopyToDataTable();
                    this.autoList.ItemsSource = selected.DefaultView;
                }

                //moveDown();
            }
            catch (Exception ex)
            {
                HI7.Class.HIUility._TXTSEARCHPTTYPE = autoTextBox.Text;
                getPttype();
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
            //if (e.Key == Key.Up || e.Key == Key.Down)
            //{


            //}
            if (e.Key == Key.Enter)
            {
                try
                {
                    DataRowView dataRow = (DataRowView)autoList.SelectedItem;
                    string pttype = dataRow.Row.ItemArray[0].ToString();
                    string namepttype = dataRow.Row.ItemArray[1].ToString();
                    if (pttype != "" && namepttype != "")
                    {
                        //MessageBox.Show(cellValue);
                        Mdr.Forms.frmMdr.idPttype = pttype;
                        Mdr.Forms.frmMdr.dataPttype = namepttype;


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


        private void autoTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Up || e.Key == Key.Down)
            {
                autoList.Focus();
            }
        }
    }
}
