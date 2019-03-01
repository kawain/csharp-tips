using Prism.Mvvm;

namespace SharedProject
{
    public class MemoModel2 : BindableBase
    {
        private int _id;
        public int Id
        {
            get { return _id; }
            set { SetProperty(ref _id, value); }
        }

        private CategoryModel _category;
        public CategoryModel Category
        {
            get { return _category; }
            set { SetProperty(ref _category, value); }
        }

        private int _categoryId;
        public int CategoryId
        {
            get { return _categoryId; }
            set { SetProperty(ref _categoryId, value); }
        }

        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private string _detail;
        public string Detail
        {
            get { return _detail; }
            set { SetProperty(ref _detail, value); }
        }

        private int _attention;
        public int Attention
        {
            get { return _attention; }
            set { SetProperty(ref _attention, value); }
        }
    }
}
