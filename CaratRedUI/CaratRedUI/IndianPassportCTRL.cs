using CaratRedFi_800RLibrary;
using CaratRedFi800RLibrary;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CaratRedUI
{
    public partial class IndianPassportCTRL : UserControl
    {
        private static IndianPassportCTRL _instance;
        ScannerConnection sc;
        Bitmap bitmap;
        bool Flag = false;
        Bitmap picture1;
        Bitmap picture2;


        public GuestCardInfo CurrentGuestCard { get; set; }

        public EventHandler BackToDashBoard2 { get; set; }
        public static IndianPassportCTRL Instance => _instance ?? (_instance = new IndianPassportCTRL());
        public IndianPassportCTRL()
        {
            Flag = false;
            InitializeComponent();
            sc = new ScannerConnection();
            sc.axFiScn1.ScanToRawEx += axFiScn1_ScanToRawEx;
            sc.axFiScn1.CreateControl();

        }

        private void IndianPassportCTRL_Load(object sender, EventArgs e)
        {

        }

        private void IndianPassportCTRL_Load_1(object sender, EventArgs e)
        {
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Flag = true;
            pictureBox1.Image = null;
            sc.ForeignId(Handle.ToInt32());
            //SplitImage(bitmap);


            Form2 form2 = new Form2(bitmap);
            form2.ShowDialog();
            pictureBox1.Image = form2.SelectedImage;
            CurrentGuestCard.Image1 = pictureBox1.Image as Bitmap;

            button5.Visible = false;
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            if (BackToDashBoard2 != null)
            {
                BackToDashBoard2(this, new EventArgs());
            }
        }

        private void axFiScn1_ScanToRawEx(object sender, AxFiScnLib._DFiScnEvents_ScanToRawExEvent e)
        {

        }

        private void button11_Click(object sender, EventArgs e)
        {
            Flag = true;
            pictureBox4.Image = null;
            sc.ForeignId(Handle.ToInt32());
            //SplitImage(bitmap);

            Form2 form2 = new Form2(bitmap);
            form2.ShowDialog();
            pictureBox4.Image = form2.SelectedImage;
            CurrentGuestCard.Image2 = pictureBox4.Image as Bitmap;
            button6.Visible = false;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = null;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            pictureBox2.Image = null;
        }

        private async void button12_Click(object sender, EventArgs e)
        {
            ApiService apiService = new ApiService();

            picture1 = (Bitmap)pictureBox1.Image;
            System.IO.MemoryStream ms1 = new MemoryStream();
            picture1.Save(ms1, System.Drawing.Imaging.ImageFormat.Jpeg);
            byte[] byteImage1 = ms1.ToArray();
            CurrentGuestCard.SigBase64_Img1 = Convert.ToBase64String(byteImage1);
            //textBox1.Text = SigBase641;

            picture2 = (Bitmap)pictureBox2.Image;
            System.IO.MemoryStream ms2 = new MemoryStream();
            picture2.Save(ms2, System.Drawing.Imaging.ImageFormat.Jpeg);
            byte[] byteImage2 = ms2.ToArray();
            CurrentGuestCard.SigBase64_Img2 = Convert.ToBase64String(byteImage2);
            //textBox1.Text = SigBase642;

            String response = await apiService.UploadInfoToFile(CurrentGuestCard);

            MessageBox.Show(response);

            CurrentGuestCard.Image1 = picture1;
            CurrentGuestCard.Image2 = picture2;
            CurrentGuestCard.Submitted = true;
            //CurrentGuestCard.GuestName = **** Guest Name through lable
        }

    }
}
