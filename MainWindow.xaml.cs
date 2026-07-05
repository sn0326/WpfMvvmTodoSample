using System.Windows;
using WpfMvvmTodoSample.Models;
using WpfMvvmTodoSample.ViewModels;
using WpfMvvmTodoSample.Views;

namespace WpfMvvmTodoSample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainViewModel ViewModel => (MainViewModel)DataContext;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var editViewModel = new TodoEditViewModel();
            var editWindow = new TodoEditWindow(editViewModel) { Owner = this };

            if (editWindow.ShowDialog() == true)
            {
                ViewModel.Todos.Add(new TodoItem { Title = editViewModel.Title });
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (ViewModel.SelectedTodo is not { } selected)
            {
                MessageBox.Show(this, "編集するTODOを選択してください。", "確認",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var editViewModel = new TodoEditViewModel(selected.Title);
            var editWindow = new TodoEditWindow(editViewModel) { Owner = this };

            if (editWindow.ShowDialog() == true)
            {
                selected.Title = editViewModel.Title;
            }
        }
    }
}
