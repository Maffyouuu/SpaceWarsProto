using System.Drawing;
using System.Windows.Forms;

namespace SpacedInvaders
{
    class Bullet : TokenGeneral
    {
        //Creating an object
        internal Bullet(PointF startLocation)
        {
            location = startLocation;
            sprite = Bitmap.FromFile("C:/Users/Matt/Desktop/SpaceInvaders_Prototype/Bullet_Blue_13px.png");
            bounds = new Rectangle((int)location.X, (int)location.Y, Global.BulletSize.Width, Global.BulletSize.Height);
        }

        //Object moving - None, Left, Right, Up and Down
        internal override void Step(double elapsed)
        {
            switch (Global.CurrentDirection) {
                case Directions.None:
                    location.Y -= (float)(elapsed * Global.bulletSpeed);
                    bounds.Y = (int)location.Y;
                    break;

                case Directions.Left:
                    location.X -= (float)(elapsed * Global.bulletSpeed);
                    bounds.X = (int)location.X;
                    break;

                case Directions.Right:
                    location.X += (float)(elapsed * Global.bulletSpeed);
                    bounds.X = (int)location.X;
                    break;

                case Directions.Up:
                    location.Y -= (float)(elapsed * Global.bulletSpeed);
                    bounds.Y = (int)location.Y;
                    break;

                case Directions.Down:
                    location.Y += (float)(elapsed * Global.bulletSpeed);
                    bounds.Y = (int)location.Y;
                    break;

            }

            //Check if object is out of the window
            Global.bulletfiring = (location.Y >= 0) && 
                (location.Y <= Form.ActiveForm.ClientSize.Height) && 
                (location.X <= Form.ActiveForm.ClientSize.Width) && 
                (location.X >= 0);
        }

        //Rendering the object
        internal override void Render(Graphics g)
        {
            g.DrawImage(sprite, bounds);
        }

        //Object hit a different object
        internal void Hit()
        {
            Global.bulletfiring = false;
        }
    }
}
