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

namespace Minecraft_Launcher_v4._6._2
{
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
        }
        public static string userName;
        private void guna2TextBox2_TextChanged(object sender, EventArgs e)
        {

        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Kullanıcı adı boş bırakılamaz!", "MC Launcher");
            }
            else
            {
                userName = textBox1.Text;
                main mn = new main();
                mn.Show();
                this.Hide();
            }
        }
    }
}
