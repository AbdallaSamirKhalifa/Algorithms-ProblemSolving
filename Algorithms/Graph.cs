using Microsoft.CSharp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Diagnostics.SymbolStore;
using System.IO.Compression;
using System.Linq;
using System.Runtime.InteropServices;

class Graph 
{
    public enum enGraphDirection { Directed, UnDirected};
    private int[,] _adjacencyMatrix;
    private Dictionary<string, int> _vertexDictionary;
    private int _numberOfVertices;
    private enGraphDirection _GraphDirectionType=enGraphDirection.UnDirected;

    public Graph(List <string> vertices, enGraphDirection direction)
    {
        _GraphDirectionType = direction;
        _numberOfVertices = vertices.Count;
        _adjacencyMatrix = new int[_numberOfVertices, _numberOfVertices];
        _vertexDictionary = new Dictionary<string, int>();

        for (int i = 0; i < _numberOfVertices; i++)
        {
            _vertexDictionary[vertices[i]] = i;
        }

        
    }


    public void AddEdge(string source, string destination, int weight)
    {
        if (_vertexDictionary.ContainsKey(source) && _vertexDictionary.ContainsKey(destination))
        {
            int sourceIndex = _vertexDictionary[source];
            int destinationIndex = _vertexDictionary[destination];
            _adjacencyMatrix[sourceIndex, destinationIndex] = weight;

            if (_GraphDirectionType == enGraphDirection.UnDirected)
            {
                _adjacencyMatrix[destinationIndex, sourceIndex] = weight;

            }
        }
        else
            Console.WriteLine($"Invalid vertices: {source} or {destination}");
    }

    public void DisplayGraph(string message)
    {
        Console.WriteLine("\n" + message + "\n");
        Console.Write("  ");

        foreach (var item in _vertexDictionary.Keys)
        {
            Console.Write(item + " ");
        }
        Console.WriteLine();
        foreach(var source in _vertexDictionary)
        {
            Console.Write(source.Key + " ");
            for (int i = 0; i < _numberOfVertices; i++)
            {
                Console.Write(_adjacencyMatrix[source.Value, i] + " ");

            }
            Console.WriteLine() ;
        }
    }

    public void DFS(string startVertex)
    {
        if (!_vertexDictionary.ContainsKey(startVertex))
        {
            Console.WriteLine($"Invalid start vertex.");
            return;
        }

        bool [] visited = new bool [ _numberOfVertices ];

        Stack<int> stack = new Stack<int>();

        int startIndex = _vertexDictionary[startVertex];
        stack.Push(startIndex);

        Console.WriteLine("\nDepth first search");
        while (stack.Count > 0)
        {
            int currentVertex = stack.Pop();

            //skip already visited vertices
            if (visited[currentVertex])
                continue;
            
            visited[currentVertex] = true;
            Console.Write($"{GetVertixName(currentVertex)} ");
            
            //add all unvisited nodes to the stack
            for (int i = _numberOfVertices-1; i >= 0; i--)
            {
                if (_adjacencyMatrix[currentVertex, i] > 0 && !visited[i])
                {
                    stack.Push(i);
                }
            }


        }
    }
    public void BFS(string startVertex)
    {
        if(!_vertexDictionary.ContainsKey(startVertex))
        {
            Console.WriteLine($"Invalid start vertex.");
            return;
        }

        bool[] visited = new bool[_numberOfVertices];//to keep track of visited vertices

        Queue<int> queue = new Queue<int>();

        int startIndex = _vertexDictionary[startVertex];
        visited[startIndex] = true;
        queue.Enqueue(startIndex);
        Console.WriteLine("\nBreadth-First Search:");
        while(queue.Count > 0 )
        {
            int currentVertex = queue.Dequeue();

            Console.WriteLine($"{GetVertixName(currentVertex)}");
            //add all visited naighbors to the queue
            for (int i = 0; i < _numberOfVertices; i++)
            {
                if (_adjacencyMatrix[currentVertex,i] > 0 && !visited[i])
                {
                    visited[i] = true;
                    queue.Enqueue(i);
                }
            }
            Console.WriteLine();
        }
    }
    private string GetVertixName(int index)
    {

        return _vertexDictionary.FirstOrDefault(pair => pair.Value == index).Key;
    }

