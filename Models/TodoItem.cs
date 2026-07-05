using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WpfMvvmTodoSample.Models
{
    public class TodoItem : INotifyPropertyChanged
    {
        private string _title = string.Empty;
        private bool _isDone;

        public Guid Id { get; } = Guid.NewGuid();

        public DateTime CreatedAt { get; } = DateTime.Now;

        public string Title
        {
            get => _title;
            set
            {
                if (_title == value) return;
                _title = value;
                OnPropertyChanged();
            }
        }

        public bool IsDone
        {
            get => _isDone;
            set
            {
                if (_isDone == value) return;
                _isDone = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
