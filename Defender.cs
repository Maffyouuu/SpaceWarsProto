using System.Drawing;

namespace SpacedInvaders
{
	class Defender : TokenGeneral
	{
		//Is the object Dead
		internal bool dead = false;

		//Creating an object
		internal Defender(PointF startLocation)
		{
			location = startLocation;
			sprite = Bitmap.FromFile ("C:/Users/Matt/Desktop/SpaceInvaders_Prototype/ship.png");
			bounds = new Rectangle((int)location.X, (int)location.Y, Global.DefenderSize.Width, Global.DefenderSize.Height);
			imgAttr.SetColorKey(Color.Black, Color.Black);
		}

		//Object moving - Left, Right, Up and Down
		internal override void Step(double elapsed)
		{
			switch (Global.DefenderDirection)
			{
				case Directions.None:
					sprite = Bitmap.FromFile("C:/Users/Matt/Desktop/SpaceInvaders_Prototype/ship.png");
					break;
				case Directions.Left:
					sprite = Bitmap.FromFile("C:/Users/Matt/Desktop/SpaceInvaders_Prototype/ship_Left.png");
					location.X -= (float)(elapsed * Global.DefenderSpeed);

					if (location.X < 0)
					{
						Global.DefenderDirection = Directions.None;
					}
					bounds.X = (int)location.X;
					break;

				case Directions.Right:
					sprite = Bitmap.FromFile("C:/Users/Matt/Desktop/SpaceInvaders_Prototype/ship_Right.png");
					location.X += (float)(elapsed * Global.DefenderSpeed);

					if (location.X + Global.DefenderSize.Width > Global.FormSize.Width)
					{
						Global.DefenderDirection = Directions.None;
					}
					bounds.X = (int)location.X;
					break;

				case Directions.Down:
					sprite = Bitmap.FromFile("C:/Users/Matt/Desktop/SpaceInvaders_Prototype/ship_Down.png");
					location.Y += (float)(elapsed * Global.DefenderSpeed);

					if (location.Y + Global.DefenderSize.Height > Global.FormSize.Height)
					{
						Global.DefenderDirection = Directions.None;
					}
					bounds.Y = (int)location.Y;
					break;

				case Directions.Up:
					sprite = Bitmap.FromFile("C:/Users/Matt/Desktop/SpaceInvaders_Prototype/ship.png");
					location.Y -= (float)(elapsed * Global.DefenderSpeed);

					if (location.Y <= 0)
					{
						Global.DefenderDirection = Directions.None;
					}
					bounds.Y = (int)location.Y;
					break;

			}
		}

		//Rendering the object
		internal override void Render(Graphics g)
		{
			g.DrawImage(sprite, bounds);
		}

		//Fininding defenders position for the bullet
		internal PointF GetBulletStartLocation()
		{
			return new PointF(location.X + (sprite.Width / 2) - (Global.BulletSize.Width / 2), location.Y);
		}
	}
}
