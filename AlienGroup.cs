using System.Drawing;

namespace SpacedInvaders
{
    class AlienGroup
    {
		//Creating private variables
        private readonly Alien[,] aliens;
		private readonly int leftMostAlien, rightMostAlien;
		private readonly Game game = new Game();

		//Creating a group of objects
		internal AlienGroup(int cols, int rows)
		{
			aliens = new Alien[cols, rows];

			for (int x = 0; x < cols; x++)
			{
				for (int y = 0; y < rows; y++)
				{
					PointF location = new Point(
						x * (Global.AlienSize.Width + Global.AlienSeparation.Width) + 10 + 200,
						y * (Global.AlienSize.Height + Global.AlienSeparation.Height) + 80 +100
					);
					aliens[x, y] = new Alien(location);
					
				}
			}

			leftMostAlien = 0;
			rightMostAlien = aliens.GetLength(0);
			Global.AlienDirection = Directions.Right;
		}

		//Moving each object
		internal void Step(double elapsed)
		{
			foreach (Alien alien in aliens) alien.Step(elapsed);
			CheckAlienDirection();
		}

		//Rendering each object
		internal void Render(Graphics graphics)
		{
			foreach (Alien alien in aliens) alien.Render(graphics);
		}

		//Checking the direction of the objects
		private void CheckAlienDirection()
		{

			//Moving - Left
			if (Global.AlienDirection == Directions.Left)
			{
				for (int y = 0; y < aliens.GetLength(1); y++)
				{
					Alien alien = aliens[leftMostAlien, y];

					if (alien.location.X <= 10)
					{
						Global.AlienDirection = Directions.Right;
						break;
					}
				}
			}
			//Moving - Right
			else
			{
				for (int y = 0; y < aliens.GetLength(1); y++)
				{
					Alien alien = aliens[rightMostAlien - 1, y];

					if (alien.location.X + Global.AlienSize.Width >= Global.FormSize.Width - 10)
					{
						Global.AlienDirection = Directions.Left;
						break;
					}
				}
			}
		}

		//Check collision on each object
		internal void CheckForCollision(Bullet bullet)
		{
			foreach (Alien alien in aliens)
			{
				if (!alien.dead && bullet.bounds.IntersectsWith(alien.bounds))
				{
					alien.HitByBullet();
					bullet.Hit();
					Global.AliensKilled++;

					//Switching level
					if (Global.AliensKilled == Global.TotalAliens() && !Ufo.alive) {
						game.LevelUp();
					}
					
					return;
				}
			}
		}
		internal void CheckForCollisionWithDefender(Defender defender)
		{
			foreach (Alien alien in aliens)
			{
				if (defender != null && !alien.dead)
				{
					if (defender.bounds.IntersectsWith(alien.bounds))
					{
						Global.lostLife = true;
						alien.HitByDefender();
						game.RestartLevel();
						return;
					}
				}
			}
		}
	}
}
