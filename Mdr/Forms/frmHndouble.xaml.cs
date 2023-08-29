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
    /// Interaction logic for frmHndouble.xaml
    /// </summary>
    public partial class frmHndouble : Window
    {
        public DataTable dt;
        public frmHndouble()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                Hi7.Class.APIConnect.getConfgXML();
                getHnbycid();
            }
            catch (Exception)
            {

            }

        }
        private void moveallposition(object sender, MouseButtonEventArgs e)
        {

        }
        private void ClickClose(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ClickMin(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;

        }
        void getHnbycid()
        {

            dt = HI7.Class.HIUility.getHn(Mdr.Forms.frmMdr._CIDREADER);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    System.Data.DataView view = new System.Data.DataView(dt);
                    System.Data.DataTable selected = view.ToTable("Selected", false, "hn", "fullname","ldate");
                    this.dataGridhn.ItemsSource = selected.DefaultView;
                    this.dataGridhn.Columns[0].Header = "hn";
                    this.dataGridhn.Columns[1].Header = "ชื่อ-สกุล";
                    this.dataGridhn.Columns[2].Header = "วันที่รับบริการล่าสุด";
                }
                else
                {
                    this.Close();
                }
            }
        }

        private void dataGridhn_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataRowView dataRow = (DataRowView)dataGridhn.SelectedItem;
            int index = dataGridhn.CurrentCell.Column.DisplayIndex;
            string hn = dataRow.Row.ItemArray[0].ToString();
            Mdr.Forms.frmMdr._HNdouble = hn;
            this.Close();
        }
    }
}
