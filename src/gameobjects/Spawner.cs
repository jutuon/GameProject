using System;
using Microsoft.Xna.Framework;

namespace GameProject
{
	public class Spawner<T> : BasicGameObject where T : GameObject
	{
		public bool RandomLocation { get; set;}
		public bool RandomAngle { get; set;}
		public Rectangle Area { get; set;}
		public float ObjectAngle { get; set;}

		public float Time { get; set;}

		private GameTime lastSpawnTime;
		private Random random;

		public Spawner()
		{
			random = new Random();
		}


		public override void Update(GameTime time)
		{

			base.Update(time);
		}

	}
}

