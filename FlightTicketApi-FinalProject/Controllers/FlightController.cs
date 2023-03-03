using FlightTicketApi_FinalProject.Business;
using FlightTicketApi_FinalProject.Entities.Concretes;
using Microsoft.AspNetCore.Mvc;

namespace FlightTicketApi_FinalProject.Controllers
{
    [Route("api/[controller]")]
    public class FlightController : ControllerBase
    {
        [HttpPost]
        public Flight CreateFlight([FromBody] Flight flight)
        {
            return FlightService.CreateFlightFromJson(flight.FlightNumber, flight.PlaneType, flight.DepartureCityId, flight.ArrivalCityId, flight.FlightTime, flight.BusinessClassRows);
        }
    }
}
