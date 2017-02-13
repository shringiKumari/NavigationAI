using System;
using UnityEngine;
using System.Collections.Generic;
using Node = System.Int32;
using Cost = System.Int32;



public class GreedyBSF : GraphSearch {

     protected PriorityQueue<Node> frontier;
     protected Dictionary<Node, Node> came_from;
     protected Dictionary<Node, Node> costSoFar;
     protected List<Vector2> NodeHeuristic = new List<Vector2>{
          new Vector2(2,2),
          new Vector2(3,2),
          new Vector2(2,3),
          new Vector2(3,3),
          new Vector2(2,1),
          new Vector2(4,2),
          new Vector2(2.5f, 2.5f),
          new Vector2(3, 1),
          new Vector2(4, 3)
          };

     private int heuristic(Node next, Node goal) {
          //return NodeHeuristic [next]; // heuristic should be based on distance
          return (int)Vector2.Distance(NodeHeuristic[next], NodeHeuristic[goal]);
     }

     public override List<Node> findPath(Node start, Node goal, bool t) {
          trace = t;

          log ("Dijkstra: looking for path from " + start + " to " + goal);

          frontier = new PriorityQueue<Node>();
          frontier.Enqueue(start, 0);
          came_from = new Dictionary<Node, Node>();
          came_from[start] = NONE;
          //costSoFar = new Dictionary<Node, Node>();
          //costSoFar [start] = 0;

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
                    //int nextCost = costSoFar [(Node)current.item] + graph.cost ((Node)current.item, next);
                    if (!came_from.ContainsKey (next)) {
                         frontier.Enqueue (next, heuristic(next, goal));
                         came_from [next] = (Node)current.item;
                         //costSoFar [next] = nextCost;
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