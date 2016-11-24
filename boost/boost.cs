using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Collections;

namespace boost
{
    public partial class boost : Form
    {
        Label pluginLoad;

        public object[] loadedPlugins { get; set; }

        public boost()
        {
            this.Width = 500;
            this.Height = 400;
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = Color.FromArgb(41, 41, 45);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackgroundImage = Properties.Resources.splsh;
            this.Icon = Properties.Resources.boostico;

            Label copyright = new Label();
            copyright.Text = "© 2016 Alex Rankin";
            copyright.ForeColor = Color.FromArgb(68, 68, 75);
            copyright.Font = new Font("Segoe UI", 12, FontStyle.Regular);
            copyright.Width = copyright.PreferredWidth;
            copyright.Height = copyright.PreferredHeight;
            copyright.Location = new Point(this.Width / 2 - (copyright.PreferredWidth / 2), this.Height- copyright.PreferredHeight);

            PictureBox loadSpinner = new PictureBox();
            loadSpinner.Width = 20;
            loadSpinner.Height = 20;
            loadSpinner.SizeMode = PictureBoxSizeMode.Zoom;
            loadSpinner.Image = Properties.Resources.load;
            loadSpinner.Location = new Point(this.Width / 2 - (loadSpinner.Width / 2) - 10, this.Height - loadSpinner.PreferredSize.Height);

            pluginLoad = new Label();
            pluginLoad.Text = "Prepareing...";
            pluginLoad.Font = new Font("Segoe UI Light", 12, FontStyle.Regular);
            pluginLoad.ForeColor = Color.White;
            pluginLoad.Width = pluginLoad.PreferredWidth;
            pluginLoad.Location = new Point(20, loadSpinner.Location.Y - 100);

            this.Controls.Add(copyright);
            this.Controls.Add(loadSpinner);
            this.Controls.Add(pluginLoad);

            Thread splash = new Thread(new ThreadStart(splashThread));
            splash.IsBackground = true;
            splash.Start();
        }

        void updateMessage(string message)
        {
            pluginLoad.Text = message;
            pluginLoad.Width = pluginLoad.PreferredWidth;
        }

        void startMain()
        {
            main m_ = new main();
            m_.Show();

            this.Hide();

            m_.FormClosed += M__FormClosed;
        }

        private void M__FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Close();
        }

        bool catcher;
        private void Exec(MethodInvoker opr)
        {
            catcher = false;

            while(!catcher)
            {
                try
                {
                    this.Invoke(opr);

                    catcher = true;

                    Debug.WriteLine("Complete");
                }
                catch(InvalidOperationException)
                {
                    catcher = false;

                    Debug.WriteLine("ISSUE");
                }
            }
        }

        void splashThread()
        {
            Exec(delegate { updateMessage("Checking for updates..."); });
            http updateCheck = new http("http://example.com/");
            if(updateCheck.getResponce().GetType() == typeof(bool))
            {
                MessageBox.Show("Failed to check for updates.\nCheck network connection");
            }

            Exec(delegate { updateMessage("Verifying activation code..."); });
            http codeCheck = new http("http://example.com/");

            if(!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\boost"))
            {
                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\boost");
            }

            Exec(delegate { startMain(); });
        }
    }
}
