using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace xdef.codegen
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public string JarPath { get; set; }
        public string ClassName { get; set; }
        public string JavaCode { get; set; }
        public string SharpCode { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }


        public event PropertyChangedEventHandler PropertyChanged;

        private void ButtonBrowse_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog()
            {
                Filter = "*.jar|*.jar"
            };
            if (dialog.ShowDialog() == true)
            {
                JarPath = dialog.FileName;
            }
        }

        private void ButtonGenerate_Click(object sender, RoutedEventArgs e)
        {
            var sharp = new SharpCodeGenerator(JarPath, ClassName);
            SharpCode = sharp.GetCode();
            var java = new JavaCodeGenerator(JarPath, ClassName);
            JavaCode = java.GetCode();
        }
    }
}
