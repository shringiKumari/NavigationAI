using UnityEngine;
using System.Collections.Generic;
using Node = System.Int32;


public abstract class GraphSearch {

	protected IntGraph graph;
	protected bool trace;
	protected const int NONE = -1;
	
	public void setGraph(IntGraph g) {
		graph = g;
	}

	public abstract List<Node> findPath (Node a, Node b, bool trace);

	protected void log(string s) {
		if (trace)
			Debug.Log (s);
	}
}


public class BFS : GraphSearch {

	protected Queue<Node> frontier;
	protected Dictionary<Node, Node> came_from;

	
	public override List<Node> findPath(Node start, Node goal, bool t) {
		trace = t;

		log ("BFS: looking for path from " + start + " to " + goal);

		frontier = new Queue<Node>();
		frontier.Enqueue(start);
		came_from = new Dictionary<Node, Node>();
		came_from[start] = NONE;

		bool done = false;

		// Main search loop
		while (frontier.Count != 0) {
			Node current = frontier.Dequeue ();
			log ("Current node is " + current);

			if (current == goal) {
				done = true;
				log ("Found goal node " + goal);
				break;
			}

			List<Node> neighbours = graph.neighbours (current);
			log (neighbours.Count + " neighbours found");

			foreach (Node next in neighbours) {
				if (!came_from.ContainsKey (next)) {
					log ("Adding to " + next + " to frontier");
					frontier.Enqueue (next);
					came_from [next] = current;

				} else {
					log ("Already visited " + next); 
				}
			}
		}

		// Reconstruct the path
		List<Node> path = null;
		if (done) {
			path = new List<Node>();

			Node current = goal;

			while (current != start) {
				path.Add(current);
				current = came_from[current];
				if (path.Contains(current)) {
					log ("Error: path contains a loop");
					return null;
				}
			}
			path.Add (start);
			path.Reverse();
		}

		return path;
	}
}

