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

}
