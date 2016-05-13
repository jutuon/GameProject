using System;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace GameProject
{

	public enum CollisionType 
	{
		Rectangle, Circle
	}

	public class CollisionSetting 
	{
		public bool Deleted { get; set;}
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
			


		public bool CheckCollision(GameObject toBeMoved, Vector2 newPosition, List<CollisionSetting> list)
		{

			foreach (var collisionSetting in list)
			{
				GameObject target = collisionSetting.ReturnTheOther(toBeMoved);


				float distance = Vector2.Distance(toBeMoved.Position+newPosition, target.Position);
				if (distance < 25) return true;

			}

			return false;

		}

		public void CheckRectangleCollision()
		{
			
		}

		public void CreateAndAddCollisionHandler(GameObject gameObject, GameObject gameObject2, CollisionType collisionType, bool checkBoth = false)
		{
			CollisionSetting setting = new CollisionSetting(gameObject, gameObject2, collisionType, checkBoth);
			gameObject.AddCollisionHandler(setting);
			if (checkBoth) gameObject2.AddCollisionHandler(setting);
		}
	}
}

