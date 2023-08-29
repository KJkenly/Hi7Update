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
    /// Interaction logic for ucHi7dialog.xaml
    /// </summary>
    public partial class ucHi7dialog
    {
        public string Message { get; set; }
        public event EventHandler NoButtonClicked;
        public event EventHandler YesButtonClicked;
        public ucHi7dialog()
        {
            InitializeComponent();
            DataContext = this;
            
        }

        private void btnucYes_Click(object sender, RoutedEventArgs e)
        {
            // ตอบรับการกดปุ่ม Yes
           
            YesButtonClicked?.Invoke(this, EventArgs.Empty);

        }

        private void btnucNo_Click(object sender, RoutedEventArgs e)
        {

            // ตอบรับการกดปุ่ม No
            NoButtonClicked?.Invoke(this, EventArgs.Empty);

        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.tbAlert.Text = "แจ้งเตือน: "+Message;
        }
    }
}
