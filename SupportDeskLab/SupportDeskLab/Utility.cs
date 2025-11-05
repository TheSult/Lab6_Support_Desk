using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportDeskLab
{
    public class Utility
    {
        public abstract class UndoEvent { }

        public class UndoAddCustomer : UndoEvent
        {
            public Customer Customer { get; }
            public UndoAddCustomer(Customer customer)
            {
                Customer = customer;
            }
        }

        public class UndoCreateTicket : UndoEvent
        {
            public Ticket Ticket { get; }
            public UndoCreateTicket(Ticket ticket)
            {
                Ticket = ticket;
            }
        }

        public class UndoServeTicket : UndoEvent
        {
            public Ticket Ticket { get; }
            public UndoServeTicket(Ticket ticket)
            {
                Ticket = ticket;
            }
        }
    }
}
