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
using EditorUML.ViewModel;

namespace EditorUML
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static Point MousePosition = new Point(0, 0); 
        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void MainWindow_OnMouseMove(object sender, MouseEventArgs e)
        {
            MousePosition = Mouse.GetPosition(sender as IInputElement);
        }

        private void FrameworkElement_OnInitialized(object? sender, EventArgs e)
        {
            if (sender != null)
            {
                var frameworkElement = (FrameworkElement) sender;
                var classViewModel = (ClassViewModel) frameworkElement.DataContext;
                classViewModel.Height = (int) frameworkElement.Height;
                classViewModel.Width = (int) frameworkElement.Width;
            }
        }
    }
}