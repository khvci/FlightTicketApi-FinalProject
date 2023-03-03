using FlightTicketApi_FinalProject.Business;
using FlightTicketApi_FinalProject.Entities.Abstracts;
using FlightTicketApi_FinalProject.Entities.Concretes;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace FlightTicketApi_FinalProject.Controllers
{
    [Route("api/[controller]")]
    public class FlightController : ControllerBase
    {
        [HttpPost]
        public ActionResult<Flight> CreateFlight([FromBody] Flight flight)
        {
            return FlightService.CreateFlightFromJson(flight.FlightNumber, flight.PlaneType, flight.DepartureCityId, flight.ArrivalCityId, flight.FlightTime, flight.BusinessClassRows);
        }


        [HttpGet]
        [Route("{flightNumber}")]
        public ActionResult<List<ISeat>> GetAvailableSeats(string flightNumber)
        {
            Flight flight = DataRepository.FlightsRepo.Flights.FirstOrDefault(f => f.FlightNumber.Equals(flightNumber));
            if (flight == null)
            {
                return BadRequest("Flight not found");
            }

            return SeatService.GetAvailableSeatsInFlight(flight);
        }

        
    }
}
