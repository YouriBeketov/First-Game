using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PasswordProtectedJail
{
    public partial class Form2 : Form
    {
        
        
        List<NameOfSavedFiles> listofsavedfiles = new List<NameOfSavedFiles>();
        
        public Form2()
        {
            InitializeComponent();
            
            Program.Readfromafile(listBox2);
            //label9.Text = DateTime.Now.ToString();
            label14.Text = listBox1.Items.Count.ToString();
            //same problem with both - they only get updated 1 time.
            Welcomesign.Text = Welcomesign.Text+ Program.WhoGotAccess();
            foreach (string savedinfo in listBox2.Items)
                
	{
        listofsavedfiles.Add(new NameOfSavedFiles() { FileName = savedinfo });
	}            
        }
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                
                label1.Text = "";
                foreach (var letter in  Program.getLastNAme(listBox1.Items[listBox1.SelectedIndex].ToString()))
                {
                    label1.Text = label1.Text +letter.ToString();
                    System.Threading.Thread.Sleep(50);
                    label1.Refresh();
                   
                }
                label2.Text = "";
                foreach (var letter in Program.getphoneNumber(listBox1.Items[listBox1.SelectedIndex].ToString()))
                {
                    label2.Text = label2.Text + letter.ToString();
                    System.Threading.Thread.Sleep(50);
                    label2.Refresh();

                }
            //label1.Text = Program.getLastNAme(listBox1.Items[listBox1.SelectedIndex].ToString());
            //label2.Text = Program.getphoneNumber(listBox1.Items[listBox1.SelectedIndex].ToString());
            }
            catch
            {
                
                label1.Text = "";
                label2.Text = "";
                
            }
        }
        private void InputB_Click(object sender, EventArgs e)
        {
            if (FirstText.Text == ""||FirstText.Text == " ")
            {
                MessageBox.Show("Entry must have a name.");
            }
            
            else
            {

            bool check;
            bool check1;
            bool check2;
            check2 = PHNUmberEntry1.Text.Contains("|");
            check1 = SecondTextBob.Text.Contains("|");
            check = FirstText.Text.Contains("|");
            if (check|| check1||check2)
            {
                MessageBox.Show("Entry can not contain '|' char. \nThank you. \nFunkyMonk.");
            }
            else if ( listBox1.Items.Contains(FirstText.Text))
            {
                

                MessageBox.Show("This entry already exists \nEntry did NOT Save.");

            }
            else
            {
                listBox1.Items.Add(FirstText.Text);

                Program.addItem(FirstText.Text, SecondTextBob.Text, PHNUmberEntry1.Text);
                label14.Text =  listBox1.Items.Count.ToString() ;
                FirstText.Text = "";
                SecondTextBob.Text = "";
                PHNUmberEntry1.Text = "";
            }
        }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            Program.DelList();
            label14.Text = listBox1.Items.Count.ToString() ;
            label1.Text = "";
            label2.Text = "";

        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //listBox1.Items.Remove[listBox1.SelectedIndex];
            try
            {
                Program.delItem(listBox1.SelectedIndex);
                listBox1.Items.RemoveAt(listBox1.SelectedIndex);
                label14.Text =  listBox1.Items.Count.ToString() ;
                
            }
            catch
            {
                MessageBox.Show("Select the person you want to delete.");
            }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text=="" || textBox1.Text== " ")
            {
                MessageBox.Show("PLease enter a name for your save\nFile did NOT save.");
                textBox1.Clear();

            }
            else if ( listBox2.Items.Contains(textBox1.Text))
            {
                MessageBox.Show("This name already exists.\nFile did NOT save.");
            }
            else if (textBox1.Text.Contains(",") || textBox1.Text.Contains(",") || textBox1.Text.Contains("|") || textBox1.Text.Contains("<") || textBox1.Text.Contains("/") || textBox1.Text.Contains("?") || textBox1.Text.Contains(">") || textBox1.Text.Contains("'") || textBox1.Text.Contains('"') || textBox1.Text.Contains("}") || textBox1.Text.Contains("{") || textBox1.Text.Contains("[") || textBox1.Text.Contains("]") || textBox1.Text.Contains("*") || textBox1.Text.Contains(":") || textBox1.Text.Contains(";") || textBox1.Text.Contains("\\"))
            {
                MessageBox.Show("Wrong Save Name.\nFile did NOT save.");
                textBox1.Clear();
                

            }

            else if (textBox1.Text == "Userinfo" || textBox1.Text == "SavedTxtNames22" || textBox1.Text == "SavedTxtNames22" + Program.WhoGotAccess() || textBox1.Text == "Userinfo"+Program.WhoGotAccess())
            {
                MessageBox.Show("Nice try hacker. \nDatabase shutdown!");
                Form1 f1 = new Form1();
                this.Hide();       
                f1.ShowDialog();
                this.Close();
            }
            else
            {//==========================================================================================================================================

                    string AddToName = Program.WhoGotAccess();
                    string nameOfFIle = textBox1.Text+AddToName;
                    string nameOfFIle2 = textBox1.Text;
                    Program.EnterListNAmeToSave(nameOfFIle);
                    listofsavedfiles.Add(new NameOfSavedFiles() { FileName = nameOfFIle2 });
                    Program.SaveSavedlist(listofsavedfiles);
                    List<Phonenumber> Savedlist = new List<Phonenumber>();
                    foreach (string info2 in listBox1.Items)
                    {
                        //listofsavedfiles.Add(new NameOfSavedFiles() { FileName = info2 });

                        Savedlist.Add(new Phonenumber() { LAstNAme = info2 ,Name = info2, NUmber = info2 }); //something fishy here 
                    }

                    listBox2.Items.Add(textBox1.Text);
                    //Program.DelList();//======================================
                    label14.Text = listBox1.Items.Count.ToString();
                    textBox1.Text = "";
                }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                string AddToName = Program.WhoGotAccess();

                listBox1.Items.Clear();
                Program.DelList();
                Program.Readfromafile(listBox2.SelectedItem.ToString() + AddToName, listBox1);

                label14.Text = listBox1.Items.Count.ToString();
            }
            catch
            {
                MessageBox.Show("Please select a file to load.");
            }
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            Program.DelList();
            //MakeLIstsofData.MAkeList1();
            listBox1.Items.Clear();
            Form1 f1 = new Form1();
            this.Hide();
            f1.ShowDialog();
            this.Close();

        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                bool FilesExists = false;
                int IdenID = listBox2.SelectedIndex;
                string AddToFileName =Program.WhoGotAccess();
                
                string x = listBox2.Items[IdenID].ToString()+AddToFileName + ".txt";

                if (System.IO.File.Exists(x))
                {

                    FilesExists = true;
                    listofsavedfiles.RemoveAt(IdenID);
                    listBox2.Items.RemoveAt(IdenID);
                    Program.SaveSavedlist(listofsavedfiles);

                    MessageBox.Show("File " + x + " deleted");

                }
                else
                {
                    MessageBox.Show("File " + x + " does not exist. This entry has been deleted.");
                    listBox2.Items.Remove(listBox2.SelectedItem.ToString());
                }
                listBox2.ClearSelected();
                //listBox2.Items.RemoveAt(listBox2.SelectedIndex);
                try
                {
                    if (FilesExists)
                    {
                        System.IO.File.Delete(x);
                    }
                }
                catch (Exception ex)
                {
                   
                    MessageBox.Show(ex.ToString());
                }
            }
            catch
            {
                MessageBox.Show("Select the file you want to delete.");
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (textBox2.Text == "" || textBox2.Text == " ")
            {
                MessageBox.Show("Please enter a name to search.");
            }
            else
            {
                if (listBox1.Items.Contains(textBox2.Text))
                {
                    int x = listBox1.FindString(textBox2.Text);
                    //MessageBox.Show("Name is in list");
                    listBox1.SetSelected(x, true);
                    textBox2.Clear();
                }
                else
                {
                    MessageBox.Show(textBox2.Text + " is not in list");
                    textBox2.Clear();
                }
            }
        }     
    }    
}
