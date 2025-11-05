using System;
using System.Collections.Generic;
using System.Linq;
using static SupportDeskLab.Utility;


namespace SupportDeskLab
{
   
     
    class Program
    {
        static int NextTicketId = 1;

        // Customer dictionary
        static Dictionary<string, Customer> Customers = new Dictionary<string, Customer>();

        // Ticket queue
        static Queue<Ticket> Tickets = new Queue<Ticket>();

        // Undo event stack
        static Stack<UndoEvent> UndoEvents = new Stack<UndoEvent>();

        static void Main()
        {
            initCustomer();

            while (true)
            {
                Console.WriteLine("\n=== Support Desk ===");
                Console.WriteLine("[1] Add customer");
                Console.WriteLine("[2] Find customer");
                Console.WriteLine("[3] Create ticket");
                Console.WriteLine("[4] Serve next ticket");
                Console.WriteLine("[5] List customers");
                Console.WriteLine("[6] List tickets");
                Console.WriteLine("[7] Undo last action");
                Console.WriteLine("[0] Exit");
                Console.Write("Choose: ");
                string choice = Console.ReadLine();

                //create switch cases and then call a reletive method 
                //for example for case 1 you need to have a method named addCustomer(); or case 2 add a method name findCustomer

                switch (choice)
                {
                    case "1": AddCustomer(); break;
                    case "2": FindCustomer(); break;
                    case "3": CreateTicket(); break;
                    case "4": ServeNext(); break;
                    case "5": ListCustomers(); break;
                    case "6": ListTickets(); break;
                    case "7": Undo(); break;
                    case "0": return;
                    default: Console.WriteLine("Invalid option."); break;
                }
            }
        }
        /*
         * Do not touch initCustomer method. this is like a seed to have default customers.
         */
        static void initCustomer()
        {
            //uncomments these 3 lines after you create the Customer Dictionary
            //Customers["C001"] = new Customer("C001", "Ava Martin", "ava@example.com");
            //Customers["C002"] = new Customer("C002", "Ben Parker", "ben@example.com");
            //Customers["C003"] = new Customer("C003", "Chloe Diaz", "chloe@example.com");
        }

        static void AddCustomer()
        {
            //look at the Demo image and add your code here
            Console.Write("New CustomerId (e.g., C004): ");
            string id = Console.ReadLine();

            if (Customers.ContainsKey(id))
            {
                Console.WriteLine("Customer already exists.");
                return;
            }

            Console.Write("Name: ");
            string name = Console.ReadLine();
            Console.Write("Email: ");
            string email = Console.ReadLine();

            var customer = new Customer(id, name, email);
            Customers[id] = customer;

            UndoEvents.Push(new UndoAddCustomer(customer));
            Console.WriteLine($"Added: {id} | {name} | {email}");
            
        }

        static void FindCustomer()
        {
            //look at the Demo image and add your code here
            Console.Write("Enter customer ID to find: ");
            string id = Console.ReadLine();

            if (Customers.TryGetValue(id, out Customer customer))
            {
                Console.WriteLine("Found: " + customer);
            }
            else
            {
                Console.WriteLine("Customer not found.");
            }
        }

        static void CreateTicket()
        {
            //look at the Demo image and add your code here
            Console.Write("Enter customer ID for ticket: ");
            string customerId = Console.ReadLine();

            if (!Customers.ContainsKey(customerId))
            {
                Console.WriteLine("Invalid customer ID.");
                return;
            }

            Console.Write("Enter ticket subject: ");
            string subject = Console.ReadLine();

            Ticket ticket = new Ticket(NextTicketId++, customerId, subject);
            Tickets.Enqueue(ticket);

            UndoEvents.Push(new UndoCreateTicket(ticket));

            Console.WriteLine("Ticket created: " + ticket);
        }

        static void ServeNext()
        {
            //look at the Demo image and add your code here
            if (Tickets.Count == 0)
            {
                Console.WriteLine("No tickets in queue.");
                return;
            }

            Ticket served = Tickets.Dequeue();
            UndoEvents.Push(new UndoServeTicket(served));

            Console.WriteLine("Serving ticket: " + served);
        }

        static void ListCustomers()
        {
            Console.WriteLine("-- Customers --");
            //look at the Demo image and add your code here
            foreach (var kvp in Customers)
            {
                Console.WriteLine(kvp.Value);
            }
        }

        static void ListTickets()
        {
           
            Console.WriteLine("-- Tickets (front to back) --");
            //look at the Demo image and add your code here
            if (Tickets.Count == 0)
            {
                Console.WriteLine("(none)");
                return;
            }

            foreach (var t in Tickets)
            {
                Console.WriteLine(t);
            }
        }

        static void Undo()
        {
            //look at the Demo image and add your code here
            if (UndoEvents.Count == 0)
            {
                Console.WriteLine("Nothing to undo.");
                return;
            }

            UndoEvent last = UndoEvents.Pop();

            if (last is UndoAddCustomer add)
            {
                Customers.Remove(add.Customer.CustomerId);
                Console.WriteLine($"Undo: Removed customer {add.Customer.CustomerId}");
            }
            else if (last is UndoCreateTicket create)
            {
                var tempQueue = new Queue<Ticket>();
                while (Tickets.Count > 0)
                {
                    Ticket t = Tickets.Dequeue();
                    if (t.TicketId != create.Ticket.TicketId)
                        tempQueue.Enqueue(t);
                }
                Tickets = tempQueue;
                Console.WriteLine($"Undo: Removed ticket #{create.Ticket.TicketId}");
            }
            else if (last is UndoServeTicket serve)
            {
                Tickets = new Queue<Ticket>(new[] { serve.Ticket }.Concat(Tickets));
                Console.WriteLine($"Undo: Returned ticket #{serve.Ticket.TicketId} to front of queue");
            }
        }
    }
}

