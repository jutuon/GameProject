using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace GameProject
{
	//TODO: user interface, animations, "infinite" background, random asteroids, collision-detection. inputmanager, sounds and music

	/// <summary>
	/// This is the main type for your game.
	/// </summary>
	public class GameProject : Game
	{
		private CollisionEngine collisionEngine;

		private Camera camera;
		private Player player;
		private Background background;
		private DrawableBasicGameObjectContainer<Asteroid> asteroids = new DrawableBasicGameObjectContainer<Asteroid>();
		private DrawableBasicGameObjectContainer<Laser> lasers = new DrawableBasicGameObjectContainer<Laser>();

		private SpriteFont font;

		private InGameWindow window;
		private TextObject playerText;

		public static TextList debugTexts;

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
			camera = new Camera(Window);

			IsMouseVisible = true;
			base.Initialize();
		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent()
		{
			collisionEngine = new CollisionEngine();

			// Create a new SpriteBatch, which can be used to draw textures.
			spriteBatch = new SpriteBatch(GraphicsDevice);

			textureContainer = new TextureContainer(this);

			font = Content.Load<SpriteFont>("fonts/arial");

			int colum = -2;
			int row = -5;

			player = new Player(textureContainer[AvailibleTextures.SpaceShip], textureContainer[AvailibleTextures.Laser], collisionEngine);
			player.ForceMove(new Vector2(0, 25));

			for (int i = 0; i < 1; i++)
			{
				Asteroid asteroid = new Asteroid(textureContainer[AvailibleTextures.Asteroid], collisionEngine);
				asteroids.Add(asteroid);
				collisionEngine.CreateAndAddCollisionHandler(player, asteroid, CollisionType.Circle, true);
				asteroid.ForceMove(new Vector2(150, 50));
				colum++;
				if (colum > 2)
				{
					colum = -5; row++;
				}


			};


			background = new Background(textureContainer[AvailibleTextures.StarBackground], collisionEngine);

			window = new InGameWindow(Window);
			window.AlignmentX = WindowAlignmentX.Left;

			debugTexts = new TextList(font);
			window.Add(debugTexts);

			TextObject playerText = debugTexts.Add("text");
			TextObject cameraText = debugTexts.Add("text");

			player.ObjectMoved += delegate(object sender, EventArgs e)
			{
				Player p = (Player) sender;
				playerText.Text = "Player: " + p;
			};

			camera.ObjectMoved += delegate(object sender, EventArgs e)
			{
				BasicGameObject p = (BasicGameObject) sender;
				cameraText.Text = "Camera: " + p;
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

