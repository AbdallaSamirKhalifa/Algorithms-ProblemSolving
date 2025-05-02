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

    public static void BubbleSort(int[] array)
    {
        int length = array.Length - 1;
        for (int i = 0; i < length ; i++)
            for (int j = 0; j < length -i ; j++)
                if (array[j] > array[j+1])
                    (array[j], array[j+1]) = (array[j+1], array[j]);
                


        
    }
    
    public static void SelectionSort(int[] array, bool asc=true)
    {
        int length = array.Length ;
        int index = 0;

        for (int i = 0; i < length - 1; i++)
        {
            index = i;
            //one by one mover the boundary of unsorted segment
            for (int j = i + 1; j < length; j++)
            {
                //find the min index in the unsorted segment
                if (array[index] > array[j] && asc)
                    index = j ;

                //find the max index in the unsorted segment
                if (array[index] < array[j ] && !asc)
                    index = j ;

            }
            //swapping the (min/max) element if found with the first element in the unsorted segment
            if(index!=i)
            (array[i], array[index])=(array[index], array[i]);
        }
    }

    public static void InsertionSort(int[] array)
    {
        int length = array.Length;
        int key = 0;
        int j = 0;
        for (int i = 0; i < array.Length; i++)
        {
            key = array[i];
            j = i - 1;

            //move elements of arr[0..i-1], that are greater than the key
            //to one position ahead of their current position
            while(j >= 0 && array[j]>key)
            {
                array[j + 1] = array[j];
                j--;
            }
            array[j + 1] = key;
        }
    }
    public static void InsertionSortDesc(int[] array)
    {
        int length = array.Length;
        int key = 0;
        int j = 0;
        for (int i = 0; i <length; i++)
        {
            key = array[i];
            j = i-1;
            while(j >= 0 && array[j] < key)
            {
                array[j +1] = array[j];
                j--;
            }
            array[j + 1] = key;
        }
    }
}

