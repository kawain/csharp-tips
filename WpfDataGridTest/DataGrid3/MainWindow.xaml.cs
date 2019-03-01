using SharedProject;
using System.Windows;
using System;

namespace DataGrid3
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

            DataContext = new
            {
                CategoryList = obj.CategoryList(),
                MemoList = obj.MemoList()
            };

        }

        //コレクション走査パスで表示して、名前.Textで取得した方が実用的かも知れないと思ったが…
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine(MyTextBox1.Text);
        }

        //
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MyTextBox1.Text = "";
            MyTextBox2.Text = "";
        }
    }
}
