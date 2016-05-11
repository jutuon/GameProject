using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace GameProject
{
	public abstract class GameObject : IUpdateable
	{

		public float X { get; protected set;}
		public float Y { get; protected set;}
		public float Angle { get; protected set;}

		public event EventHandler ObjectMoved;

		public GameObject()
		{
			X = 0;
			Y = 0;
			Angle = 0;
		}

		public void Move(float x, float y)
		{
			X += x; Y += y;
			OnObjectMoved();
		}

		public void MoveForward(float ammount)
		{
			float x = (float) Math.Cos(Angle)*ammount;
			float y = (float) Math.Sin(Angle)*ammount;

			Move(x, -y);
		}

		public void Turn(float radians) { Angle += radians;}

		public void CloneState(GameObject gameobject)
		{
			X = gameobject.X;
			Y = gameobject.Y;
			Angle = gameobject.Angle;
		}

		public virtual void Update(GameTime time) {}

		private void OnObjectMoved()
		{
			EventHandler handler = ObjectMoved;
			if (handler != null) handler(this, null);
		}
	}
}

