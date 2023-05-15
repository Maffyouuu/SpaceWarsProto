using System;
using System.Drawing;
using System.Windows.Forms;

namespace SpacedInvaders
{
    class Meteor : TokenGeneral
    {
		private readonly Game game = new Game();

		//Creating an object
		internal Meteor(PointF startLocation)
        {
			location = startLocation;
            sprite = Bitmap.FromFile("C:/Users/Matt/Desktop/SpaceInvaders_Prototype/meteor.png");
            bounds = new Rectangle((int)location.X, (int)location.Y, sprite.Size.Width, sprite.Size.Height);
        }

		//Object moving - Down
		internal override void Step(double elapsed)
		{
			switch (Global.MeteorDirection)
			{
				case Directions.Down:
					location.Y += (float)(elapsed * Global.MeteorSpeed);

					if (location.Y > Form.ActiveForm.ClientSize.Height)
					{
						Global.MeteorRandomSpawnGenerator();
						location.Y = 0-Global.MeteorRandomSpawn;
						location.X = Global.MeteorRandomSpawn;
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

		//Check collision with defender
		internal void CheckForCollisionWithDefender(Defender defender)
		{
			if (defender != null)
			{
				if (defender.bounds.IntersectsWith(bounds))
				{
					Global.lostLife = true;
					HitByDefender();
					game.RestartLevel();
					return;
				}
			}
		}

		//Object collision with defender
		internal void HitByDefender()
		{
			Global.PlayersRemaining -= 1;
		}
	}
}
