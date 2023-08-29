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
    /// Interaction logic for frmLastfiveauthencode.xaml
    /// </summary>
    public partial class frmLastfiveauthencode : Window
    {
        public frmLastfiveauthencode()
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
    }
}
