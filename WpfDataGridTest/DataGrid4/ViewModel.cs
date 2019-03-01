using Prism.Commands;
using Prism.Mvvm;
using SharedProject;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace DataGrid4
{
    public class ViewModel : BindableBase
    {
        public ObservableCollection<MemoModel1> MemoList { get; set; }
        public ObservableCollection<CategoryModel> CategoryList { get; set; }

        private MemoModel1 _memoItem;
        public MemoModel1 MemoItem
        {
            get { return _memoItem; }
            set
            {
                SetProperty(ref _memoItem, value);
                SelectedComboBox();
            }
        }

        private CategoryModel _categoryItem;
        public CategoryModel CategoryItem
        {
            get { return _categoryItem; }
            set { SetProperty(ref _categoryItem, value); }
        }

        public DelegateCommand<object> Button1Command { get; private set; }

        public ViewModel()
        {
            var obj = new DB();
            MemoList = obj.MemoList();
            CategoryList = obj.CategoryList();

            Button1Command = new DelegateCommand<object>(Button1Execute);
        }

        private void SelectedComboBox()
        {
            CategoryItem = CategoryList.FirstOrDefault(v => v.Id == MemoItem.CategoryId);
        }

        private void Button1Execute(object o)
        {
            if (MemoItem != null)
            {
                //CommandParameterでElementName取得
                var win = o as Window;
                var cb = win.FindName("MyComboBox") as ComboBox;
                var tb1 = win.FindName("MyTextBox1") as TextBox;
                var tb2 = win.FindName("MyTextBox2") as TextBox;

                //UpdateSourceTrigger=Explicit で任意のタイミング（↓）でバインディング元へ反映
                //https://docs.microsoft.com/ja-jp/dotnet/framework/wpf/data/how-to-control-when-the-textbox-text-updates-the-source
                BindingExpression be = cb.GetBindingExpression(ComboBox.SelectedValueProperty);
                be.UpdateSource();
                BindingExpression be1 = tb1.GetBindingExpression(TextBox.TextProperty);
                be1.UpdateSource();
                BindingExpression be2 = tb2.GetBindingExpression(TextBox.TextProperty);
                be2.UpdateSource();

                Console.WriteLine(MemoItem.Category.Name);
                Console.WriteLine(MemoItem.Title);
            }
        }
    }
}
