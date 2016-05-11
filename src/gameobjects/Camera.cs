using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject
{
	public class Camera : GameObject
	{
		private int width, heigth;
		public GameWindow Window { get; private set;}

		private GameObject currentlyFollowing;

		public Camera(int width, int heigth, GameWindow window)
		{
			this.width = width; this.heigth = heigth;
			Window = window;
		}


		public Vector2? ToScreenCoordinants(DrawableGameObject gameObject)
		{
			float xDistance = 0, yDistance = 0;

			xDistance = gameObject.X - X;
			yDistance = -(gameObject.Y - Y);
			if (Math.Abs(xDistance) > width + gameObject.Texture.Width || Math.Abs(yDistance) > heigth + gameObject.Texture.Height) return null;

			int screenCenterX = Window.ClientBounds.Width/2;
			int screenCenterY = Window.ClientBounds.Height/2;

			return new Vector2(xDistance + screenCenterX, yDistance + screenCenterY);
		}

		public Vector2? ToScreenCoordinants(float x, float y)
		{
			float xDistance = 0, yDistance = 0;

			xDistance = x - X;
			yDistance = y - Y;
			if (Math.Abs(xDistance) > width || Math.Abs(yDistance) > heigth) return null;

			int screenCenterX = Window.ClientBounds.Width/2;
			int screenCenterY = Window.ClientBounds.Height/2;

			return new Vector2(xDistance + screenCenterX, yDistance + screenCenterY);
		}

		public void Follow(GameObject gameobject)
		{

			//TODO: what if followed object is removed?
			if (currentlyFollowing != null) gameobject.ObjectMoved -= CopyPosition;

			gameobject.ObjectMoved += CopyPosition;
			currentlyFollowing = gameobject;
		}


		private void CopyPosition(object sender, EventArgs e)
		{
			GameObject player = (GameObject) sender;
			X = player.X; Y = player.Y;
		}

	}
}

