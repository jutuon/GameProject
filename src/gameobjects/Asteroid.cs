using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace GameProject
{
	public class Asteroid  : GameObject
	{
		private float lifeArea;
		private int movementSpeed;
		private float spinningSpeed;

		public Asteroid(Texture2D texture, CollisionEngine engine) : base(texture, engine)
		{
			Random random = new Random();
			lifeArea = 500;
			movementSpeed = random.Next(1, 2);
			spinningSpeed = MathHelper.ToRadians(random.Next(1, 6) - 3);
		}

		public override void Update(GameTime time)
		{
			Move((float)movementSpeed);
			DrawingAngle += spinningSpeed;

			if (Position.X < -lifeArea || Position.X > lifeArea || Position.Y < -lifeArea || Position.Y > lifeArea) Destroy();
		}
			
	}
}

