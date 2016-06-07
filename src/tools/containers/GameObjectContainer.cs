using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections;

namespace GameProject
{
	public class GameObjectContainer<T> : IUpdateable, IEnumerable<T> where T : BasicGameObject
	{

		protected List<T> list;
		private List<T> toBeRemoved;

		public GameObjectContainer()
		{
			list = new List<T>();
			toBeRemoved = new List<T>();
		}

		public void Add(T content)
		{
			list.Add(content);
		}

		public void AddToBeRemoved(T content)
		{
			toBeRemoved.Add(content);
		}

		public void Update(GameTime time)
		{
			foreach (T item in list)
			{
				item.Update(time);
			}

			if (toBeRemoved.Count == 0) return;

			foreach (T item in toBeRemoved)
			{
				list.Remove(item);
			}

			toBeRemoved.Clear();
		}

		public IEnumerator<T> GetEnumerator()
		{
			return list.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return list.GetEnumerator();
		}


		public T this[int i]
		{
			get 
			{
				return list[i];
			}
		}

	}
}

