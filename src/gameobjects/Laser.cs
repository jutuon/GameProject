using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace GameProject
{
	public class Laser : GameObject
	{ 
		private float lifeArea;
		private String tag;


		public Laser(Texture2D texture, BasicGameObject parent, CollisionEngine engine, float startingPosition) : base(texture, engine)
		{
			CloneState(parent);
			lifeArea = 500;
			Move(startingPosition);
		}
			

		public override void Update(GameTime time)
		{
			Move(5);

			if (Position.X < -lifeArea || Position.X > lifeArea || Position.Y < -lifeArea || Position.Y > lifeArea) Destroy();
		}


	}
}

