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

			Vector2 screenPosition = (gameObject.Position - Position) * new Vector2(1,-1);
			//check if out of object out of camera area
			if (Math.Abs(screenPosition.X) > width + gameObject.Texture.Width || Math.Abs(screenPosition.Y) > heigth + gameObject.Texture.Height) return null;

			//move to center
			Vector2 screenCenter = new Vector2(Window.ClientBounds.Width/2, Window.ClientBounds.Height/2);

			return screenPosition + screenCenter;
		}

		public Vector2? ToScreenCoordinants(Vector2 position)
		{

			Vector2 screenPosition = (position - this.Position) * new Vector2(1,-1);
			if (Math.Abs(screenPosition.X) > width || Math.Abs(screenPosition.Y) > heigth) return null;
			//move to center
			Vector2 screenCenter = new Vector2(Window.ClientBounds.Width/2, Window.ClientBounds.Height/2);

			return screenPosition + screenCenter;
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
			Position = player.Position;
		}

	}
}

