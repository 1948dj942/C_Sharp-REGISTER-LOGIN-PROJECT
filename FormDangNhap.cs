using System;
using System.Windows.Forms;
using System.IO;
using Newtonsoft.Json;
using System.Web.Script.Serialization;
using System.Drawing;
using System.Data.SQLite;

namespace Sign_In_WinformCS
{
    public partial class FormDangNhap : Form
    {
        public FormDangNhap()
        {
            InitializeComponent();
        }
        
        private void textBox1_TextChanged(object sender, EventArgs e) { }

        private void label1_Click(object sender, EventArgs e) { }

        private void textBox1_TextChanged_1(object sender, EventArgs e) { }
        class dangKy
        {
            public string hoten { get; set; }
            public string diachi { get; set; }
            public string email { get; set; }
            public string tenTK { get; set; }
            public string matkhau { get; set; }
            public string sdt { get; set; }
        }
        private void label4_Click(object sender, EventArgs e) { }
        //Button chinh - dang nhap: kiem tra mat khau, tai khoan, luu du lieu dang nhap,...
        private void loginButton_Click(object sender, EventArgs e)
        {
                string tenTK = usernameTextBox.Text;
                string matkhau = passwordTextBox.Text;

                if (tenTK == "Vui lòng nhập tên đăng nhập" || matkhau == "Vui lòng nhập mật khẩu")
                {
                    MessageBox.Show("Bạn chưa nhập tên đăng nhập hoặc mật khẩu!");
                    return;
                }

                bool kiTuDacBiet = false;
                bool so = false;
                bool chuInHoa = false;
                bool chuInThuong = false;

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
                bool flag = chuoiMe.Contains(chuoiCon);
                if (!flag)
                {
                    MessageBox.Show("Ten dang nhap khong hop le! Phai co duoi @gmail.com");
                    return;
                }
                else
                {
                    //tai khoan: tenTK
                    //mat khau: matkhau

                    string link = @"C:\Users\Admin\source\repos\FirstCS\Sign In WinformCS\bin\Debug\registerDetail.json";
                    var obj = new dangKy
                    {
                        tenTK = tenTK,
                        matkhau = matkhau,
                    };
                    var jasonString = JsonConvert.SerializeObject(obj, Formatting.Indented);
                    using (var jo = new StreamWriter(link))
                    {
                        JavaScriptSerializer js = new JavaScriptSerializer();
                        dangKy dkObj = js.Deserialize<dangKy>(jasonString);
                        string acc = dkObj.tenTK;
                        string passwd = dkObj.matkhau;
                        if (acc != tenTK || passwd != matkhau)
                        {
                            MessageBox.Show("Đăng nhập thất bại! Hãy kiểm tra lại tài khoản hoặc mật khẩu của bạn!");
                        }
                        else
                        {
                            MessageBox.Show("Đăng nhập thành công! Xin chào!");
                            object[] ToJSON = { tenTK, matkhau };
                            string output = JsonConvert.SerializeObject(ToJSON);
                            File.WriteAllText("signInDetail.json", output);
                        }
                    }
                }

                var sql = "Data Source=dangNhap.db";
                //SQLiteConnection.CreateFile(sql);
                SQLiteConnection conn = new SQLiteConnection(sql);
                try
                {
                    conn.Open();
                    //CreateTable(conn);
                    //InsertData(conn, "toanpham31803@gmail.com", "12345678@Tt");
                    //InsertData(conn, "locpham288@gmail.com", "12345678@Ll");
                    SelectData(conn);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                finally
                {
                    conn.Close();
                }
        }

        //Cac ham quan ly SQLite
        //Ham show data:
        private static void SelectData(SQLiteConnection conn)
        {
            var tenTK = "TenTK";
            var matkhau = "MatKhau";
            Console.WriteLine($"{tenTK,-20:s}{matkhau,-35:s}");
            var sql = "SELECT * FROM DangNhap";
            var cmd = new SQLiteCommand(sql, conn);
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine($"{reader.GetString(0),-20:d}" +
                    $"{reader.GetString(1),-35:s}");
            }
        }
        //Ham nhap data:
        private static void InsertData(SQLiteConnection conn, string tenTK, string matkhau)
        {
            var sql = "INSERT INTO DangNhap(TenTK, MatKhau) " +
                "VALUES(@tenTK, @matkhau)";
            var cmd = new SQLiteCommand(sql, conn);
            cmd.Parameters.AddWithValue("@tenTK", tenTK);
            cmd.Parameters.AddWithValue("@matkhau", matkhau);
            cmd.ExecuteNonQuery();
        }
        //Ham tao bang:
        private static void CreateTable(SQLiteConnection conn)
        {
            string sql = "CREATE TABLE IF NOT EXISTS DangNhap(" +
                    "tenTK VARCHAR(50) PRIMARY KEY," +
                    "matkhau VARCHAR(24)," +
                    ")";
            var cmd = new SQLiteCommand(sql, conn);
            cmd.ExecuteNonQuery();
        }

        //Button tat form dang nhap va chuyen sang form dang ky
        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide(); //close the signIn form
            FormDangKy fdk = new FormDangKy(); //tao FormDangKy moi 
            fdk.Show(); //show FormDangKy
        }
        //Button dung debug
        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label1_Click_1(object sender, EventArgs e) { }

        private void label2_Click(object sender, EventArgs e) { }
        //Cac ham cho placeholder - form dang nhap
        //Ten tai khoan
        private void usernameTextBox_Enter(object sender, EventArgs e)
        {
            if (usernameTextBox.Text == "Vui lòng nhập tên đăng nhập")
            {
                usernameTextBox.Text = "";
                usernameTextBox.ForeColor = Color.Black;
            }
        }

        private void usernameTextBox_Leave(object sender, EventArgs e)
        {
            if (usernameTextBox.Text == "")
            {
                usernameTextBox.Text = "Vui lòng nhập tên đăng nhập";
                usernameTextBox.ForeColor = Color.LightGray;
            }
        }
        //Mat khau
        private void passwordTextBox_Enter(object sender, EventArgs e)
        {
            if (passwordTextBox.Text == "Vui lòng nhập mật khẩu")
            {
                passwordTextBox.Text = "";
                passwordTextBox.ForeColor = Color.Black;
            }
        }

        private void passwordTextBox_Leave(object sender, EventArgs e)
        {
            if (passwordTextBox.Text == "")
            {
                passwordTextBox.Text = "Vui lòng nhập mật khẩu";
                passwordTextBox.ForeColor = Color.LightGray;
            }
        }

        public void passwordTextBox_TextChanged(object sender, EventArgs e) { }

        private void FormDangNhap_Load(object sender, EventArgs e)
        {

        }
    }
}
