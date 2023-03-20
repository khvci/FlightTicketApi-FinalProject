using FlightTicketApi_FinalProject.DataRepository;
using FlightTicketApi_FinalProject.Entities.Abstracts;
using FlightTicketApi_FinalProject.Entities.Concretes;

namespace FlightTicketApi_FinalProject.Business
{
    public static class TicketService
    {
        /// <summary>
        /// Creates a ticket to buy a seat on a flight.
        /// </summary>
        /// <param name="flight">The flight to buy a seat on.</param>
        /// <param name="selectedSeat">The selected seat to buy.</param>
        /// <returns>The created ticket.</returns>
        public static Ticket CreateTicketToBuySeat(Flight flight, ISeat selectedSeat)
        {
            selectedSeat.IsAvailable = false;
            double price = selectedSeat.Price;
            Ticket ticket = new Ticket(flight, selectedSeat, price);
            TicketsRepo.Tickets.Add(ticket);
            return ticket;
        }

        /// <summary>
        /// Returns a ticket and makes the seat available again.
        /// </summary>
        /// <param name="ticket">The ticket to return.</param>
        public static void ReturnTicket(Ticket ticket)
        {
            ISeat seat = ticket.SelectedSeat;
            seat.IsAvailable = true;
            TicketsRepo.Tickets.Remove(ticket);
        }
    }
}
