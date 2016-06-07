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
		private double lastShotTime;


		public Player(Texture2D texture, Texture2D laser, CollisionEngine engine, GameWorld world) : base(texture, engine)
		{
			laserTexture = laser;
			this.world = world;
		}


		public Laser Shoot(GameTime time)
		{
			if (time.TotalGameTime.TotalSeconds < lastShotTime + 0.2) return null;

			Laser laser = new Laser(laserTexture, this, collisionEngine, 5);


			lastShotTime = time.TotalGameTime.TotalSeconds;

			world.AddGameObject(laser);


			return laser;
		}
			
	}
}

