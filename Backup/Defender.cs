using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace SpacedInvaders
{
	class Defender : TokenGeneral
	{
       
		double spriteDelay = 100d;
		bool exploding = false;
		bool dead = false;

		internal bool Dead { get { return dead; } }

		internal Defender(PointF startLocation)
		{
			location = startLocation;
            
          sprite = Bitmap.FromFile ("c:/ship.bmp");

			bounds = new Rectangle((int)location.X, (int)location.Y, sprite.Size.Width, sprite.Size.Height);

			imgAttr.SetColorKey(Color.Black, Color.Black);
		}

		

		internal override void Render(Graphics g)
		{
			g.DrawImage(sprite, bounds);
		}


		internal override void Step(double elapsed){}
			}
}
