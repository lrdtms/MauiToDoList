using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MauiToDoListApp
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<TaskItem> Tasks { get; set; }
        public ICommand AddTaskCommand { get; }
        public ICommand DeleteCommand { get; }

        private string _newTaskTitle;
        public string NewTaskTitle
        {
            get => _newTaskTitle;
            set
            {
                _newTaskTitle = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(NewTaskTitle)));
            }
        }

        public MainViewModel()
        {
            Tasks = new ObservableCollection<TaskItem>
            {
                new TaskItem { Title = "Buy groceries", IsCompleted = false },
                new TaskItem { Title = "Walk the dog", IsCompleted = true }
            };

            AddTaskCommand = new Command(AddTask);
            DeleteCommand = new Command<TaskItem>(DeleteTask);
        }

        private void AddTask()
        {
            if (!string.IsNullOrWhiteSpace(NewTaskTitle))
            {
                Tasks.Add(new TaskItem { Title = NewTaskTitle, IsCompleted = false });
                NewTaskTitle = string.Empty;
            }
        }

        private void DeleteTask(TaskItem task)
        {
            if (Tasks.Contains(task))
                Tasks.Remove(task);
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
