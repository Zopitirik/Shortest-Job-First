//Name : Hikmet ERGÜN
//E-mail : hikmet@hikmetergun.net

using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ShortestJobFirst
{
    public partial class Form1 : Form
    {
        public static int TMAX = 20;
        List<ThreadClass> ThreadList = new List<ThreadClass>();
        public ThreadClass TYTemp1; //Temporary Variable
        public ThreadClass TYTemp2; //Temporary Variable
        public Random NumberG = new Random(); //Random Number VariableW
        public Random WorkingTimeG = new Random(); //Random Number Variable
        public bool FirstTime = true; //First time control

        public int NumberGenerator()
        {
            return NumberG.Next(1000, 9999);
        } //Random Thread Number Generator
        public int WorkingTimeGenerator()
        {
            return WorkingTimeG.Next(1, 10);
        } //Random Thread Number Generator
        public void CreateThread()
        {
            try
            {
                TYTemp1 = new ThreadClass();
                TYTemp1.WorkingTime = WorkingTimeGenerator();
                TYTemp1.ThreadNumber = NumberGenerator();
                ThreadList.Add(TYTemp1);
                textBox1.AppendText("\r\nThread created. Thread Number : " + TYTemp1.ThreadNumber + " Working Time : " + TYTemp1.WorkingTime);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        } //Create Thread
        public int FindShortestJob()
        {
            int min = 0;
            for (int i = 0; i < ThreadList.Count; i++)
            {
                if (ThreadList[i].WorkingTime < ThreadList[min].WorkingTime)
                {
                    min = i;
                }
            }
            return min;
        } //Find Shortest Job in Array
        public bool SimulationFinished()
        {
            if (ThreadList.Count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        } //Simulation Finished?

        public Form1()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox2.Text != "" && Int32.Parse(textBox2.Text) <= 20)
            {
                SimulationTimer.Enabled = true;
                button1.Enabled = false;
                button2.Enabled = true;
                button3.Enabled = true;
                button4.Enabled = true;
                textBox2.Enabled = false;
                //List<ThreadClass> ThreadList = new List<ThreadClass>();
                for (int i = 0; i < Int32.Parse(textBox2.Text); i++)
                {
                    CreateThread();
                }
                TYTemp1 = ThreadList[FindShortestJob()];
                SimulationTimer.Enabled = true;
                textBox1.AppendText("\r\nSimulation Started...");
                TYTemp1._Thread.Start();
                textBox1.AppendText("\r\nThread Started... Thread Number : " + TYTemp1.ThreadNumber + " Working Time : " + TYTemp1.WorkingTime);
            }
            else
            {
                textBox1.AppendText("\r\nYou must enter number of thread and the number must be lower than 20");
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (button2.Text == "Suspend")
            {
                SimulationTimer.Enabled = false;
                button2.Text = "Resume";
                button1.Enabled = false;
                button3.Enabled = true;
                button4.Enabled = false;
                textBox2.Enabled = false;
            }
            else
            {
                SimulationTimer.Enabled = true;
                button2.Text = "Suspend";
                button1.Enabled = false;
                button3.Enabled = true;
                button4.Enabled = true;
                textBox2.Enabled = false;
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            SimulationTimer.Enabled = false;
            button1.Enabled = true;
            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;
            textBox2.Enabled = true;
        }
        private void button4_Click(object sender, EventArgs e)
        {
            CreateThread();
        }
        private void SimulationTimer_Tick(object sender, EventArgs e)
        {
            if (!SimulationFinished())
            {
                TYTemp2 = ThreadList[FindShortestJob()];
                if (TYTemp1.ThreadNumber == TYTemp2.ThreadNumber)
                {
                    if (TYTemp1.WorkingTime <= 0)
                    {
                        ThreadList.RemoveAt(ThreadList.IndexOf(TYTemp1));
                        textBox1.AppendText("\r\nThread job finished... Thread Number : " + TYTemp1.ThreadNumber);
                    }
                    else
                    {
                        TYTemp1.WorkingTime--;
                        textBox1.AppendText("\r\nThread working... Thread Number : " + TYTemp1.ThreadNumber + " Working Time : " + TYTemp1.WorkingTime);
                    }
                }
                else
                {
                    TYTemp1 = TYTemp2;
                    if (TYTemp2._Thread.ThreadState == System.Threading.ThreadState.Suspended)
                    {
                        TYTemp1._Thread.Resume();
                        textBox1.AppendText("\r\nThread working again... Thread Number : " + TYTemp1.ThreadNumber + " Working Time : " + TYTemp1.WorkingTime);
                    }
                    else if (TYTemp1._Thread.ThreadState == System.Threading.ThreadState.Unstarted)
                    {
                        TYTemp1._Thread.Start();
                        textBox1.AppendText("\r\nThread started... Thread Number : " + TYTemp1.ThreadNumber + " Working Time : " + TYTemp1.WorkingTime);
                    }
                }
            }
            else
            {
                SimulationTimer.Enabled = false;
                button1.Enabled = true;
                button2.Enabled = false;
                button3.Enabled = false;
                button4.Enabled = false;
                textBox2.Enabled = true;
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            button1.Enabled = true;
            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;
            textBox2.Enabled = true;
        }
    }
}
