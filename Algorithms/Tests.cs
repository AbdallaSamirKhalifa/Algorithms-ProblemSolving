using Algorithms;
using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Metadata;
using System.Security;
using static clsAlgorithms;


public static class Tests
{
    public static void LinearSearchTests()
    {
        int[] arr = { 64, 34, 25, 12, 22, 11, 90 };

        int target = 22;
        Console.WriteLine("Original Array:");
        foreach (var item in arr)
            Console.Write(item + " ");
        Console.WriteLine();

        int result = LinearSearch(arr, target);

        if (result is -1)
            Console.WriteLine("Eelemnt is not found.");
        else
            Console.WriteLine($"Eelement is found at index: {result}");

    }

    public static void BinarySearchTests(int targetParam)
    {
        int[] arr = new int[100000];
        for (int i = 0; i < arr.Length; i++)
        {
            arr[i] = i + 1;
        }


        int result = BinarySearch(arr, targetParam, out int trials);

        if (result is -1)
            Console.WriteLine($"Element not found in the array, and its been {trials} iterations to go through the elements.");
        else
            Console.WriteLine($"Element found at index: {result}, and its been {trials} iterations to find the element.");



    }


    public static void BubbleSortTest()
    {
        int[] arr = { 64, 34, 25, 12, 22, 11, 90, 1 };

        Console.WriteLine("Original array:");
        foreach (int i in arr)
            Console.Write(i + " ");

        Console.WriteLine();

        BubbleSort(arr);

        Console.WriteLine("\nSorted array:");
        foreach (int i in arr)
            Console.Write(i + " ");
    }

    public static void SelectionSortTest(bool asc)
    {
        int[] arr = { 64, 34, 25, 12, 0, -1, 22, 11, 90, 1 };

        Console.WriteLine("Original array:");
        foreach (int i in arr)
            Console.Write(i + " ");

        Console.WriteLine();

        SelectionSort(arr, asc);

        Console.WriteLine("\nSorted array:");
        foreach (int i in arr)
            Console.Write(i + " ");
    }

    public static void InsertionSortTest()
    {
        int[] arr = { 64, 34, 25, 12, 0, -1, 22, 11, 90, 1 };

        Console.WriteLine("Original array:");
        foreach (int i in arr)
            Console.Write(i + " ");

        Console.WriteLine();

        InsertionSort(arr);

        Console.WriteLine("\nSorted array:");
        foreach (int i in arr)
            Console.Write(i + " ");
    }

    public static void InsertionSortDescTest()
    {
        int[] arr = { 64, 34, 25, 12, 0, -1, 22, 11, 90, 1 };

        Console.WriteLine("Original array:");
        foreach (int i in arr)
            Console.Write(i + " ");

        Console.WriteLine();

        InsertionSortDesc(arr);

        Console.WriteLine("\nSorted array:");
        foreach (int i in arr)
            Console.Write(i + " ");
    }

    /// <summary>
    /// PreOrder, PostOrder, InOrderTraversal tests
    /// </summary>
    public static void BinaryTreeTests()
    {
        var binaryTree = new BinaryTree<int>();
        Console.WriteLine("Values to be inserted: 5,3,8,1,4,6,9\n");

        binaryTree.Insert(5);
        binaryTree.Insert(3);
        binaryTree.Insert(8);
        binaryTree.Insert(1);
        binaryTree.Insert(4);
        binaryTree.Insert(6);
        binaryTree.Insert(9);

        binaryTree.PrintTree(binaryTree.Root);

        Console.WriteLine("\nPreOrder Traversal (Current-Left SubTree - Right SubTree):");
        binaryTree.PreOrderTraversal();


        Console.WriteLine("\nPostorder Traversal (Left SubTree - Right SubTree - Current):");
        binaryTree.PostOrderTraversal();

        Console.WriteLine("\nInorder Traversal: Left-Current-Right");
        binaryTree.InOrderTraversal();

        Console.WriteLine("\nLevel Order Traversal: Left-Current-Right");
        binaryTree.LevelOrderTraversal();
    }

    public static void BinarySearchTreeInsertionTests()
    {
        Console.WriteLine("\nInserting : 45, 15, 79, 90, 10, 55, 12, 20, 50\r\n");

        var bst = new BinarySearchTree<int>();
        bst.Insert(45);
        bst.Insert(15);
        bst.Insert(79);
        bst.Insert(90);
        bst.Insert(10);
        bst.Insert(55);
        bst.Insert(12);
        bst.Insert(20);
        bst.Insert(50);

        bst.PrintTree(bst.Root);

        Console.WriteLine("\nInOrder Traversal:");
        bst.InOrderTraversal();

        Console.WriteLine("\nPreOrder Traversal:");
        bst.PreOrderTraversal();

        Console.WriteLine("\nPostOrder Traversal:");
        bst.PostOrderTraversal();
    }

