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
		public float Width { get; protected set;}
		public float Heigth { get; protected set;}

		private List<CollisionSetting> collisions; //TODO: change collision setting saving to better data structure than list?
		protected CollisionEngine collisionEngine;

		public GameObject(Texture2D texture, CollisionEngine engine) : base(texture)
		{
			collisionEngine = engine;
			collisions = new List<CollisionSetting>();
		}

		public override void Move(float x, float y)
		{
			Vector2 newPosition = new Vector2(x, y) + Position;

			if(collisionEngine.CheckCollision(this, newPosition, collisions)) return;

			Position = newPosition;
		}

		public void ForceMove(Vector2 amount)
		{
			Position = Position + amount;
		}

		public void AddCollisionHandler(CollisionSetting setting) {
			collisions.Add(setting);
		}
	}
}