    public void Dijkstra(string startVertex)
    {
        if (!_vertexDictionary.ContainsKey(startVertex))
        {
            Console.WriteLine("Invalid start vertex, vertex not found.");
            return;
        }

        int startIndex = _vertexDictionary[startVertex];
        int[] destances = new int[_numberOfVertices];//stores shortest distances
        bool[] visited = new bool[_numberOfVertices];//stores processed vertices
        string[] predecessors = new string[_numberOfVertices];//tracks the previous vertex on the shortest path

        for (int i = 0; i < _numberOfVertices; i++)
        {
            destances[i] = int.MaxValue;
           visited[i] = false;
            predecessors[i] = null;
        }
        destances[startIndex] = 0;//destance to the source is 0
        int minVertex = -1;

        //main loop process for each vertex
        for (int i = 0; i < _numberOfVertices-1; i++)

        {
            //find the unvisited vertex witht the smallest distane
            minVertex = GetMinDestanceVertex(destances, visited);
            visited[minVertex] = true;

            //update destances for all neighbores of the current vertex
            for (int v = 0; v < _numberOfVertices; v++)
            {
                /*update the destance if 
                 * there is an edge
                 * the vertext is unvisited
                 * the new destance is shorter
                 */
                if (!visited[v] && _adjacencyMatrix[minVertex, v] > 0
                    && destances[minVertex]!=int.MaxValue
                    && destances[minVertex] + _adjacencyMatrix[minVertex, v] < destances[v]
                    )
                {
                    destances[v] = destances[minVertex] + _adjacencyMatrix[minVertex, v];
                    predecessors[v] = GetVertixName(minVertex);

                }

            }


        }


        Console.WriteLine($"\nShortest path from vertex {startVertex}:");
        for (int i = 0; i < _numberOfVertices; i++)
        {
            Console.WriteLine($"{startVertex} -> {GetVertixName(i)}:" +
                $" Diestance = {destances[i]}, path = {GetPath(predecessors,i)}");
        }

    }

    public void DijkstraMinHeap(string startVertex)
    {
        if (!_vertexDictionary.ContainsKey(startVertex))
        {
            Console.WriteLine("Invalid start vertex, vertex not found.");
            return;
        }

        int startIndex = _vertexDictionary[startVertex];

        int[]distances = new int[_numberOfVertices];
        bool[] visitid = new bool[_numberOfVertices];
        string[] predecessors = new string[_numberOfVertices];

        for (int i = 0; i < _numberOfVertices; i++)
        {
            distances[i] = int.MaxValue;
            visitid[i] = false;
            predecessors[i] = null;
        }

        distances[startIndex] = 0;
        //MinHeap to store vertices with their distances
        var priorityQueue = new SortedSet<(int distance, int vertexIndex)>
            (Comparer<(int distance, int vertexIndex)>.Create((x, y) =>
            x.distance == y.distance ? x.vertexIndex.CompareTo(y.vertexIndex) :
            x.distance.CompareTo(y.distance))
            );
        priorityQueue.Add((0, startIndex));

        while(priorityQueue.Count > 0 )
        {
            //extract the vertex with the smallest distance
            var (currentDistance, currentIndex) = priorityQueue.Min;
            priorityQueue.Remove(priorityQueue.Min);

            //skip the vertex if it's already visited
            if (visitid[currentIndex])
                continue;
            visitid[currentIndex] = true;
            //update distances for all the neighbors of the current vertex
            for (int neighbor = 0; neighbor < _numberOfVertices; neighbor++)
            {
                //check if there is an edge and the neighbor is unvisited
                if (_adjacencyMatrix[currentIndex, neighbor] > 0 && !visitid[neighbor])
                {
                    //calculate the new distance
                    int newDistance = distances[currentIndex]+_adjacencyMatrix[currentIndex, neighbor];
                    if(newDistance < distances[neighbor])
                    {
                        priorityQueue.Remove((distances[neighbor], neighbor));//remove the old distance
                        distances[neighbor] = newDistance;
                        predecessors[neighbor] = GetVertixName(currentIndex);
                        priorityQueue.Add((newDistance, neighbor));
                    }

                }
            }

        }
        Console.WriteLine($"\nShortest path from vertex {startVertex}:");
        for (int i = 0; i < _numberOfVertices; i++)
        {
            Console.WriteLine($"{startVertex} -> {GetVertixName(i)}:" +
                $" Diestance = {distances[i]}, path = {GetPath(predecessors, i)}");
        }
    }


