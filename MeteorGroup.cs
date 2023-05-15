using System.Drawing;

namespace SpacedInvaders
{
    class MeteorGroup
	{
		//Creating private variables
        private readonly Meteor[] meteors;
		private readonly Game game = new Game();

		//Creating a group of objects
		internal MeteorGroup(int cols)
		{
			meteors = new Meteor[cols];

			for (int x = 0; x < cols; x++)
			{
				Global.MeteorRandomSpawnGenerator();
				PointF location = new Point(Global.MeteorRandomSpawn, 0 - Global.MeteorRandomSpawn);
				meteors[x] = new Meteor(location);
			}
			Global.MeteorDirection = Directions.Down;
		}

		//Moving each object
		internal void Step(double elapsed)
		{
			foreach (Meteor meteor in meteors) meteor.Step(elapsed);
		}

		//Rendering each object
		internal void Render(Graphics graphics)
		{
			foreach (Meteor meteor in meteors) meteor.Render(graphics);
		}

		//Check collision with defender
		internal void CheckForCollisionWithDefender(Defender defender)
		{
			foreach (Meteor meteor in meteors) 
			{
				if (defender != null)
				{
					if (defender.bounds.IntersectsWith(meteor.bounds))
					{
						Global.lostLife = true;
						meteor.HitByDefender();
						game.RestartLevel();
						return;
					}
				}
			}
		}
	}
}
