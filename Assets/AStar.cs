using System;
using UnityEngine;
using System.Collections.Generic;
using Node = System.Int32;
using Cost = System.Int32;

public class AStar : GraphSearch {

     protected PriorityQueue<Node> frontier;
     protected Dictionary<Node, Node> came_from;
     protected Dictionary<Node, Node> costSoFar;
     protected List<Vector3> NodeHeuristic = new List<Vector3>();

     private float heuristic(Node next, Node goal) {
          return Vector3.Distance(NodeHeuristic[next], NodeHeuristic[goal]);
     }

     public void setPositions(List<Vector3> waypointPositions) {
          NodeHeuristic = waypointPositions;
     }

     public override List<Node> findPath(Node start, Node goal, bool t) {
          trace = t;

          log ("AStar: looking for path from " + start + " to " + goal);

          frontier = new PriorityQueue<Node>();
          frontier.Enqueue(start, 0);
          came_from = new Dictionary<Node, Node>();
          came_from[start] = NONE;
          costSoFar = new Dictionary<Node, Node>();
          costSoFar [start] = 0;

          bool done = false;

          // Main search loop
          while (frontier.Count != 0) {
               CostedItem current = (CostedItem)frontier.Dequeue ();
               log ("Current node is " + (Node)current.item);

               if ((Node)current.item == goal) {
                    done = true;
                    log ("Found goal node " + goal);
                    break;
               }

               List<Node> neighbours = graph.neighbours ((Node)current.item);
               log (neighbours.Count + " neighbours found");

               foreach (Node next in neighbours) {
                    log ("Adding to " + next + " to frontier");
                    int nextCost = costSoFar [(Node)current.item] + graph.cost ((Node)current.item, next);
                    if (!costSoFar.ContainsKey (next) || nextCost < costSoFar [next]) {
                         frontier.Enqueue (next, nextCost + heuristic(next, goal));
                         came_from [next] = (Node)current.item;
                         costSoFar [next] = nextCost;
                    }

                    else {
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


