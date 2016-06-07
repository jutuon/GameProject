﻿using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace GameProject
{
	public class GameWorld : IUpdateable
	{
		private CollisionEngine collisionEngine;

		private GameObjectContainer<GameObject> gameobjects;
		private GameObjectContainer<Camera> cameras;
		private GameObjectContainer<Player> players;

		private Background background;
		public Background Background { 
			get
			{
				return background;
			}
			set
			{ 
				background = value;
				AddToCameras(value);
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="GameProject.GameWorld"/> class.
		/// Creates one camera by default
		/// </summary>
		/// <param name="window">Window.</param>
		public GameWorld(GameWindow window)
		{
			gameobjects = new GameObjectContainer<GameObject>();
			cameras = new GameObjectContainer<Camera>();
			players = new GameObjectContainer<Player>();

			collisionEngine = new CollisionEngine();

			AddCamera(new Camera(window));
		}

		public void AddGameObject(GameObject gameobject)
		{
			gameobjects.Add(gameobject);
			AddToCameras(gameobject);
		}

		public void AddCamera(Camera camera)
		{
			camera.AddAllToCamera<GameObject>(gameobjects);
			camera.AddAllToCamera<Player>(players);
			cameras.Add(camera);
		}

		public void AddPlayer(Player player)
		{
			players.Add(player);
			AddToCameras(player);
		}

		private void AddToCameras(DrawableBasicGameObject gameobject)
		{
			foreach (var item in cameras)
			{
				item.AddToCamera(gameobject);
			}
		}

		public void Draw(SpriteBatch spriteBatch, GameTime time)
		{
			foreach (var item in cameras)
			{
				item.Draw(spriteBatch, time);
			}
		}


		public void LoadLevel(TextureContainer textures)
		{
			Background = new Background(textures[AvailibleTextures.StarBackground]);

			int colum = -2;
			int row = -5;

			Player player = new Player(textures[AvailibleTextures.SpaceShip], textures[AvailibleTextures.Laser], collisionEngine, this);
			player.ForceMove(new Vector2(0, 25));
			cameras[0].Follow(player);
			AddPlayer(player);

			for (int i = 0; i < 1; i++)
			{
				Asteroid asteroid = new Asteroid(textures[AvailibleTextures.Asteroid], collisionEngine);
				AddGameObject(asteroid);

				collisionEngine.CreateAndAddCollisionHandler(player, asteroid, CollisionType.Circle, true);
				asteroid.ForceMove(new Vector2(150, 50));
				colum++;
				if (colum > 2)
				{
					colum = -5; row++;
				}
			}

		}

		public Player GetPlayer(int i)
		{
			return players[i];
		}

		public Camera GetCamera(int i)
		{
			return cameras[i];
		}

		public void Update(GameTime time)
		{
			gameobjects.Update(time);
			players.Update(time);
			cameras.Update(time);
		}

		public void SetUserInput(InputManager input)
		{
			input.SetCameraControls(cameras[0]);
			input.SetPlayerControls(players[0]);
		}


		public int ObjectCount()
		{
			return gameobjects.Count + players.Count + cameras.Count;
		}
	}
}

