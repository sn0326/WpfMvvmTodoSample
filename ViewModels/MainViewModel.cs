using System.Collections.ObjectModel;
using System.Windows.Input;
using WpfMvvmTodoSample.Models;

namespace WpfMvvmTodoSample.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public ObservableCollection<TodoItem> Todos { get; } = new();

        private TodoItem? _selectedTodo;
        public TodoItem? SelectedTodo
        {
            get => _selectedTodo;
            set
            {
                if (SetProperty(ref _selectedTodo, value))
                {
                    (DeleteCommand as RelayCommand)?.RaiseCanExecuteChanged();
                }
            }
        }

        public ICommand DeleteCommand { get; }

        public MainViewModel()
        {
            DeleteCommand = new RelayCommand(_ => DeleteSelectedTodo(), _ => SelectedTodo is not null);

            Todos.Add(new TodoItem { Title = "牛乳を買う" });
            Todos.Add(new TodoItem { Title = "会議の資料を作成する" });
            Todos.Add(new TodoItem { Title = "本を返却する", IsDone = true });
        }

        private void DeleteSelectedTodo()
        {
            if (SelectedTodo is null) return;
            Todos.Remove(SelectedTodo);
            SelectedTodo = null;
        }
    }
}
