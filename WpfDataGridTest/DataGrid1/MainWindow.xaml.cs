using SharedProject;
using System.Windows;

namespace DataGrid1
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var obj = new DB();
            MyDataGrid.ItemsSource = obj.MemoList();
        }
    }
}
