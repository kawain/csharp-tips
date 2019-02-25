using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfDataGridTest.Models
{
    public class DBConn
    {
        //このクラス特有のプロパティ
        public SQLiteConnection Conn { get; private set; }

        //シングルトンのプロパティ
        private static DBConn _instance;
        public static DBConn Instance
        {
            //null合体演算子
            get { return _instance ?? (_instance = new DBConn()); }
        }

        //プライベートコンストラクタ
        private DBConn()
        {

            string dbfile = "../../../newnotes.db";
            Conn = new SQLiteConnection("Data Source=" + dbfile);

            //テーブル存在確認
            int count = 0;

            Conn.Open();

            SQLiteCommand cmd = Conn.CreateCommand();

            cmd.CommandText = @"SELECT count(*) FROM sqlite_master WHERE type='table' AND name='memos';";

            SQLiteDataReader reader = cmd.ExecuteReader();

            reader.Read();

            count = Convert.ToInt32(reader["count(*)"]);

            reader.Close();

            Conn.Close();

            if (count == 0)
            {
                Console.WriteLine("※※※データベースファイルがありません※※※");
                Application.Current.Shutdown();
            }
        }
    }
}
