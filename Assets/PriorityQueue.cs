// A simple priority queue
// Contributed by Chris Madge
using System.Collections.Generic;

public struct CostedItem { //Mono has a bug that prevents nullable generic structs being used - Arrrghhhhh!
	public object item;
	public int cost;

	public CostedItem(object item, int cost) {
		this.item = item;
		this.cost = cost;
	}
}

public class PriorityQueue<T> {
	private List<CostedItem> items = new List<CostedItem>();

	public void add(T item, int cost) {
		items.Add (new CostedItem(item, cost));
	}

	public void Enqueue(T item, int cost) {
		add (item, cost);
	}

	public CostedItem? Dequeue() { //return costeditem or null '?'
		return poll ();
	}

	public int Count {
		get {
			return items.Count;
		}
	}

	public CostedItem? poll() {
		int minIdx = findMinIndex ();
		if (minIdx < 0) {
			return null;
		}
		CostedItem? item = items[minIdx];
		items.RemoveAt (minIdx);
		return item;
	}

	private int findMinIndex() {
		CostedItem? min = null;
		int idx = -1;
		for (int i = 0; i < items.Count; i++) {
			if (!min.HasValue) {
				idx = i;
				min = items [i];
			} else if (items[i].cost <= min.Value.cost) {
				idx = i;
				min = items [i];
			}
		}
		return idx;
	}
}
