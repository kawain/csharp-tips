using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfDataGridTest.Models
{
    public class SingleCategoryList
    {
        private static ObservableCollection<CategoryModel> _instance;

        private SingleCategoryList()
        {

        }

        public static ObservableCollection<CategoryModel> Instance
        {
            get
            {
                if (_instance == null)
                {
                    ObservableCollection<CategoryModel> list = new ObservableCollection<CategoryModel>();

                    var Conn = DBConn.Instance.Conn;

                    Conn.Open();

                    SQLiteCommand cmd = Conn.CreateCommand();

                    cmd.CommandText = @"SELECT * FROM categories ORDER BY name;";
                    SQLiteDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        list.Add(new CategoryModel()
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            Name = reader["name"].ToString()
                        });
                    }

                    reader.Close();

                    Conn.Close();

                    _instance = list;
                }
                return _instance;
            }
        }
    }
}
