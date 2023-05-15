using System;
using System.Drawing;
using System.Windows.Forms;

namespace SpacedInvaders
{
    class Ufo : TokenGeneral
    {
		//Is the object Dead
		internal static bool alive = true;
		private Game game = new Game();

		//Creating an object
		internal Ufo(PointF startLocation)
        {
			location = startLocation;
            sprite = Bitmap.FromFile("C:/Users/Matt/Desktop/SpaceInvaders_Prototype/ufo.png");
            bounds = new Rectangle((int)location.X, (int)location.Y, Global.UfoSize.Width, Global.UfoSize.Height);
        }

		//Object moving - Left
		internal override void Step(double elapsed)
		{
			switch (Global.UfoDirection)
			{
				case Directions.Left:
					location.X -= (float)(elapsed * Global.UfoSpeed);

					if (location.X < 0)
					{
						Global.RandomNumberSpawnGenerator();
						location.X = Form.ActiveForm.ClientSize.Width + Global.RandomNumberGenerator;
						location.Y = Global.RandomNumberGenerator;
					}
					bounds.X = (int)location.X;
					bounds.Y = (int)location.Y;
					break;

				default:
					throw new Exception("Invalid direction for Ufo!");
			}
		}

		//Rendering the object
		internal override void Render(Graphics g)
		{
			g.DrawImage(sprite, bounds);
		}
		
		//Check collision with bullet
		internal void CheckForCollision(Bullet bullet)
		{
			if (bullet != null && alive && bullet.bounds.IntersectsWith(bounds))
			{
				HitByBullet();
				bullet.Hit();

				if (Global.AliensKilled == Global.TotalAliens() && !Ufo.alive)
				{
					game.LevelUp();
				}
				return;
			}
			
		}

		//Check collision with defender
		internal void CheckForCollisionWithDefender(Defender defender)
		{
			if (defender != null)
			{
				if (alive && defender.bounds.IntersectsWith(bounds))
				{
					Global.lostLife = true;
					HitByDefender();
					game.RestartLevel();
					return;
				}
			}
		}

		//Object collision with bullet
		internal void HitByBullet()
		{
			Global.UfoHealth--;
			if (Global.UfoHealth == 0) {
				alive = false;
				sprite = Bitmap.FromFile("C:/Users/Matt/Desktop/SpaceInvaders_Prototype/Alien3_transparent.png");
			}
		}

		//Object collision with defender
		internal void HitByDefender()
		{
			Global.PlayersRemaining -= 1;
		}
	}
}
