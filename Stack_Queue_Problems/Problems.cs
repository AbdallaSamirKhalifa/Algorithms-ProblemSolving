using System.Security.Principal;

namespace Stack_Queue_Problems
{
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

        public static void SchedualingPrinter()
        {
            Queue<string>printerQueue = new Queue<string>();

            for (int i = 0; i < 10; i++)
            {
                printerQueue.Enqueue($"Page {i}");
            }
            while( printerQueue.Count > 0)
            {
                Console.WriteLine($"Printing {printerQueue.Dequeue()}");
            }
        }

        public static void TrafficSignalSimulation()
        {
            Queue<string>TrafficSignal=new Queue<string>();
            TrafficSignal.Enqueue("Car 1");
            TrafficSignal.Enqueue("Truck 1");
            TrafficSignal.Enqueue("Bike 1");
            TrafficSignal.Enqueue("Bus 1");

            while(TrafficSignal.Count > 0)
            {
                Console.WriteLine($"\n{TrafficSignal.Dequeue()} has passed the signal.");
                
                if(TrafficSignal.Count>0)
                {
                    Console.WriteLine($"\nVehicles waiting:");
                    Console.WriteLine($"{string.Join(", ", TrafficSignal)}");
                }

            }
            Console.WriteLine("\nNo vehicles are waiting.");
        }

        public static void TicketSystemSimulation()
        {
            Queue<string> Tickets = new Queue<string>();

            for (int i = 101; i < 106; i++)
            {
                Tickets.Enqueue($"{i}");
                Console.WriteLine($"Ticket {i} Issued.");
            }

            Console.WriteLine("\nTicketing system simulation started:");
            while ( Tickets.Count > 0)
            {
                Console.WriteLine($"\nProcessing Tiket {Tickets.Dequeue()}");

                if(Tickets.Count>0) 
                Console.WriteLine($"\nRemaining tickets\n{string.Join(", ", Tickets)}");
            }
            Console.WriteLine("\nThe queue is empty, no remaining tickets.");
        }
    }
}
