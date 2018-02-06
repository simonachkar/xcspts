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
    public partial class Form3 : Form
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

        // Train
        PictureBox[] T;
        Train[] t;
        int trainPartsNum = 6;
        bool train_toggled = false;

        // Ambulance
        PictureBox A;
        Ambulance a;
        bool amb_toggled = false;

        Queue temp;

        public Form3(int mean, int min)
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

            T = new PictureBox[trainPartsNum];
            t = new Train[trainPartsNum];


            InitializeComponent();
        }

        private void Form3_Load_1(object sender, EventArgs e)
        {


            nb_Generated_Cars = 0;
            t1 = new TrafficLight(pictureBox1, "green", "horizontal");
            t2 = new TrafficLight(pictureBox2, "green", "horizontal");
            t3 = new TrafficLight(pictureBox3, "red", "vertical");
            t4 = new TrafficLight(pictureBox4, "red", "vertical");

        }

        private void timer1_Tick_1(object sender, EventArgs e)
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

        private void timer2_Tick_1(object sender, EventArgs e)
        {
            if (!t1.IsPaused)
            {
                t1.ChangeColor(ref pictureBox1);
                t2.ChangeColor(ref pictureBox2);
                t3.ChangeColor(ref pictureBox3);
                t4.ChangeColor(ref pictureBox4);
                this.Update();
            }

        }

        private void timer3_Tick_1(object sender, EventArgs e)
        {
            MoveCars();

            if (train_toggled)
            {
                MoveTrain();
                MoveCars();  
                t1.Pause(ref pictureBox1);
                t2.Pause(ref pictureBox2);
                t3.Pause(ref pictureBox3);
                t4.Pause(ref pictureBox4);

                if (t[trainPartsNum - 1].IsReachedDestination)
                {

                    t1.Unpause(ref pictureBox1, "green");
                    t2.Unpause(ref pictureBox2, "green");
                    t3.Unpause(ref pictureBox3, "red");
                    t4.Unpause(ref pictureBox4, "red");

                    train_toggled = false;
                }
            }

            if (amb_toggled)
            {
                a.Move(ref A, new Point(0, 200), new Point(200, 0));
                MoveCars();
                t1.Pause(ref pictureBox1);
                t2.Pause(ref pictureBox2);
                t3.Pause(ref pictureBox3);
                t4.Pause(ref pictureBox4);

                if (a.IsReachedDestination)
                {

                    t1.Unpause(ref pictureBox1, "green");
                    t2.Unpause(ref pictureBox2, "green");
                    t3.Unpause(ref pictureBox3, "red");
                    t4.Unpause(ref pictureBox4, "red");

                    amb_toggled = false;
                }
            }
        }
        //////////////////////////////////////////////////////////////////////////////////////////////////////////
        // Custom Methods: -------------------------------------------------------------------------------------//
        //////////////////////////////////////////////////////////////////////////////////////////////////////////

        // Generate Cars -----------------------------------------------------------------------------------------
        private void GenerateCar()
        {
            int num = rnd.Next(1, 5);
            // num = 2;
            switch (num)
            {
                case 1: // -> right 
                    stop_pos_right++;
                    D[nb_Generated_Cars] = new PictureBox
                    {
                        Location = new System.Drawing.Point(800, 250),
                        Size = new System.Drawing.Size(55, 55),
                        SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize,
                       BackColor = Color.Transparent,
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
                        Size = new System.Drawing.Size(55, 55),
                        SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize,
                        BackColor = Color.Transparent,
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
                        Size = new System.Drawing.Size(55, 55),
                        SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize,
                        BackColor = Color.Transparent,
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
                        Size = new System.Drawing.Size(55, 55),
                        SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize,
                        BackColor = Color.Transparent,
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
                        if (D[i].Location.X > 350 && D[i].Location.X < 370)
                        {
                            switch (rnd.Next(1, 3))
                            {
                                case 1:
                                    MoveRight(c[i], ref D[i]);
                                    break;
                                case 2:
                                default:
                                    c[i].Position = "bottom";
                                    c[i].Change(ref D[i]);
                                    break;
                            }
                  

                        }
                            
                        break;

                    case "left":
                        MoveLeft(c[i], ref D[i]);
                        if (D[i].Location.X > 300 && D[i].Location.X < 330)
                        {
                            switch (rnd.Next(1, 3))
                            {
                                case 1:
                                    MoveLeft(c[i], ref D[i]);
                                    break;
                                case 2:
                                default:
                                    c[i].Position = "top";
                                    c[i].Change(ref D[i]);
                                    break;
                            }
                        }
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
            if (D.Location.X > 400 && D.Location.X < 420 + 80 * stop_pos_right)
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

        private void button1_Click(object sender, EventArgs e)
        {
            ToogleTrain();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ToogleAmbulance();
        }


        // Move Left --------------------------------------------------------------------------------------------------
        private void MoveLeft(Car c, ref PictureBox D)
        {
            if (D.Location.X > 220 - 80 * stop_pos_left && D.Location.X < 240)
            {
                if (c.TL.Color == "green")
                {
                    c.Move(ref D);

                    if (stop_pos_left != 0)
                        stop_pos_left -= 2;
                }
                else if (c.TL.Color == "red" || c.TL.Color == "yellow")
                {
                    stop_pos_left += 2;
                }
            }
            else
            {
                c.Move(ref D);
            }
        }

        // Move Bottom --------------------------------------------------------------------------------------------------
        private void MoveBottom(Car c, ref PictureBox D)
        {
            if (D.Location.Y > 320 && D.Location.Y < 340 + 80 * stop_pos_bottom)
            {
                if (c.TL.Color == "green")
                {
                    c.Move(ref D);

                    if (stop_pos_bottom != 0)
                        stop_pos_bottom = -2;
                }
                else if (c.TL.Color == "red" || c.TL.Color == "yellow")
                {
                    stop_pos_bottom += 2;
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
            if (D.Location.Y > 170 - 80 * stop_pos_top && D.Location.Y < 185)
            {
                if (c.TL.Color == "green")
                {
                    c.Move(ref D);

                    if (stop_pos_top != 0)
                        stop_pos_top -= 2;
                }
                else if (c.TL.Color == "red" || c.TL.Color == "yellow")
                {
                    stop_pos_top += 2;
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

        // Toggle Train ---------------------------------------------------------------------------------------
        private void ToogleTrain()
        {


            int x = 35;

            T[0] = new PictureBox
            {
                Location = new System.Drawing.Point(300, 700),
                Size = new System.Drawing.Size(35, 35),
                SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage,
                TabIndex = 0,
                TabStop = false
            };
            t[0] = new Train(T[0], 1, "top");
            T[0].Show();
            this.Controls.Add(T[0]);

            for (int i = 1; i < trainPartsNum; i++)
            {
                T[i] = new PictureBox
                {
                    Location = new System.Drawing.Point(300, 700 + x),
                    Size = new System.Drawing.Size(35, 35),
                    SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage,
                    TabIndex = 0,
                    TabStop = false
                };
                t[i] = new Train(T[i], 2, "top");
                T[i].Show();
                this.Controls.Add(T[i]);

                x += 35;
            }

            train_toggled = true;
        }

        // Move Train --------------------------------------------------------------------------------------------------
        private void MoveTrain()
        {
            for (int i = 0; i < trainPartsNum; i++)
            {
                t[i].MovePartTop(ref T[i], new Point(0, 250), new Point(360, 0));

            }
        }

        // Toggle Ambulance ---------------------------------------------------------------------------------------
        private void ToogleAmbulance()
        {
            A = new PictureBox
            {
                Location = new System.Drawing.Point(800, 290),
                Size = new System.Drawing.Size(65, 25),
                SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage,
                TabIndex = 0,
                TabStop = false
            };
            a = new Ambulance(A);
            A.Show();
            this.Controls.Add(A);

            amb_toggled = true;
        }


    }
}

