using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject
{
	public enum AvailibleTextures 
	{
		Box, SpaceShip, Asteroid, Laser, StarBackground
	}

	public class TextureContainer : ArrayContainer<Texture2D>
	{
		public TextureContainer(Game game) : base(Enum.GetNames(typeof(AvailibleTextures)).Length)
		{
			this[AvailibleTextures.Box] = game.Content.Load<Texture2D>("sprites/box");
			this[AvailibleTextures.SpaceShip] = game.Content.Load<Texture2D>("sprites/spaceship");

			this[AvailibleTextures.Asteroid] = game.Content.Load<Texture2D>("sprites/asteroid");
			this[AvailibleTextures.Laser] = game.Content.Load<Texture2D>("sprites/laser");
			this[AvailibleTextures.StarBackground] = game.Content.Load<Texture2D>("sprites/stars");
		}

		public Texture2D this[AvailibleTextures texture]
		{
			get 
			{
				return base[(int) texture];
			}
			private set 
			{
				base[(int) texture] = value;
			}
		}
	}
}

