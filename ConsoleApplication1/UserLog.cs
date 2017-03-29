using Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using Util;

namespace ConsoleApplication1
{
    class UserLog
    {
        private int userId;

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

        private string selectedDataType;

        public string SelectedDataType
        {
            get { return selectedDataType; }
            set { selectedDataType = value; }
        }

        private Data selectedData;

        public Data SelectedData
        {
            get { return selectedData; }
            set { selectedData = value; }
        }

        private string typeAffichage;

        public string TypeAffichage
        {
            get { return typeAffichage; }
            set { typeAffichage = value; }
        }

        private ConnexionDb connexion = ConnexionDb.Instance();

        public void showError(Action action)
        {
            Console.WriteLine("Erreur de saisie. Veuillez réessayer !");
            action();
        }


        public void authUser()
        {
            Console.Write("Login : ");
            loginUser = ConsoleUtil.ReadLine();

            Console.Write("Password : ");
            password = ConsoleUtil.ReadLineMasked();

            User user = Login.Log(loginUser, password);

            if (Login.checkRole(user, "admin")) showUsers();
            else showError(() => authUser());
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

            int selectedUser = 0;

            string input = ConsoleUtil.ReadLine();

            if (Int32.TryParse(input, out selectedUser))
            {
                if (checkUser(selectedUser))
                {
                    User user = users.Find(u => u.Id == selectedUser);
                    typeSelectData(user.Id);
                }
                else showError(() => selectUser());
            }
            else
            {
                showError(() => selectUser());
            }
        }

        public bool checkUser(int id)
        {
            return users.Any(user => user.Id == id);
        }

        public void typeSelectData(int id)
        {
            userId = id;

            Console.Write("Sélectionner le type d'affichage (id/objet) : ");
            TypeAffichage = ConsoleUtil.ReadLine();

            datas = UserUtil.getDatas(id);

            if (string.Equals(TypeAffichage, "id", StringComparison.OrdinalIgnoreCase))
            {
                showDataId();
            }
            else if (string.Equals(TypeAffichage, "objet", StringComparison.OrdinalIgnoreCase))
            {
                showObjectData();
            }
            else showError(() => typeSelectData(userId));
        }

        public void showDataId() 
        {
            bool isGoodId = false;
            foreach (Data data in datas)
            {
                Console.WriteLine(data.Id);
            }
            Console.WriteLine("Sélectionnez un identifiant : ");

            int selectedDataId = 0;

            if(Int32.TryParse(ConsoleUtil.ReadLine(), out selectedDataId))
            {
                foreach (Data data in datas)
                {
                    if (selectedDataId == data.Id) isGoodId = true;
                }
                if (isGoodId == true)
                {

                    selecData(selectedDataId);
                    selectAction(SelectedData);
                }
                else showError(() => showDataId());
            }
            else showError(() => showDataId());
        }

        public void showObjectData()
        {
            bool isGoodName = false;
            List<Type> listObjets = ObjectsUtil.getAllClasses();
            foreach (Type item in listObjets)
            {
                Console.WriteLine(item.Name);
            }
            Console.WriteLine("Sélectionnez un type d'objet : ");

            SelectedDataType = ConsoleUtil.ReadLine();

            foreach (Type item in listObjets)
            {
                if (item.Name == SelectedDataType) isGoodName = true;
            }
            if (isGoodName == true)
            {
                selectDataFromType();
                typeSelectData(userId);
            }
            else showError(() => showObjectData());
        }

        public void selecData(int id, List<Data> datasList = null)
        {
            if (datasList == null)
            {
                datasList = Datas;

                
            }

            SelectedData = datasList.Find(data => data.Id == id);

            string datajson = SelectedData.DataJson;

            if(String.Equals(TypeAffichage, "id"))
            {
                SelectedDataType = JsonUtils.getJsonObjectType(datajson);
            }

            Console.WriteLine(datajson);
        }
           
        public void selectDataFromType()
        {
            List<string> jsonObject = new List<string>();
            bool isObject = false;

            List<Data> subDataList = new List<Data>();

            foreach (Data d in datas)
            {

                isObject = JsonUtils.GetJson(d.DataJson, SelectedDataType);

                if (isObject)
                {
                    Console.WriteLine("[" + d.Id + "] - " + d.DataJson);
                    subDataList.Add(d);
                }
            }

            Console.WriteLine("Séléctionnez un objet : ");

            int idData = 0;

            if (Int32.TryParse(ConsoleUtil.ReadLine(), out idData)) {

                selecData(idData, subDataList);
                selectAction(SelectedData);
            }
            else showError(() => selectDataFromType());
        }

        public void selectAction(Data data)
        {
            Console.Write("Quelle action voulez vous effectuer ? ( c = create , u = update , d = delete ) : ");
            string selectedAction = ConsoleUtil.ReadLine().ToLower();

            switch (selectedAction) {
                case "c":
                    string createData = ConsoleUtil.ReadLine();

                    if (JsonUtils.IsValidJson(createData))
                    {
                        Data dataCreated = new Data();
                        dataCreated.DataJson = createData;
                        dataCreated.UserId = SelectedUser.Id;
                        connexion.insertData(dataCreated);
                        selectUser();
                    }
                    else showError(() => selectAction(data));

                    break;

                case "u":

                    string updateData = ConsoleUtil.ReadLine();

                    bool isJsonValid = JsonUtils.GetJson(updateData, SelectedDataType);

                    if (isJsonValid)
                    {
                        data.DataJson = updateData;
                        connexion.UpdateData(data);
                        selectUser();
                    }
                    else showError(() => selectAction(data));

                    break;

                case "d":

                    Console.Write("Voulez-vous vraiment supprimer l'objet : " + data.DataJson + " (Y/N) : ");
                    string response = ConsoleUtil.ReadLine().ToLower();

                    if(string.Equals(response, "y"))
                    {
                        connexion.deleteData(data.Id);
                        selectUser();
                    }
                    else if (string.Equals(response, "n"))
                    {
                        selectUser();
                    }
                    else showError(() => selectAction(data));

                    break;
                default:
                    showError(() => selectAction(data));
                    break;
            }
        }
    }
}