    /// <summary>
    /// Solving shortes time to travel problem
    /// </summary>
    /// <param name="source">The station you want to start from</param>
    /// <param name="distenation">The distination you want to travel to</param>
    public void Dijkstra(string source, string distenation)
    {
        if (!_vertexDictionary.ContainsKey(source) || !_vertexDictionary.ContainsKey(distenation))
        {
            Console.WriteLine("Invalid start or end vertex, vertex not found.");
            return;
        }

        int startIndex = _vertexDictionary[source];
        int[] distances = new int[_numberOfVertices];
        bool[]visited = new bool[_numberOfVertices];
        string[] predecessors = new string[_numberOfVertices];

        for (int i = 0; i < _numberOfVertices; i++)
        {
            distances[i] = int.MaxValue;
            visited[i] = false;
            predecessors[i] = null;
        }
        distances[startIndex]  = 0;

        var priorityQueue = new SortedSet<(int distance, int vertexIndex)>(
            Comparer<(int distance, int vertexIndex)>.Create((x, y) =>
            x.distance == y.distance ? x.vertexIndex.CompareTo(y.vertexIndex) :
            x.distance.CompareTo(y.distance)
            ));

        priorityQueue.Add((0, startIndex));

        while ( priorityQueue.Count > 0 )
        {
            var (currentDistance, currentIndex) = priorityQueue.Min;
            priorityQueue.Remove(priorityQueue.Min);

            if (visited[currentIndex])
                continue;
            visited[currentIndex] = true;

            for (int neighbor = 0; neighbor < _numberOfVertices; neighbor++)
            {
                if (_adjacencyMatrix[currentIndex,neighbor] >0 && !visited[neighbor])
                {
                    int newDistance = _adjacencyMatrix[currentIndex,neighbor] + distances[currentIndex];
                    if(newDistance < distances[neighbor]) {
                        {
                            priorityQueue.Remove((distances[neighbor], neighbor));
                            distances[neighbor] = newDistance;
                            predecessors[neighbor] = GetVertixName(currentIndex);
                            priorityQueue.Add((newDistance, neighbor));
                        } 
                    }
                }    
            }
        }

        int endIndex = _vertexDictionary[distenation];
        Console.WriteLine($"\nShortest path from {source} to {distenation}:");
        if (distances[endIndex] == int.MaxValue)
        {
            Console.WriteLine($"No path leads to {distenation}");
        }
        else
        {
            string path = GetPath(predecessors, endIndex);
            Console.WriteLine($"Path: {path}");
            Console.WriteLine($"Distance: {distances[endIndex]}");
        }
    }
    private string GetPath(string[] predecessors, int currentIndex)
    {
        //Base case: if there is no predecessors, return the current vertex
        if (predecessors[currentIndex]==null)
            return GetVertixName(currentIndex);

        return GetPath(predecessors, _vertexDictionary[predecessors[currentIndex]])
            + " -> "+GetVertixName(currentIndex);
    }
    private int GetMinDestanceVertex(int[] destances, bool[] visited)
    {
        int minDestance = int.MaxValue;
        int minIndex = -1;

        for (int i = 0; i < _numberOfVertices; i++)
        {
            if (!visited[i] && destances[i] < minDestance)
            {
                minDestance = destances[i];
                minIndex = i;
            }
        }
        return minIndex;
    }
}
