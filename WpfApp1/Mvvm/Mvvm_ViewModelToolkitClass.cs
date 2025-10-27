using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using System.Data.SQLite;
using System.Runtime.CompilerServices;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace WpfApp1 {
     partial class Mvvm_ViewModelToolkitClass: ObservableObject {
        public ObservableCollection<User> Users { get; } = new();

        [RelayCommand]
        private void Close(System.Windows.Window w) {
            w.Close();
        }

        [RelayCommand]
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
		[RelayCommand]
		private void Clear() {
                       Users.Clear();

		}
	}

}
