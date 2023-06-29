using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectAPD
{
    public partial class Form1 : Form
    {
        Form2 form2;
        Emplopeex user;
        int id;
        APD65_63011212019Entities context = new APD65_63011212019Entities();
        public Form1(Form2 form2, Emplopeex user)
        {
            this.form2 = form2;
            this.user = user;
            InitializeComponent();
            label8.Text = user.FirstName;
            emplopeexBindingSource.DataSource = context.Emplopeexes.ToList(); 
            customerxBindingSource.DataSource = context.Customerxes.ToList();
            productxBindingSource.DataSource = context.Productxes.ToList();
            billxBindingSource.DataSource=context.Billxes.Select(b => new {
                b.BillId,
                b.Customerx.Fname,
                b.Customerx.Lname,
                b.Customerx.Phone,
                b.Date,
                b.Time,
                b.TotalPrice
            }).ToList();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void guna2ControlBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void guna2Panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            form2.Visible = true;
        }
        private Image LoadImage(string url)
        {
            HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            myHttpWebRequest.UserAgent = "Chrome/105.0.0.0";
            HttpWebResponse myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
            Stream streamResponse = myHttpWebResponse.GetResponseStream();
            Bitmap bmp = new Bitmap(streamResponse);
            streamResponse.Dispose();
            return bmp;
        }

        private void guna2Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {

        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            //ค้นหา
            emplopeexBindingSource.DataSource = context.Emplopeexes.Where(emp => (emp.FirstName.ToString()).Contains(guna2TextBox1.Text)
            || (emp.status.ToString()).Contains(guna2TextBox1.Text)
            || (emp.ID.ToString()).Contains(guna2TextBox1.Text)
            || (emp.UserName.ToString()).Contains(guna2TextBox1.Text)).ToList();
        }

        private void guna2Panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void guna2TextBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void guna2DataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void guna2Button2_Click_1(object sender, EventArgs e)
        {
            try
            {
                //ลบ
                string id = guna2DataGridView3.SelectedRows[0].Cells[0].Value.ToString();
                context.Emplopeexes.Remove(context.Emplopeexes.Where(em => em.ID.ToString() == id).First());
                context.SaveChanges();
                emplopeexBindingSource.DataSource = context.Emplopeexes.ToList();
            }
            catch { }

        }

        private void guna2TextBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void guna2DataGridView5_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            this.id = int.Parse(guna2DataGridView5.SelectedRows[0].Cells[0].Value.ToString());

            var pddata = context.Productxes.Where(p => p.ProductId == id).First();
            var image = pddata.Image;
            pictureBox1.Image = LoadImage(image);
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            try
            {
                FormAddEm f5 = new FormAddEm(emplopeexBindingSource);
                f5.ShowDialog();
            }
            catch { }

        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            try
            {
                FormEditEm f6 = new FormEditEm(emplopeexBindingSource, id);
                f6.ShowDialog();
            }
            catch
            {

            }

        }

        private void guna2DataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //แก้ไข
            this.id = int.Parse(guna2DataGridView3.SelectedRows[0].Cells[0].Value.ToString());
        }

        private void guna2Button7_Click(object sender, EventArgs e)
        {
            try
            {
                FormAddCtm f7 = new FormAddCtm(customerxBindingSource);
                f7.ShowDialog();
            }
            catch { }

        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            try
            {
                string id = guna2DataGridView4.SelectedRows[0].Cells[0].Value.ToString();
                context.Customerxes.Remove(context.Customerxes.Where(em => em.Cid.ToString() == id).First());
                context.SaveChanges();
                customerxBindingSource.DataSource = context.Customerxes.ToList();
            }
            catch { }

        }

        private void guna2DataGridView4_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //แก้ไข
            this.id = int.Parse(guna2DataGridView4.SelectedRows[0].Cells[0].Value.ToString());
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            try
            {
                FormEditCtm f8 = new FormEditCtm(customerxBindingSource, id);
                f8.ShowDialog();
            } catch 
            { 
            
            }
        }

        private void guna2Button8_Click(object sender, EventArgs e)
        {
            //ค้นหา
            customerxBindingSource.DataSource = context.Customerxes.Where(emp => (emp.Fname.ToString()).Contains(guna2TextBox2.Text)
            || (emp.Cid.ToString()).Contains(guna2TextBox2.Text)
            || (emp.Lname.ToString()).Contains(guna2TextBox2.Text)
            || (emp.Phone.ToString()).Contains(guna2TextBox2.Text)).ToList();
        }

        private void guna2Button12_Click(object sender, EventArgs e)
        {
            productxBindingSource.DataSource = context.Productxes.Where(emp => (emp.ProductId.ToString()).Contains(guna2TextBox4.Text)
            || (emp.Name).Contains(guna2TextBox4.Text)
            || (emp.Tid.ToString()).Contains(guna2TextBox4.Text)
            || (emp.Description.ToString()).Contains(guna2TextBox4.Text)
            || (emp.TypeProduct.TypeName).Contains(guna2TextBox4.Text)).ToList();
        }

        private void guna2Button11_Click(object sender, EventArgs e)
        {
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            productxBindingSource.DataSource = context.Productxes.Where(emp => (emp.Name).Contains(comboBox1.Text)).ToList();
        }

        private void guna2TextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2DataGridView5_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void tabPage4_Click(object sender, EventArgs e)
        {

        }
        private void guna2DataGridView6_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }
    }
}
