using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Dialogs;

namespace ArbolitoAva
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnTestButtonClick(object sender, RoutedEventArgs e)
        {
            var dialog = new SaveFileDialog();
            dialog.ShowAsync(this);
        }
    }
}
