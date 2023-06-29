using Guna.UI2.WinForms;
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
    public partial class FormAddEm : Form
    {
        APD65_63011212019Entities context = new APD65_63011212019Entities();
        BindingSource f5;
        public FormAddEm(BindingSource f5)
        {
            InitializeComponent();

            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this.f5 = f5;
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (guna2TextBox1.Text == String.Empty || guna2TextBox2.Text == String.Empty || guna2ComboBox1.Text == String.Empty || guna2TextBox4.Text == String.Empty || guna2TextBox5.Text == String.Empty)
                {
                    MessageBox.Show("กรุณาเพิ่มข้อมูล");
                }
                else
                {
                    Emplopeex em1 = new Emplopeex();
                    em1.FirstName = guna2TextBox1.Text;
                    em1.LastName = guna2TextBox2.Text;
                    em1.status = guna2ComboBox1.Text;
                    em1.UserName = guna2TextBox4.Text;
                    em1.Password = guna2TextBox5.Text;

                    context.Emplopeexes.Add(em1);
                    context.SaveChanges();
                    this.Close();
                    f5.DataSource = context.Emplopeexes.ToList();
                }
            }catch { }

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
