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
    public partial class Form1 : Form
    {

        int passWordCout = 0;
        public Form1()
        {
            InitializeComponent();
            progressBar1.Minimum = 0;
            progressBar1.Maximum = 100;
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool Login;
            //Login = Program.CheckPAssword(textBox1.Text, textBox2.Text);
            
            Login = Program.CheckPAssword2(textBox1.Text, textBox2.Text);
            if (Login)
            {
                Program.DelList();
                Form2 f2 = new Form2();
                this.Hide();
                f2.ShowDialog();
                this.Close();
            }
            else
            {
                if (passWordCout == 4)
                {

                    MessageBox.Show("Wrong username/password combinatiom.\nThe application has been locked.");
                    this.Close();

                }
                else
                {
                    progressBar1.Value += 25;
                    passWordCout += 1;
                    MessageBox.Show("Wrong username/password combination. \nTry Again.");
                    textBox2.Clear();
                }
                
            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }
        // WHAYYYTTTT!!!?????

       private void textBox2_KeyUp(object sender, KeyEventArgs e)
{
     if (e.KeyCode == Keys.Enter)
     {
                Login_Button.PerformClick();
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Form3 f3 = new Form3();
            this.Hide();
            f3.ShowDialog();
            this.Close();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
        
    }
}
