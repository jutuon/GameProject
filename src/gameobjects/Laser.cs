using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace GameProject
{
	public class Laser : GameObject
	{ 

		public Laser(Texture2D texture, GameObject parent, CollisionEngine engine, float startingPosition) : base(texture, engine)
		{
			CloneState(parent);
			Move(startingPosition);
		}
			

		public override void Update(GameTime time)
		{
			Move(5);

			base.Update(time);
		}


	}
}

