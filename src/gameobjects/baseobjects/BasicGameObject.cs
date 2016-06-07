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

		private Vector2 position;
		public Vector2 Position
		{
			get
			{ 
				return position;
			}
			set
			{
				position = value;
				OnObjectMoved();
			}
		}



		public event EventHandler ObjectMoved;
		public event EventHandler OnDestroy;

		public BasicGameObject()
		{
			Position = new Vector2(0, 0);
		}

		protected virtual void Move(Vector2 amount)
		{
			Position += amount;
			OnObjectMoved();
		}

		protected void Move(float x, float y)
		{
			Move(new Vector2(x, y));
		}
			
		public virtual void Update(GameTime time) {}

		private void OnObjectMoved()
		{
			EventHandler handler = ObjectMoved;
			if (handler != null) handler(this, null);
		}

		public void Destroy()
		{
			EventHandler handler = OnDestroy;
			if (handler != null) handler(this, null);
		}

		public virtual void MoveUp()
		{
			Move(0, 2);
		}

		public virtual void MoveDown()
		{
			Move(0, -2);
		}

		public virtual void MoveRight()
		{
			Move(2, 0);
		}

		public virtual void MoveLeft()
		{
			Move(-2, 0);
		}





		public override string ToString()
		{
			return string.Format("Position={0}", Position);
		}
	}
}

