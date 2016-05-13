using System;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace GameProject
{

	public enum CollisionType 
	{
		Rectangle, Circle, Eclipce
	}

	public class CollisionSetting 
	{
		public bool Deleted { get; set;} //TODO:implement removing collisions
		public GameObject Target1 { get; private set;}
		public GameObject Target2 { get; private set;}
		public CollisionType CollisionType { get; set;}
		public bool CheckBoth { get; set;}

		public CollisionSetting(GameObject target1, GameObject target2, CollisionType collisionType, bool checkBoth)
		{
			this.Target1 = target1;
			this.Target2 = target2;
			this.CollisionType = collisionType;
			this.CheckBoth = checkBoth;
		}

		public GameObject ReturnTheOther(GameObject gameobject)
		{
			if (Target1 == gameobject) return Target2;
			return Target1;
		}
		
	}

	public class CollisionEngine : IUpdateable
	{

		public CollisionEngine()
		{
			
		}


		public void Update(GameTime time)
		{
			
		}
			


		/// <summary>
		/// Checks the collision.
		/// </summary>
		/// <returns><c>true</c>, if collision found, <c>false</c> otherwise.</returns>
		/// <param name="toBeMoved">GameObject that will be moved.</param>
		/// <param name="newPosition">The new position of GameObject after moving</param>
		/// <param name="list">List of GameObject's CollisionSettings</param>
		public bool CheckCollision(GameObject toBeMoved, Vector2 newPosition, List<CollisionSetting> list)
		{

			foreach (var collisionSetting in list)
			{
				GameObject target = collisionSetting.ReturnTheOther(toBeMoved);

				bool result = false;

				if (collisionSetting.CollisionType == CollisionType.Circle) 
					result = CheckCircleCollision(toBeMoved, newPosition, target);
				//else if (collisionSetting.CollisionType == CollisionType.Rectangle) ;
				//else if (collisionSetting.CollisionType == CollisionType.Eclipce) ;
					
				
				if (result) return true;

			}

			return false;

		}

		public bool CheckCircleCollision(GameObject toBeMoved, Vector2 newPosition, GameObject target)
		{
			float offset = Math.Max(toBeMoved.Width, toBeMoved.Heigth);
			float targetoffset = Math.Max(target.Width, target.Heigth);

			/*
			float moveAngle = (float)Math.Acos(toBeMoved.Position.X - newPosition.X);

			

			float totalOffsetX = (float) Math.Cos(moveAngle) * (offset + targetoffset);
			float totalOffsetY = (float) Math.Sin(moveAngle) * (offset + targetoffset);

			newPosition = newPosition + new Vector2(totalOffsetX, -totalOffsetY);
			*/

			//Console.WriteLine(offset + targetoffset);

			float distance = Vector2.Distance(newPosition, target.Position);
			if (distance < (offset + targetoffset) / 2) return true;

			return false;
		}

		public void CreateAndAddCollisionHandler(GameObject gameObject, GameObject gameObject2, CollisionType collisionType, bool checkBoth = false)
		{
			CollisionSetting setting = new CollisionSetting(gameObject, gameObject2, collisionType, checkBoth);
			gameObject.AddCollisionHandler(setting);
			if (checkBoth) gameObject2.AddCollisionHandler(setting);
		}
	}
}

