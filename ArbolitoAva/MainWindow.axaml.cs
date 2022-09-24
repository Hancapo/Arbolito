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
            CbSplitBy.SelectedIndex = 0;
        }

        private void OnBtnSplitSourceBrowseClick(object sender, RoutedEventArgs e)
        {
            var dlg = new OpenFolderDialog();
            var result = dlg.ShowAsync(this).Result;
            if (!string.IsNullOrWhiteSpace(result))
            {
                TbSplitSourceField.Text = result.ToString();
            }
        }

        private void OnBtnSplitYmapBrowseClick(object sender, RoutedEventArgs e)
        {
            var dlg = new OpenFolderDialog();
            var result = dlg.ShowAsync(this).Result;
            if (!string.IsNullOrWhiteSpace(result))
            {
                TbSplitYmapField.Text = result.ToString();
            }
        }

        private void OnBtnSplitOutputBrowseClick(object sender, RoutedEventArgs e)
        {
            var dlg = new OpenFolderDialog();
            var result = dlg.ShowAsync(this).Result;
            if (!string.IsNullOrWhiteSpace(result))
            {
                TbSplitOutputField.Text = result.ToString();
            }
        }
    }
}