    public static void BinarySearchTreeSerchTests()
    {
        var bst = new BinarySearchTree<int>();
        bst.Insert(45);
        bst.Insert(15);
        bst.Insert(79);
        bst.Insert(90);
        bst.Insert(10);
        bst.Insert(55);
        bst.Insert(12);
        bst.Insert(20);
        bst.Insert(50);

        BinaryTreeNode<int> node = bst.Search(90);

        Console.WriteLine($"Does the BTS contains 79? {bst.Contains(79)}");
        Console.WriteLine($"Does the BTS contains 100? {bst.Contains(100)}");
        if (node != null)
        {
            Console.WriteLine("Node Found: " + node.Value);
        }
        else
            Console.WriteLine("Node not found");

    }

    public static void AVLTreeInsertionTests()
    {
        AVLTree<int> tree = new AVLTree<int>();

        //RR
        // int[] values = { 10, 20, 30 };

        //LL
        //  int[] values = { 30, 20, 10 };

        //LR
        // int[] values = { 30, 10, 20 };

        //RL
        //int[] values = { 10, 30, 20 };

        // Inserting values
        int[] values = { 10, 20, 30, 40, 50, 25 };
        foreach (var value in values)
        {
            Console.WriteLine($"Inserting {value} into the AVL tree.");
            tree.Insert(value);
            tree.PrintTree();
            Console.WriteLine("\n-------------------------------------------------\n");
        }
    }

    public static void AVLTreeDeletionTests()
    {

        AVLTree<int> tree = new AVLTree<int>();


        // Inserting values
        int[] values = { 10, 20, 30, 40, 35, 32, 50, 25 };
        foreach (var value in values)
        {
            tree.Insert(value);
        }

        tree.PrintTree();

        tree.Delete(30);
        Console.WriteLine("\nAfter Deletion.\n");

        tree.PrintTree();

    }

    public static void AVLTreeSearchAndExistsTests()
    {
        AVLTree<int> tree = new AVLTree<int>();

        // Inserting values
        int[] values = { 10, 20, 30, 40, 50, 25 };
        foreach (var value in values)
        {
            tree.Insert(value);
        }

        // Print the tree
        tree.PrintTree();

        // Searching for values
        int searchValue = 30;
        bool found = tree.Exists(searchValue);
        Console.WriteLine($"\nSearch for value {searchValue}: " + (found ? "Found" : "Not Found"));

        searchValue = 60;
        found = tree.Exists(searchValue);
        Console.WriteLine($"Search for value {searchValue}: " + (found ? "Found" : "Not Found"));



        // Searching for values and printing the results
        int searchValue2 = 30;
        AVLNode<int> foundNode = tree.Search(searchValue2);
        Console.WriteLine($"\nSearch for value {searchValue2}: " + (foundNode != null ? $"Found node with value: {foundNode.Value}" : "Not Found"));

        searchValue2 = 60;
        foundNode = tree.Search(searchValue);
        Console.WriteLine($"Search for value {searchValue2}: " + (foundNode != null ? $"Found node with value: {foundNode.Value}" : "Not Found"));

    }

    public static void AutoCompleteTests()
    {
        AVLTree<string> tree = new AVLTree<string>();
        string[] words = { "Ahmad", "Mohammed", "Motasem", "Mohab", "Abla", "Abeer", "Abdullah", "Abbas", "Montaser", "Khalil", "Khalid" };

        foreach (var word in words)
        {
            tree.Insert(word);
        }

        AVLAutoComplete aVLAutoComplete = new AVLAutoComplete(tree);

        
        do
        {
            Console.Clear();
            tree.PrintTree();
            Console.WriteLine("\nEnter a prefix to search:\n");
            string prefix = Console.ReadLine();
            var completions = aVLAutoComplete.AutoComplete(prefix);

            Console.WriteLine($"\nSuggestions for '{prefix}':\n");
            foreach (var completion in completions)
            {
                Console.WriteLine(completion);
            }
        }
        while (Console.ReadKey().Key == ConsoleKey.Y);
    }

    public static void RedBlackTreeInsertionTests()
    {
        RedBlackTree rbTree = new RedBlackTree();


        // Test values to be inserted into the tree
        int[] values = { 10, 20, 30, 15, 25, 35, 5, 19 };
        foreach (var value in values)
        {
            Console.WriteLine($"Inserting {value} to the tree\n");
            rbTree.Insert(value);
            rbTree.PrintTree();
            Console.WriteLine("\n--------------------------------\n");
        }
    }

