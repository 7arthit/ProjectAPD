using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectAPD
{
    public partial class Form2 : Form
    {
        APD65_63011212019Entities context = new APD65_63011212019Entities();
        public Form2()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var UserNames = context.Emplopeexes.Select(em => em.UserName).ToList();
            var Passwords = context.Emplopeexes.Select(em => em.Password).ToList();

            if (UserNames.Contains(guna2TextBox1.Text))
            {
                if (Passwords.Contains(guna2TextBox2.Text))
                {
                    var user = context.Emplopeexes.Where(em => em.UserName == guna2TextBox1.Text).First();
                    if (user != null)
                    {
                        if (user.status.ToLower().Equals("Owner".ToLower()))
                        {
                            Form1 form1 = new Form1(this, user);
                            form1.Visible = true;
                        }

                        else if (user.status.ToLower().Equals("Seller".ToLower()))
                        {
                            Form3 form3 = new Form3(this, user);
                            form3.Visible = true;
                        }
                        else if(user.status.ToLower().Equals("Supervise".ToLower()))
                        {
                            Form4 form4 = new Form4(this, user);
                            form4.Visible = true;
                        }
                        this.Visible = false;
                    }
                }
            } else
            {
                MessageBox.Show("ข้อมูลไม่ถูกต้อง");
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2ControlBox2_Click(object sender, EventArgs e)
        {

        }

        private void guna2ControlBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
