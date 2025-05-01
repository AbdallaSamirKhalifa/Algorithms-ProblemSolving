using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;


public static class clsAlgorithms
{
    public static int LinearSearch(int[] array, int target)
    {
        int n = array.Length;
        for (int i = 0; i < n; i++)
        {
            if(array[i] == target)
                return i;
        }
        return -1;
    }

    public static int BinarySearch(int[] array, int target, out int trials)
    {
        int end = array.Length - 1, start = 0, middle = 0, trial = 0;

        while(start <= end)
        {
            middle = start + (end - start) / 2;
           trial++;
            trials = trial;


            //check if the target is present in the middle
            if (array[middle] == target)
                return middle;

            //if the target is greater than the middle, ignore the left half
            if (array[middle] < target)
                start = middle + 1;
            else
                end = middle - 1;//this means its smaller than the middle, so we ignore the right half


        }
        trials=trial;
        return -1;
    }


}

