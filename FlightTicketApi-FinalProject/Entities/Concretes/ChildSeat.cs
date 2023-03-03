using FlightTicketApi_FinalProject.Entities.Abstracts;
using FlightTicketApi_FinalProject.Helpers;

namespace FlightTicketApi_FinalProject.Entities.Concretes
{
    public class ChildSeat : ISeat
    {
        public int SeatRow { get; set; }
        public string SeatColumn { get; set; }
        public bool IsAvailable { get; set; }
        public double Price { get; set; }

        public ChildSeat()
        {
            this.IsAvailable = true;
            this.Price = SeatPrices.Child;
        }

        public ChildSeat(int seatRow, string seatColumn)
        {
            this.SeatRow = seatRow;
            this.SeatColumn = seatColumn;
        }
    }
}
