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

namespace Hi7.UsercontrolHi7
{
    /// <summary>
    /// Interaction logic for UserControlReadtext.xaml
    /// </summary>
    public partial class UserControlReadtext : UserControl
    {
        public string Message { get; set; }
        public UserControlReadtext()
        {
            InitializeComponent();
            this.tbRead.Text = "รายละเอียดของโปรแกรมมีดังต่อไปนี้: " + Message;
        }
    }
}
