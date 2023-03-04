using FlightTicketApi_FinalProject.DataRepository;
using FlightTicketApi_FinalProject.Entities.Abstracts;
using FlightTicketApi_FinalProject.Entities.Concretes;

namespace FlightTicketApi_FinalProject.Business
{
    public static class TicketService
    {
        public static Ticket CreateTicketToBuySeat(Flight flight, ISeat selectedSeat)
        {
            selectedSeat.IsAvailable = false;
            double price = selectedSeat.Price;
            Ticket ticket = new Ticket(flight, selectedSeat, price);
            TicketsRepo.Tickets.Add(ticket);
            return ticket;
        }

        public static void ReturnTicket(Ticket ticket)
        {
            ISeat seat = ticket.SelectedSeat;
            seat.IsAvailable = true;
            TicketsRepo.Tickets.Remove(ticket);
        }
    }
}
