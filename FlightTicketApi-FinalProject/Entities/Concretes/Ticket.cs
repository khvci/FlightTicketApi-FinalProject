using FlightTicketApi_FinalProject.Entities.Abstracts;
using System;
using System.Collections.Generic;

namespace FlightTicketApi_FinalProject.Entities.Concretes
{
    public class Ticket
    {
        public string TicketToken { get; set; }
        public string FlightNumber { get; set; }
        public string Departure { get; set; }
        public string Arrival { get; set; }
        public DateTime FlightTime { get; set; }
        public ISeat SelectedSeat { get; set; }
        public double price { get; set; }
        public Ticket() { }
        public Ticket(Flight flight, ISeat selectedSeat, double price)
        {
            this.TicketToken = Guid.NewGuid().ToString();
            this.FlightNumber = flight.FlightNumber;
            this.Departure = flight.DepartureCity;
            this.Arrival = flight.ArrivalCity;
            this.FlightTime = flight.FlightTime;
            this.price = price;
            this.SelectedSeat = selectedSeat;
        }
    }
}