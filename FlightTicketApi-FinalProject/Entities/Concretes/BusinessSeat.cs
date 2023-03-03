using FlightTicketApi_FinalProject.Entities.Abstracts;
using FlightTicketApi_FinalProject.Helpers;

namespace FlightTicketApi_FinalProject.Entities.Concretes
{
    public class BusinessSeat : ISeat
    {
        public int SeatRow { get; set; }
        public string SeatColumn { get; set; }
        public bool IsAvailable { get; set; }
        public double Price { get; set; }

        public BusinessSeat()
        {
            this.IsAvailable = true;
            this.Price = SeatPrices.Business;
        }

        public BusinessSeat(int seatRow, string seatColumn) : this()
        {
            this.SeatRow = seatRow;
            this.SeatColumn = seatColumn;
        }
    }
}
