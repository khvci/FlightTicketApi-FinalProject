using FlightTicketApi_FinalProject.DataRepository;
using FlightTicketApi_FinalProject.Entities.Abstracts;
using FlightTicketApi_FinalProject.Entities.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FlightTicketApi_FinalProject.Business
{
    public static class FlightService
    {
        public static Flight CreateFlightFromJson(string flightNumber, int planeType, int departureCityId, int arrivalCityId, DateTime flightTime, int businessClassRows)
        {
            Flight flight = new Flight(flightNumber, planeType, departureCityId, arrivalCityId, flightTime, businessClassRows);
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
