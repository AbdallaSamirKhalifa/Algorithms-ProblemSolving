using System.Collections;
using System.Data;
using System.Net.NetworkInformation;
using static Stack_Queue_Problems.Problems;
namespace Stack_Queue_Problems
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //ConvertDecimalToBinaryWithStack(64);
            UndoOperation();

            Console.ReadKey();
        }
    }


    public static class Problems
    {
        public static void BrowserBackButoon()
        {
            List<string> Pages = new List<string>();
            for (int i = 1; i < 10; i++)
            {
                Pages.Add($"Page {i}");
            }

            Stack<string> history = new Stack<string>();

            //going forward into pages
            foreach (var item in Pages)
            {
                history.Push(item);
            }

            //pressing the back button till getting into the first page
            while(history.Count > 0)
            {
                Console.WriteLine(history.Pop());
            }
        }

        public static void ConvertDecimalToBinaryWithStack(int number)
        {
            
            Stack<int> binary = new Stack<int>();
            while(number > 0)
            {
                binary.Push(number % 2);
                number /= 2;

            }
            
            Console.WriteLine(string.Join("", binary));
        }

        public static void UndoOperation()
        {
            Stack<string>history = new Stack<string>();

            for(int i = 0; i < 10; i++)
            {
                history.Push($"Operation {i}");

            }
            while( history.Count > 0)
            {
                Console.WriteLine("Undo "+history.Pop());
            }
            

            
            
        }
    }
}
