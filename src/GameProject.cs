using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace GameProject
{
	//TODO: user interface, animations, collision detection, sounds and music

	/// <summary>
	/// This is the main type for your game.
	/// </summary>
	public class GameProject : Game
	{
		private GameWorld gameWorld;
		private InputManager inputManager;

		private TextureContainer textureContainer;
		private SpriteFont font;

		private InGameWindow window;
		public static TextList debugTexts;

		private GraphicsDeviceManager graphics;
		private SpriteBatch spriteBatch;

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
			gameWorld = new GameWorld(Window);
			inputManager = new InputManager(this);

			IsMouseVisible = true;
			base.Initialize();
		}
		public TextObject text;
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

			gameWorld.LoadLevel(textureContainer);
			gameWorld.SetUserInput(inputManager);


			window = new InGameWindow(Window);
			window.AlignmentX = ComponentAlignmentX.Left;
			window.AlignmentY = ComponentAlignmentY.Top;

			debugTexts = window.CreateAndAddTextList(font);
			debugTexts.PreferredLineWidth = 0;

			TextObject playerText = debugTexts.Add("text");
			TextObject cameraText = debugTexts.Add("text");
			//cameraText.PositionOffset = new Vector2(100, 10);
			debugTexts.Add("text");
			text = debugTexts.Add("text");


			gameWorld.GetPlayer(0).ObjectMoved += delegate(object sender, EventArgs e)
			{
				Player p = (Player) sender;
				playerText.Text = "Player: " + p;
			};

			gameWorld.GetCamera(0).ObjectMoved += delegate(object sender, EventArgs e)
			{
				BasicGameObject p = (BasicGameObject) sender;
				cameraText.Text = "Camera: " + p;
			};


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
			text.Text = gameWorld.ObjectCount() +"";

			inputManager.Update(gameTime);
			gameWorld.Update(gameTime);

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

			gameWorld.Draw(spriteBatch, gameTime);
			window.Draw(spriteBatch);

			spriteBatch.End();
			base.Draw(gameTime);
		}
	}
}

