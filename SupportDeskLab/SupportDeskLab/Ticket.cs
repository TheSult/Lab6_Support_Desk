using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportDeskLab
{
    public class Ticket
    {
        public int TicketId { get; }
        public string CustomerId { get; }
        public string Subject { get; }
        public DateTime CreatedAt { get; } = DateTime.Now;

        public Ticket(int id, string customerId, string subject)
        {
            TicketId = id;
            CustomerId = customerId;
            Subject = subject;
        }

        public override string ToString()
        {
            return "#" + TicketId + " (" + CustomerId + ") - " + Subject + " @ " + CreatedAt.ToShortTimeString();
        }
    }
}
