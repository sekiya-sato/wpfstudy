using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SQLite;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            this.Close();
		}

        private void Button_Click_1(object sender, RoutedEventArgs e) {
            // DB ファイルをアプリケーションの実行ディレクトリに作成/オープン
            var dbPath = System.IO.Path.Combine(System.AppContext.BaseDirectory, "sample.db");
            var connString = $"Data Source={dbPath}";
            using var conn = new SQLiteConnection(connString);
            conn.Open();

            // テーブル作成
            using (var cmd = conn.CreateCommand()) {
                cmd.CommandText = @"
CREATE TABLE IF NOT EXISTS user (
    Name TEXT NOT NULL,
    Age INTEGER NOT NULL,
    Address TEXT
);";
                cmd.ExecuteNonQuery();

                // 既存データがあるか確認
                cmd.CommandText = "SELECT COUNT(*) FROM user;";
                var count = (long)cmd.ExecuteScalar();

                if (count == 0) {
                    // サンプル5レコードを挿入
                    using var tran = conn.BeginTransaction();
                    cmd.Transaction = tran;
                    cmd.CommandText = "INSERT INTO user (Name, Age, Address) VALUES ($n, $a, $addr);";

                    var pName = cmd.CreateParameter(); pName.ParameterName = "$n";
                    var pAge = cmd.CreateParameter(); pAge.ParameterName = "$a";
                    var pAddr = cmd.CreateParameter(); pAddr.ParameterName = "$addr";
                    cmd.Parameters.Add(pName);
                    cmd.Parameters.Add(pAge);
                    cmd.Parameters.Add(pAddr);

                    var records = new[] {
                        new { Name = "Alice", Age = 30, Address = "Tokyo" },
                        new { Name = "Bob", Age = 25, Address = "Osaka" },
                        new { Name = "Charlie", Age = 35, Address = "Hokkaido" },
                        new { Name = "Diana", Age = 28, Address = "Nagoya" },
                        new { Name = "Eve", Age = 22, Address = "Kyoto" }
                    };

                    foreach (var r in records) {
                        pName.Value = r.Name;
                        pAge.Value = r.Age;
                        pAddr.Value = r.Address;
                        cmd.ExecuteNonQuery();
                    }

                    tran.Commit();
                }

                // データ取得して DataGrid にセット
                cmd.CommandText = "SELECT Name, Age, Address FROM user;";
                using var reader = cmd.ExecuteReader();
                var list = new List<UserRow>();
                while (reader.Read()) {
                    list.Add(new UserRow {
                        Name = reader.IsDBNull(0) ? string.Empty : reader.GetString(0),
                        Age = reader.IsDBNull(1) ? 0 : reader.GetInt32(1),
                        Address = reader.IsDBNull(2) ? string.Empty : reader.GetString(2)
                    });
                }
				conn.Close();

				dg1.ItemsSource = list;
			}
		}
        private void Button_Click_2(object sender, RoutedEventArgs e) {
			var dbPath = System.IO.Path.Combine(System.AppContext.BaseDirectory, "sample.db");
			var connString = $"Data Source={dbPath}";
			using var conn = new SQLiteConnection(connString);
			conn.Open();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT COUNT(*) FROM user;" ;
			var count = (long)cmd.ExecuteScalar();
				// サンプル5レコードを挿入
			using var tran = conn.BeginTransaction();
			cmd.Transaction = tran;
			cmd.CommandText = "INSERT INTO user (Name, Age, Address) VALUES ($n, $a, $addr);";

			var pName = cmd.CreateParameter(); pName.ParameterName = "$n";
			var pAge = cmd.CreateParameter(); pAge.ParameterName = "$a";
			var pAddr = cmd.CreateParameter(); pAddr.ParameterName = "$addr";
			cmd.Parameters.Add(pName);
			cmd.Parameters.Add(pAge);
			cmd.Parameters.Add(pAddr);

			var records = new[] {
					new { Name = "Alice "+count, Age = 35, Address = "Tokyo" },
					new { Name = "Bob "+count, Age = 28, Address = "Osaka" },
					new { Name = "Charlie "+count, Age = 30, Address = "Hokkaido" },
					new { Name = "Diana "+count, Age = 29, Address = "Nagoya" },
					new { Name = "Eve "+count, Age = 21, Address = "Kyoto" }
				};

			foreach (var r in records) {
				pName.Value = r.Name;
				pAge.Value = r.Age;
				pAddr.Value = r.Address;
				cmd.ExecuteNonQuery();
			}

			tran.Commit();

			// データ取得して DataGrid にセット
			cmd.CommandText = "SELECT Name, Age, Address FROM user;";
			using var reader = cmd.ExecuteReader();
			var list = new List<UserRow>();
			while (reader.Read()) {
				list.Add(new UserRow {
					Name = reader.IsDBNull(0) ? string.Empty : reader.GetString(0),
					Age = reader.IsDBNull(1) ? 0 : reader.GetInt32(1),
					Address = reader.IsDBNull(2) ? string.Empty : reader.GetString(2)
				});
			}
			conn.Close();

			dg1.ItemsSource = list;



		}


		private class UserRow {
            public string Name { get; set; } = "";
            public int Age { get; set; }
            public string Address { get; set; } = "";
        }
	}
}