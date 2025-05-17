using Microsoft.CSharp;
using System;
using System.Collections;
using System.Collections.Generic;

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
        foreach (var pair in _vertexDictionary)
        {
            if(pair.Value == index)
                return pair.Key;
        }
        return null;
    }
}
