using SharedProject;
using System.Windows;

namespace DataGrid2
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
            MyComboBox.ItemsSource = obj.CategoryList();
            MyDataGrid.ItemsSource = obj.MemoList();
        }
    }
}
