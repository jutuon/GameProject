using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace GameProject
{
	/// <summary>
	/// Game object with basic features like position and angle
	/// </summary>
	public abstract class BasicGameObject : IUpdateable
	{

		public Vector2 Position { get; protected set;}
		public float Angle { get; protected set;}


		public event EventHandler ObjectMoved;

		public BasicGameObject()
		{
			Position = new Vector2(0, 0);
			Angle = 0;
		}

		public virtual void Move(Vector2 amount)
		{
			Position += amount;
			OnObjectMoved();
		}

		public void Move(float x, float y)
		{
			Move(new Vector2(x, y));
		}

		public void MoveForward(float amount)
		{
			float x = (float) Math.Cos(Angle)*amount;
			float y = (float) Math.Sin(Angle)*amount;

			Move(x, -y);
		}

		public void Turn(float radians) { Angle += radians;}

		public void CloneState(BasicGameObject gameobject)
		{
			Position = gameobject.Position;
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

