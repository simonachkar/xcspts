using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TraffSim
{
    public partial class Form1 : Form
    {
        int app_counter = 0;
        Queue<double> cars_time = new Queue<double>();
        int nbCars;
        int nb_Generated_Cars;
        PictureBox[] D;
        Car[] c;
        Random rnd = new Random();
        TrafficLight t1, t2;
        int stop_pos_right = -1, stop_pos_top = -1;

        Queue temp;
        public Form1(int mean, int min)
        {

            Queue times = Program.Expo(mean, min);
            
            // Just to check times of Cars ---------------------
            temp = new Queue(times);
            MessageBox.Show(string.Format("{0}", Print(temp)));
            // -------------------------------------------------

            nbCars = times.Count;

            while (times.Count != 0)
            {
                cars_time.Enqueue(((double)times.Dequeue()));
            }

            D = new PictureBox[nbCars];
            c = new Car[nbCars];
       
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            nb_Generated_Cars = 0;
            t1 = new TrafficLight(pictureBox1, "green", "vertical");
            t2 = new TrafficLight(pictureBox2, "red", "horizontal");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (app_counter < nbCars)
            {
                app_counter++;

                if (app_counter / 60 == (int)cars_time.First())
                {
                    GenerateCar();
                    cars_time.Dequeue();
                    // MessageBox.Show(string.Format("counter: {0}, cars: {1}, {2}", app_counter, cars_time.Dequeue(), nbCars));
                }
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            t1.ChangeColor(ref pictureBox1);
            t2.ChangeColor(ref pictureBox2);
            this.Update();
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            MoveCars();
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////
        // Custom Methods: -----------------------------------------------------------------------------------  //
        //////////////////////////////////////////////////////////////////////////////////////////////////////////

        // Generate Cars -----------------------------------------------------------------------------------------
        private void GenerateCar()
        {
            int num = rnd.Next(1, 3);
            switch (num)
            {
                case 1: // -> right 
                    stop_pos_right++;
                    D[nb_Generated_Cars] = new PictureBox
                    {
                        Location = new System.Drawing.Point(800, 343),
                        Size = new System.Drawing.Size(80, 40),
                        SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage,
                        TabIndex = 0,
                        TabStop = false
                    };
                    c[nb_Generated_Cars] = new Car(D[nb_Generated_Cars], "right", t2);
                    D[nb_Generated_Cars].Show();
                    this.Controls.Add(D[nb_Generated_Cars]);
                    break;

                case 2: // -> top             
                default:
                    stop_pos_top++;
                    D[nb_Generated_Cars] = new PictureBox
                    {
                        Location = new System.Drawing.Point(153, -100),
                        Size = new System.Drawing.Size(40, 80),
                        SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage,
                        TabIndex = 0,
                        TabStop = false
                    };
                    c[nb_Generated_Cars] = new Car(D[nb_Generated_Cars], "top", t1);
                    D[nb_Generated_Cars].Show();
                    this.Controls.Add(D[nb_Generated_Cars]);
                    break;
            }
            nb_Generated_Cars++;
        }

        // Move Cars -------------------------------------------------------------------------------------------------
        private void MoveCars()
        {
            for (int i = 0; i < nb_Generated_Cars; i++)
            {
                switch (c[i].Position)
                {
                    case "right":
                        MoveRight(c[i], ref D[i]);
                        break;

                    case "top":
                    default:
                        MoveTop(c[i], ref D[i]);
                        break;
                }
            }
        }

        // Move Right ------------------------------------------------------------------------------------------------
        private void MoveRight(Car c, ref PictureBox D)
        {
            if (D.Location.X > 300  && D.Location.X < 320 + 100 * stop_pos_right)
            {
                if (c.TL.Color == "green")
                {
                    c.Move(ref D);

                    if (stop_pos_right != -1)
                        stop_pos_right--;
                }
            }
            else
            {
                c.Move(ref D);
            }

        }

        // Move Top --------------------------------------------------------------------------------------------------
        private void MoveTop(Car c, ref PictureBox D)
        {
            if (D.Location.Y > 230 - 100 * stop_pos_top && D.Location.Y < 250)
            {
                if (c.TL.Color == "green")
                {
                    c.Move(ref D);

                    if (stop_pos_top != -1)
                        stop_pos_top--;
                }
            }
            else
            {
                c.Move(ref D);
            }
        }

        private String Print(Queue q)
        {
            String s = "Number of Cars: " + q.Count + "\n";
            while (q.Count != 0)
            {
                s += "\n";
                s += (double)q.Dequeue();
            }
            return s;
        }

    }

}
