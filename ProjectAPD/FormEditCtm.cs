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
    public partial class FormEditCtm : Form
    {
        APD65_63011212019Entities context = new APD65_63011212019Entities();
        BindingSource f8;
        int id;
        public FormEditCtm(BindingSource f8, int id)
        {
            InitializeComponent();
            this.MinimizeBox = false;
            this.MaximizeBox = false;

            var ctm = context.Customerxes.Where(ed => ed.Cid == id).First();
            guna2TextBox1.Text = ctm.Fname;
            guna2TextBox2.Text = ctm.Lname;
            guna2TextBox3.Text = ctm.Phone.ToString();
   
            this.f8 = f8;
            this.id = id;
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (guna2TextBox1.Text == String.Empty || guna2TextBox2.Text == String.Empty || guna2TextBox3.Text == String.Empty)
                {
                    MessageBox.Show("กรุณาเพิ่มข้อมูล");
                }
                else
                {
                    var ctm1 = context.Customerxes.Where(ed => ed.Cid == id).First();

                    ctm1.Fname = guna2TextBox1.Text;
                    ctm1.Lname = guna2TextBox2.Text;
                    ctm1.Phone = guna2TextBox3.Text;

                    context.SaveChanges();
                    this.Close();
                    f8.DataSource = context.Customerxes.ToList();
                }

            } catch { }

        }

        private void FormEditCtm_Load(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
