using System;
using System.Windows.Forms;
//using System.Collections;
using System.Drawing;

namespace SpacedInvaders
{
	class Game
	{
		private Defender defender;
		private Image buffer;
		private Graphics bufferGraphics;
		private Graphics displayGraphics;
		private Form form;
		private Font font = new Font("Impact", 14);
		private Font largeFont = new Font("Impact", 26);
		private Brush fontBrush = Brushes.White;

		private double renderElapsed = 0d;
	

  
		// internal Game() { }
		// private static Game instance = new Game();
		//internal static Game Instance { get { return instance; } }



		internal void Initialize(Form mainForm)
		{
			this.form = mainForm;

			//Set up the off-screen buffer used for double-buffering
			buffer = new Bitmap(mainForm.Width, mainForm.Height);
			bufferGraphics = Graphics.FromImage(buffer);
			displayGraphics = mainForm.CreateGraphics();

		}
 

		internal void GameLoop()
		{
      
			while (form.Created)
			{
				render();// (elapsed);
				Application.DoEvents();

			}
		}

		private void render()  //(double elapsed)
		{
           

			bufferGraphics.Clear(Color.Black);
      
			if (!Global.GameOver)
			{
             defender.Render(bufferGraphics);
			}
			else
			{
				// Show "Game Over" message in the center of the screen
				bufferGraphics.DrawString("Game Over", largeFont, fontBrush, 310, 290);
				bufferGraphics.DrawString("Press F5 to start a new game", font, fontBrush, 278, 350);
			}

			// Display banner
			bufferGraphics.DrawString("Score: " + Global.Score, font, fontBrush, 10, 10);
			bufferGraphics.DrawString("Level: " + Global.CurrentLevel, font, fontBrush, 630, 10);
			bufferGraphics.DrawString("Players: " + Global.PlayersRemaining, font, fontBrush, 710, 10);

			// Blit the off-screen buffer on to the display
			displayGraphics.DrawImage(buffer, 0, 0);
		}



		internal void OnKeyDown(object sender, KeyEventArgs e)
		{

			if (e.KeyCode == Keys.Escape)
				Application.Exit();
            
			// If game is not active
			if (Global.GameOver)
			{
				if (e.KeyCode == Keys.F5)
					startNewGame();
			}
			switch (e.KeyCode)
			{
				case Keys.Left:
			//	Global.DefenderDirection = Directions.Left;
				break;
			case Keys.Right:
			//	Global.DefenderDirection = Directions.Right;
				break;
			case Keys.Down:
			//Global.DefenderDirection = Directions.None;
				break;}

			
		}


		private void startNewGame()
		{
			Global.GameOver = false;
			Global.Score = 0;
			Global.PlayersRemaining = 3;
			

			defender = new Defender(new Point(form.ClientSize.Width / 2 - 20, 
			form.ClientSize.Height - 50));


		}
       
	}
}