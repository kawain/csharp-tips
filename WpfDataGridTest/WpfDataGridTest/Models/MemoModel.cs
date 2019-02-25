using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfDataGridTest.Models
{
    public class MemoModel: BindableBase
    {
        public int Id { get; set; }
        //public CategoryModel Category { get; set; }

        private CategoryModel _category;
        public CategoryModel Category
        {
            get { return _category; }
            set { SetProperty(ref _category, value); }
        }

        public int CategoryId { get; set; }
        public string Title { get; set; }
        public string Detail { get; set; }
        public int Attention { get; set; }
    }
}
