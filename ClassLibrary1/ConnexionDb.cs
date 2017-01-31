using Classes;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Util
{
    public class ConnexionDb
    {
        private static MySqlConnection connection;

        private static ConnexionDb instance = null;
        public static ConnexionDb Instance ()
        {
            if(instance == null)
            {
                instance = new ConnexionDb();
                InitConnexion();

            }
            return instance;
        }

        private static void InitConnexion()
        {
            try
            {
                string connectionString = "SERVER=127.0.0.1; DATABASE=frameworkdb; UID=root; PASSWORD=";
                connection = new MySqlConnection(connectionString);
                connection.Open();
            }
            catch (MySqlException e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public int checkConnection(string login, string password)
        {
            int result = 0;
            try
            {
                MySqlCommand cmd = new MySqlCommand("SELECT id FROM user WHERE login=@login AND password=@pwd", connection);
                cmd.Parameters.AddWithValue("@login", login);
                cmd.Parameters.AddWithValue("@pwd", password);
                cmd.Prepare();
                MySqlDataReader reader = cmd.ExecuteReader();

                result = reader.GetInt32("id") > 0 ? reader.GetInt32("id") : -1;
            }
            catch (MySqlException e)
            {
                MessageBox.Show(e.Message);
            }

            return result;
        }

        public User getUser(int id)
        {
            User user = new User();
            try
            {
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM user WHERE id=@id", connection);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Prepare();

                MySqlDataReader reader = cmd.ExecuteReader();

                user.Id = reader.GetInt32("id");
                user.LastName = reader.GetString("lastname");
                user.FirstName = reader.GetString("firstname");
            }
            catch (MySqlException e)
            {
                MessageBox.Show(e.Message);     
            }
            return user;
        }

        public int insertData(Data data)
        {
            int result = 0;

            try
            {
                MySqlCommand cmd = new MySqlCommand("INSERT INTO Data (datajson, User_id) VALUES (@datajson, @userId)", connection);
                cmd.Parameters.AddWithValue("@datajson", data.DataJson);
                cmd.Parameters.AddWithValue("@userId", data.UserId);
                cmd.Prepare();

                result = cmd.ExecuteNonQuery();
            }
            catch (MySqlException e)
            {
                MessageBox.Show(e.Message);
            }

            return result;
        }
    }
}
