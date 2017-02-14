using UnityEngine;
using System;
using System.Linq;
using System.Collections.Generic;

public class SearchExample : MonoBehaviour {

	public int nodes  = 9;
	public int source = 0;
	public int target = 6;
     public List<GameObject> waypointList = new List<GameObject> ();

	protected IntGraph graph;
	
	// Use this for initialization
	void Start () {
		graph = new AdjListGraph ();

		buildExample ();

		//BFS algorithm = new BFS ();

          //Dijkstra algorithm = new Dijkstra ();

          //GreedyBSF algorithm = new GreedyBSF ();

          AStar algorithm = new AStar ();
          algorithm.setGraph (graph);

          List<Vector3> tempWaypointPositions = new List<Vector3>();

          foreach (GameObject g in waypointList) {
               tempWaypointPositions.Add (g.transform.position);
          }
          algorithm.setPositions (tempWaypointPositions);

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
          foreach (int n in Enumerable.Range(0, waypointList.Count)) {
			graph.addNode ((int) n);
		}

		// Add edges
		addUnitCostEdges (0, 1);
		addUnitCostEdges (0, 3);
		addUnitCostEdges (0, 2);

		addUnitCostEdges (1, 4);
          addUnitCostEdges (1, 6);
          addUnitCostEdges (1, 10);

		addUnitCostEdges (2, 7);
		addUnitCostEdges (2, 5);
          addUnitCostEdges (2, 3);

		addUnitCostEdges (3, 9);
		addUnitCostEdges (3, 8);

		addUnitCostEdges (4, 5);
		addUnitCostEdges (4, 7);
          addUnitCostEdges (4, 10);

		addUnitCostEdges (5, 7);
          addUnitCostEdges (5, 10);

		addUnitCostEdges (6, 9);
          addUnitCostEdges (6, 10);

		addUnitCostEdges (7, 8);

          addUnitCostEdges (8, 9);

	}

	// Add undirected edge with unit cost
	private void addUnitCostEdges(int a, int b) {
		graph.addEdge (a, b, 1);
		graph.addEdge (b, a, 1);
	}

}



