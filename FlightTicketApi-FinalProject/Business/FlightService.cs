using FlightTicketApi_FinalProject.DataRepository;
using FlightTicketApi_FinalProject.Entities.Abstracts;
using FlightTicketApi_FinalProject.Entities.Concretes;
using System.Collections.Generic;
using System.Linq;

namespace FlightTicketApi_FinalProject.Business
{
    public static class FlightService
    {
        public static Flight AddFlightToRepository(Flight flight)
        {
            FlightsRepo.Flights.Add(flight);
            return flight;
        }

        public static List<ISeat> GetAvailableSeats(string flightNumber)
        {
            Flight flight = FlightsRepo.Flights.FirstOrDefault(f => f.FlightNumber.Equals(flightNumber));
            return SeatService.GetAvailableSeatsInFlight(flight);
        }
    }
}
