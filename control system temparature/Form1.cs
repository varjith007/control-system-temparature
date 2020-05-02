using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace control_system_temparature
{
    public partial class Form1 : Form
    {

        double u_man;

        static public double gain = 2.5;
        static public double time_constant = 22;
        static public double time_delay = 3;
        static public double t_env = 22;
        static public Queue<double> DelayedSignals = new Queue<double>();
        static public double dt = 0.1;
        static public double T;
        static public double u;
        static public double ti;
        public static int timercount = 0;

        public Form1()
        {
            InitializeComponent();
            Signalarray();

        }

        public void Signalarray()
        {

            DelayedSignals = new Queue<double>();
            for (double t = 0; t < time_delay; t += dt)
                DelayedSignals.Enqueue(0);

        }

        public class AirHeater
        {
           

            public static double calculatetemparature()
            {
                
                DelayedSignals.Enqueue(u);
                DelayedSignals.Dequeue();

                double DT_dt = (1 / time_constant) * ((t_env - T) + gain * time_delay);
                T += dt * DT_dt;
                return T;

            }
        }


     

        public double checkBox1_CheckedChanged_1(object sender, EventArgs e)
        {
            //double u;
            

            if (checkBox1.Checked == true)
            {

                //Form2 fm2 = new Form2();


                u = Form2.controlsignal ;
                trackBarSignal.AllowDrop = false;
                label9.Text = Convert.ToString(u);
                return u;

            }
            else
            {
                u = ti;
                return u;

            }

            


        }

        

        private void chart1_Click(object sender, EventArgs e)
        {
            //chart1.DataSource = T; 
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            //AirHeater.calculatetemparature();
            ++timercount;
            DelayedSignals.Enqueue(u);

            if (timercount < DelayedSignals.Count)
            {
                DelayedSignals.Dequeue();
            }

            double DT_dt = (1 / time_constant) * ((t_env - T) + gain * u);
            T += dt * DT_dt;

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Start();
            textGain.Text = Convert.ToString(gain);
            textEnTemp.Text = Convert.ToString(time_constant);
            textTimeConstant.Text = Convert.ToString(t_env);
            textDelay.Text = Convert.ToString(time_delay);
            label9.Text = Convert.ToString(ti);
            textSignal.Text = Convert.ToString(T);

            if (checkBox1.Checked == true)
            {

                //Form2 fm2 = new Form2();


                u = Form2.controlsignal;
                //trackBarSignal.AllowDrop = false;
                label9.Text = Convert.ToString(u);
                //return u;

            }
            else
            {
                u = 0.1 * trackBarSignal.Value;
                //return u;

            }


        }

        private void trackBarSignal_ValueChanged(object sender, EventArgs e)
        {
            ti = 0.1 * trackBarSignal.Value;
        }

        private void trackBarSignal_Scroll(object sender, EventArgs e)
        {
            ti = 0.1 * trackBarSignal.Value;
        }
    }
}
