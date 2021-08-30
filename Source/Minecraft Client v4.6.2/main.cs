using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CmlLib.Core;
using CmlLib.Core.Auth;
using System.Threading;
using System.Net;

namespace Minecraft_Launcher_v4._6._2
{
    public partial class main : Form
    {
        private int ram;
        private string versiyon;
        public main()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
        }
        MSession session;
        private void main_Load(object sender, EventArgs e)
        {
            label5.Text = "";
            comboBox1.SelectedItem = "Versiyon Seç";
            comboBox2.SelectedItem = "RAM Seç";
            label1.Text = login.userName;
            session = MSession.GetOfflineSession(label1.Text);
            path();

            var request = WebRequest.Create("https://cravatar.eu/helmavatar/"+label1.Text+"/190.png");
            using (var response = request.GetResponse())
            using (var stream = response.GetResponseStream())
            {
                pictureBox3.Image = Bitmap.FromStream(stream);
            }
        }
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == "Versiyon Seç" || comboBox2.SelectedItem == "RAM Seç")
            {
                MessageBox.Show("Lütfen bir versiyon ve RAM miktarı seçtiğinizden emin olun.", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                listBox1.Visible = true;
                guna2Button1.Enabled = false;
                Thread thread = new Thread(() => Launch());
                thread.IsBackground = true;
                thread.Start();
            }
        }
        private void path()
        {
            var path = new MinecraftPath();
            var launcher = new CMLauncher(path);
            launcher.ProgressChanged += (s, e) =>
            {

            };

            foreach (var item in launcher.GetAllVersions())
            {
                comboBox1.Items.Add(item.Name);
            }
        }
        private void Launch()
        {
            var path = new MinecraftPath();
            var launcher = new CMLauncher(path);
            launcher.FileChanged += (e) =>
            {
                listBox1.Items.Add(string.Format("[{0}] {1} - {2}/{3}", e.FileKind.ToString(), e.FileName, e.ProgressedFileCount, e.TotalFileCount));
                listBox1.SelectedIndex = listBox1.Items.Count - 1;
            };
            ram = Convert.ToInt32(comboBox2.SelectedItem.ToString());
            var launchOption = new MLaunchOption
            {
                MaximumRamMb = ram,
                Session = session,
                ServerIp = textBox1.Text,
                //GameLauncherName = "Yazılımcı Çocuk",
                GameLauncherVersion = "1.1.0"
            };
            versiyon = comboBox1.SelectedItem.ToString();
            var islem = launcher.CreateProcess(versiyon, launchOption);
            islem.Start();

            Thread.Sleep(60000);

            listBox1.Items.Clear();
            guna2Button1.Enabled = true;
        }
        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }
        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2ControlBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            login lg = new login();
            lg.Show();
            this.Hide();
        }

        private void pictureBox4_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void guna2HtmlLabel2_Click(object sender, EventArgs e)
        {

        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
