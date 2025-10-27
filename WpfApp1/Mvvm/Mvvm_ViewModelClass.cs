using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using System.Data.SQLite;
using System.Runtime.CompilerServices;

namespace WpfApp1 {
    public class Mvvm_ViewModelClass : INotifyPropertyChanged {
        public ObservableCollection<User> Users { get; } = new();

        public ICommand LoadCommand { get; }
        public ICommand CloseCommand { get; }

        public Mvvm_ViewModelClass() {
            LoadCommand = new RelayCommand(_ => Load());
            CloseCommand = new RelayCommand(param => {
                if (param is System.Windows.Window w) {
                    w.Close();
                }
            });
        }

        private void Load() {
			var dbPath = System.IO.Path.Combine(System.AppContext.BaseDirectory, "sample.db");
			using var conn = new SQLiteConnection($"Data Source={dbPath}");
			conn.Open();

            using var cmd = conn.CreateCommand();
            cmd.CommandText = @"
CREATE TABLE IF NOT EXISTS user (
    Name TEXT NOT NULL,
    Age INTEGER NOT NULL,
    Address TEXT
);";
            cmd.ExecuteNonQuery();

            cmd.CommandText = "SELECT Name, Age, Address FROM user;";
            using var reader = cmd.ExecuteReader();

            Users.Clear();
            while (reader.Read()) {
                Users.Add(new User {
                    Name = reader.IsDBNull(0) ? string.Empty : reader.GetString(0),
                    Age = reader.IsDBNull(1) ? 0 : reader.GetInt32(1),
                    Address = reader.IsDBNull(2) ? string.Empty : reader.GetString(2),
                });
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }

    public class RelayCommand : ICommand {
        private readonly System.Action<object?> _execute;
        private readonly System.Func<object?, bool>? _canExecute;

        public RelayCommand(System.Action<object?> execute, System.Func<object?, bool>? canExecute = null) {
            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object? parameter) => _canExecute?.Invoke(parameter) ?? true;
        public void Execute(object? parameter) => _execute(parameter);
        public event System.EventHandler? CanExecuteChanged;
        public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, System.EventArgs.Empty);
    }
}
