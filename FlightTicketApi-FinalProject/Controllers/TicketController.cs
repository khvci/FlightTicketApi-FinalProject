using FlightTicketApi_FinalProject.DataRepository;
using FlightTicketApi_FinalProject.Entities.Abstracts;
using FlightTicketApi_FinalProject.Entities.Concretes;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace FlightTicketApi_FinalProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TicketController : ControllerBase
    {
        [HttpPost]
        [Route("buy-ticket")]
        public ActionResult<Ticket> BuyTicket([FromBody] SeatSelection seatSelection)
        {
            Flight flight = FlightsRepo.Flights.FirstOrDefault(f => f.FlightNumber.Equals(seatSelection.FlightNumber));
            if (flight == null)
            {
                return BadRequest("Flight not found");
            }

            int maxBusinessRows = flight.BusinessClassRows;
            int seatRow = seatSelection.SeatRow;
            string seatColumn = seatSelection.SeatColumn;

            if (seatRow <= maxBusinessRows)
            {
                ISeat selectedSeat = flight.Seats.FirstOrDefault(s => s.SeatRow == seatRow && s.SeatColumn == seatColumn && s is BusinessSeat && s.IsAvailable);
                if (selectedSeat == null)
                {
                    return BadRequest($"Seat {seatRow}{seatColumn} is not available in Business Class");
                }

                selectedSeat.IsAvailable = false;
                double price = selectedSeat.Price;
                Ticket ticket = new Ticket(flight, selectedSeat, price);
                TicketsRepo.Tickets.Add(ticket);
                return Ok(ticket);
            }
            else
            {
                ISeat selectedSeat = flight.Seats.FirstOrDefault(s => s.SeatRow == seatRow && s.SeatColumn == seatColumn && s is RegularSeat && s.IsAvailable);
                if (selectedSeat == null)
                {
                    return BadRequest($"Seat {seatRow}{seatColumn} is not available in Regular Class");
                }

                selectedSeat.IsAvailable = false;
                double price = selectedSeat.Price;
                Ticket ticket = new Ticket(flight, selectedSeat, price);
                TicketsRepo.Tickets.Add(ticket);
                return Ok(ticket);
            }
        }

        [HttpPost]
        [Route("return-ticket")]
        public ActionResult<string> ReturnTicket([FromBody] TicketReturnRequest request)
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

            ISeat seat = ticket.SelectedSeat;
            seat.IsAvailable = true;
            TicketsRepo.Tickets.Remove(ticket);

            return Ok("Ticket returned successfully");
        }

        [HttpGet]
        [Route("all-tickets")]
        public ActionResult<List<Ticket>> GetAllTickets()
        {
            if (TicketsRepo.Tickets == null)
            {
                return BadRequest("Tickets repository is not initialized");
            }
            return Ok(TicketsRepo.Tickets);
        }
    }


    public class SeatSelection
    {
        public string FlightNumber { get; set; }
        public int SeatRow { get; set; }
        public string SeatColumn { get; set; }
    }

    public class TicketReturnRequest
    {
        public string TicketToken { get; set; }
    }
}
