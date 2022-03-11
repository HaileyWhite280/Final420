using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Priority_Queue;

public static class Search
{
    public delegate bool SearchAlgorithm(GraphNode source, GraphNode destination, ref List<GraphNode> path, int maxSteps);

    static public bool BuildPath(SearchAlgorithm searchAlgorithm, GraphNode source, GraphNode destination, ref List<GraphNode> path, int steps = int.MaxValue)
    {
        if (source == null || destination == null) return false;

        // reset graph nodes
        GraphNode.ResetNodes();

        // search for path from source to destination nodes        
        bool found = searchAlgorithm(source, destination, ref path, steps);

        return found;
    }

    public static bool DFS(GraphNode source, GraphNode destination, ref List<GraphNode> path, int maxSteps)
    {
        bool found = false;

        var nodes = new Stack<GraphNode>();

        //push node to stack
        nodes.Push(source);

        int steps = 0;
        while(!found && nodes.Count > 0 && steps++ < maxSteps)
        {
            //Get node at top of stack
            var node = nodes.Peek();

            bool forward = false;

            //mark as visited
            node.visited = true;

            foreach(var neighbor in node.neighbors)
            {
                //check if node has been visited
                if(!neighbor.visited)
                {
                    nodes.Push(neighbor);
                    forward = true;

                    //check if destination
                    if(neighbor == destination)
                    {
                        found = true;
                    }

                    break;
                }
            }

            //check if going forward, if not pop off from stack
            if(!forward)
            {
                nodes.Pop();
            }
        }

        path = new List<GraphNode>(nodes);

        //reverse stack so its first in last out
        path.Reverse();

        return found;
    }

    public static bool BFS(GraphNode source, GraphNode destination, ref List<GraphNode> path, int maxSteps)
    {
        bool found = false;

        var nodes = new Queue<GraphNode>();

        //set source node visited to true
        source.visited = true;

        //enqueue source node
        nodes.Enqueue(source);

        int steps = 0;
        while (!found && nodes.Count > 0 && steps++ < maxSteps)
        {
            //dequeue node
            var node = nodes.Dequeue();

            foreach(var neighbor in node.neighbors)
            {
                if (!neighbor.visited)
                {
                    neighbor.visited = true;

                    neighbor.parent = node;

                    nodes.Enqueue(neighbor);
                }

                if (neighbor == destination)
                {
                    found = true;
                    break;
                }
            }
        }

        path = new List<GraphNode>();

        if(found)
        {
            var node = destination;

            while(node != null)
            {
                path.Add(node);

                node = node.parent;
            }

            path.Reverse();
        }
        else
        {
            path = nodes.ToList();
        }

        return found;
    }

    public static bool Dijkstra(GraphNode source, GraphNode destination, ref List<GraphNode> path, int maxSteps)
    {
        bool found = false;

        //create priority queue
        var nodes = new SimplePriorityQueue<GraphNode>();

        //set source node cost to 0
        source.cost = 0;

        //enqueue source node with source cost as priority
        nodes.Enqueue(source, source.cost);

        //set number of steps
        int steps = 0;
        while(!found && nodes.Count > 0 && steps++ < maxSteps)
        {
            //dequeue node
            var node = nodes.Dequeue();

            //check if destination
            if (node == destination)
            {
                found = true;
                break;
            }

            foreach(var neighbor in node.neighbors)
            {
                neighbor.visited = true;

                //calculate cost to neighbor
                float cost = node.cost + node.DistanceTo(neighbor);

                //add priority to queue
                if(cost < neighbor.cost)
                {
                    //set neighbor cost to cost
                    neighbor.cost = cost;

                    //set neighbor parent to node
                    neighbor.parent = node;

                    //enqueue without dupes, neighbor cost as priority
                    nodes.EnqueueWithoutDuplicates(node, neighbor.cost);
                }
            }
        }

        if(found)
        {
            //create path from destination to source using node parents
            path = new List<GraphNode>();
            CreatePathFromParents(destination, ref path);
        }
        else
        {
            path = nodes.ToList();
        }

        return found;

    }

    public static bool AStar(GraphNode source, GraphNode destination, ref List<GraphNode> path, int maxSteps)
    {
        bool found = false;
        // create priority queue
        var nodes = new SimplePriorityQueue<GraphNode>();

        // set source cost to 0
        source.cost = 0;
        // set heuristic to the distance of the source to the destination
        float heuristic = Vector3.Distance(source.transform.position, destination.transform.position);
        // enqueue source node with the source cost + source heuristic as the priority
        nodes.Enqueue(source, source.cost + heuristic);

        // set the current number of steps
        int steps = 0;
        while (!found && nodes.Count > 0 && steps++ < maxSteps)
        {
            // dequeue node
            var node = nodes.Dequeue();

            // check if node is the destination node
            if (node == destination)
            {
                // set found to true
                found = true;
                break;
            }

            foreach (var neighbor in node.neighbors)
            {
                neighbor.visited = true; // not needed for algorithm (debug)

                // calculate cost to neighbor = node cost + distance to neighbor
                // float cost = <node cost + node.DistanceTo(neighbor)>
                float cost = node.cost + node.DistanceTo(neighbor);
                // if cost < neighbor cost, add to priority queue
                if (cost < neighbor.cost)
                {
                    // set neighbor cost to cost
                    neighbor.cost = cost;
                    // set neighbor parent to node
                    neighbor.parent = node;
                    // calculate heuristic = distance from neighbor to destination
                    heuristic = Vector3.Distance(neighbor.transform.position, destination.transform.position);
                    // enqueue without duplicates, neighbor cost + heuristic as priority
                    // the closer the neighbor to the destination the higher the priority
                    nodes.EnqueueWithoutDuplicates(neighbor, neighbor.cost + heuristic);
                }
            }
        }
        if (found)
        {
            // create path from destination to source using node parents
            path = new List<GraphNode>();
            CreatePathFromParents(destination, ref path);
        }
        else
        {
            path = nodes.ToList();
        }
        return found;
    }

    public static void CreatePathFromParents(GraphNode node, ref List<GraphNode> path)
    {
        // while node not null
        while (node != null)
        {
            // add node to list path
            path.Add(node);
            // set node to node parent
            node = node.parent;
        }



        // reverse path
        path.Reverse();
    }
}