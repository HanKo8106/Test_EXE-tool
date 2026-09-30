using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Test_EXE
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private System.Timers.Timer m_tUpdateFwProgress = null;
        private int m_iReturnCode = 99;
        private String m_strFileDir = String.Empty;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            //TbExe.Text = "";
            TbExitCode.Text = "";
        }

        private void BtnBowse_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            //移動上層在指定下層路徑
            dlg.RestoreDirectory = true;
            dlg.InitialDirectory = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            // Set filter for file extension and default file extension
            dlg.Filter = "Exe Files (*.exe)|*.exe";

            dlg.ShowDialog();

            TbExe.Text = dlg.FileName;
        }

        private void BtnKill_Click(object sender, RoutedEventArgs e)
        {
            //Kill Update Tool
            Process[] process = Process.GetProcesses();
            foreach (Process p in process)
            {
                if (p.ProcessName == "TbExe.Text")
                {
                    p.Kill();
                    break;
                }
            }
        }

        private void BtnRun_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (TbExe.Text == "")
                    MessageBox.Show("no file");
                else
                {
                    //System.Diagnostics.Process process = System.Diagnostics.Process.Start(System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\" + strEXE, "-s");
                    System.Diagnostics.Process process = System.Diagnostics.Process.Start(TbExe.Text, "-s");
                    process.WaitForExit();
                    m_iReturnCode = process.ExitCode;
                    TbExitCode.Text = m_iReturnCode.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("no file");
            }
        }

        private void BtnDosRun_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string strAgentPath = TbExe.Text;//= System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\" + strEXE;
                if (TbExe.Text == "")
                    MessageBox.Show("no file");
                else
                {
                    String strProcessName = System.IO.Path.GetFileNameWithoutExtension(TbExe.Text);

                    System.Diagnostics.Process process = new System.Diagnostics.Process();
                    process.StartInfo.FileName = strAgentPath;
                    process.StartInfo.Arguments = "-b";
                    process.StartInfo.CreateNoWindow = false;
                    process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                    process.StartInfo.Verb = "runas";
                    process.Start();
                    process.WaitForExit();
                    m_iReturnCode = process.ExitCode;
                    TbExitCode.Text = m_iReturnCode.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("no file");
            }
        }

        private void BtnRun2_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (TbExe.Text == "")
                    MessageBox.Show("no file");
                else
                {
                    //System.Diagnostics.Process process = System.Diagnostics.Process.Start(System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\" + strEXE, "-s");
                    System.Diagnostics.Process process = System.Diagnostics.Process.Start(TbExe.Text, "-s 6934 6936 6862");
                    process.WaitForExit();
                    m_iReturnCode = process.ExitCode;
                    TbExitCode.Text = m_iReturnCode.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("no file");
            }
        }
    }
}
