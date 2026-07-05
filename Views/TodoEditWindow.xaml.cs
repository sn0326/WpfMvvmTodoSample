using System.Windows;
using WpfMvvmTodoSample.ViewModels;

namespace WpfMvvmTodoSample.Views
{
    public partial class TodoEditWindow : Window
    {
        public TodoEditViewModel ViewModel { get; }

        public TodoEditWindow(TodoEditViewModel viewModel)
        {
            InitializeComponent();
            ViewModel = viewModel;
            DataContext = ViewModel;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ViewModel.Title))
            {
                MessageBox.Show(this, "内容を入力してください。", "入力エラー",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
