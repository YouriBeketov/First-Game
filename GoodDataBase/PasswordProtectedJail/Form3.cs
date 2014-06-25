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
    public partial class Form3 : Form
    {
        List<MakeAcount> IfoAct = new List<MakeAcount>();
        public Form3()
        {
            InitializeComponent();
            progressBar1.Minimum = 0;
            progressBar1.Maximum = 100;
            progressBar2.Minimum = 0;
            progressBar2.Maximum = 100;
            progressBar3.Minimum = 0;
            progressBar3.Maximum = 100;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (textBox1.Text.Length < 4)
            {
                try
                {
                    progressBar1.Value -= 100;
                    MessageBox.Show("Username must be at least 4 char");
                }
                catch
                {
                    MessageBox.Show("Username must be at least 4 char");
                }

            }
            else if (Program.IsUsernameInUse(textBox1.Text))
            {//This part is not finished! works but not everytime.
                MessageBox.Show("Username " + textBox1.Text + " is already in use.");
                textBox1.Clear();
                textBox2.Clear();
                textBox2.Clear();
            }           
            else
            {
                try
                {
                    progressBar1.Value += 100;
                    if (textBox2.Text.Length < 4)
                    {
                        try
                        {
                            progressBar2.Value -= 100;
                            MessageBox.Show("Password must be at least 4 char!");
                            textBox2.Clear();
                            textBox3.Clear();
                        }
                        catch
                        {
                            MessageBox.Show("Password must be at least 4 char!");
                            textBox2.Clear();
                            textBox3.Clear();
                        }

                    }
                    else
                    {
                        progressBar2.Value += 100;
                        if (textBox2.Text != textBox3.Text)
                        {
                            MessageBox.Show("Password does not match!");
                            textBox2.Clear();
                            textBox3.Clear();
                        }
                        else
                        {
                            progressBar3.Value += 100;
                            IfoAct.Add(new MakeAcount() { UserName = textBox1.Text, Password = textBox3.Text });
                            Program.EnterListNAmeToSave(IfoAct);
                            MessageBox.Show("Login: " + textBox1.Text + "\nPassword: " + textBox3.Text);
                            Form1 f1 = new Form1();
                            this.Hide();
                            f1.ShowDialog();
                            this.Close();
                        }
                    }
                }
                catch
                {
                    if (textBox2.Text.Length < 4)
                    {
                        try
                        {
                            progressBar2.Value -= 100;
                            MessageBox.Show("Password must be at least 4 char!");
                            textBox2.Clear();
                            textBox3.Clear();
                        }
                        catch
                        {
                            MessageBox.Show("Password must be at least 4 char!");
                            textBox2.Clear();
                            textBox3.Clear();
                        }

                    }
                    else
                    {
                        try
                        {
                            progressBar2.Value += 100;
                            if (textBox2.Text != textBox3.Text)
                            {
                                MessageBox.Show("Password does not match!");
                                textBox2.Clear();
                                textBox3.Clear();
                            }
                            else
                            {
                                progressBar3.Value += 100;
                                IfoAct.Add(new MakeAcount() { UserName = textBox1.Text, Password = textBox3.Text });
                                Program.EnterListNAmeToSave(IfoAct);
                                MessageBox.Show("Login: " + textBox1.Text + "\nPassword: " + textBox3.Text);
                                Form1 f1 = new Form1();
                                this.Hide();
                                f1.ShowDialog();
                                this.Close();
                            }

                        }
                        catch
                        {
                            if (textBox2.Text != textBox3.Text)
                            {
                                MessageBox.Show("Password does not match!");
                                textBox2.Clear();
                                textBox3.Clear();
                            }
                            else
                            {
                                try
                                {
                                    progressBar3.Value += 100;
                                    IfoAct.Add(new MakeAcount() { UserName = textBox1.Text, Password = textBox3.Text });
                                    Program.EnterListNAmeToSave(IfoAct);
                                    MessageBox.Show("Login: " + textBox1.Text + "\nPassword: " + textBox3.Text);
                                    Form1 f1 = new Form1();
                                    this.Hide();
                                    f1.ShowDialog();
                                    this.Close();
                                }
                                catch
                                {
                                    IfoAct.Add(new MakeAcount() { UserName = textBox1.Text, Password = textBox3.Text });
                                    Program.EnterListNAmeToSave(IfoAct);
                                    MessageBox.Show("Login: " + textBox1.Text + "\nPassword: " + textBox3.Text);
                                    Form1 f1 = new Form1();
                                    this.Hide();
                                    f1.ShowDialog();
                                    this.Close();

                                }

                            }
                        }

                    }
                }
            }
        }

        private void Form3_Load(object sender, EventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();
            this.Hide();
            f1.ShowDialog();
            this.Close();
        }
    }
}
