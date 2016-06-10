using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace GameProject
{
	public class Asteroid  : GameObject
	{
		private float lifeArea;
		private float movementSpeed;
		private float spinningSpeed;

		public Asteroid(Texture2D texture, CollisionEngine engine) : base(texture, engine)
		{
			Random random = new Random();
			lifeArea = 700;
			movementSpeed =  (float) random.NextDouble() * 2 + 1;
			spinningSpeed = MathHelper.ToRadians(random.Next(1, 6) - 3);
		}

		public override void Update(GameTime time)
		{
			Move(movementSpeed);
			DrawingAngle += spinningSpeed;

			if (Position.X < -lifeArea || Position.X > lifeArea || Position.Y < -lifeArea || Position.Y > lifeArea) Destroy();
		}
			
	}
}

