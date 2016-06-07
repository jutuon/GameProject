using System;
using Microsoft.Xna.Framework;

namespace GameProject
{
	public class Spawner : BasicGameObject 
	{

		public bool RandomLocation { get; set;}
		public bool RandomAngle { get; set;}
		public Rectangle Area { get; set;}
		public float ObjectAngle { get; set;}

		public int Time {
			get
			{
				return timer.WaitingTime;
			}
			set
			{
				timer.WaitingTime = value;
			}
		}

		private Timer timer;
		private Random random;

		public Spawner(bool randomLocation, bool randomAngle)
		{
			timer = new Timer();
			random = new Random();
			RandomAngle = randomAngle;
			RandomLocation = randomLocation;
		}

		public delegate void CreateNew(Vector2 position, float angle);
		public event CreateNew OnCreateObject;

		private void SpawnObject()
		{

			Vector2 position;

			if (RandomLocation)
			{
				float x = 0, y = 0;

				double r1 = random.NextDouble(), r2 = random.NextDouble();
				x = (float) (Area.Left + (Math.Abs(Area.Right - Area.Left) * r1));
				y = (float) (Area.Bottom + (Math.Abs(Area.Top - Area.Bottom) * r2));

				position = new Vector2(x, y);

			}
			else
			{
				position = new Vector2(Area.Center.X, Area.Center.Y);
			}

			float angle = 0;
			if (RandomAngle)
			{
				angle = MathHelper.ToRadians(random.Next(0, 361));
			}

			OnCreateObject(position, angle);

		}

		public override void Update(GameTime time)
		{
			if (timer.TimeElapsed(time)) SpawnObject();

			base.Update(time);
		}

	}
}

