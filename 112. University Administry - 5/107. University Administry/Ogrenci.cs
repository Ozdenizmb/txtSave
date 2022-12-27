using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace _107.University_Administry
{
    public partial class Ogrenci : Form
    {
        List<Veriler> dataOgrenci = new List<Veriler>();

        public Ogrenci()
        {
            InitializeComponent();
        }

        private bool OgrenciVarMi(String no)
        {
            Veriler tmp = dataOgrenci.Find(x => x.no == no);

            if(tmp != null) {
                return true;
            }
            else {
                return false;
            }
        }

        private void txtTemizle()
        {
            txtNo.Text = "";
            txtAd.Text = "";
            txtSoyad.Text = "";
            cmbBolum.Text = "";
            cmbCinsiyet.Text = "";
            cmbDogumTrh.Text = "";
        }

        private void veriOku()
        {
            String desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            String dosyaYolu = Path.Combine(desktopPath, "VeriDeposu.txt");
            // Masaüstünde "VeriDeposu" isimli TXT dosyasının adresini otomatik olak bulan işlemlerdir.
            // Bu sayede her bilgisayarda MASAÜSTÜ yolu değişse de bu problem yaratmayacaktır.

            DataTable table = new DataTable();

            table.Columns.Add("No", typeof(String));
            table.Columns.Add("Ad", typeof(String));
            table.Columns.Add("Soyad", typeof(String));
            table.Columns.Add("Bölüm", typeof(String));
            table.Columns.Add("Cinsiyet", typeof(String));
            table.Columns.Add("DogumTarihi", typeof(String));


            dataGridView1.DataSource = table;

            String[] lines = File.ReadAllLines(dosyaYolu);
            String[] data;

            for (int i = 0; i < lines.Length; i++)
            {
                data = lines[i].ToString().Split(',');

                String[] row = new String[data.Length];

                for (int j = 0; j < data.Length; j++)
                {
                    row[j] = data[j].Trim();
                }

                table.Rows.Add(row);

                int sayac = Convert.ToInt32(lblIndex.Text);
                sayac++;
                lblIndex.Text = sayac.ToString();
            }
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            if (OgrenciVarMi(txtNo.Text))
            {
                MessageBox.Show("Bu Kayıt Zaten Var.", "Özdeniz Üniversitesi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                Veriler ogrenci = new Veriler();

                ogrenci.no = txtNo.Text;
                ogrenci.ad = txtAd.Text;
                ogrenci.soyad = txtSoyad.Text;
                ogrenci.bolum = cmbBolum.Text;
                ogrenci.cinsiyet = cmbCinsiyet.Text;
                ogrenci.dogumTarihi = cmbDogumTrh.Text;
                dataOgrenci.Add(ogrenci);

                int sayacYedek = Convert.ToInt32(lblIndex.Text); 
                txtTemizle();

                String desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                String dosyaYolu = Path.Combine(desktopPath, "VeriDeposu.txt");
                // Masaüstünde "VeriDeposu" isimli TXT dosyasının adresini otomatik olak bulan işlemlerdir.
                // Bu sayede her bilgisayarda MASAÜSTÜ yolu değişse de bu problem yaratmayacaktır.

                String myText = ogrenci.no + "," + ogrenci.ad + "," + ogrenci.soyad + "," + ogrenci.bolum + "," + ogrenci.cinsiyet + "," + ogrenci.dogumTarihi;

                StreamWriter writer = new StreamWriter(dosyaYolu);
                for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                {
                    for (int j = 0; j < dataGridView1.Columns.Count; j++)
                    {
                        writer.Write(dataGridView1.Rows[i].Cells[j].Value.ToString());
                        if (j != dataGridView1.Columns.Count - 1)
                        {
                            writer.Write(",");
                        }
                    }
                    writer.WriteLine("");
                }
                writer.WriteLine(myText);
                writer.Close();

                veriOku();

                sayacYedek++;
                lblIndex.Text = sayacYedek.ToString();

                MessageBox.Show("Öğrenci Sisteme Kayıt Edilmiştir.", "Özdeniz Üniversitesi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnBul_Click(object sender, EventArgs e)
        {

            if (OgrenciVarMi(txtNo.Text))
            {
                int indis = dataOgrenci.FindIndex(x => x.no == txtNo.Text);

                txtAd.Text = dataOgrenci[indis].ad;
                txtSoyad.Text = dataOgrenci[indis].soyad;
                cmbBolum.Text = dataOgrenci[indis].bolum;
                cmbCinsiyet.Text = dataOgrenci[indis].cinsiyet;
                cmbDogumTrh.Text = dataOgrenci[indis].dogumTarihi;
            }
            else
            {
                MessageBox.Show("Böyle Bir Kayıt Bulunmamaktadır!", "Özdeniz Üniversitesi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnSil_Click(object sender, EventArgs e)
        {

            if (OgrenciVarMi(txtNo.Text))
            {
                dataOgrenci.RemoveAll(x => x.no == txtNo.Text);

                lblIndex.Text = dataOgrenci.Count.ToString();

                MessageBox.Show("Kayıt Sistemden Silinmiştir.", "Özdeniz Üniversitesi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Böyle Bir Kayıt Bulunmamaktadır!", "Özdeniz Üniversitesi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnguncelle_Click(object sender, EventArgs e)
        {
            if (OgrenciVarMi(txtNo.Text))
            {
                dataOgrenci.RemoveAll(x => x.no == txtNo.Text);

                Veriler ogrenci = new Veriler();

                ogrenci.no = txtNo.Text;
                ogrenci.ad = txtAd.Text;
                ogrenci.soyad = txtSoyad.Text;
                ogrenci.bolum = cmbBolum.Text;
                ogrenci.cinsiyet = cmbCinsiyet.Text;
                ogrenci.dogumTarihi = cmbDogumTrh.Text;
                dataOgrenci.Add(ogrenci);

                txtTemizle();

                MessageBox.Show("Öğrenci Kaydı Sistemde Güncellenmiştir.", "Özdeniz Üniversitesi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Böyle Bir Kayıt Bulunmamaktadır!", "Özdeniz Üniversitesi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void Ogrenci_Load(object sender, EventArgs e)
        {
            String desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            String dosyaYolu = Path.Combine(desktopPath, "VeriDeposu.txt");
            // Masaüstünde "VeriDeposu" isimli TXT dosyasının adresini otomatik olak bulan işlemlerdir.
            // Bu sayede her bilgisayarda MASAÜSTÜ yolu değişse de bu problem yaratmayacaktır.
            

            if (File.Exists(dosyaYolu))
            {
                // Dosya Mevcut
                veriOku();
            }
            else
            {
                // Dosya Mevcut Değil
                File.Create(dosyaYolu).Dispose();
            }
        }
    }
}
