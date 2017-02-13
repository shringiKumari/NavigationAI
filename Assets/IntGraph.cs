using UnityEngine;
using System.Collections.Generic;

using Node = System.Int32;
using Cost = System.Int32;
using AdjList = System.Collections.Generic.Dictionary <int, int>;

public interface IntGraph {
	bool addNode(Node a);                 // true if node added
	bool addEdge(Node a, Node b, Cost c);  // true if edge added
	List<Node> nodes();
	List<Node> neighbours(Node a);
	Cost cost(Node a, Node b);    // -1 if no edge
}


class AdjListGraph : IntGraph {
	
	protected Dictionary<Node, AdjList> adjLists;
	
	public AdjListGraph() {
		adjLists = new Dictionary<Node, AdjList> ();
	}

	public bool hasNode(Node a) {
		return adjLists.ContainsKey(a);
	}

	// Add a new node
	public bool addNode(Node a) {
		
		if (a > -1 && !hasNode(a)) {
			// Add the node with empty edge dictionary
			adjLists[a] = new AdjList();
			return true;
		} else {
			// Node already exists or negative index
			return false;
		}
	}
	
	// Add the edge a->b
	public bool addEdge(Node a, Node b, Cost c) {

		// Check nodes exist but edge doesn't
		if (hasNode (a) && hasNode (b) && !adjLists [a].ContainsKey (b)) {
			// Add new edge
			adjLists [a] [b] = c;
			return true;
				
		} else {
			return false;
		}
	}
	
	// List all the nodes
	public List<Node> nodes() {
		return new List<Node>(adjLists.Keys);
	}
	
	// List the neighbour nodes for a given node
	public List<Node> neighbours(Node a) {
		return new List<Node>(adjLists[a].Keys);
	}
	
	// The cost for edge a->b
	public Cost cost(Node a, Node b) {
		return adjLists[a][b];
	}
}