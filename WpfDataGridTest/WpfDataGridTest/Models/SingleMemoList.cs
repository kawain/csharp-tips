using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfDataGridTest.Models
{
    public class SingleMemoList
    {
        private static ObservableCollection<MemoModel> _instance;

        private SingleMemoList()
        {

        }

        public static ObservableCollection<MemoModel> Instance
        {
            get
            {
                if (_instance == null)
                {

                    ObservableCollection<MemoModel> list = new ObservableCollection<MemoModel>();

                    var Conn = DBConn.Instance.Conn;

                    Conn.Open();

                    SQLiteCommand cmd = Conn.CreateCommand();

                    cmd.CommandText = @"SELECT
memos.id,
memos.category_id,
memos.title,
memos.detail,
memos.attention,
categories.id AS cid,
categories.name 
FROM memos LEFT JOIN categories ON
memos.category_id = categories.id
ORDER BY memos.id DESC;";
                    SQLiteDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        list.Add(new MemoModel()
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            Category = new CategoryModel()
                            {
                                Id = Convert.ToInt32(reader["cid"]),
                                Name = reader["name"].ToString()
                            },
                            CategoryId = Convert.ToInt32(reader["cid"]),
                            Title = reader["title"].ToString(),
                            Detail = reader["detail"].ToString(),
                            Attention = Convert.ToInt32(reader["attention"])
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
