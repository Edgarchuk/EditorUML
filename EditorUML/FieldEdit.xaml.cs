using System.Windows;
using System.Windows.Input;

namespace EditorUML
{
    public partial class FieldEdit : Window
    {
        public FieldEdit()
        {
            InitializeComponent();
        }

        private void FieldEdit_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) (sender as Window).Close();
        }
    }
}