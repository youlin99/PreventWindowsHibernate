using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PreventWindowsSleep
{
    public partial class Form1 : Form
    {
        private log4net.ILog log = log4net.LogManager.GetLogger("WindowsFormLogger");
        public Form1()
        {
            InitializeComponent(); // init
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void TriggerTimer()
        {
            timer.Enabled = false;
            int interval;
            int defaultInterval = 5;
            int unit = 60 * 1000; // ms 
            if (!int.TryParse(this.textBox1.Text, out interval))
            {
                interval = defaultInterval;
            }

            interval = interval * unit;

            timer.Interval = interval;

            timer.Enabled = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            log.Info("form loading...");
            TriggerTimer();
            WindowState = FormWindowState.Minimized;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            WindowsSleep.PreventSleep(true);
            WindowsSleep.ResetSleepTimer(true);
            log.Info("Prevent + Reset");
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            //判断是否选择的是最小化按钮
            if (WindowState == FormWindowState.Minimized)
            {
                //隐藏任务栏区图标
                this.ShowInTaskbar = false;
                //图标显示在托盘区
                notifyIcon1.Visible = true;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            TriggerTimer();
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                //还原窗体显示    
                WindowState = FormWindowState.Normal;                
                //激活窗体并给予它焦点
                this.Activate();
                //任务栏区显示图标
                this.ShowInTaskbar = true;
                //托盘区图标隐藏
                notifyIcon1.Visible = false;
            }
        }


        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {

            //隐藏任务栏区图标
            this.ShowInTaskbar = false;
            //图标显示在托盘区
            notifyIcon1.Visible = true;
        }

    }


}
