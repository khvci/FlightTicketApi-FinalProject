using FlightTicketApi_FinalProject.Entities.Concretes;
using System.Collections.Generic;

namespace FlightTicketApi_FinalProject.DataRepository
{
    public static class FlightsRepo
    {
        public static List<Flight> Flights { get; set; } = new List<Flight>();
    }
}
