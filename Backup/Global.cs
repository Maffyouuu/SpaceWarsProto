using System;
using System.Drawing;

namespace SpacedInvaders
{
	class Global
	{
        // Layout related constants
        internal static readonly Size FormSize          = new Size(800, 600);
    

        // Stats related
        internal static int Score = 0;
        internal static bool GameOver = true;
        internal static bool LevelFinished = false;
        internal static int CurrentLevel = 1;
        internal static int PlayersRemaining = 3;

		  //internal static Directions DefenderDirection = Directions.None;

   
    }
}
