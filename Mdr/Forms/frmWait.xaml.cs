using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ThaiNationalIDCard;

namespace Mdr.Forms
{
    /// <summary>
    /// Interaction logic for frmWait.xaml
    /// </summary>
    public partial class frmWait : Window
    {
        public frmWait()
        {
            InitializeComponent();
           
            BackgroundWorker bckWrk = new BackgroundWorker();
            bckWrk.DoWork += new DoWorkEventHandler(bckWrkIND_DoWork);
            bckWrk.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bckWrkIND_RunWorkerCompleted);
            bckWrk.RunWorkerAsync();

        }


        //show animation

        public void bckWrkIND_DoWork(object sender, DoWorkEventArgs e)
        {
            System.Text.EncodingProvider provider = System.Text.CodePagesEncodingProvider.Instance;// แปลง encode tis-620
            Encoding.RegisterProvider(provider);
            var th = new ThaiIDCard();
            Personal Personal = th.readAllPhoto();//อ่านภาพ
            Mdr.Forms.frmMdr.personalCard = Personal;
            //load the popup

        }
        public void bckWrkIND_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //this.Close();
            //hide the popup       }
        }
    }
}
