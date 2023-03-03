using FlightTicketApi_FinalProject.Entities.Abstracts;
using FlightTicketApi_FinalProject.Entities.Concretes;
using Microsoft.AspNetCore.Mvc;
using System;

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

    }
}
