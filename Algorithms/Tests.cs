using System;
using System.Collections.Generic;
using static clsAlgorithms;


public static class Tests
{
    public static void LinearSearchTests()
    {
        int [] arr= { 64, 34, 25, 12, 22, 11, 90 };

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
            arr[i] = i+1;
        }


        int result = BinarySearch(arr, targetParam,out int trials);

        if (result is -1)
            Console.WriteLine($"Element not found in the array, and its been {trials} iterations to go through the elements.");
        else
            Console.WriteLine($"Element found at index: {result}, and its been {trials} iterations to find the element.");

        

    }


    public static void BubbleSortTest()
    {
        int[] arr = { 64, 34, 25, 12, 22, 11, 90 ,1};

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

        SelectionSort(arr,asc);

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
        if(node != null)
        {
            Console.WriteLine("Node Found: "+node.Value);
        }
        else
            Console.WriteLine("Node not found");

    }
}
