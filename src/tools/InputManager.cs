using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GameProject
{
	public class InputManager : IUpdateable
	{
		private Player player;
		private Camera camera;
		private Game game;

		public InputManager(Game game)
		{
			this.game = game;
		}

		public void SetCameraControls(Camera camera)
		{
			this.camera = camera;
		}

		public void SetPlayerControls(Player player)
		{
			this.player = player;
		}


		public void Update(GameTime time)
		{
			KeyboardState state = Keyboard.GetState();
			if (state.IsKeyDown(Keys.Escape)) game.Exit();

			if (state.IsKeyDown(Keys.Up)) camera.MoveUp();
			if (state.IsKeyDown(Keys.Down)) camera.MoveDown();
			if (state.IsKeyDown(Keys.Right)) camera.MoveRight();
			if (state.IsKeyDown(Keys.Left)) camera.MoveLeft();


			if (state.IsKeyDown(Keys.W)) player.MoveForward();
			if (state.IsKeyDown(Keys.S)) player.MoveBackward();
			if (state.IsKeyDown(Keys.D)) player.TurnRight();
			if (state.IsKeyDown(Keys.A)) player.TurnLeft();

			if (state.IsKeyDown(Keys.Space))
			{
				player.Shoot(time);
			}


		}
	}
}

