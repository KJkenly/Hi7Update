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
    /// Interaction logic for frmScreen.xaml
    /// </summary>
    public partial class frmScreen : Window
    {
        public frmScreen()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            int nWidth = (int)System.Windows.SystemParameters.PrimaryScreenWidth;
            int nHieght = (int)System.Windows.SystemParameters.PrimaryScreenHeight;
            this.LayoutTransform = new ScaleTransform(nWidth, nHieght);
        }
    }
}
