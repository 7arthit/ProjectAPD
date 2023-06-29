using AForge.Controls;
using Guna.UI2.WinForms;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace ProjectAPD
{
    public partial class Form4 : Form
    {
        Form2 form2;
        Emplopeex user;
        int id;
        APD65_63011212019Entities context = new APD65_63011212019Entities();
        List<Productx> listcombo = new List<Productx>();
        int totalPrice;
        public Form4(Form2 form2, Emplopeex user)
        {
            this.form2 = form2;
            this.user = user;
            InitializeComponent();
            label8.Text = user.FirstName;
            productxBindingSource1.DataSource = context.Productxes.ToList();
        }

        private void guna2ControlBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_MouseDown(object sender, MouseEventArgs e)
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

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            form2.Visible = true;
        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

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

        private void guna2DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            this.id = int.Parse(guna2DataGridView1.SelectedRows[0].Cells[0].Value.ToString());

            var pddata = context.Productxes.Where(p => p.ProductId == id).First();
            var image = pddata.Image;
            pictureBox1.Image = LoadImage(image);
        }

        private void guna2Button11_Click(object sender, EventArgs e)
        {
            string productName;
            int productPrice;
            string productDescription;
            string productImage;
            int productType;

            try
            {
                string url = "https://www.jib.co.th/web/product/readProduct/" + guna2TextBox4.Text;
                HtmlWeb web = new HtmlWeb();
                HtmlAgilityPack.HtmlDocument doc = web.Load(url);

                HtmlNode titleNode = doc.DocumentNode
                    .SelectSingleNode("//meta[@property=\"og:title\"]");
                productName = titleNode.Attributes["content"].Value;

                HtmlNode descriptionNode = doc.DocumentNode
                  .SelectSingleNode("//meta[@property=\"og:description\"]");
                productDescription = descriptionNode.Attributes["content"].Value;

                HtmlNode imageNode = doc.DocumentNode
                  .SelectSingleNode("//meta[@property=\"og:image\"]");
                productImage = imageNode.GetAttributeValue("content", "");

                HtmlNode priceNode = doc.DocumentNode
                    .SelectSingleNode("//div[@class=" +
                   "\"col-lg-12 col-md-12 col-sm-12 col-xs-12 text-center price_block\"]");
                String price = priceNode.InnerText;
                price = new string(price.Where(c => char.IsDigit(c)).ToArray());
                productPrice = int.Parse(price);

                List<string> pType = new List<string>();
                foreach (var ptype in doc.DocumentNode.SelectNodes("//div[@class=" + "\"step_nav\"]//a"))
                {
                    Console.WriteLine(ptype.GetAttributeValue("href", ""));
                    pType.Add(ptype.GetAttributeValue("href", ""));
                }

                string str = pType[2];
                string[] bits = str.Split('/');
                string subStr = bits[bits.Length - 1];
                productType = int.Parse(subStr);

                var ids = context.Productxes.Select(x => x.ProductId).ToList();
                int change = 0;
                if (!ids.Contains(int.Parse(guna2TextBox4.Text)))
                {
                    Productx product = new Productx
                    {
                        ProductId = int.Parse(guna2TextBox4.Text),
                        Name = productName,
                        UnitPrice = productPrice,
                        Description = productDescription,
                        Image = productImage,
                        Tid = productType
                    };
                    context.Productxes.Add(product);
                    change = context.SaveChanges();
                }
                MessageBox.Show("สินค้าถูกเพิ่ม " + change + " รายการ");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                MessageBox.Show("สินค้าหมด");
            }
        }

        private void guna2TextBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2Button12_Click(object sender, EventArgs e)
        {
            productxBindingSource1.DataSource = context.Productxes.Where(emp => (emp.ProductId.ToString()).Contains(guna2TextBox4.Text)
            || (emp.Name).Contains(guna2TextBox4.Text)
            || (emp.Tid.ToString()).Contains(guna2TextBox4.Text)
            || (emp.TypeProduct.TypeName).Contains(guna2TextBox4.Text)).ToList();
        }

        private void guna2Button10_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button9_Click(object sender, EventArgs e)
        {
            try
            {
                string id = guna2DataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                context.Productxes.Remove(context.Productxes.Where(em => em.ProductId.ToString() == id).First());
                context.SaveChanges();
                productxBindingSource1.DataSource = context.Productxes.ToList();
            }
            catch { }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            productxBindingSource1.DataSource = context.Productxes.Where(emp => (emp.Name).Contains(comboBox1.Text)).ToList();
        }



        private void guna2DataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void guna2DataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            addItemSet();
        }
        private void addItemSet()
        {
            string id = guna2DataGridView3.SelectedRows[0].Cells[0].Value.ToString();
            var product = context.Productxes.Where(p => p.ProductId.ToString().Equals(id)).First();
            listcombo.Add(product);

            int total = 0;
            foreach (var p in listcombo)
            {
                total += p.UnitPrice;
            }
            label21.Text = total.ToString();
            productxBindingSource.DataSource = new List<Productx>(listcombo);

            //guna2DataGridView4.Rows[0].Selected = true;
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            try
            {
                int id = int.Parse(guna2DataGridView2.SelectedRows[0].Cells[0].Value.ToString());

                foreach (DataGridViewRow item in guna2DataGridView2.SelectedRows)
                {
                    guna2DataGridView2.Rows.RemoveAt(item.Index);
                }
                int newTotal = 0;
                foreach (Productx p in listcombo)
                {
                    newTotal += p.UnitPrice;
                }
                totalPrice = newTotal;
                label21.Text = totalPrice.ToString();
                productxBindingSource.DataSource = listcombo;
            }
            catch { }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
                int total = 0;
                foreach (var p in listcombo)
                {
                    total += (p.UnitPrice) - (int)(p.UnitPrice * 0.1);
                }

                if (listcombo.Count > 1)
                {

                    ComboSet comboSet = new ComboSet
                    {
                        Name = guna2TextBox1.Text,
                        SetPrice = total
                    };

                    context.ComboSets.Add(comboSet);
                    context.SaveChanges();

                    int comboSetId = context.ComboSets.Max(b => b.Id);

                    foreach (Productx product in listcombo)
                    {
                        ComboSetItem item = new ComboSetItem
                        {
                            ComboSetId = comboSetId,
                            ProductId = product.Pid
                        };
                        context.ComboSetItems.Add(item);
                        context.SaveChanges();
                    }
                    MessageBox.Show("บันทึกสำเร็จ");
                    listcombo.Clear();
                    productxBindingSource.DataSource = null;
                    guna2TextBox1.Text = String.Empty;
            }
                else
                {
                    MessageBox.Show("ต้องมีสินค้ามากว่า 1 ชิ้น");
                }
            }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            productxBindingSource1.DataSource = context.Productxes.Where(emp => (emp.Name).Contains(comboBox2.Text)).ToList();
        }
    }
}
