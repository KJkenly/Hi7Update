using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
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
using Newtonsoft.Json.Linq;


namespace Hi7.Forms
{
    /// <summary>
    /// Interaction logic for frmMainmenu.xaml
    /// </summary>
    public partial class frmMainmenu : Window
    {

        int APP_TOTAL;
        private string[,] app;
        string strUserLogin,strUserFname;
        JArray roles_all;
        bool SELECT_ROLE, INSERT_ROLE, UPDATE_ROLE, DELETE_ROLE;


        public frmMainmenu()
        {
            InitializeComponent();
        }

        private void borderNurse_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            //  eventClick(0);
          //  frmMainmenu = new frmMainmenu();
            //     frmMainmenu.Show();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {

        }
     
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
           Class.APIConnect.getConfgXML();
        
            getRole();
            setMenu();

        }

        private void menuMdr_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //  eventClick(((int)menuMdr.Tag);
            for (int i = 0; i <= APP_TOTAL - 1; i++)
            {
                for (int j = 0; j < 1; j++)
                {
                    if (app[i, 1] == "mdr.exe") {
                        eventClick(app[i, 1]);
                    }
                  
                }
            }
        }

        private void borderNurse_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //  eventClick(((int)menuMdr.Tag);
            for (int i = 0; i <= APP_TOTAL - 1; i++)
            {
                for (int j = 0; j < 1; j++)
                {
                    if (app[i, 1] == "nrs.exe")
                    {
                        eventClick(app[i, 1]);
                    }

                }
            }
        }

        private void borderDoctor_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //  eventClick(((int)menuMdr.Tag);
            for (int i = 0; i <= APP_TOTAL - 1; i++)
            {
                for (int j = 0; j < 1; j++)
                {
                    if (app[i, 1] == "dtr.exe")
                    {
                        eventClick(app[i, 1]);
                    }

                }
            }
        }

        private void borderStp_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //  eventClick(((int)menuMdr.Tag);
            for (int i = 0; i <= APP_TOTAL - 1; i++)
            {
                for (int j = 0; j < 1; j++)
                {
                    if (app[i, 1] == "stp.exe")
                    {
                        eventClick(app[i, 1]);
                    }

                }
            }
        }

        private void borderDtt_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //  eventClick(((int)menuMdr.Tag);
            for (int i = 0; i <= APP_TOTAL - 1; i++)
            {
                for (int j = 0; j < 1; j++)
                {
                    if (app[i, 1] == "dtt.exe")
                    {
                        eventClick(app[i, 1]);
                    }

                }
            }
        }

        private void borderLab_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //  eventClick(((int)menuMdr.Tag);
            for (int i = 0; i <= APP_TOTAL - 1; i++)
            {
                for (int j = 0; j < 1; j++)
                {
                    if (app[i, 1] == "lab.exe")
                    {
                        eventClick(app[i, 1]);
                    }

                }
            }
        }

        private void borderXray_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //  eventClick(((int)menuMdr.Tag);
            for (int i = 0; i <= APP_TOTAL - 1; i++)
            {
                for (int j = 0; j < 1; j++)
                {
                    if (app[i, 1] == "xray.exe")
                    {
                        eventClick(app[i, 1]);
                    }

                }
            }
        }

        private void borderPhm_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //  eventClick(((int)menuMdr.Tag);
            for (int i = 0; i <= APP_TOTAL - 1; i++)
            {
                for (int j = 0; j < 1; j++)
                {
                    if (app[i, 1] == "phm.exe")
                    {
                        eventClick(app[i, 1]);
                    }

                }
            }
        }

        private void borderExit_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }





        //    string app_path = @"C:\HIWIN\";
        string app_path = @Class.APIConnect.APP_PATH;
        private void setMenu() {


            // Menu MDR
            for (int i=0; i<= APP_TOTAL - 1;i++) {
                for (int j = 0; j< 1; j++)
                {

                    // โปรแกรมเวชระเบียน
                    if (app[i, 1] =="mdr.exe")
                    {
                        this.borderMdr.BorderBrush = Brushes.White;
                        this.borderMdr.BorderThickness = new Thickness(0, 0, 0, 0);
                        this.borderMdr.Background = Brushes.White;
                        this.borderMdr.Padding = new Thickness(5);
                        this.borderMdr.CornerRadius = new CornerRadius(15);
                    }
                    // โปรแกรมพยาบาล
                    if (app[i, 1] == "nrs.exe")
                    {
                        this.borderNurse.BorderBrush = Brushes.White;
                        this.borderNurse.BorderThickness = new Thickness(0, 0, 0, 0);
                        this.borderNurse.Background = Brushes.White;
                        this.borderNurse.Padding = new Thickness(5);
                        this.borderNurse.CornerRadius = new CornerRadius(15);
                    }

                    // โปรแกรมตั้งค่าระบบ
                    if (app[i, 1] == "stp.exe")
                    {
                        this.borderStp.BorderBrush = Brushes.White;
                        this.borderStp.BorderThickness = new Thickness(0, 0, 0, 0);
                        this.borderStp.Background = Brushes.White;
                        this.borderStp.Padding = new Thickness(5);
                        this.borderStp.CornerRadius = new CornerRadius(15);
                    }

                    // โปรแกรมแพทย์ตรวจโรค
                    if (app[i, 1] == "dtr.exe")
                    {
                        this.borderDoctor.BorderBrush = Brushes.White;
                        this.borderDoctor.BorderThickness = new Thickness(0, 0, 0, 0);
                        this.borderDoctor.Background = Brushes.White;
                        this.borderDoctor.Padding = new Thickness(5);
                        this.borderDoctor.CornerRadius = new CornerRadius(15);
                    }


                    // โปรแกรมเทคนิคการแพทย์
                    if (app[i, 1] == "lab.exe")
                    {
                        this.borderLab.BorderBrush = Brushes.White;
                        this.borderLab.BorderThickness = new Thickness(0, 0, 0, 0);
                        this.borderLab.Background = Brushes.White;
                        this.borderLab.Padding = new Thickness(5);
                        this.borderLab.CornerRadius = new CornerRadius(15);
                    }



                    // โปรแกรมรังสีวิทยา
                    if (app[i, 1] == "xry.exe")
                    {
                        this.borderXray.BorderBrush = Brushes.White;
                        this.borderXray.BorderThickness = new Thickness(0, 0, 0, 0);
                        this.borderXray.Background = Brushes.White;
                        this.borderXray.Padding = new Thickness(5);
                        this.borderXray.CornerRadius = new CornerRadius(15);
                    }

                    // โปรแกรมห้องคลอด
                    if (app[i, 1] == "lr.exe")
                    {
                        this.borderLr.BorderBrush = Brushes.White;
                        this.borderLr.BorderThickness = new Thickness(0, 0, 0, 0);
                        this.borderLr.Background = Brushes.White;
                        this.borderLr.Padding = new Thickness(5);
                        this.borderLr.CornerRadius = new CornerRadius(15);
                    }

                    // โปรแกรมวิสัญญี
                    if (app[i, 1] == "or.exe")
                    {
                        this.borderOr.BorderBrush = Brushes.White;
                        this.borderOr.BorderThickness = new Thickness(0, 0, 0, 0);
                        this.borderOr.Background = Brushes.White;
                        this.borderOr.Padding = new Thickness(5);
                        this.borderOr.CornerRadius = new CornerRadius(15);
                    }

                    // โปรแกรมทันตะกรรม
                    if (app[i, 1] == "dtt.exe")
                    {
                        this.borderDtt.BorderBrush = Brushes.White;
                        this.borderDtt.BorderThickness = new Thickness(0, 0, 0, 0);
                        this.borderDtt.Background = Brushes.White;
                        this.borderDtt.Padding = new Thickness(5);
                        this.borderDtt.CornerRadius = new CornerRadius(15);
                    }

                    // โปรแกรมทันตะกรรม
                    if (app[i, 1] == "phm.exe")
                    {
                        this.borderPhm.BorderBrush = Brushes.White;
                        this.borderPhm.BorderThickness = new Thickness(0, 0, 0, 0);
                        this.borderPhm.Background = Brushes.White;
                        this.borderPhm.Padding = new Thickness(5);
                        this.borderPhm.CornerRadius = new CornerRadius(15);
                    }
                    // โปรแกรมผู้ป่วยใน
                    if (app[i, 1] == "ipd.exe")
                    {
                        //this.borderIpd.BorderBrush = Brushes.White;
                        //this.borderIpd.BorderThickness = new Thickness(0, 0, 0, 0);
                        //this.borderIpd.Background = Brushes.White;
                        //this.borderIpd.Padding = new Thickness(5);
                        //this.borderIpd.CornerRadius = new CornerRadius(15);
                    }
                    // โปรแกรมคลินิคพิเศษ
                    if (app[i, 1] == "spc.exe")
                    {
                        //this.borderSpc.BorderBrush = Brushes.White;
                        //this.borderSpc.BorderThickness = new Thickness(0, 0, 0, 0);
                        //this.borderSpc.Background = Brushes.White;
                        //this.borderSpc.Padding = new Thickness(5);
                        //this.borderSpc.CornerRadius = new CornerRadius(15);
                    }
                  
                }
              
            }
        

        }

      
        private void eventClick(string strExeFile)
        {
            try
            {
                app_path = Class.APIConnect.API_PATH;
                string param = strUserLogin + " " + " " + SELECT_ROLE + " " + INSERT_ROLE + " " + UPDATE_ROLE + " " + DELETE_ROLE;
               
                Process pro = new Process();
                pro.StartInfo.FileName = app_path + strExeFile;
                pro.StartInfo.Arguments = param;
                pro.Start();

            }
            catch (Exception e)

            {
                // MessageBox.Show("Error getRole " + e.Message);
            }

        }

        private void getRole()
        {
                  
            try
            {
                strUserLogin = Class.APIConnect.API_USER;
                strUserFname = "";

                SELECT_ROLE = (Boolean)Class.APIConnect.JSON_DATA_ROLES[0].SelectToken("role_action").SelectToken("SELECT");
                INSERT_ROLE = (Boolean)Class.APIConnect.JSON_DATA_ROLES[0].SelectToken("role_action").SelectToken("INSERT");
                UPDATE_ROLE = (Boolean)Class.APIConnect.JSON_DATA_ROLES[0].SelectToken("role_action").SelectToken("UPDATE");
                DELETE_ROLE = (Boolean)Class.APIConnect.JSON_DATA_ROLES[0].SelectToken("role_action").SelectToken("DELETE");

                JArray jsonArray = Class.APIConnect.JSON_DATA_MODULES;
                    
                APP_TOTAL = jsonArray.Count;
                app = new string[APP_TOTAL, 2];
             
                for (int i = 0; i <= jsonArray.Count - 1; i++)
                {
                    for (int j = 0; j <= 1; j++)
                    {
                        if (j == 0)
                        {
                            app[i, j] = (string)jsonArray[i].SelectToken("module_descript");
                        }
                        else
                        {
                            app[i, j] = (string)jsonArray[i].SelectToken("module_type").SelectToken("APPNAME");

                        }

                    }
                }

            }
            catch (Exception e)
            {
                MessageBox.Show("Error getRole() " + e.Message);
            }

        }
    }
}
