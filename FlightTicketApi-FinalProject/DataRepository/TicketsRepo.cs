using FlightTicketApi_FinalProject.Entities.Concretes;
using System.Collections.Generic;

namespace FlightTicketApi_FinalProject.DataRepository
{
    public static class TicketsRepo
    {
        /// <summary>
        /// Gets or sets the list of tickets in the repository.
        /// </summary>
        public static List<Ticket> Tickets { get; set; } = new List<Ticket>();
    }
}
