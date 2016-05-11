﻿using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace GameProject
{
	//TODO: user interface, animations, "infinite" background, random asteroids, collision-detection. inputmanager, sounds and music
	//TODO: add generics to event delegates

	/// <summary>
	/// This is the main type for your game.
	/// </summary>
	public class GameProject : Game
	{
		private Camera camera;
		private Player player;
		private Background background;
		private DrawableGameObjectContainer<Asteroid> asteroids = new DrawableGameObjectContainer<Asteroid>();
		private DrawableGameObjectContainer<Laser> lasers = new DrawableGameObjectContainer<Laser>();

		private SpriteFont font;

		private InGameWindow window;
		private TextObject playerText;

		public static TextObject debugText;

		private TextureContainer textureContainer;

		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;

		public GameProject()
		{
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
		}

		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.  Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		/// </summary>
		protected override void Initialize()
		{
			// TODO: Add your initialization logic here
			camera = new Camera(Window.ClientBounds.Width,Window.ClientBounds.Height, Window);


			base.Initialize();
		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent()
		{

			// Create a new SpriteBatch, which can be used to draw textures.
			spriteBatch = new SpriteBatch(GraphicsDevice);

			textureContainer = new TextureContainer(this);

			font = Content.Load<SpriteFont>("fonts/arial");

			int colum = -2;
			int row = -5;

			for (int i = 0; i < 50; i++)
			{
				Asteroid asteroid = new Asteroid(textureContainer[AvailibleTextures.Asteroid]);
				asteroids.Add(asteroid);

				asteroid.Move(colum * 150, row * 150);
				colum++;
				if (colum > 2)
				{
					colum = -5; row++;
				}


			};

			player = new Player(textureContainer[AvailibleTextures.SpaceShip], textureContainer[AvailibleTextures.Laser]);
			background = new Background(textureContainer[AvailibleTextures.StarBackground]);

			window = new InGameWindow(Window);
			playerText = new TextObject(font);

			TextObject cameraText = new TextObject(font);
			cameraText.X += 150; window.Add(cameraText);

			debugText = new TextObject(font);
			debugText.X -= 50;
			window.Add(playerText); window.Add(debugText);
			window.PositionX = WindowPositionX.Center;

			player.ObjectMoved += delegate(object sender, EventArgs e)
			{
				Player p = (Player) sender;
				playerText.Text = "X: " + p.X + "\nY: " + p.Y;
			};

			camera.ObjectMoved += delegate(object sender, EventArgs e)
			{
				GameObject p = (GameObject) sender;
				cameraText.Text = "Camera: \nX: " + p.X + "\nY: " + p.Y;
			};

			camera.Follow(player);
		}
			
		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update(GameTime gameTime)
		{
			/*
			// For Mobile devices, this logic will close the Game when the Back button is pressed
			// Exit() is obsolete on iOS
			#if !__IOS__ &&  !__TVOS__
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape)) Exit();
			#endif
            */

			KeyboardState state = Keyboard.GetState();
			if (state.IsKeyDown(Keys.Escape)) Exit();

			if (state.IsKeyDown(Keys.Up)) camera.Move(0, 2);
			if (state.IsKeyDown(Keys.Down)) camera.Move(0, -2);
			if (state.IsKeyDown(Keys.Right)) camera.Move(2, 0);
			if (state.IsKeyDown(Keys.Left)) camera.Move(-2, 0);


			if (state.IsKeyDown(Keys.W)) player.MoveForward(2);
			if (state.IsKeyDown(Keys.S)) player.MoveForward(-2);
			if (state.IsKeyDown(Keys.D)) player.Turn(0.05f);
			if (state.IsKeyDown(Keys.A)) player.Turn(-0.05f);

			if (state.IsKeyDown(Keys.Space))
			{
				player.Shoot(lasers, gameTime);
			}


			lasers.Update(gameTime);

			base.Update(gameTime);
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime)
		{
			graphics.GraphicsDevice.Clear(Color.CornflowerBlue);
            
			//TODO: Add your drawing code here
            
			spriteBatch.Begin();

			background.Draw(spriteBatch, gameTime, camera);
			player.Draw(spriteBatch, gameTime, camera);

			asteroids.Draw(spriteBatch, gameTime, camera);
			lasers.Draw(spriteBatch, gameTime, camera);


			window.Draw(spriteBatch);

			spriteBatch.End();


			base.Draw(gameTime);
		}
	}
}
