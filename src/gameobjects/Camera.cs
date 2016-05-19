using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace GameProject
{

	public class ScreenCoordinateInfo
	{
		public Vector2 ScreenCoords { get; private set;}
		public bool OnScreen { get; private set;}
		public DrawableBasicGameObject GameObject { get; private set;}
		public Camera Camera { get; private set;}

		public ScreenCoordinateInfo(DrawableBasicGameObject gameObject, Camera camera)
		{
			this.GameObject = gameObject;
			this.ScreenCoords = Vector2.Zero;
			this.OnScreen = false;
			this.Camera = camera;

			ReCalculate();
			gameObject.ObjectMoved += UpdateObjectScreenCoordinates;
			gameObject.OnDestroy += (object sender, EventArgs e) => camera.RemoveFromCamera(this);

		}

		private void UpdateObjectScreenCoordinates(object o, EventArgs ea)
		{
			ReCalculate();
		}

		public void ReCalculate()
		{
			ScreenCoords = Camera.ToScreenCoordinants(GameObject);
			OnScreen = Camera.AreScreenCoordinantsOnScreen(ScreenCoords, GameObject);
		}

	}

	/// <summary>
	/// Camera, converts world coordinates to screen coordinates
	/// </summary>
	public class Camera : BasicGameObject, IDrawable
	{
		public GameWindow Window { get; private set;}
		private BasicGameObject currentlyFollowing;

		private List<ScreenCoordinateInfo> coordinates;


		/// <summary>
		/// Initializes a new instance of the <see cref="GameProject.Camera"/> class.
		/// </summary>
		/// <param name="window">Game window where camera is used</param>
		public Camera(GameWindow window)
		{
			Window = window;
			coordinates = new List<ScreenCoordinateInfo>();
			ObjectMoved += (sender, e) => UpdateAllScreenCoordinates();
		}


		/// <summary>
		/// Convert world coordinates to screen coordinates
		/// </summary>
		/// <returns>The screen coordinants.</returns>
		/// <param name="gameObject">Game object</param>
		public Vector2 ToScreenCoordinants(DrawableBasicGameObject gameObject)
		{
    		return ToScreenCoordinants(gameObject.Position);
		}

		/// <summary>
		/// Convert world coordinates to screen coordinates
		/// </summary>
		/// <returns>The screen coordinants.</returns>
		/// <param name="position">World coordinates</param>
		public Vector2 ToScreenCoordinants(Vector2 position)
		{
			//calculate screen coordinates
			Vector2 screenPosition = (position - Position) * new Vector2(1,-1);

			//move object to center of the screen
			Vector2 screenSize = new Vector2(Window.ClientBounds.Width, Window.ClientBounds.Height);
			Vector2 final = screenPosition + screenSize/2;

			return final;
		}

		/// <summary>
		/// Are the screen coordinants on screen area.
		/// </summary>
		/// <returns><c>true</c>, if screen coordinants on screen was ared, <c>false</c> otherwise.</returns>
		/// <param name="screenCoordinants">Screen coordinants.</param>
		/// <param name="gameObject">Game object,</param>
		public bool AreScreenCoordinantsOnScreen(Vector2 screenCoordinants, DrawableBasicGameObject gameObject = null)
		{

			Vector2 finalAndTextureLeft = screenCoordinants;
			Vector2 finalAndTextureRight = screenCoordinants;

			if (gameObject != null)
			{
				Vector2 textureSize = new Vector2(gameObject.Texture.Width/2, gameObject.Texture.Height/2);
				finalAndTextureLeft += textureSize;
				finalAndTextureRight -= textureSize;
			}

			if (finalAndTextureLeft.X < 0|| finalAndTextureRight.X > Window.ClientBounds.Width  || 
				finalAndTextureLeft.Y < 0|| finalAndTextureRight.Y > Window.ClientBounds.Height ) return false;

			return true;
		}



		/// <summary>
		/// Follow the specified gameobject.
		/// </summary>
		/// <param name="gameobject">Gameobject</param>
		public void Follow(BasicGameObject gameobject)
		{

			//TODO: what if followed object is removed?
			if (currentlyFollowing != null) gameobject.ObjectMoved -= CopyPosition;

			gameobject.ObjectMoved += CopyPosition;
			currentlyFollowing = gameobject;
		}

		/// <summary>
		/// Method for ObjectMoved event
		/// </summary>
		/// <param name="sender">Sender</param>
		/// <param name="e">E</param>
		private void CopyPosition(object sender, EventArgs e)
		{
			BasicGameObject gameobject = (BasicGameObject) sender;
			Position = gameobject.Position;
		}


		public DrawableBasicGameObject AddToCamera(DrawableBasicGameObject gameObject)
		{
			coordinates.Add(new ScreenCoordinateInfo(gameObject, this));
			return gameObject;
		}
			

		private void UpdateAllScreenCoordinates()
		{
			foreach (var item in coordinates) item.ReCalculate();
		}

		public void RemoveFromCamera(ScreenCoordinateInfo coordinateInfo)
		{
			coordinates.Remove(coordinateInfo);
		}


		public void Draw(SpriteBatch spriteBatch, GameTime time)
		{
			foreach (var item in coordinates) item.GameObject.Draw(spriteBatch, item);
		}
	}
}

