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
        /// Buys a ticket for a given seat selection.
        /// </summary>
        /// <param name="seatSelection">The seat selection data transfer object.</param>
        /// <returns>An ActionResult of type Ticket.</returns>
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
        /// Returns a ticket.
        /// </summary>
        /// <param name="request">The ticket return request data transfer object.</param>
        /// <returns>An ActionResult of type string.</returns>
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
        /// Gets all tickets from the repository.
        /// </summary>
        /// <returns>An ActionResult of type List of Ticket.</returns>
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

    /// <summary>
    /// A data transfer object representing a seat selection.
    /// </summary>
    public class SeatSelectionDTO
    {
        public string FlightNumber { get; set; }
        public int SeatRow { get; set; }
        public string SeatColumn { get; set; }
    }

    /// <summary>
    /// A data transfer object representing a ticket return request.
    /// </summary>
    public class TicketReturnRequestDTO
    {
        public string TicketToken { get; set; }
    }
}
