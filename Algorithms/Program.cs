using System;
using System.Collections.Generic;
using static Tests;


internal class Program
{
    static void Main(string[] args)
    {
        //LinearSearchTests();
        //BinarySearchTests(12344);
        //BubbleSortTest();
        //Console.WriteLine("Ascending:");
        //SelectionSortTest(true);
        //SelectionSortTest(false);

        //InsertionSortTest();
        //Console.WriteLine("\n\nDescending");
        //InsertionSortDescTest(); 

        //BinaryTreeTests();

        //BinarySearchTreeInsertionTests();
        //BinarySearchTreeSerchTests();
        //AVLTreeInsertionTests();
        //AVLTreeDeletionTests();
        //AVLTreeSearchAndExistsTests();
        //AutoCompleteTests();
        //RedBlackTreeInsertionTests();
        //ReddBlackTreeSearchTests();
        //RedBlackTreeDeletionTests();
        //BFSTest();
        //DFSTest();
        //DijkstraTest();
        //DijkstraUsinMinHeapTest();
        //ShortestTimeToTravelTest();
        CoinChangeProblem(new SortedSet<int> {1,5,10,20,50 }, 156);
        Console.ReadKey();
    }
}
