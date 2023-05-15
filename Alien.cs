using System;
using System.Drawing;

namespace SpacedInvaders
{
    class Alien : TokenGeneral
    {
		//Is the object Dead
		internal bool dead = false;

		//Creating an object
		internal Alien(PointF startLocation)
        {
			location = startLocation;
            sprite = Bitmap.FromFile("C:/Users/Matt/Desktop/SpaceInvaders_Prototype/Alien.png");
            bounds = new Rectangle((int)location.X, (int)location.Y, Global.AlienSize.Width, Global.AlienSize.Height);
        }

		//Object moving - Left and Right
		internal override void Step(double elapsed)
		{
			switch (Global.AlienDirection)
			{
				case Directions.Left:
					location.X -= (float)(elapsed * Global.AlienSpeed);
					bounds.X = (int)location.X;
					break;

				case Directions.Right:
					location.X += (float)(elapsed * Global.AlienSpeed);
					bounds.X = (int)location.X;
					break;

				default:
					throw new Exception("Invalid direction for alien!");
			}
		}

		//Rendering the object
		internal override void Render(Graphics g)
		{
			g.DrawImage(sprite, bounds);
		}

		//Object being hit by a different object - Bullet
		internal void HitByBullet()
		{
			dead = true;
			sprite = Bitmap.FromFile("C:/Users/Matt/Desktop/SpaceInvaders_Prototype/Alien3_transparent.png");
			Global.Score += 10;
		}
		internal void HitByDefender()
		{
			Global.PlayersRemaining -= 1;
		}
	}
}