    public static void ReddBlackTreeSearchTests()
    {
        RedBlackTree rbTree = new RedBlackTree();


        // Test values to be inserted into the tree
        int[] values = { 10, 20, 30, 15, 25, 35, 5, 19 };
        foreach (var value in values)
        {
            rbTree.Insert(value);
        }

        rbTree.PrintTree();
        Console.WriteLine("\n--------------------------------\n");

        int searchValue = 10;
        RedBlackTree.Node FoundNode = rbTree.Find(searchValue);
        if (FoundNode != null)
            Console.WriteLine($"Node wiht value {searchValue} found with color {(FoundNode.IsRed ? "Red" : "Black")}");
        else Console.WriteLine($"Node with value {searchValue} is not found");
        
        Console.WriteLine("\n--------------------------------\n");
        
        searchValue = 200;
        FoundNode = rbTree.Find(searchValue);
        
        if (FoundNode != null)
            Console.WriteLine($"Node wiht value {searchValue} found with color {(FoundNode.IsRed ? "Red" : "Black")}");
        else Console.WriteLine($"Node with value {searchValue} is not found");
        

    }

    public static void RedBlackTreeDeletionTests()
    {
        RedBlackTree rbTree = new RedBlackTree();


        // Test values to be inserted into the tree
        int[] values = { 10, 20, 30, 15, 25, 35, 5, 19 };
        foreach (var value in values)
        {
            rbTree.Insert(value);
        }

        rbTree.PrintTree();
        Console.WriteLine("\n--------------------------------\n");

        if (rbTree.Delete(250))
        {
            Console.WriteLine("After deleting 250:");
            rbTree.PrintTree();
        }
        else
            Console.WriteLine("No node deleted couldn't find 250");


        Console.WriteLine("\n--------------------------------\n");

        if (rbTree.Delete(10))
        {
            Console.WriteLine("After deleting 10:");
            rbTree.PrintTree();
        }
        else
            Console.WriteLine("No node deleted couldn't find 10");
    }

    public static void BFSTest()
    {
        List<string> vertices = new List<string> { "0", "1", "2", "3", "4" };

        Graph graph = new Graph(vertices, Graph.enGraphDirection.UnDirected);

        graph.AddEdge("0", "1", 1);
        graph.AddEdge("0", "2", 1);

        graph.AddEdge("1", "2", 1);
        graph.AddEdge("1", "3", 1);

        graph.AddEdge("2", "3", 1);
        graph.AddEdge("2", "4", 1);



        graph.DisplayGraph("Adjacency Matrix (Undirected Graph):");

        graph.BFS("0");
    }
    
    public static void DFSTest()
    {
        List<string> vertices = new List<string> { "0", "1", "2", "3", "4" };

        Graph graph = new Graph(vertices, Graph.enGraphDirection.UnDirected);

        graph.AddEdge("0", "1", 1);
        graph.AddEdge("0", "2", 1);
        graph.AddEdge("0", "3", 1);

        graph.AddEdge("2", "3", 1);
        graph.AddEdge("2", "4", 1);




        graph.DisplayGraph("Adjacency Matrix (Undirected Graph):");

        graph.DFS("0");
    }

    public static void DijkstraTest()
    {
        List<string> vertices = new List<string> { "A", "B", "C", "D", "E" };

        Graph graph = new Graph(vertices, Graph.enGraphDirection.Directed);

        graph.AddEdge("A", "B", 4);
        graph.AddEdge("A", "C", 1);
        graph.AddEdge("C", "B", 2);
        graph.AddEdge("C", "D", 4);
        graph.AddEdge("B", "E", 4);
        graph.AddEdge("D", "E", 1);

        graph.DisplayGraph("Adjacency Matrix");
        graph.Dijkstra("A");
    }
    public static void DijkstraUsinMinHeapTest()
    {
        List<string> vertices = new List<string> { "A", "B", "C", "D", "E" };

        // Create a directed graph for the transport network
        Graph graph = new Graph(vertices, Graph.enGraphDirection.UnDirected);

        // Add connections (edges) with travel times (weights)
        graph.AddEdge("A", "B", 10);
        graph.AddEdge("A", "C", 15);
        graph.AddEdge("B", "D", 12);
        graph.AddEdge("C", "D", 10);
        graph.AddEdge("B", "E", 15);
        graph.AddEdge("D", "E", 2);

        graph.DisplayGraph("Adjacency Matrix");
        graph.DijkstraMinHeap("A");
    }
    public static void ShortestTimeToTravelTest()
    {
        List<string> stations = new List<string> { "A", "B", "C", "D", "E" };

        // Create a directed graph for the transport network
        Graph transportGraph = new Graph(stations, Graph.enGraphDirection.UnDirected);

        // Add connections (edges) with travel times (weights)
        transportGraph.AddEdge("A", "B", 10);
        transportGraph.AddEdge("A", "C", 15);
        transportGraph.AddEdge("B", "D", 12);
        transportGraph.AddEdge("C", "D", 10);
        transportGraph.AddEdge("B", "E", 15);
        transportGraph.AddEdge("D", "E", 2);

        transportGraph.DisplayGraph("Transport Network Adjacency Matrix:");
        Console.WriteLine("\nFinding the shortest path from A to E:");
        transportGraph.Dijkstra("A","E");
    }
}
