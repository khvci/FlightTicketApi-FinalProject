using FlightTicketApi_FinalProject.Entities.Concretes;
using System.Collections.Generic;

namespace FlightTicketApi_FinalProject.DataRepository
{
    public static class TicketsRepo
    {
        public static List<Ticket> Tickets { get; set; } = new List<Ticket>();
    }
}
