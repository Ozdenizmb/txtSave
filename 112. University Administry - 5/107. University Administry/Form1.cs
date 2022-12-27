using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _107.University_Administry
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Ogrenci frm = new Ogrenci();
            frm.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Kütüphane Yönetimi Henüz Sisteme Eklenmemiştir!", "Özdeniz Üniversitesi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Yemekhane Yönetimi Henüz Sisteme Eklenmemiştir!", "Özdeniz Üniversitesi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
