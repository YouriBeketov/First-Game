using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PasswordProtectedJail
{
    class MakeAcount
    {
        public string UserName { get; set; }
        public string Password { get; set; }

    }
    class NameOfSavedFiles
    {
        public string FileName { get; set; }
    }
   
    class Phonenumber
    {
        public string NUmber { get; set; }
        public string Name { get; set; }
        public string LAstNAme { get; set; }
    }
    class MakeLIstsofData// no i do think sooo 
    {
        public static List<Phonenumber> MAkeList1()
        {
            List<Phonenumber> NamesList = new List<Phonenumber>();
            return (NamesList);
        }
    }
    static class Program
    {
        
        public static List<Phonenumber> NamesList = new List<Phonenumber>();
        public static List<NameOfSavedFiles> SavedFilesListPR = new List<NameOfSavedFiles>();
        public static List<MakeAcount> UserWhoGotAccess = new List<MakeAcount>();
        public static Dictionary<string, string> UserDic = new Dictionary<string, string>();

        public static bool IsUsernameInUse(string x)
        {
            if (UserDic.ContainsKey(x))
            {
                return (true);
            }
            else
            {

                return (false);
            }

        }
        public static void DelList()
        {
            NamesList.Clear();
        }
        public static string getLastNAme(string First)
        {

            foreach (var LookUP in NamesList)
            {
                if (LookUP.Name == First)
                {
                    return (LookUP.LAstNAme.ToString());
                }
                else
                { continue; }
            }

            return "Error 2lastname";
        }

        public static string getphoneNumber(string Numberinput)
        {
            foreach (var LookUP in NamesList)
            {
                if (LookUP.Name == Numberinput)
                {
                    return (LookUP.NUmber.ToString());
                }
                else
                { continue; }
            }

            return "Error 2Numberinput";


        }
        public static void delItem (int x)
        {
            NamesList.RemoveAt(x);
        }
        public static void addItem(string First, string Last, string PhNUmber)
        {
            NamesList.Add(new Phonenumber() { Name = First, LAstNAme = Last, NUmber = PhNUmber });

        }
        public static void SaveSavedlist(List<NameOfSavedFiles> x)
        {
            
            try
            {
                string UserUniqueSave = WhoGotAccess();
                string Nameoffile = "SavedTxtNames22" + UserUniqueSave + ".txt";
                TextWriter tw = new StreamWriter(Nameoffile);
                foreach (var item in x)
                {
                    tw.WriteLine(item.FileName);

                }
                tw.Close();

            }
            catch (Exception xp)
            {
             
                MessageBox.Show(xp.ToString());
                string UserUniqueSave = WhoGotAccess();
                string Nameoffile = "SavedTxtNames22" + UserUniqueSave + ".txt";
                StreamReader myReader = new StreamReader(Nameoffile);
                string data = "";

                while (data != null)
                { data = myReader.ReadLine(); }

                if (data == null)
                {
                    myReader.Close();
                    TextWriter tw = new StreamWriter(Nameoffile);
                    foreach (var item in x)
                    {
                        tw.WriteLine(item.FileName);

                    }
                    tw.Close();
                }
            }
            
        }
        public static void EnterListNAmeToSave(List<MakeAcount> x)
        {
            try
            {              
                using (System.IO.StreamWriter file = new System.IO.StreamWriter("Userinfo.txt", true))
                {
                    foreach (var info in x)
                    {
                        file.WriteLine(info.UserName + "|" + info.Password);
                    }
                    file.Close();
                }
            }
            catch (Exception EX)
            {
                MessageBox.Show(EX.ToString());
                
            }
        }
                   
        public static void EnterListNAmeToSave(String x)
        {
            try
            {
                string Nameoffile = x + ".txt";
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(Nameoffile, true))
                {
                    foreach (var item in NamesList)/////////////issue hereeeeeee-----------------------------------------------
                    {
                        file.WriteLine(item.Name + "|" + item.LAstNAme + "|" + item.NUmber);                      
                    }
                    file.Close();
                }               
            }
            catch
            {
                MessageBox.Show("Incorrect file name !\nYour save failed.");
            }
        }
        public static ListBox Readfromafile(ListBox y)
        {
            try
            {
                string UserUniqueSave = WhoGotAccess();
                string Nameoffile = "SavedTxtNames22" + UserUniqueSave + ".txt";
                StreamReader myReader = new StreamReader(Nameoffile);
                string data = "";                           
                    while (data != null)
                    {
                        data = myReader.ReadLine();
                        if (data != null)
                        {
                            y.Items.Add(data);
                        }
                    }
                    myReader.Close();
                    return (y);                   
                }
            catch
            {
                MessageBox.Show("There are no saved files in this acount.");
                return (y);
            }

        }
        /*
        public static void DeletsingleTxtLign(string x)
        {
            StreamReader myReader = new StreamReader("SavedTxtNames22.txt");
            string data = "";
            while (data != null)
            {

                data = myReader.ReadLine();
                if (data == x)
                {
                    // how do i delete a line ???
                }
            }   
        }
         */
        
        public static Dictionary<string,string> Readfromafile()
        {
            try
            {
                StreamReader myReader = new StreamReader("Userinfo.txt");
                string data = "";
                try
                {
                    while (data != null)
                    {
                        string[] DatafromFIle = new string[2];
                        data = myReader.ReadLine();
                        DatafromFIle = data.Split('|'); //// goes to exception 
                        if (DatafromFIle != null)
                        {
                            try
                            {                               
                                UserDic.Add(DatafromFIle[0], DatafromFIle[1]);

                            }
                            catch
                            {

                            }
                        }
                    }
                    myReader.Close();
                    return (UserDic);
                }
                catch (Exception p)
                {
                    myReader.Close(); ////// Took me one hour to find this ! 
                   // MessageBox.Show(p.ToString());////whyyyyyyyyyyyyyyyyyyyyyyyyyyyyyy?????????????????
                    return (UserDic);
                }
            }
            catch 
            {
                MessageBox.Show("You currently do not have any users.\nPlease create an account. ");
                return (UserDic);
            }
        }        
        public static bool CheckPAssword2(string username, string password)
        {

            bool GoodUserName;
            bool GoodPAssword;       
            UserDic = Readfromafile();
            if (UserDic.ContainsKey(username))
            {
                GoodUserName = true;
                if (UserDic[username].Equals(password))
                {
                    UserWhoGotAccess.Add(new MakeAcount() { UserName = username, Password=password});
                    GoodPAssword = true;
                    return (GoodPAssword);
                }
                else
                {
                    GoodUserName = false;
                    return (GoodUserName);
                }
            }
            else
            {
                return (false);
            }
            }  
        public static string WhoGotAccess ()
        {
            string NameAG="";
            foreach (var item in UserWhoGotAccess)
            {
               NameAG  = item.UserName;             
            }
            return(NameAG);
        }
        public static ListBox Readfromafile(string x, ListBox y)
        {  
            string [] DatafromFIle=new string[4] ;
            string NameOfFileToRead = x + ".txt";
            StreamReader myReader = new StreamReader(NameOfFileToRead);
            string data = "";
            try
            {
                while (data != null)
                {
                    data = myReader.ReadLine();
                    DatafromFIle = data.Split('|'); //// goes to exception 
                    if (DatafromFIle != null)
                        y.Items.Add(DatafromFIle[0]);
                    Program.addItem(DatafromFIle[0], DatafromFIle[1], DatafromFIle[2]);
                    // NamesList.Add(new Phonenumber() { Name = DatafromFIle[0], LAstNAme = DatafromFIle[1], NUmber = DatafromFIle[2] });
                }
                myReader.Close();
                return (y);
            }
            catch (Exception p)
            {
                myReader.Close(); /// you mofo!!!!!!!!!!! 
                MessageBox.Show("Loaded "+ NameOfFileToRead);
                //MessageBox.Show(p.ToString());
                return (y);
            }     
        }
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
