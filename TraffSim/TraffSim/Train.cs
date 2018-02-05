using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TraffSim
{
    class Train
    {
        // Train's Images.
        Image train_1 = Image.FromFile("train-1.png");
        Image train_2 = Image.FromFile("train-2.png");

        //Position
        String direction;
        public String Direction
        {
            get { return direction; }
            set { direction = value; }
        }

        //when the train is out of sight
        bool isReachedDestination;
        public bool IsReachedDestination
        {
            get { return isReachedDestination; }
            set { isReachedDestination = value; }
        }

        // Constructor: -------------------------------------------------------------------------------------
        public Train(PictureBox pb, int num, String direction)
        {
            isReachedDestination = false;
            this.direction = direction;
            if (num == 1)
            {
                pb.Image = train_1;
            }
            else
            {
                pb.Image = train_2;
            }

        }


        // Move Part ----------------------------------------------------------------------------------------
        public void MovePartTop(ref PictureBox pb, Point rotate1, Point rotate2)
        {

            if (pb.Location.Y > -50)
            {
                if (pb.Location.Y > rotate1.Y)
                {
                    pb.Top -= 10;
                }
                else
                {                    
                    if (pb.Location.X < rotate2.X)
                    {
                       pb.Left += 10;
                    }
                    else pb.Top -= 10;
                }
      
            }
            else
            {
               // MessageBox.Show("reached Destination");
                isReachedDestination = true;
            } 
        }
    }
}
