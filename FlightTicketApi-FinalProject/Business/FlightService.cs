using FlightTicketApi_FinalProject.DataRepository;
using FlightTicketApi_FinalProject.Entities.Abstracts;
using FlightTicketApi_FinalProject.Entities.Concretes;
using System.Collections.Generic;
using System.Linq;

namespace FlightTicketApi_FinalProject.Business
{
    public static class FlightService
    {
        /// <summary>
        /// Adds a flight to the repository.
        /// </summary>
        /// <param name="flight">The flight to add to the repository.</param>
        /// <returns>The added flight.</returns>
        public static Flight AddFlightToRepository(Flight flight)
        {
            FlightsRepo.Flights.Add(flight);
            return flight;
        }

        /// <summary>
        /// Gets a list of available seats for a flight.
        /// </summary>
        /// <param name="flightNumber">The flight number to get available seats for.</param>
        /// <returns>A list of available seats for the flight.</returns>
        public static List<ISeat> GetAvailableSeats(string flightNumber)
        {
            Flight flight = FlightsRepo.Flights.FirstOrDefault(f => f.FlightNumber.Equals(flightNumber));
            return SeatService.GetAvailableSeatsInFlight(flight);
        }
    }
}
