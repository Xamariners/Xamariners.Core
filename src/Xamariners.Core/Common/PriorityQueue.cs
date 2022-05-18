using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xamariners.Common
{
	public class PriorityQueue<T> where T : class
	{
		private readonly Dictionary<QueuePriority, List<T>> _queues = new Dictionary<QueuePriority, List<T>>();
		private readonly object _lock = new object();

		public PriorityQueue()
		{
			_queues[QueuePriority.Low] = new List<T>();
			_queues[QueuePriority.High] = new List<T>();
		}

		public void Enqueue(T item, QueuePriority priority = QueuePriority.Low, bool placeAtTheTop = false)
		{
			lock (_lock)
			{
				if (placeAtTheTop)
					_queues[priority].Insert(0, item);
				else
					_queues[priority].Add(item);
			}
		}

		public T Dequeue()
		{
			lock (_lock)
			{
				return DequeueItem(QueuePriority.High) ?? DequeueItem(QueuePriority.Low);
			}
		}

		private T DequeueItem(QueuePriority priority)
		{
			var item = _queues[priority].FirstOrDefault();

			if (item != null)
				_queues[priority].Remove(item);

			return item;
		}

		public void Remove(T item)
		{
			lock (_lock)
			{
				_queues[QueuePriority.High].Remove(item);
				_queues[QueuePriority.Low].Remove(item);
			}
		}

		public bool TryPeek(Func<T, bool> predicate, out T item)
		{
			lock (_lock)
			{
				if(_queues == null || !_queues.Any())
				{
						item = null;
						return false;
				}
					
                item =  _queues[QueuePriority.High].FirstOrDefault(predicate) ?? _queues[QueuePriority.Low].FirstOrDefault(predicate);
                return item != null;
            }
        }
    }
}