using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Diagnostics;

namespace AlwaysAwake
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern EXECUTION_STATE SetThreadExecutionState(EXECUTION_STATE esFlags);
        private void Form1_Load(object send, EventArgs e)
        {
            Visible = false; // Hide form window.
            ShowInTaskbar = false; // Remove from taskbar.
            Opacity = 0;
            System.Timers.Timer timer = new System.Timers.Timer(30000);
            timer.Elapsed += new System.Timers.ElapsedEventHandler(PreventSleep);
            timer.Enabled = true;

        }

        void PreventSleep(object sender, System.Timers.ElapsedEventArgs e)
        {
            noIdeal();
            Point a = Cursor.Position;
            a.X = a.X - 5;
            a.Y = a.Y - 5;
            Cursor.Position = a;
            System.Threading.Thread.Sleep(50);
            a.X = a.X + 5;
            a.Y = a.Y + 5;
            Cursor.Position = a;
            //string txt = @"C:\Users\sreenath.selvaraj\AppData\Local\Microsoft\Teams\Update.exe" ;
            //string arg = @"--processStart " + "\"Teams.exe\"";
            //ProcessStartInfo startInfo = new ProcessStartInfo();
            //startInfo.CreateNoWindow = false;
            //startInfo.UseShellExecute = false;
            //startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            //startInfo.FileName = txt;
            //startInfo.Arguments = arg;
            //Process exeProcess = Process.Start(startInfo);

        }
        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Application.Exit();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            notifyIcon1.Dispose();
            Application.Exit();
        }

        public void noIdeal()
        {
            SetThreadExecutionState(EXECUTION_STATE.ES_DISPLAY_REQUIRED | EXECUTION_STATE.ES_CONTINUOUS);
        }
    }

    [FlagsAttribute]
    public enum EXECUTION_STATE : uint
    {
        ES_AWAYMODE_REQUIRED = 0x00000040,
        ES_CONTINUOUS = 0x80000000,
        ES_DISPLAY_REQUIRED = 0x00000002,
        ES_SYSTEM_REQUIRED = 0x00000001
    }
}
