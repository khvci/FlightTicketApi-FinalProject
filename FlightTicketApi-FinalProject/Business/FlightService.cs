using FlightTicketApi_FinalProject.DataRepository;
using FlightTicketApi_FinalProject.Entities.Concretes;
using System;

namespace FlightTicketApi_FinalProject.Business
{
    public static class FlightService
    {
        public static Flight CreateFlightFromJson(string flightNumber, int planeType, int departureCityId, int arrivalCityId, DateTime flightTime, int businessClassRows)
        {
            if (FlightsRepo.Flights.Exists(f => f.FlightNumber == flightNumber))
            {
                throw new Exception($"Flight with flight number {flightNumber} already exists.");
            }

            Flight flight = new Flight(flightNumber, planeType, departureCityId, arrivalCityId, flightTime, businessClassRows);
            FlightsRepo.Flights.Add(flight);
            return flight;
        }
    }
}
