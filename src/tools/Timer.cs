using System;
using Microsoft.Xna.Framework;

namespace GameProject
{
	public class Timer
	{
		public int WaitingTime { get; set;}
		private TimeSpan previousTime;

		public Timer()
		{
			previousTime = new TimeSpan();
		}

		public bool TimeElapsed(GameTime time)
		{
			if (time.TotalGameTime.Subtract(previousTime).Milliseconds >= WaitingTime)
			{
				previousTime = time.TotalGameTime;
				return true;
			}

			return false;
		}

	}
}

