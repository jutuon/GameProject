﻿using System;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace GameProject
{
	public class Player : DrawableGameObject
	{

		private Texture2D laserTexture;
		private double lastShotTime;


		public Player(Texture2D texture, Texture2D laser) : base(texture)
		{
			laserTexture = laser;
		}


		public void Shoot(GameObjectContainer<Laser> laserContainer, GameTime time)
		{
			if (time.TotalGameTime.TotalSeconds < lastShotTime + 0.2) return;

			Laser laser = new Laser(laserTexture, this);
			laser.MoveForward(5);

			lastShotTime = time.TotalGameTime.TotalSeconds;
			laserContainer.Add(laser);
			laser.NotInAllowedArea += delegate(object sender, EventArgs e)
			{
				laserContainer.AddToBeRemoved((Laser)sender);
			};
		}
			
	}
}
