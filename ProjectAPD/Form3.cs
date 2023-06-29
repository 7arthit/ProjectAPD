using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ProjectAPD
{
    public partial class Form3 : Form
    {
        Form2 form2;
        Emplopeex user;
        int id;
        APD65_63011212019Entities context = new APD65_63011212019Entities();
        List<Productx> listproduct = new List<Productx>();
        //List<Productx> listcombo = new List<Productx>();
        int totalPrice;
        Boolean blf = false;
        public Form3(Form2 form2, Emplopeex user)
        {
            this.form2 = form2;
            this.user = user;
            InitializeComponent();
            label8.Text = user.FirstName;
            customerxBindingSource.DataSource = context.Customerxes.ToList();
            productxBindingSource1.DataSource = context.Productxes.ToList();
            comboSetBindingSource.DataSource = context.ComboSets.ToList();
            billxBindingSource1.DataSource=context.Billxes.Select(b => new {
                b.BillId,
                b.Customerx.Fname,
                b.Customerx.Lname,
                b.Customerx.Phone,
                b.Date,
                b.Time,
                b.TotalPrice
            }).ToList();


        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void guna2ControlBox1_MouseDown(object sender, MouseEventArgs e)
        {

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

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            form2.Visible = true;
        }

        private void guna2ControlBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
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

        private void guna2DataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            this.id = int.Parse(guna2DataGridView2.SelectedRows[0].Cells[0].Value.ToString());

            var pddata = context.Productxes.Where(p => p.ProductId == id).First();
            var image = pddata.Image;
            pictureBox1.Image = LoadImage(image);
        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button9_Click(object sender, EventArgs e)
        {
            Qrcode qrcode = new Qrcode(productxBindingSource,listproduct,label20);
            qrcode.ShowDialog();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            productxBindingSource1.DataSource = context.Productxes.Where(emp => (emp.ProductId.ToString()).Contains(guna2TextBox1.Text) 
            || (emp.Name).Contains(guna2TextBox1.Text)
            || (emp.Tid.ToString()).Contains(guna2TextBox1.Text)
            || (emp.Description.ToString()).Contains(guna2TextBox1.Text)
            || (emp.TypeProduct.TypeName).Contains(guna2TextBox1.Text)).ToList();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void DataGridViewProductList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            this.id = int.Parse(DataGridViewProductList.SelectedRows[0].Cells[0].Value.ToString());

            var pddata1 = context.Productxes.Where(p => p.ProductId == id).First();
            var image = pddata1.Image;
            //pictureBox2.Image = LoadImage(image);
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            try
            {
                int id = int.Parse(DataGridViewProductList.SelectedRows[0].Cells[0].Value.ToString());

                foreach (DataGridViewRow item in DataGridViewProductList.SelectedRows)
                {
                    DataGridViewProductList.Rows.RemoveAt(item.Index);
                }
                if (blf)
                {
                    int newTotal = 0;
                    foreach (Productx p in listproduct)
                    {
                        newTotal += p.UnitPrice;
                    }
                    totalPrice = newTotal;
                    label20.Text = totalPrice.ToString();
                }


                productxBindingSource.DataSource = listproduct;
                blf = true;
                //textBox9.Text = String.Empty;
                //textBox6.Text = String.Empty;
                //textBox8.Text = String.Empty;
                //textBox7.Text = String.Empty;
                //pictureBox2.Image = null;
            }
            catch { }
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            try
            {
                //listproduct.Clear();
                //totalPrice = 0;
                //label20.Text = totalPrice.ToString();
                //productxBindingSource.DataSource = null;
                //textBox9.Text = String.Empty;
                //textBox6.Text = String.Empty;
                //textBox8.Text = String.Empty;
                //textBox7.Text = String.Empty;
                //pictureBox2.Image = null;
                //guna2TextBox5.Text = String.Empty;
                //guna2TextBox7.Text = String.Empty;
                //guna2TextBox6.Text = String.Empty;
            }
            catch { }
        }

        private void guna2Button8_Click(object sender, EventArgs e)
        {
            //ค้นหา
            customerxBindingSource.DataSource = context.Customerxes.Where(emp => (emp.Cid.ToString()).Contains(guna2TextBox2.Text)
            || (emp.Fname.ToString()).Contains(guna2TextBox2.Text) 
            || (emp.Lname.ToString()).Contains(guna2TextBox2.Text) 
            || (emp.Phone.ToString()).Contains(guna2TextBox2.Text)).ToList();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            productxBindingSource1.DataSource = context.Productxes.Where(emp => (emp.Name).Contains(comboBox1.Text)).ToList();
        }

        private void guna2Button7_Click(object sender, EventArgs e)
        {
            FormAddCtm f7 = new FormAddCtm(customerxBindingSource);
            f7.ShowDialog();
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            try
            {
                FormEditCtm f8 = new FormEditCtm(customerxBindingSource, id);
                f8.ShowDialog();
            }catch { }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            try
            {
                Customerx customrt = new Customerx
                {
                    Fname = guna2TextBox5.Text,
                    Lname = guna2TextBox7.Text,
                    Phone = guna2TextBox6.Text,
                };
                context.Customerxes.Add(customrt);
                context.SaveChanges();


                int ctmid = context.Customerxes.Max(x => x.Cid);

                Billx billx = new Billx
                {
                    Date = DateTime.Now.Date,
                    Time = DateTime.Now.TimeOfDay,
                    Cid = ctmid,
                    TotalPrice = int.Parse(label20.Text),
                };

                context.Billxes.Add(billx);
                context.SaveChanges();

                int bill_Id = context.Billxes.Max(x => x.BillId);

                foreach (var item in listproduct)
                {
                    Item itemx = new Item
                    {
                        BillId = bill_Id,
                        Pid = item.Pid,
                        Price = item.UnitPrice
                    };
                    context.Items.Add(itemx);
                    context.SaveChanges();
                    
                }
                MessageBox.Show("บันทึกรายการ");
                listproduct.Clear();
                totalPrice = 0;
                label20.Text = totalPrice.ToString();
                productxBindingSource.DataSource = null;
                guna2TextBox5.Text = String.Empty;
                guna2TextBox7.Text = String.Empty;
                guna2TextBox6.Text = String.Empty;
            } catch { }


        }

        private void guna2DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //แก้ไข
            this.id = int.Parse(guna2DataGridView1.SelectedRows[0].Cells[0].Value.ToString());
        }

        private void guna2Button10_Click(object sender, EventArgs e)
        {
            guna2TextBox5.Text = String.Empty;
            guna2TextBox7.Text = String.Empty;
            guna2TextBox6.Text = String.Empty;
        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            try
            {
                string id = guna2DataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                context.Customerxes.Remove(context.Customerxes.Where(em => em.Cid.ToString() == id).First());
                context.SaveChanges();
                customerxBindingSource.DataSource = context.Customerxes.ToList();
            }
            catch
            {

            }

        }

        private void tabPage4_Click(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        //private void addItemSet()
        //{
            //string id = guna2DataGridView3.SelectedRows[0].Cells[0].Value.ToString();
            //var product = context.Productxes.Where(p => p.ProductId.ToString().Equals(id)).First();
            //listcombo.Add(product);

            //int total = 0;
            //foreach (var p in listcombo)
            //{
            //    total += p.UnitPrice;
            //}
            //label2.Text = total.ToString();
            //productxBindingSource1.DataSource = new List<Productx>(listcombo);
            //
            //guna2DataGridView4.Rows[0].Selected = true;
        //}

        private void DataGridViewProductList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void guna2DataGridView5_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            int id = int.Parse(guna2DataGridView5.SelectedRows[0].Cells[0].Value.ToString());
            var set = context.ComboSets.Where(s => s.Id.Equals(id)).First();

            var setItems = context.ComboSetItems.Where(si => si.ComboSetId.Equals(set.Id)).ToList();

            foreach (var si in setItems)
            {
                var product = context.Productxes.Where(p => p.Pid.Equals(si.ProductId)).First();
                listproduct.Add(product);
            }

            totalPrice += set.SetPrice;

            label20.Text = totalPrice.ToString();
            productxBindingSource.DataSource = new List<Productx>(listproduct);
            blf = false;
        }

        private void label18_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button3_Click_1(object sender, EventArgs e)
        {
            //ค้นหา
            billxBindingSource1.DataSource = context.Customerxes.Where(emp => (emp.Fname.ToString()).Contains(guna2TextBox2.Text)
            || (emp.Lname.ToString()).Contains(guna2TextBox2.Text)
            || (emp.Phone.ToString()).Contains(guna2TextBox2.Text)).ToList();
        }

        private void guna2DataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //List<Productx> productxes = new List<Productx>();

            //int id = int.Parse(guna2DataGridView3.SelectedRows[0].Cells[0].Value.ToString());

            //var items = context.Billxes.Where(b => b.BillId.Equals(id)).ToList();

            //foreach (var item in items)
            //{
            //    var product = context.Productxes.Where(p => p.Equals(item.Pid)).First();
            //    productxes.Add(product);   
            //}

            //productxBindingSource3.DataSource = productxes;
        }
    }
}