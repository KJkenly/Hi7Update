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

namespace Mdr.Forms
{
    /// <summary>
    /// Interaction logic for frmReferOut.xaml
    /// </summary>
    public partial class frmReferIn : Window
    {
        public frmReferIn()
        {
            InitializeComponent();
        }
        private void ClickClose(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ClickMin(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;

        }

        private void btnClickSave(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("a new version has been detected!Do you want to update?", "Title", MessageBoxButton.YesNo, MessageBoxImage.Question);
        }

        private void frmReferin_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void txtHn_KeyDown(object sender, KeyEventArgs e)
        {

        }
    }
}
