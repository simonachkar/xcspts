using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TraffSim
{
    class Car
    {
        // Car's Images. (Coming from the Left, Right, Top, Bottom). 
        Image car_left_white = Image.FromFile("car-l.png");
        Image car_right_white = Image.FromFile("car-r.png");
        Image car_top_white = Image.FromFile("car-u.png");
        Image car_bottom_white = Image.FromFile("car-d.png");
        Image car_left_taxi = Image.FromFile("car-l-taxi.png");
        Image car_right_taxi = Image.FromFile("car-r-taxi.png");
        Image car_top_taxi = Image.FromFile("car-u-taxi.png");
        Image car_bottom_taxi = Image.FromFile("car-d-taxi.png");
        Image car_left_red = Image.FromFile("car-l-red.png");
        Image car_right_red = Image.FromFile("car-r-red.png");
        Image car_top_red = Image.FromFile("car-u-red.png");
        Image car_bottom_red = Image.FromFile("car-d-red.png");
        Image car_left_blue = Image.FromFile("car-l-blue.png");
        Image car_right_blue = Image.FromFile("car-r-blue.png");
        Image car_top_blue = Image.FromFile("car-u-blue.png");
        Image car_bottom_blue = Image.FromFile("car-d-blue.png");

        //Random Image Picker
        Random rnd = new Random();

        //Position
        String position;
        public String Position
        {
            get { return position; }
            set { position = value; }
        }

        //Corresponding Traffic Light
        TrafficLight tl;
        public TrafficLight TL
        {
            get { return tl; }
            set { tl = value; }
        }

        // Car Form (1 -> white; 2 -> red; 3 -> taxi; 4 -> blue)
        int carForm;

        // Constructor: -------------------------------------------------------------------------------------
        public Car(PictureBox pb, String position, TrafficLight tl)
        {
            this.position = position;
            this.tl = tl;
            carForm = rnd.Next(1, 5);
          

            switch (carForm)
            {
                case 1: // -> white 
                    if (position == "left")
                        pb.Image = car_left_white;
                    else if (position == "right")
                        pb.Image = car_right_white;
                    else if (position == "top")
                        pb.Image = car_top_white;
                    else if (position == "bottom")
                        pb.Image = car_bottom_white;
                    break;

                case 2: // -> red 
                    if (position == "left")
                        pb.Image = car_left_red;
                    else if (position == "right")
                        pb.Image = car_right_red;
                    else if (position == "top")
                        pb.Image = car_top_red;
                    else if (position == "bottom")
                        pb.Image = car_bottom_red;
                    break;

                case 3: // -> taxi 
                    if (position == "left")
                        pb.Image = car_left_taxi;
                    else if (position == "right")
                        pb.Image = car_right_taxi;
                    else if (position == "top")
                        pb.Image = car_top_taxi;
                    else if (position == "bottom")
                        pb.Image = car_bottom_taxi;
                    break;

                case 4: // -> blue
                default:
                    if (position == "left")
                        pb.Image = car_left_blue ;
                    else if (position == "right")
                        pb.Image = car_right_blue;
                    else if (position == "top")
                        pb.Image = car_top_blue;
                    else if (position == "bottom")
                        pb.Image = car_bottom_blue;
                    break;
            }
          
        }

        // Move ----------------------------------------------------------------------------------------------
        public void Move(ref PictureBox pb)
        {
            switch (this.position)
            {
                case "right":
                    pb.Left -= 10;
                    break;

                case "left":
                    pb.Left += 10;
                    break;

                case "bottom":
                    pb.Top -= 10;
                    break;

                case "top":
                default:
                    pb.Top += 10;
                    break;
            }
        }

        public void Change(ref PictureBox pb)
        {
            switch (carForm)
            {
                case 1: // -> white 
                    if (position == "left")
                        pb.Image = car_left_white;
                    else if (position == "right")
                        pb.Image = car_right_white;
                    else if (position == "top")
                        pb.Image = car_top_white;
                    else if (position == "bottom")
                        pb.Image = car_bottom_white;
                    break;

                case 2: // -> red 
                    if (position == "left")
                        pb.Image = car_left_red;
                    else if (position == "right")
                        pb.Image = car_right_red;
                    else if (position == "top")
                        pb.Image = car_top_red;
                    else if (position == "bottom")
                        pb.Image = car_bottom_red;
                    break;

                case 3: // -> taxi 
                    if (position == "left")
                        pb.Image = car_left_taxi;
                    else if (position == "right")
                        pb.Image = car_right_taxi;
                    else if (position == "top")
                        pb.Image = car_top_taxi;
                    else if (position == "bottom")
                        pb.Image = car_bottom_taxi;
                    break;

                case 4: // -> blue
                default:
                    if (position == "left")
                        pb.Image = car_left_blue;
                    else if (position == "right")
                        pb.Image = car_right_blue;
                    else if (position == "top")
                        pb.Image = car_top_blue;
                    else if (position == "bottom")
                        pb.Image = car_bottom_blue;
                    break;
            }
        }


    }
}
