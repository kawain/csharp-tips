using Dapper;
using System;
using System.Collections.ObjectModel;
using System.Data.SQLite;
using System.IO;
using System.Windows;

namespace SharedProject
{
    public class DB
    {
        private string _dbfile = @"..\..\..\newnotes.db";
        private SQLiteConnection _conn = null;

        public DB()
        {

            if (File.Exists(_dbfile) == false)
            {
                Console.WriteLine("データベースファイルがありません");
                Application.Current.Shutdown();
            }

            _conn = new SQLiteConnection("Data Source=" + _dbfile);

        }

        public ObservableCollection<CategoryModel> CategoryList()
        {
            var list = new ObservableCollection<CategoryModel>();

            _conn.Open();

            string sql = "SELECT * FROM categories ORDER BY name;";

            var result = _conn.Query<CategoryModel>(sql);

            foreach (var v in result)
            {
                list.Add(new CategoryModel()
                {
                    Id = v.Id,
                    Name = v.Name
                });
            }

            _conn.Close();

            return list;
        }

        public ObservableCollection<MemoModel1> MemoList()
        {
            var list = new ObservableCollection<MemoModel1>();

            _conn.Open();

            string sql = "SELECT * FROM memos A INNER JOIN categories B ON A.category_id = B.id ORDER BY A.id DESC;";

            var result = _conn.Query<MemoModel1, CategoryModel, MemoModel1>(
                sql,
                (memo, cate) =>
                {
                    memo.Category = cate;
                    return memo;
                },
                splitOn: "Id"
            );

            foreach (var v in result)
            {
                list.Add(new MemoModel1()
                {
                    Id = v.Id,
                    Category = v.Category,
                    CategoryId = v.Category.Id,
                    Title = v.Title,
                    Detail = v.Detail,
                    Attention = v.Attention
                });
            }

            _conn.Close();

            return list;
        }

    }
}
