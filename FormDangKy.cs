using System;
using System.Windows.Forms;
using System.IO;
using System.Web.Script.Serialization;
using System.Drawing;
using System.Data.SQLite;

namespace Sign_In_WinformCS
{
    public partial class FormDangKy : Form
    {
        public FormDangKy()
        {
            InitializeComponent();
        }
        private void label1_Click(object sender, EventArgs e) { }

        private void label8_Click(object sender, EventArgs e) { }

        private void textBox1_TextChanged(object sender, EventArgs e) { }
     
        class dangKy
        {
            public string hoten { get; set; }
            public string diachi { get; set; }
            public string email { get; set; }
            public string tenTK {get;set;}
            public string matkhau { get; set; }
            public string sdt { get; set; }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string hoten = textBox1.Text;
            string diachi = textBox2.Text;
            string email = textBox3.Text;
            string tenTK = textBox4.Text;
            string matkhau = textBox6.Text;
            string sdt = textBox5.Text;
            if (tenTK == "Nhập tên đăng nhập của bạn" || matkhau == "Nhập mật khẩu đăng nhập của bạn")
            {
                MessageBox.Show("Bạn chưa nhập đủ các thông tin!");
                return;
            }
            if (hoten == "Nhập tên của bạn" || diachi == "Nhập địa chỉ của bạn")
            {
                MessageBox.Show("Bạn chưa nhập đủ các thông tin!");
                return;
            }
            if (email == "Nhập email cá nhân" || sdt == "Nhập số điện thoại của bạn")
            {
                MessageBox.Show("Bạn chưa nhập đủ các thông tin!");
                return;
            }

            //Các kí tự cần kiểm tra
            bool kiTuDacBiet = false;
            bool so = false;
            bool chuInHoa = false;
            bool chuInThuong = false;

            //Kiểm tra họ tên:
            if(hoten.Length<2 || hoten.Length > 50)
            {
                MessageBox.Show("Vui long nhap ten tu 2-50 ki tu!");
            }
            else
            {
                for (int i = 0; i <= hoten.Length - 1; ++i)
                {
                    //kiem tra ki tu dac biet
                    if ((hoten[i] >= 33 && hoten[i] <= 47)
                        || (hoten[i] >= 58 && hoten[i] <= 64)
                        || (hoten[i] >= 91 && hoten[i] <= 96)
                        || (hoten[i] >= 123 && hoten[i] <= 126))
                    {
                        kiTuDacBiet = true;
                    }
                    //kiem tra so
                    if ((hoten[i] >= 48 && hoten[i] <= 57)) so = true;
                }
                if (kiTuDacBiet)
                {
                    MessageBox.Show("Ho ten khong duoc chua ki tu dac biet!");
                    return;
                }
                if (so)
                {
                    MessageBox.Show("Ho ten khong duoc chua chu so!");
                    return;
                }
            }

            //Kiểm tra địa chỉ:
            if (diachi.Length < 5 || diachi.Length > 50)
            {
                MessageBox.Show("Vui long nhap dia chi tu 5-50 ki tu!");
            }
            else
            {
                for (int i = 0; i <= diachi.Length - 1; ++i)
                {
                    //kiem tra ki tu dac biet
                    if ((diachi[i] >= 33 && diachi[i] <= 47)
                        || (diachi[i] >= 58 && diachi[i] <= 64)
                        || (diachi[i] >= 91 && diachi[i] <= 96)
                        || (diachi[i] >= 123 && diachi[i] <= 126))
                    {
                        kiTuDacBiet = true;
                    }
                }
                if (kiTuDacBiet)
                {
                    MessageBox.Show("Dia chi khong duoc chua ki tu dac biet!");
                    return;
                }
            }

            //Kiểm tra số điện thoại:
            if (sdt.Length < 5 || sdt.Length > 24)
            {
                MessageBox.Show("Số điện thoại phải từ 5-24 kí tự!");
                return;
            }
            else
            {
                for (int i = 0; i <= sdt.Length - 1; ++i)
                {
                    //kiem tra ki tu dac biet
                    if ((sdt[i] >= 33 && sdt[i] <= 47)
                        || (sdt[i] >= 58 && sdt[i] <= 64)
                        || (sdt[i] >= 91 && sdt[i] <= 96)
                        || (sdt[i] >= 123 && sdt[i] <= 126))
                    {
                        kiTuDacBiet = true;
                    }
                    //kiem tra chu in hoa
                    if ((sdt[i] >= 65 && sdt[i] <= 90)) chuInHoa = true;
                    //kiem tra chu in thuong
                    if ((sdt[i] >= 97 && sdt[i] <= 122)) chuInThuong = true;
                }
                if (kiTuDacBiet)
                {
                    MessageBox.Show("So dien thoai khong duoc chua ki tu dac biet!");
                    return;
                }
                if (chuInHoa)
                {
                    MessageBox.Show("So dien thoai khong duoc chua chu in hoa!");
                    return;
                }
                if (chuInThuong)
                {
                    MessageBox.Show("So dien thoai khong duoc chua chu thuong!");
                    return;
                }
            }

