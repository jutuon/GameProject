using System;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace GameProject
{
	public class Player : GameObject
	{
		private GameWorld world;
		private Texture2D laserTexture;
		private Timer timer;


		public Player(Texture2D texture, Texture2D laser, CollisionEngine engine, GameWorld world) : base(texture, engine)
		{
			timer = new Timer();
			timer.WaitingTime = 200;
			laserTexture = laser;
			this.world = world;
		}


		public Laser Shoot(GameTime time)
		{
			if (timer.TimeElapsed(time))
			{
				Laser laser = new Laser(laserTexture, this, collisionEngine, 5);

				world.AddGameObject(laser, true);

				return laser;
			}

			return null;
		}
			
	}
}

