using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace bookclub_desktop
{
    internal class DBHelper
    {
        MySqlConnection conn;

        public DBHelper()
        {
            MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder();
            builder.Server = "localhost";
            builder.Port = 3306;
            builder.Database = "members";
            builder.UserID = "root";
            builder.Password = "";
            conn = new MySqlConnection(builder.ConnectionString);
            conn.Open();
        }
        internal List<Member> ReadMembers()
        {
            List<Member> members = new List<Member>();
            string sql = "select * from members";
            MySqlCommand command = conn.CreateCommand();
            command.CommandText = sql;
            using (MySqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    int id = reader.GetInt32("id");
                    string name = reader.GetString("name");
                    string? gender = reader["gender"].ToString();
                    DateTime birth_date = reader.GetDateTime("birth_date")  ;
                    bool banned = reader.GetBoolean("banned");
                    Member member = new Member(id, name, gender,birth_date, banned);
                    members.Add(member);
                }
            }
            return members;
        }
    }
}