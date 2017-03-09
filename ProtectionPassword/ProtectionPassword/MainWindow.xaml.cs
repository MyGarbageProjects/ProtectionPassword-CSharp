using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
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

namespace ProtectionPassword
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string txtSourse;
        public MainWindow()
        {
            InitializeComponent();
            txtSourse = "using System;using System.Windows.Forms;namespace SteamPassword{static class Program{[STAThread]static void Main(){System.Diagnostics.Process.Start(@\"SETPATHTOEXEFILE\"); Clipboard.SetText(@\"SETPASSWORD\");}}}";
           // ReturnIcoFromExe();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            string nameProgram = txtPathToProgrammStart.Text;
            for (int i = 0;;  i++)
            {
                int _t = nameProgram.IndexOf("\\");
                if (_t != -1)
                    nameProgram = nameProgram.Remove(0, _t + 1);
                else
                    break;
            }
            txtSourse = txtSourse.Replace("SETPATHTOEXEFILE", txtPathToProgrammStart.Text);
            txtSourse = txtSourse.Replace("SETPASSWORD", txtPassCliboard.Text);
            CSharpCodeProvider csc = new CSharpCodeProvider(new Dictionary<string, string>() { { "CompileVersion", "v4.5" } });
            CompilerParameters parameters = new CompilerParameters(new[] { "mscorlib.dll", "System.Windows.Forms.dll", "System.dll" }, AppDomain.CurrentDomain.BaseDirectory + "2"+nameProgram, true);//AppDomain.CurrentDomain.BaseDirectory+txtPathToProgrammStart.Text, true);
            //parameters.OutputAssembly = @"C:\Users\XTreme.ws\Desktop\test.exe";//AppDomain.CurrentDomain.BaseDirectory + "test.exe";
            //ReturnIcoFromExe();
            //parameters.CompilerOptions = string.Format("/target:winexe /win32icon:\"{0}\"", System.IO.Path.GetTempPath() + "FilefromProgrammKadi.ico");
            //parameters.CompilerOptions = string.Format("/target:winexe /win32icon:\"{0}\"", @"C:\Users\XTreme.ws\Desktop\FilefromProgrammKadi.ico");
            if(txtPathToIcon.Text != "")
                parameters.CompilerOptions = string.Format("/target:winexe /win32icon:\"{0}\"", txtPathToIcon.Text);
            parameters.GenerateExecutable = true;
            CompilerResults results = csc.CompileAssemblyFromSource(parameters, txtSourse);
            if (results.Errors.HasErrors)
            {
                string _error = "";
                results.Errors.Cast<CompilerError>().ToList().ForEach(error => _error += error.ErrorText + "\r\n");
                MessageBox.Show(_error);
            }
            else
            {
                MessageBox.Show("The program is created in the same folder");
            }
        }


        private void ReturnIcoFromExe()
        {
            //Console.WriteLine();


            //ImageSource IS;
            //using (System.Drawing.Icon sysicon = System.Drawing.Icon.ExtractAssociatedIcon(@"C:\Users\XTreme.ws\Desktop\2notepad++.exe"))//txtPathToProgrammStart.Text))
            //IS = System.Windows.Interop.Imaging.CreateBitmapSourceFromHIcon(sysicon.Handle, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            //ImageBrush IB = new ImageBrush(IS);
            //IB.Stretch = Stretch.None;
            //MainGrid.Background = IB;//new ImageBrush(IS);


            //System.Drawing.Image img  = System.Drawing.Image.FromStream(Assembly.LoadFile(@"C:\Users\XTreme.ws\Desktop\2notepad++.exe").GetManifestResourceStream("2notepad++"),true);
            //img.Save(@"C:\Users\XTreme.ws\Desktop\" + "FilefromProgrammKadi.icon");


            //System.Drawing.Bitmap bmp = default(System.Drawing.Bitmap);
            //bmp = new System.Drawing.Bitmap(System.Drawing.Icon.ExtractAssociatedIcon(@"C:\Users\XTreme.ws\Desktop\2notepad++.exe").ToBitmap());
            //bmp.Save(System.IO.Path.GetTempPath() + "FilefromProgrammKadi.ico");
            //bmp.Save(@"C:\Users\XTreme.ws\Desktop\" + "FilefromProgrammKadi.ico",System.Drawing.Imaging.ImageFormat.Icon);


            //Assembly ass = Assembly.LoadFile(@"F:\Program Files\AutoIt3\Au3Info.exe");//@"C:\Users\XTreme.ws\Desktop\2notepad++.exe");


            //string[] asse = ass.GetManifestResourceNames();

        }


        ///-----------------------------------

        private void button_Click(object sender, RoutedEventArgs e)
        {
            using (System.Windows.Forms.OpenFileDialog OFD = new System.Windows.Forms.OpenFileDialog())
            {
                OFD.Filter = "ICO Файлы (*.ico)|*.ico";
                OFD.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                if (OFD.ShowDialog()== System.Windows.Forms.DialogResult.OK)
                {
                    txtPathToIcon.Text = OFD.FileName;
                }
            }
        }
        private void btnSelectExe_Click(object sender, RoutedEventArgs e)
        {
            using (System.Windows.Forms.OpenFileDialog OFD = new System.Windows.Forms.OpenFileDialog())
            {
                OFD.Filter = "Exe Файлы (*.exe)|*.exe|All (*.*)|*.*";
                OFD.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                if (OFD.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    txtPathToProgrammStart.Text = OFD.FileName;
                }
            }
        }
    }
}
