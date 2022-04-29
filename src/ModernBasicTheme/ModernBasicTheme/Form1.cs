using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DWM;

namespace ModernBasicTheme
{
    public partial class Form1 : Form
    {

        List<IntPtr> badWindows = new List<IntPtr>();

        bool allow_close = false;

        public Form1()
        {
            InitializeComponent();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Process.EnterDebugMode();
        }

        // Disable
        private void button2_Click(object sender, EventArgs e)
        {
            allow_close = true;
            updateTimer.Enabled = false;
            blockedTimer.Enabled = false;
            updateTimer.Stop();
            blockedTimer.Stop();
            foreach (KeyValuePair<IntPtr, string> window in WindowEnumerate.GetOpenWindows())
            {
                BasicTheme.EnableDwmNCRendering(window.Key);
            }
        }


        // 250ms update timer
        private void updateTimer_Tick(object sender, EventArgs e)
        {
            foreach (KeyValuePair<IntPtr, string> window in WindowEnumerate.GetOpenWindows())
            {
                if (!badWindows.Contains(window.Key))
                {
                    BasicTheme.DisableDwmNCRendering(window.Key);
                }
                else
                {
                    BasicTheme.EnableDwmNCRendering(window.Key);
                }

            }
        }

        // 1000ms blocked timer
        private void blockedTimer_Tick(object sender, EventArgs e)
        {
            badWindows = new List<IntPtr>();

            foreach (string p in waterMarkTextBox1.Lines)
            {
                foreach (Process pc in Process.GetProcessesByName(p))
                {
                    foreach (IntPtr hWnd in WindowEnumerate.EnumerateProcessWindowHandles(pc.Id))
                    {
                        badWindows.Add(hWnd);
                    }
                }

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            allow_close = false;
            updateTimer.Enabled = true;
            blockedTimer.Enabled = true;
            blockedTimer_Tick(this, new EventArgs());
            updateTimer_Tick(this, new EventArgs());
        }

        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Show();
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            allow_close = true;
            Close();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!allow_close)
            {
                e.Cancel = true;
                Hide();
                return;
            } else
            {
                button2_Click(this, new EventArgs());
            }
        }

        private void notifyIcon1_Click(object sender, EventArgs e)
        {
            contextMenuStrip1.Show(MousePosition);
        }
    }
}
