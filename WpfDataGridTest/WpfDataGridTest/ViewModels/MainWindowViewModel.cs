using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfDataGridTest.Models;

namespace WpfDataGridTest.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        public ObservableCollection<MemoModel> MemoList { get; set; }
        public ObservableCollection<CategoryModel> CategoryList { get; set; }

        private MemoModel _item;
        public MemoModel Item
        {
            get { return _item; }
            set { SetProperty(ref _item, value); }
        }

        private CategoryModel _comboBoxItem;
        public CategoryModel ComboBoxItem
        {
            get { return _comboBoxItem; }
            set { SetProperty(ref _comboBoxItem, value); }
        }


        public DelegateCommand SelectionChangedCommand { get; private set; }

        public DelegateCommand Button1Command { get; private set; }

        public MainWindowViewModel()
        {
            MemoList = SingleMemoList.Instance;
            CategoryList = SingleCategoryList.Instance;

            SelectionChangedCommand = new DelegateCommand(SelectionChangedExecute);
            Button1Command = new DelegateCommand(Button1Execute);

        }

        private void SelectionChangedExecute()
        {
            ComboBoxItem = CategoryList.FirstOrDefault(v => v.Id == Item.CategoryId);
        }

        private void Button1Execute()
        {
            Console.WriteLine(Item.Id);

            Item.Category = ComboBoxItem;
            //Item.CategoryId = ComboBoxItem.Id;
            //Item.Category.Name = ComboBoxItem.Name;

            Console.WriteLine(ComboBoxItem.Id);
            Console.WriteLine(ComboBoxItem.Name);

            Console.WriteLine(Item.Title);
            Console.WriteLine(Item.Detail);
            Console.WriteLine(Item.Attention);
        }

    }
}
