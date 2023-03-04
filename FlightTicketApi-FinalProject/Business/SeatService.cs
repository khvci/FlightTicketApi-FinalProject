using FlightTicketApi_FinalProject.Entities.Abstracts;
using FlightTicketApi_FinalProject.Entities.Concretes;
using System.Collections.Generic;
using System.Linq;

namespace FlightTicketApi_FinalProject.Business
{
    public class SeatService
    {
        public static List<ISeat> CreateSeatsInFlight(int planeType, int businessClassRows, PlaneConfiguration[] _configurations, ColumnCharacters[] _columnCharacters)
        {
            int _planeCapacity = (int)_configurations[planeType];
            int _maxSeatsInBusinessRows = 4;
            int _maxSeatsInRegularRows = 6;

            List<ISeat> Seats = new List<ISeat>();

            for (int i = 1; i <= businessClassRows; i++)
            {
                for (int j = 0; j < _maxSeatsInBusinessRows; j++)
                {
                    ISeat businessSeat = new BusinessSeat(i, _columnCharacters[j].ToString());
                    Seats.Add(businessSeat);
                }
            }

            for (int i = ++businessClassRows; i <= _planeCapacity; i++)
            {
                for (int j = 0; j < _maxSeatsInRegularRows; j++)
                {
                    ISeat regularSeat = new RegularSeat(i, _columnCharacters[j].ToString());
                    Seats.Add(regularSeat);
                }
            }

            return Seats;
        }

        public static List<ISeat> GetAvailableSeatsInFlight(Flight flight)
        {
            List<ISeat> availableSeats = flight.Seats.Where(s => s.IsAvailable).ToList();

            return availableSeats;
        }
    }
}
