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
using NPoco;

namespace WpfApp1 {
     partial class Mvvm_ViewModelToolkitNpocoClass: ObservableObject {
        [ObservableProperty]
        private  ObservableCollection<UserToolkit> users = new ObservableCollection<UserToolkit>();

        [RelayCommand]
        private void Close(System.Windows.Window w) {
            w.Close();
        }

        [RelayCommand]
        private void Load() {
            var dbPath = System.IO.Path.Combine(System.AppContext.BaseDirectory, "sample.db");
            using var conn = new SQLiteConnection($"Data Source={dbPath}");
            conn.Open();
            var db = new Database(conn);
            var users = db.Fetch<UserToolkit>();
			Users = new ObservableCollection<UserToolkit>(users);

		}
		[RelayCommand]
		private void Clear() {
                       Users.Clear();

		}
	}

}
