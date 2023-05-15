using System;
using System.Drawing;

namespace SpacedInvaders
{
	class Global
	{
        // Layout related constants
        internal static readonly Size FormSize = new Size(800, 600);

        // Stats related
        internal static int Score = 0;
        internal static int AllScore = 0;
        internal static bool GameOver = true;
        internal static bool LevelFinished = false;
        internal static int CurrentLevel = 1;
        internal static int LastLevel = 4;
        internal static int PlayersRemaining = 3;
        internal static Boolean lostLife = false;
        internal static readonly Size DefenderSize = new Size(31,23);

        // Defender
        internal static Directions DefenderDirection = Directions.None;
        internal static Directions CurrentDirection = Directions.None;
        internal static float DefenderSpeed = 1f / 5f;

        //Alien
        internal static Directions AlienDirection = Directions.Right;
        internal static float AlienSpeed = 1f / 20f;
        internal static readonly Size AlienSize = new Size(40, 40);
        internal static readonly Size AlienSeparation = new Size(30, 20);
        internal static int AliensRow = 2;
        internal static int AliensCol = 2;
        internal static int AliensKilled = 0;
        internal static int TotalAliens()
        {
            return AliensRow * AliensCol;
        }

        //Ufo
        internal static Directions UfoDirection = Directions.Left;
        internal static float UfoSpeed = 1f/3f;
        internal static readonly Size UfoSize = new Size(80, 38);
        internal static int UfoHealth = 3;

        //Meteor
        internal static Directions MeteorDirection = Directions.Down;
        internal static float MeteorSpeed = 1f / 2f;
        internal static readonly Size MeteorSize = new Size(12, 35);
        internal static int MeteorsCol = 5;
        internal static readonly Size MeteorSeparation = new Size(MeteorRandomSpawn, MeteorRandomSpawn);

        //Bullet
        internal static readonly Size BulletSize = new Size(13,13);
        internal static bool bulletfiring = false;
        internal static float bulletSpeed = 1f / 3f; 

        static readonly Random r = new Random();

        //Random Number Generator
        internal static int RandomNumberGenerator = r.Next(0, Global.FormSize.Height - 100);
        internal static void RandomNumberSpawnGenerator()
        {
            RandomNumberGenerator = r.Next(0, Global.FormSize.Height - 100);
        }

        //Meteor random Spawn Generator
        internal static int MeteorRandomSpawn = r.Next(0, 800);
        internal static void MeteorRandomSpawnGenerator()
        {
            MeteorRandomSpawn = r.Next(0, 800);
        }
    }
}
