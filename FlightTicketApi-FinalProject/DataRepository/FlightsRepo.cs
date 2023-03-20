using FlightTicketApi_FinalProject.Entities.Concretes;
using System.Collections.Generic;

namespace FlightTicketApi_FinalProject.DataRepository
{
    public static class FlightsRepo
    {
        /// <summary>
        /// Gets or sets the list of flights in the repository.
        /// </summary>
        public static List<Flight> Flights { get; set; } = new List<Flight>();
    }
}
