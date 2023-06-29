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
    public partial class FormEditEm : Form
    {
        APD65_63011212019Entities context = new APD65_63011212019Entities();
        BindingSource f6;
        int id;
        public FormEditEm(BindingSource f6, int id)
        {
            InitializeComponent();
            this.MinimizeBox = false;
            this.MaximizeBox = false;

            var em = context.Emplopeexes.Where(ed => ed.ID == id).First();
            guna2TextBox1.Text = em.FirstName;
            guna2TextBox2.Text = em.LastName;
            guna2ComboBox1.Text = em.status;
            guna2TextBox4.Text = em.UserName;
            guna2TextBox5.Text = em.Password;

            this.f6 = f6;
            this.id = id;
        }

        private void FormEditEm_Load(object sender, EventArgs e)
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
                    var em1 = context.Emplopeexes.Where(ed => ed.ID == id).First();
                    em1.FirstName = guna2TextBox1.Text;
                    em1.LastName = guna2TextBox2.Text;
                    em1.status = guna2ComboBox1.Text;
                    em1.UserName = guna2TextBox4.Text;
                    em1.Password = guna2TextBox5.Text;

                    context.SaveChanges();
                    this.Close();
                    f6.DataSource = context.Emplopeexes.ToList();
                }

            }
            catch { }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
