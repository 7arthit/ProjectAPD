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
    public partial class FormAddCtm : Form
    {
        APD65_63011212019Entities context = new APD65_63011212019Entities();
        BindingSource f7;
        public FormAddCtm(BindingSource f7)
        {
            InitializeComponent();
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this.f7 = f7;
        }

        private void FormAddCtm_Load(object sender, EventArgs e)
        {
      
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
                    Customerx ctm1 = new Customerx();
                    ctm1.Fname = guna2TextBox1.Text;
                    ctm1.Lname = guna2TextBox2.Text;
                    ctm1.Phone = guna2TextBox3.Text;

                    context.Customerxes.Add(ctm1);
                    context.SaveChanges();
                    this.Close();
                    f7.DataSource = context.Customerxes.ToList();
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
