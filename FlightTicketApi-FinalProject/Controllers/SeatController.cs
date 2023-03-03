using FlightTicketApi_FinalProject.Entities.Abstracts;
using FlightTicketApi_FinalProject.Entities.Concretes;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace FlightTicketApi_FinalProject.Controllers
{
    [Route("api/[controller]")]
    public class SeatController : ControllerBase
    {
        [HttpGet]
        public ISeat TestCreatingABusinessSeat()
        {
            var enums = (ColumnCharacters[])Enum.GetValues(typeof(ColumnCharacters));
            return new BusinessSeat()
            {
                SeatRow = 1,
                SeatColumn = enums[0].ToString()
            };
        }

        public static List<Flight> Flights = new List<Flight>();
        [HttpPost]
        public Flight CreateFlight(string flightNumber, PlaneConfiguration planeConfiguration, Destination departure, Destination arrival, DateTime flightTime, int businessClassRows)
        {
            Flight _flight = new Flight(flightNumber, planeConfiguration, departure, arrival, flightTime, businessClassRows);
            Flights.Add(_flight);
            return _flight;

        }

    }
}
