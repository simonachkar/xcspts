using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TraffSim
{
    class Ambulance
    {

        // Ambulance's Images.
        Image ambulance_img = Image.FromFile("ambulance.png");

        //when the train is out of sight
        bool isReachedDestination;
        public bool IsReachedDestination
        {
            get { return isReachedDestination; }
            set { isReachedDestination = value; }
        }

        // Constructor: -------------------------------------------------------------------------------------
        public Ambulance(PictureBox pb)
        {
            isReachedDestination = false;
            pb.Image = ambulance_img;

        }

        // Move ----------------------------------------------------------------------------------------------
        public void Move(ref PictureBox pb, Point rotate1, Point rotate2)
        {

            if (pb.Location.X > -100)
            {
                if (pb.Location.X < 420 && pb.Location.X > 220 )
                {
                    pb.Top -= 3;
                    pb.Left -= 13;
                }
                else
                {
                    pb.Left -= 13;
                }

                //if (pb.Location.Y > rotate1.Y)
                //{
                //    pb.Top -= 10;
                //}
                //else
                //{
                //    if (pb.Location.X < rotate2.X)
                //    {
                //        pb.Left += 10;
                //    }
                //    else pb.Top -= 10;
                //}

            }
            else
            {
                // MessageBox.Show("reached Destination");
                isReachedDestination = true;
            }
        }


    }
}



