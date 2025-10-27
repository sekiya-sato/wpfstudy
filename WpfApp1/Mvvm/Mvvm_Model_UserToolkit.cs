using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Text;
using NPoco;

namespace WpfApp1 {
	[TableName("user")]
    public partial class UserToolkit: ObservableObject {
        [ObservableProperty]
        private string name = string.Empty;
		[ObservableProperty]
		private int age;
		[ObservableProperty]
		private string address = string.Empty;
	}
}
