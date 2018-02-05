using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TraffSim
{
    class TrafficLight
    {
        // Traffic Light's Images (horizontal and vertical).
        Image TL_Hor_Red = Image.FromFile("tl-h-r.png");
        Image TL_Hor_Green = Image.FromFile("tl-h-g.png");
        Image TL_Hor_Yellow = Image.FromFile("tl-h-y.png");
        Image TL_Ver_Red = Image.FromFile("tl-v-r.png");
        Image TL_Ver_Green = Image.FromFile("tl-v-g.png");
        Image TL_Ver_Yellow = Image.FromFile("tl-v-y.png");

        /* Counter for the red and green light, so they last longer than the yellow light.
            The sequences are: G -> Y -> R -> R -> G and R -> R -> G -> G -> Y */
        private int counter_red = 1, counter_green = 0;

        // Traffic Light's color. ("red", "green" or "yellow").
        private String color;
        // Getters and Setters for Color
        public String Color
        {
            get { return color; }
            set { color = value; }
        }

        // Traffic Light's position, for drawing on the PictureBox. ("horizontal" or "vertical").
        private String position;
        // Getters and Setters for Position
        public String Position
        {
            get { return position; }
            set { position = value; }
        }

        // If tl is paused, stuck on red (for train)
        private bool isPaused;
        // Getters and Setters for Position
        public bool IsPaused
        {
            get { return isPaused; }
            set { isPaused = value; }
        }




        // Constructor: ------------------------------------------------------------------------------
        public TrafficLight(PictureBox pb, String init_Color, String init_Position)
        {
            this.isPaused = false;
            this.color = init_Color;
            this.position = init_Position;
            this.Draw(pb);
        }

        // Draw Traffic Light ------------------------------------------------------------------------
        public void Draw(PictureBox pb)
        {
            switch (this.position)
            {
                case "vertical":
                    switch (this.color)
                    {
                        case "yellow":
                            pb.Image = TL_Ver_Yellow;
                            break;

                        case "green":
                            pb.Image = TL_Ver_Green;
                            break;

                        case "red":
                        default:
                            pb.Image = TL_Ver_Red;
                            break;
                    }
                    break;

                case "horizontal":
                default:
                    switch (this.color)
                    {
                        case "yellow":
                            pb.Image = TL_Hor_Yellow;
                            break;

                        case "green":
                            pb.Image = TL_Hor_Green;
                            break;

                        case "red":
                        default:
                            pb.Image = TL_Hor_Red;
                            break;
                    }
                    break;
            }
        }

        // Change TL colors -------------------------------------------------------------------------
        public void ChangeColor(ref PictureBox pb)
        {
            if (this.color == "green")
            {
                if (counter_green >= 1)
                {
                    counter_green--;
                }
                else
                {
                    counter_green = 1;
                    color = "yellow";
                }
            }
            else if (color == "red")
            {
                if (counter_red >= 1)
                {
                    counter_red--;
                }
                else
                {
                    counter_red = 0;
                    color = "green";
                }
            }
            else { counter_red = 0; counter_green = 1; color = "red"; }
            Draw(pb);
        }

        // Make Traffic Light Red, when Train passes
        public void Pause(ref PictureBox pb)
        {
            this.isPaused = true;
            color = "red";
            Draw(pb);
        }

        // Unpause TL
        public void Unpause(ref PictureBox pb, string color)
        {
            this.isPaused = false;
            this.color = color;
            Draw(pb);
        }



    }
}
