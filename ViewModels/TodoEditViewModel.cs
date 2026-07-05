namespace WpfMvvmTodoSample.ViewModels
{
    public class TodoEditViewModel : ViewModelBase
    {
        private string _title;

        public TodoEditViewModel(string title = "")
        {
            _title = title;
        }

        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }
    }
}
