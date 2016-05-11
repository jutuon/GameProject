using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace GameProject
{
	public class Laser : DrawableGameObject
	{ 
		private float lifeArea;
		private String tag;

		public event EventHandler NotInAllowedArea;

		public Laser(Texture2D texture, GameObject parent) : base(texture)
		{
			CloneState(parent);
			lifeArea = 500;
		}
			

		public override void Update(GameTime time)
		{
			MoveForward(5);

			if (Position.X < -lifeArea || Position.X > lifeArea || Position.Y < -lifeArea || Position.Y > lifeArea) OnNotInAllowedArea();
		}

		private void OnNotInAllowedArea()
		{
			EventHandler handler = NotInAllowedArea;

			if (handler != null) handler(this, null);
		}
	}
}

