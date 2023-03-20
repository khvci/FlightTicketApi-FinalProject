using FlightTicketApi_FinalProject.Business;
using FlightTicketApi_FinalProject.DataRepository;
using FlightTicketApi_FinalProject.Entities.Abstracts;
using FlightTicketApi_FinalProject.Entities.Concretes;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FlightTicketApi_FinalProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TicketController : ControllerBase
    {
        /// <summary>
        /// Buy a new ticket with the specified seat
        /// </summary>
        /// <param name="seatSelection"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("buy")]
        public ActionResult<Ticket> BuyTicket([FromBody] SeatSelectionDTO seatSelection)
        {
            try
            {
                Flight flight = FlightsRepo.Flights.FirstOrDefault(f => f.FlightNumber.Equals(seatSelection.FlightNumber));

                if (flight == null)
                {
                    return BadRequest("Flight not found");
                }

                ISeat selectedSeat;
                string seatType = seatSelection.SeatRow <= flight.BusinessClassRows ? "BusinessSeat" : "RegularSeat";
                selectedSeat = flight.Seats.FirstOrDefault(
                    s => s.SeatRow == seatSelection.SeatRow && s.SeatColumn == seatSelection.SeatColumn && s.GetType().Name == seatType && s.IsAvailable);

                if (selectedSeat == null)
                {
                    return BadRequest($"Seat {seatSelection.SeatRow}{seatSelection.SeatColumn} is not available.");
                }

                Ticket ticket = TicketService.CreateTicketToBuySeat(flight, selectedSeat);
                return Ok(ticket);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }

        }

        /// <summary>
        /// Return a ticket and make it available again
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        
        [HttpDelete]
        [Route("return")]
        public ActionResult<string> ReturnTicket([FromBody] TicketReturnRequestDTO request)
        {
            try
            {
                if (TicketsRepo.Tickets == null)
                {
                    return BadRequest("Tickets repository is not initialized");
                }

                Ticket ticket = TicketsRepo.Tickets.FirstOrDefault(t => t.TicketToken.Equals(request.TicketToken));

                if (ticket == null)
                {
                    return BadRequest("Ticket not found");
                }

                TicketService.ReturnTicket(ticket);
                return Ok("Ticket returned successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Get all tickets from tickets repository
        /// </summary>
        /// <returns></returns>
        
        [HttpGet]
        [Route("all")]
        public ActionResult<List<Ticket>> GetAllTickets()
        {
            try
            {
                if (TicketsRepo.Tickets == null)
                {
                    return BadRequest("Tickets repository is not initialized");
                }
                return Ok(TicketsRepo.Tickets);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }

    public class SeatSelectionDTO
    {
        public string FlightNumber { get; set; }
        public int SeatRow { get; set; }
        public string SeatColumn { get; set; }
    }

    public class TicketReturnRequestDTO
    {
        public string TicketToken { get; set; }
    }
}
