using AForge.Video.DirectShow;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZXing;

namespace ProjectAPD
{
    public partial class Qrcode : Form
    {
        FilterInfoCollection webcams;
        VideoCaptureDevice videoIn;
        BindingSource bindingSource;
        List<Productx> Productxlist;
        Label totalprice;

        APD65_63011212019Entities context = new APD65_63011212019Entities();

        public Qrcode(BindingSource bindingSource, List<Productx> productxes, Label totalprice)
        {
            InitializeComponent();

            this.bindingSource = bindingSource;
            this.Productxlist = productxes;
            this.totalprice = totalprice;   
        }

        private void Qrcode_Load(object sender, EventArgs e)
        {
            webcams = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo webcam in webcams)
            {
                comboBox1.Items.Add(webcam.Name);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
    
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (videoIn != null && videoIn.IsRunning)
            {
                videoSourcePlayer1.Stop();
            }
            if (!(videoIn != null && videoIn.IsRunning))
            {
                videoIn = new VideoCaptureDevice(webcams[comboBox1.SelectedIndex].MonikerString);
                videoSourcePlayer1.VideoSource = videoIn;
                videoSourcePlayer1.Start();
            }
        }

        private void Qrcode_FormClosed(object sender, FormClosedEventArgs e)
        {
            //bindingSource.DataSource = Productxlist;
            //if (videoIn != null && videoIn.IsRunning)
            //{
            //    videoSourcePlayer1.Stop();
            //} 
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            var capture = videoSourcePlayer1.GetCurrentVideoFrame();
            if (capture != null)
            {
                BarcodeReader reader = new BarcodeReader();
                var result = reader.Decode(capture);
                if (result != null)
                {
                    Console.WriteLine(result.Text);
                    int productId = int.Parse(new string(result.Text.Where(c => char.IsDigit(c)).ToArray()));
                    try
                    {
                        var product = context.Productxes.Where(p => p.ProductId.Equals(productId)).First();
                        Console.WriteLine(product.ProductId + " " + product.Name);
                        Productxlist.Add(product);
                        int total = 0;
                        foreach (var p in Productxlist)
                        {
                            total += p.UnitPrice;
                        }
                        totalprice.Text = total.ToString();
                        bindingSource.DataSource = context.Productxes.Where(p => p.ProductId.Equals(productId)).ToList();
                        bindingSource.DataSource = Productxlist;
                    }
                    catch (Exception ex)
                    {
                        if (videoIn != null && videoIn.IsRunning)
                        {
                            videoSourcePlayer1.Stop();
                        }
                        MessageBox.Show("ไม่พบสินค้า");
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }
    }
}
