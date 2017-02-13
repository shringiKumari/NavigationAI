using UnityEngine;
using System;
using System.Linq;
using System.Collections.Generic;

public class SearchExample : MonoBehaviour {

	public int nodes  = 9;
	public int source = 0;
	public int target = 6;

	protected IntGraph graph;
	
	// Use this for initialization
	void Start () {
		graph = new AdjListGraph ();

		buildExample ();

		//BFS algorithm = new BFS ();

          //Dijkstra algorithm = new Dijkstra ();

          GreedyBSF algorithm = new GreedyBSF ();

          //AStar algorithm = new AStar ();
          algorithm.setGraph (graph);

		//List<int> path = bfs.findPath (source, target, true);
          List<int> path = algorithm.findPath (source, target, true);

		if (path != null) {
			String p = "";
			for(int i = 0; i < path.Count; i++) {
				if (i > 0) p += ", ";
				p += path[i];
			}
			Debug.Log ("Path found: " + p);
		} else {
			Debug.Log ("No path found");
		}
	}

	
	// Update is called once per frame
	void Update () {
		
	}

	// Build the example from the lecture slides
	public void buildExample() {

		// Add the nodes
		foreach (int n in Enumerable.Range(0, nodes)) {
			graph.addNode ((int) n);
		}

		// Add edges
		addUnitCostEdges (0, 1);
		addUnitCostEdges (0, 2);
		addUnitCostEdges (0, 4);

		addUnitCostEdges (1, 5);
		addUnitCostEdges (2, 3);
		addUnitCostEdges (2, 6);

		addUnitCostEdges (3, 5);
		addUnitCostEdges (3, 6);
		addUnitCostEdges (4, 5);
		addUnitCostEdges (4, 7);

		addUnitCostEdges (5, 8);
		addUnitCostEdges (6, 8);
		addUnitCostEdges (7, 8);
	}

	// Add undirected edge with unit cost
	private void addUnitCostEdges(int a, int b) {
		graph.addEdge (a, b, 1);
		graph.addEdge (b, a, 1);
	}

}



