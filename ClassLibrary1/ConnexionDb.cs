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

        public User checkConnection(string login, string password)
        {
            //List<Role> roles = new List<Role>();
            User user = new User();

            try
            {
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM user WHERE login=@login AND password=@pwd", connection);
                cmd.Parameters.AddWithValue("@login", login);
                cmd.Parameters.AddWithValue("@pwd", password);
                cmd.Prepare();

                MySqlDataReader reader = cmd.ExecuteReader();

                while(reader.Read())
                {
                    user.Id = reader.GetInt32("id");
                    user.LastName = reader.GetString("lastname");
                    user.FirstName = reader.GetString("firstname");
                }

                reader.Close();  
            }
            catch (MySqlException e)
            {
                MessageBox.Show(e.Message);
            }

            if(user.Id != 0)
            {
                user.Roles = getUserRoles(user.Id);

                // Récupérer data
            }

            return user;
        }

        public List<Role> getUserRoles(int userId)
        {
            List<Role> roles = new List<Role>();
            try
            {
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM user_has_role as uhr " +
                                                    "JOIN role AS r ON r.id = uhr.Role_id WHERE User_id=@userId", connection);
                cmd.Parameters.AddWithValue("@userId", userId);
                cmd.Prepare();

                MySqlDataReader reader = cmd.ExecuteReader();


                while (reader.Read())
                {
                    Role role = new Role();
                    role.Id = reader.GetInt32("Role_id");
                    role.Name = reader.GetString("name");

                    roles.Add(role);

                }

                reader.Close();
            }
            catch (MySqlException e)
            {
                MessageBox.Show(e.Message);
            }

            return roles;
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

                while(reader.Read())
                {
                    user.Id = reader.GetInt32("id");
                    user.LastName = reader.GetString("lastname");
                    user.FirstName = reader.GetString("firstname");
                }

                reader.Close();
            }
            catch (MySqlException e)
            {
                MessageBox.Show(e.Message);     
            }
            return user;
        }

        public int insertUser(User user)
        {
            int result = 0;
            try
            {
                MySqlCommand cmd = new MySqlCommand("INSERT INTO user (firstname, lastname, login, password) VALUES (@firstname, @lastname, @login, @password) SELECT LAST_INSERT_ID()", connection);
                cmd.Parameters.AddWithValue("@firstname", user.FirstName);
                cmd.Parameters.AddWithValue("@lastname", user.LastName);
                cmd.Parameters.AddWithValue("@login", user.Login);
                cmd.Parameters.AddWithValue("@password", user.Password);
                cmd.Prepare();

                result = cmd.ExecuteNonQuery();
                long userid = cmd.LastInsertedId;
                user.Id = (int)userid;
            }
            catch (MySqlException e)
            {
                MessageBox.Show(e.Message);
            }
            this.addRole(user);
            return result;
        }

        public void addRole (User user)
        {
            try
            {
                foreach (Role role in user.Roles)
                {
                    MySqlCommand cmd = new MySqlCommand("INSERT INTO user_has_role (User_id, Role_id) VALUES (@userid, @roleid)", connection);
                    cmd.Parameters.AddWithValue("@userid", user.Id);
                    cmd.Parameters.AddWithValue("@roleid", role.Id);
                    cmd.Prepare();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (MySqlException e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public int insertData(Data data)
        {
            int result = 0;

            try
            {
                MySqlCommand cmd = new MySqlCommand("INSERT INTO data (datajson, User_id) VALUES (@datajson, @userId)", connection);
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

        public string getData(int idData)
        {
            string result = "";
            try
            {
                MySqlCommand cmd = new MySqlCommand("SELECT datajson FROM data WHERE id=@id", connection);
                cmd.Parameters.AddWithValue("@id", idData);
                cmd.Prepare();

                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    result = reader.GetString("datajson");
                }

            } catch(MySqlException e)
            {
                MessageBox.Show(e.Message);
            }

            return result;
        }

        public List<Role> getRoles()
        {
            List<Role> roles = new List<Role>();

            try
            {
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM role", connection);

                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Role role = new Role();
                    role.Id = reader.GetInt32("id");
                    role.Name = reader.GetString("name");

                    roles.Add(role);

                }

                reader.Close();
            }
            catch (MySqlException e)
            {
                MessageBox.Show(e.Message);
            }

            return roles;
        }

        public List<User> getUsers()
        {
            List<User> users = new List<User>();

            try
            {
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM user", connection);

                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    User user = new User();
                    user.Id = reader.GetInt32("id");
                    user.FirstName = reader.GetString("firstname");
                    user.LastName = reader.GetString("lastname");

                    users.Add(user);
                }
                reader.Close();
            }
            catch (MySqlException e)
            {
                MessageBox.Show(e.Message);
            }

            return users;
        }

        public List<Data> getDatas(int id)
        {
            List<Data> datas = new List<Data>();

            try
            {
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM data WHERE User_id=@id", connection);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Prepare();

                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Data data = new Data();
                    data.Id = reader.GetInt32("id");
                    data.UserId = reader.GetInt32("User_id");
                    data.DataJson = reader.GetString("datajson");

                    datas.Add(data);
                }
                reader.Close();
            }
            catch (MySqlException e)
            {
                MessageBox.Show(e.Message);
            }

            return datas;

        }
    }
}
