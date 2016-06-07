using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject
{
	/// <summary>
	/// Game object with collision handling
	/// </summary>
	public abstract class GameObject : DrawableBasicGameObject
	{
		private float angle;
		public float Angle 
		{ 
			get
			{
				return angle;
			} 
			set
			{
				DrawingAngle = value;
				angle = value;
			}
		}


		public float Width { get; protected set;}
		public float Heigth { get; protected set;}

		private List<CollisionSetting> collisions; //TODO: change collision setting saving to better data structure than list?
		protected CollisionEngine collisionEngine;

		public GameObject(Texture2D texture, CollisionEngine engine) : base(texture)
		{
			Angle = 0;
			Width = texture.Width;
			Heigth = texture.Height;
			collisionEngine = engine;
			collisions = new List<CollisionSetting>();
		}

		/// <summary>
		/// Move the specified amount towards current direction
		/// </summary>
		/// <param name="amount">Amount.</param>
		protected void Move(float amount)
		{
			float x = (float) Math.Cos(Angle)*amount;
			float y = (float) Math.Sin(Angle)*amount;

			Move(new Vector2(x, -y));
		}

		public void Turn(float radians) { Angle += radians;}


		public virtual void TurnLeft()
		{
			Turn(-0.05f);
		}

		public virtual void TurnRight()
		{
			Turn(0.05f);
		}

		public virtual void MoveBackward()
		{
			Move(-2);
		}

		public virtual void MoveForward()
		{
			Move(2);
		}

		public void CloneState(GameObject gameobject)
		{
			Position = gameobject.Position;
			Angle = gameobject.Angle;
		}

		protected override void Move(Vector2 amount)
		{
			Vector2 newPosition = amount + Position;

			if(collisionEngine.CheckCollision(this, newPosition, collisions)) return;

			base.Move(amount);
		}

		public void ForceMove(Vector2 amount)
		{
			Position = Position + amount;
		}

		public void AddCollisionHandler(CollisionSetting setting) {
			collisions.Add(setting);
		}

		public override string ToString()
		{
			return string.Format("Width={0}, Heigth={1}, Angle={2}", Width, Heigth, Angle) + base.ToString();
		}
		
	}
}

