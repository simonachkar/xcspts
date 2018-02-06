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
    public partial class Form2 : Form
    {
        int app_counter = 0;
        Queue<double> cars_time = new Queue<double>();
        int nbCars;
        int nb_Generated_Cars;
        PictureBox[] D;
        Car[] c;
        Random rnd = new Random();
        TrafficLight t1, t2, t3, t4;
        int stop_pos_right = -1, stop_pos_left = -1, stop_pos_bottom = -1, stop_pos_top = -1;

        Queue temp;

        public Form2(int mean, int min)
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

        private void Form2_Load(object sender, EventArgs e)
        {

            nb_Generated_Cars = 0;
            t1 = new TrafficLight(pictureBox1, "green", "horizontal");
            t2 = new TrafficLight(pictureBox2, "green", "horizontal");
            t3 = new TrafficLight(pictureBox3, "red", "vertical");
            t4 = new TrafficLight(pictureBox4, "red", "vertical");
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
            t3.ChangeColor(ref pictureBox3);
            t4.ChangeColor(ref pictureBox4);
            this.Update();
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            MoveCars();
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////
        // Custom Methods: -------------------------------------------------------------------------------------//
        //////////////////////////////////////////////////////////////////////////////////////////////////////////

        // Generate Cars -----------------------------------------------------------------------------------------
        private void GenerateCar()
        {
            int num = rnd.Next(1, 5);
       
            switch (num)
            {
                case 1: // -> right 
                    stop_pos_right++;
                    D[nb_Generated_Cars] = new PictureBox
                    {
                        Location = new System.Drawing.Point(800, 250),
                        Size = new System.Drawing.Size(55, 25),
                        SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage,
                        TabIndex = 0,
                        TabStop = false
                    };
                    c[nb_Generated_Cars] = new Car(D[nb_Generated_Cars], "right", t1);
                    D[nb_Generated_Cars].Show();
                    this.Controls.Add(D[nb_Generated_Cars]);
                    break;

                case 2: // -> left 
                    stop_pos_left++;
                    D[nb_Generated_Cars] = new PictureBox
                    {
                        Location = new System.Drawing.Point(-100, 290),
                        Size = new System.Drawing.Size(55, 25),
                        SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage,
                        TabIndex = 0,
                        TabStop = false
                    };
                    c[nb_Generated_Cars] = new Car(D[nb_Generated_Cars], "left", t2);
                    D[nb_Generated_Cars].Show();
                    this.Controls.Add(D[nb_Generated_Cars]);
                    break;

                case 3: // -> bottom 
                    stop_pos_bottom++;
                    D[nb_Generated_Cars] = new PictureBox
                    {
                        Location = new System.Drawing.Point(362, 800),
                        Size = new System.Drawing.Size(25, 55),
                        SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage,
                        TabIndex = 0,
                        TabStop = false
                    };
                    c[nb_Generated_Cars] = new Car(D[nb_Generated_Cars], "bottom", t3);
                    D[nb_Generated_Cars].Show();
                    this.Controls.Add(D[nb_Generated_Cars]);
                    break;

                case 4: // -> top
                default:
                    stop_pos_top++;
                    D[nb_Generated_Cars] = new PictureBox
                    {
                        Location = new System.Drawing.Point(310, -100),
                        Size = new System.Drawing.Size(25, 55),
                        SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage,
                        TabIndex = 0,
                        TabStop = false
                    };
                    c[nb_Generated_Cars] = new Car(D[nb_Generated_Cars], "top", t4);
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

                    case "left":
                        MoveLeft(c[i], ref D[i]);
                        break;

                    case "bottom":
                        MoveBottom(c[i], ref D[i]);
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
            if (D.Location.X > -100)
            {
                if (D.Location.X > 400 && D.Location.X < 415 + 80 * stop_pos_right)
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

        }

        // Move Left --------------------------------------------------------------------------------------------------
        private void MoveLeft(Car c, ref PictureBox D)
        {
            if (D.Location.X < 800)
            {
                if (D.Location.X > 220 - 80 * stop_pos_left && D.Location.X < 235)
                {
                    if (c.TL.Color == "green")
                    {
                        c.Move(ref D);

                        if (stop_pos_left != -1)
                            stop_pos_left--;
                    }
                }
                else
                {
                    c.Move(ref D);
                }
            }
        }

        // Move Bottom --------------------------------------------------------------------------------------------------
        private void MoveBottom(Car c, ref PictureBox D)
        {
            if (D.Location.Y > -100)
            {
                if (D.Location.Y > 320 && D.Location.Y < 335 + 70 * stop_pos_bottom)
                {
                    if (c.TL.Color == "green")
                    {
                        c.Move(ref D);

                        if (stop_pos_bottom != -1)
                            stop_pos_bottom--;
                    }
                }
                else
                {
                    c.Move(ref D);

                }
            }
        }

        // Move Top --------------------------------------------------------------------------------------------------
        private void MoveTop(Car c, ref PictureBox D)
        {
            if (D.Location.Y < 800)
            {
                if (D.Location.Y > 170 - 70 * stop_pos_top && D.Location.Y < 185)
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

