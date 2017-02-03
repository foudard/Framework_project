﻿using Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using Util;

namespace ConsoleApplication1
{
    class UserLog
    {
        private string loginUser;

        public string LoginUser
        {
            get { return loginUser; }
            set { loginUser = value; }
        }


        private string password;

        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        private List<User> users;

        public List<User> Users
        {
            get { return users; }
            set { users = value; }
        }

        private User selectedUser;

        public User SelectedUser
        {
            get { return selectedUser; }
            set { selectedUser = value; }
        }

        private List<Data> datas;

        public List<Data> Datas
        {
            get { return datas; }
            set { datas = value; }
        }




        public void authUser()
        {
            Console.Write("Login : ");
            loginUser = Console.ReadLine();

            Console.Write("Password : ");
            password = ConsoleUtil.ReadLineMasked();

            User user = Login.Log(loginUser, password);

            if(Login.checkRole(user, "admin"))
            {
                showUsers();
            }

        }

        public void showUsers()
        {

            users = UserUtil.getUsers();

            foreach (User user in users)
            {
                Console.WriteLine("[" + user.Id + "] - " + user.FirstName + " " + user.LastName);
            }

            selectUser();
        }


        public void selectUser()
        {

            Console.WriteLine("Séléctionner un user : ");

            int selectedUser = Int32.Parse(Console.ReadLine());

            if (checkUser(selectedUser))
            {
                User user = users.Find(u => u.Id == selectedUser);
                typeSelectData(user.Id);
            }

        }

        public bool checkUser(int id)
        {
            return users.Any(user => user.Id == id);

        }

        public void typeSelectData(int id)
        {

            Console.Write("Selectionner le type d'affichage (id/objet) : ");
            string type = Console.ReadLine();

            datas = UserUtil.getDatas(id);

            if (string.Equals(type, "id", StringComparison.OrdinalIgnoreCase))
            {
                showDataId();
            }
            else if (string.Equals(type, "objet", StringComparison.OrdinalIgnoreCase))
            {
                showObjectData();
            }

        }

        public void showDataId() 
        {
            foreach (Data data in datas)
            {
                Console.WriteLine(data.Id);
            }

            Console.WriteLine("Séléctionnez un identifiant : ");
            int selectedDataId = Int32.Parse(Console.ReadLine());
            selecData(selectedDataId);
           
        }

        public void showObjectData()
        {
            List<Type> listObjets = ObjectsUtil.getAllClasses();

            foreach (Type item in listObjets)
            {
                Console.WriteLine(item.Name);
            }

            Console.WriteLine("Séléctionnez un type d'objet : ");
            string selectedDataType = Console.ReadLine();
            selectDataFromType(selectedDataType);

            Console.ReadKey();

        }

        public void selecData(int id)
        {
            string datajson = datas.Find(data => data.Id == id).DataJson;

            Console.WriteLine(datajson);
        }
           
        public void selectDataFromType(string type)
        {
            List<string> jsonObject = new List<string>();
            bool isObject = false;

            foreach (Data d in datas)
            {

                isObject = JsonUtils.GetJson(d.DataJson, type);

                if (isObject)
                {
                    Console.WriteLine(d.DataJson);
                }
            }
            Console.ReadKey();
        }
    }
}
