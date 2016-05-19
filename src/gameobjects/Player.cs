using System;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace GameProject
{
	public class Player : GameObject
	{

		private Texture2D laserTexture;
		private double lastShotTime;


		public Player(Texture2D texture, Texture2D laser, CollisionEngine engine) : base(texture, engine)
		{
			laserTexture = laser;
		}


		public Laser Shoot(GameObjectContainer<Laser> laserContainer, GameTime time, Camera camera)
		{
			if (time.TotalGameTime.TotalSeconds < lastShotTime + 0.2) return null;

			Laser laser = new Laser(laserTexture, this, collisionEngine);
			laser.MoveForward(5);

			lastShotTime = time.TotalGameTime.TotalSeconds;
			laserContainer.Add(laser);
			laser.OnDestroy += delegate(object sender, EventArgs e)
			{
				laserContainer.AddToBeRemoved((Laser)sender);
			};

			camera.AddToCamera(laser);

			return laser;
		}
			
	}
}

