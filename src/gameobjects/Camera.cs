﻿using System;
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

		private bool update;
		private bool destroyIfNotOnCamera;

		public ScreenCoordinateInfo(DrawableBasicGameObject gameObject, Camera camera, bool destroyIfNotOnCamera)
		{
			this.GameObject = gameObject;
			this.ScreenCoords = Vector2.Zero;
			this.OnScreen = false;
			this.Camera = camera;
			this.destroyIfNotOnCamera = destroyIfNotOnCamera;

			update = true;
			gameObject.ObjectMoved += (object sender, EventArgs e) => update = true;
			gameObject.OnDestroy += (object sender, EventArgs e) => camera.RemoveFromCamera(this);

		}

		public void UpdateCoordinatesIfMoved()
		{
			if (update)
			{
				ReCalculate();
				update = false;
			}
		}

		public void ReCalculate()
		{
			//TODO: recalculate coordinates only if object is close the camera
			OnScreen = IsObjectOnScreen();
			if (OnScreen) ScreenCoords = ToScreenCoordinants();
			else if (destroyIfNotOnCamera) GameObject.DestroyAtUpdate = true;
			update = false;
		}


		/// <summary>
		/// Convert world coordinates to screen coordinates
		/// </summary>
		/// <returns>The screen coordinants.</returns>
		public Vector2 ToScreenCoordinants()
		{
			return ToScreenCoordinants(GameObject.Position);
		}

		public Vector2 ToScreenCoordinants(Vector2 worldCoordinates)
		{
			//calculate screen coordinates
			Vector2 screenPosition = (worldCoordinates - Camera.Position) * new Vector2(1,-1);

			//move object to center of the screen
			Vector2 screenSize = new Vector2(Camera.WindowWidth, Camera.WindowHeight);
			Vector2 final = screenPosition + screenSize/2;

			return final;
		}

		/// <summary>
		/// Is the gameobject on the screen.
		/// </summary>
		/// <returns><c>true</c>, if gameobject is on screen, <c>false</c> otherwise.</returns>
		public bool IsObjectOnScreen()
		{
			float distanceX = (-GameObject.Texture.Width) + Math.Abs(GameObject.Position.X - Camera.Position.X);
			float distanceY = (-GameObject.Texture.Height) + Math.Abs(GameObject.Position.Y - Camera.Position.Y);
		
			if (distanceX <= Camera.WindowWidth/2  && distanceY <= Camera.WindowHeight/2) return true;

			return false;
		}
	}

	/// <summary>
	/// Camera, converts world coordinates to screen coordinates
	/// </summary>
	public class Camera : BasicGameObject, IDrawable
	{
		public int WindowWidth { get; private set;}
		public int WindowHeight { get; private set;}
		private BasicGameObject currentlyFollowing;

		private List<ScreenCoordinateInfo> coordinates;

		private bool updateAll = false;

		/// <summary>
		/// Initializes a new instance of the <see cref="GameProject.Camera"/> class.
		/// </summary>
		/// <param name="window">Game window where camera is used</param>
		public Camera(GameWindow window)
		{
			window.ClientSizeChanged += delegate
			{
				WindowHeight = window.ClientBounds.Height;
				WindowWidth = window.ClientBounds.Width;
			};
			coordinates = new List<ScreenCoordinateInfo>();
			ObjectMoved += (sender, e) => updateAll = true;
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


		public DrawableBasicGameObject Add(DrawableBasicGameObject gameObject)
		{
			return Add(gameObject, false);
		}

		public DrawableBasicGameObject Add(DrawableBasicGameObject gameObject, bool destroyIfNotOnCamera)
		{
			coordinates.Add(new ScreenCoordinateInfo(gameObject, this, destroyIfNotOnCamera));
			return gameObject;
		}

		public void AddAllToCamera<T>(GameObjectContainer<T> container) where T : DrawableBasicGameObject
		{
			foreach (var item in container)
			{
				Add(item);
			}
		}

		public void RemoveFromCamera(ScreenCoordinateInfo coordinateInfo)
		{
			coordinates.Remove(coordinateInfo);
		}


		public void Draw(SpriteBatch spriteBatch, GameTime time)
		{
			foreach (var item in coordinates)
			{
				item.GameObject.Draw(spriteBatch, item);
			}
		}

		public override void Update(GameTime time)
		{
			if (updateAll){
				foreach (var item in coordinates) item.ReCalculate();
				updateAll = false;
			}
			else
			{
				foreach (var item in coordinates) item.UpdateCoordinatesIfMoved();
			}
		}
	}
}