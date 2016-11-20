using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace boost
{
    public partial class main : Form
    {
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        public main()
        {
            InitializeComponent();

            this.Width = Screen.FromControl(this).Bounds.Width;
            this.Height = Screen.FromControl(this).Bounds.Height;
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = Color.FromArgb(41, 41, 45);
            this.MouseDown += Main_MouseDown;
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(0, 0);

            Panel sidebar = new Panel();
            sidebar.Width = 100;
            sidebar.BackColor = Color.FromArgb(44, 44, 49);
            sidebar.Height = this.Height - 60;
            sidebar.Location = new Point(0, 20);

            Panel toolbar = new Panel();
            toolbar.Width = this.Width;
            toolbar.BackColor = Color.FromArgb(54, 184, 183);
            toolbar.Height = 40;
            toolbar.Location = new Point(0, this.Height - 40);

            Panel topBar = new Panel();
            topBar.BackColor = Color.FromArgb(48, 48, 55);
            topBar.Width = this.Width;
            topBar.Height = 20;

            this.Controls.Add(sidebar);
            this.Controls.Add(toolbar);
            this.Controls.Add(topBar);
        }

        private void Main_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
    }
}
