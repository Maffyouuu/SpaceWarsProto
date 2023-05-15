using System;
using System.Windows.Forms;
using System.Drawing;

namespace SpacedInvaders
{
	class Game
	{
		//Classes
		private MeteorGroup meteors;
		private Defender defender;
		private AlienGroup aliens;
		private Ufo ufo;
		private Bullet bullet;

		//Other variables
		private Image buffer;
		private Graphics bufferGraphics;
		private Graphics displayGraphics;
		private Form form;
		private readonly Font font = new Font("Impact", 14);
		private readonly Font largeFont = new Font("Impact", 26);
		private readonly Brush fontBrush = Brushes.White;

		//Initializing the window
		internal void Initialize(Form mainForm)
		{
			this.form = mainForm;
			buffer = new Bitmap(mainForm.Width, mainForm.Height);
			//backgroundSize = (mainForm.Width, mainForm.Height);
			bufferGraphics = Graphics.FromImage(buffer);
			displayGraphics = mainForm.CreateGraphics();
		}

		//Making a continues loop for the game
		internal void GameLoop()
		{
			DateTime start;
			double elapsed = 0d;

			while (form.Created)
			{
				start = DateTime.Now;
				Render();
				Application.DoEvents();

				//If game is not over
				if (!Global.GameOver)
				{
					Step(elapsed);
					detectCollision();
				}
				elapsed = (DateTime.Now - start).TotalMilliseconds;
			}
		}

		//Move objects
		private void Step(double elapsed)
		{
			defender.Step(elapsed);
			aliens.Step(elapsed);
			ufo.Step(elapsed);
			meteors.Step(elapsed);

			if (!Global.bulletfiring) Global.CurrentDirection = Global.DefenderDirection;
			if (Global.bulletfiring) bullet.Step(elapsed); 

		}

		//Render objects
		private void Render()
		{
			bufferGraphics.DrawImage(Bitmap.FromFile("C:/Users/Matt/Desktop/SpaceInvaders_Prototype/stars_background.png"), 0,0);

			if (!Global.GameOver)
			{
				defender.Render(bufferGraphics);
				aliens.Render(bufferGraphics);
				ufo.Render(bufferGraphics);
				meteors.Render(bufferGraphics);
				if (Global.bulletfiring) bullet.Render(bufferGraphics);
			}
			else 
			{
				//Start game screen
				if (Global.CurrentLevel == 1 && !Global.lostLife) {
					bufferGraphics.DrawString("Start Game", largeFont, fontBrush, 280, 290);
					bufferGraphics.DrawString("Press F5 to load first level", font, fontBrush, 278, 350);
				}
				//Level Finished screen
				else if (Global.CurrentLevel != Global.LastLevel && !Global.lostLife)
				{
					bufferGraphics.DrawString("Level Finished", largeFont, fontBrush, 280, 290);
					bufferGraphics.DrawString("Press F5 to load new level", font, fontBrush, 278, 350);
				}
				//Lost a life screen
				else if (Global.lostLife && Global.PlayersRemaining != 0) 
				{
					bufferGraphics.DrawString("You lost a life", largeFont, fontBrush, 280, 290);
					bufferGraphics.DrawString("Press F5 to restart level", font, fontBrush, 278, 350);
				}
				//Game over screen
				else
				{
					bufferGraphics.DrawString("Game Over", largeFont, fontBrush, 310, 290);
					bufferGraphics.DrawString("Press F5 to start a new game", font, fontBrush, 278, 350);
				}
			}
			// Display banner
			bufferGraphics.DrawString("All Levels Score: " + Global.AllScore, font, fontBrush, 10, 10);
			bufferGraphics.DrawString("Score: " + Global.Score, font, fontBrush, 10, 30);
			bufferGraphics.DrawString("Level: " + Global.CurrentLevel, font, fontBrush, 630, 10);
			bufferGraphics.DrawString("Players: " + Global.PlayersRemaining, font, fontBrush, 710, 10);
			bufferGraphics.DrawString("Ufo Health: " + Global.UfoHealth, font, fontBrush, 630, 30);
			bufferGraphics.DrawString("Ufo Status: " + Ufo.alive, font, fontBrush, 630, 50);

			// Blit the off-screen buffer on to the display
			displayGraphics.DrawImage(buffer, 0, 0);
		}

		//On key press
		internal void OnKeyDown(object sender, KeyEventArgs e)
		{
			//ESC Key
			if (e.KeyCode == Keys.Escape) Application.Exit();

			// If game is not active
			//F5 Key
			if (Global.GameOver)
			{
				if (e.KeyCode == Keys.F5) {
					Global.LevelFinished = false;
					startNewGame();
				}
			}

			//4 Arrows & Space
			switch (e.KeyCode)
			{
				case Keys.Left:
					Global.DefenderDirection = Directions.Left;
					break;

				case Keys.Right:
					Global.DefenderDirection = Directions.Right;
					break;

				case Keys.Up:
					Global.DefenderDirection = Directions.Up;
					break;

				case Keys.Down:
					Global.DefenderDirection = Directions.Down;
					break;

				case Keys.Space:
				{
					if (!Global.bulletfiring)
					{
						Global.bulletfiring = true;
						bullet = new Bullet(defender.GetBulletStartLocation());
					}
					break;

				}
			}
		}

		//Restart Level
		internal void RestartLevel()
		{
			Global.GameOver = true;
		}

		//Change variables on Level Up
		internal void LevelUp() {
			Global.CurrentLevel++;
			Global.AllScore += Global.Score;
			Global.AlienSpeed += 1f / 20f;
			Global.AliensRow += 1;
			Global.AliensCol += 2;
			Global.GameOver = true;
		}

		//Creating objects
		//Reset variables on start of game
		private void startNewGame()
		{
			Global.GameOver = false;
			Global.lostLife = false;
			Global.AliensKilled = 0;
			Ufo.alive = true;
			Global.UfoHealth = 3;

			if (Global.CurrentLevel == Global.LastLevel || Global.PlayersRemaining == 0){
				Global.PlayersRemaining = 3;
				Global.AlienSpeed = 1f / 20f;
				Global.CurrentLevel = 1;
				Global.AliensRow = 2;
				Global.AliensCol = 2;
				Global.AllScore = 0;
			}

			Global.Score = 0;
			defender = new Defender(new Point(form.ClientSize.Width / 2 - 20, form.ClientSize.Height - 50));
			aliens = new AlienGroup(Global.AliensRow, Global.AliensCol);
			ufo = new Ufo(new Point(form.ClientSize.Width + Global.RandomNumberGenerator, Global.RandomNumberGenerator));
			meteors = new MeteorGroup(Global.MeteorsCol);
		}

		//Detection of collision
		private void detectCollision()
		{
			if (Global.bulletfiring) { 
				aliens.CheckForCollision(bullet);
				ufo.CheckForCollision(bullet);
			}
			aliens.CheckForCollisionWithDefender(defender);
			ufo.CheckForCollisionWithDefender(defender);
			meteors.CheckForCollisionWithDefender(defender);
		}
	}
}