            //Kiểm tra mật khẩu
            if (matkhau.Length < 8 || matkhau.Length > 24)
            {
                MessageBox.Show("Mật khẩu phải từ 8-24 kí tự!");
                return;
            }
            else
            {
                //Kiểm tra mật khẩu:
                for (int i = 0; i <= matkhau.Length - 1; ++i)
                {
                    //kiem tra ki tu dac biet
                    if ((matkhau[i] >= 33 && matkhau[i] <= 47)
                        || (matkhau[i] >= 58 && matkhau[i] <= 64)
                        || (matkhau[i] >= 91 && matkhau[i] <= 96)
                        || (matkhau[i] >= 123 && matkhau[i] <= 126))
                    {
                        kiTuDacBiet = true;
                    }
                    //kiem tra so
                    if ((matkhau[i] >= 48 && matkhau[i] <= 57)) so = true;
                    //kiem tra chu in hoa
                    if ((matkhau[i] >= 65 && matkhau[i] <= 90)) chuInHoa = true;
                    //kiem tra chu in thuong
                    if ((matkhau[i] >= 97 && matkhau[i] <= 122)) chuInThuong = true;
                }
                if (!kiTuDacBiet)
                {
                    MessageBox.Show("Mat khau phai co it nhat mot ki tu dac biet!");
                    return;
                }
                if (!so)
                {
                    MessageBox.Show("Mat khau phai co it nhat mot chu so!");
                    return;
                }
                if (!chuInHoa)
                {
                    MessageBox.Show("Mat khau phai co it nhat mot chu in hoa!");
                    return;
                }
                if (!chuInThuong)
                {
                    MessageBox.Show("Mat khau phai co it nhat mot chu thuong!");
                    return;
                }
            }

            //Kiểm tra tên đăng nhập
            string chuoiCon = "@gmail.com";
            string chuoiMe = tenTK;
            //bool flag = false;
            bool flag = chuoiMe.Contains(chuoiCon);
            if (!flag)
            {
                MessageBox.Show("Ten dang nhap khong hop le! Phai co duoi @gmail.com");
                return;
            }

            //Kiểm tra email (tương tự tên đăng nhập)
            string chuoiConEM = "@gmail.com";
            string chuoiMeEM = email;
            bool flagEM = chuoiMeEM.Contains(chuoiConEM);
            if (!flagEM)
            {
                MessageBox.Show("Dinh dang email khong hop le! Phai co duoi @gmail.com");
                return;
            }

            else
            {
                MessageBox.Show("Xin chào! Bạn đã đăng ký thành công!");     
            }

            //pass value:
            dangKy dk = new dangKy();
            var obj = new dangKy
            {
                hoten = hoten,
                diachi = diachi,
                email = email,
                tenTK = tenTK,
                matkhau = matkhau,
                sdt = sdt
            };
            JavaScriptSerializer js = new JavaScriptSerializer();
            string jsonString = js.Serialize(obj);
            File.WriteAllText("registerDetail.json", jsonString);

            FormDangNhap FDN = new FormDangNhap();
            FDN.usernameTextBox.Text = textBox4.Text;
            FDN.passwordTextBox.Text = textBox6.Text;
            this.Hide();
            FDN.Show();
        }

        private void label7_Click(object sender, EventArgs e) { }

        private void label8_Click_1(object sender, EventArgs e) { }

        private void button2_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            FormDangNhap fdn = new FormDangNhap();
            fdn.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            if(textBox1.Text== "Nhập tên của bạn")
            {
                textBox1.Text = "";
                textBox1.ForeColor = Color.Black;
            }
        }
        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                textBox1.Text = "Nhập tên của bạn";
                textBox1.ForeColor = Color.LightGray;
            }
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            if (textBox2.Text == "Nhập địa chỉ của bạn")
            {
                textBox2.Text = "";
                textBox2.ForeColor = Color.Black;
            }
        }
        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                textBox2.Text = "Nhập địa chỉ của bạn";
                textBox2.ForeColor = Color.LightGray;
            }
        }

        private void textBox3_Enter(object sender, EventArgs e)
        {
            if (textBox3.Text == "Nhập email cá nhân")
            {
                textBox3.Text = "";
                textBox3.ForeColor = Color.Black;
            }
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            if (textBox3.Text == "")
            {
                textBox3.Text = "Nhập email cá nhân";
                textBox3.ForeColor = Color.LightGray;
            }
        }

        private void textBox4_Enter(object sender, EventArgs e)
        {
            if (textBox4.Text == "Nhập tên đăng nhập của bạn")
            {
                textBox4.Text = "";
                textBox4.ForeColor = Color.Black;
            }
        }

        private void textBox4_Leave(object sender, EventArgs e)
        {
            if (textBox4.Text == "")
            {
                textBox4.Text = "Nhập tên đăng nhập của bạn";
                textBox4.ForeColor = Color.LightGray;
            }
        }

        private void textBox6_Enter(object sender, EventArgs e)
        {
            if (textBox6.Text == "Nhập mật khẩu đăng nhập của bạn")
            {
                textBox6.Text = "";
                textBox6.ForeColor = Color.Black;
            }
        }

        private void textBox6_Leave(object sender, EventArgs e)
        {
            if (textBox6.Text == "")
            {
                textBox6.Text = "Nhập mật khẩu đăng nhập của bạn";
                textBox6.ForeColor = Color.LightGray;
            }
        }

        private void textBox5_Enter(object sender, EventArgs e)
        {
            if (textBox5.Text == "Nhập số điện thoại của bạn")
            {
                textBox5.Text = "";
                textBox5.ForeColor = Color.Black;
            }
        }

        private void textBox5_Leave(object sender, EventArgs e)
        {
            if (textBox5.Text == "")
            {
                textBox5.Text = "Nhập số điện thoại của bạn";
                textBox5.ForeColor = Color.LightGray;
            }
        }

        private void FormDangKy_Load(object sender, EventArgs e)
        {

        }
    }
}
