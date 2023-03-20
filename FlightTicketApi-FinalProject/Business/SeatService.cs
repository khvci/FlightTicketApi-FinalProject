using FlightTicketApi_FinalProject.DataRepository;
using FlightTicketApi_FinalProject.Entities.Abstracts;
using FlightTicketApi_FinalProject.Entities.Concretes;
using System.Collections.Generic;
using System.Linq;

namespace FlightTicketApi_FinalProject.Business
{
    public class SeatService
    {
        /// <summary>
        /// Creates a list of seats for a flight.
        /// </summary>
        /// <param name="planeType">The type of plane.</param>
        /// <param name="businessClassRows">The number of rows in business class.</param>
        /// <param name="configurations">An array of plane configurations.</param>
        /// <param name="columnCharacters">An array of column characters.</param>
        /// <returns>A list of seats for the flight.</returns>
        public static List<ISeat> CreateSeatsInFlight(int planeType, int businessClassRows, PlaneConfiguration[] configurations, ColumnCharacters[] columnCharacters)
        {
            int _planeCapacity = (int)configurations[planeType];
            int _maxSeatsInBusinessRows = 4;
            int _maxSeatsInRegularRows = 6;

            List<ISeat> Seats = new List<ISeat>();

            for (int i = 1; i <= businessClassRows; i++)
            {
                for (int j = 0; j < _maxSeatsInBusinessRows; j++)
                {
                    ISeat businessSeat = new BusinessSeat(i, columnCharacters[j].ToString());
                    Seats.Add(businessSeat);
                }
            }

            for (int i = ++businessClassRows; i <= _planeCapacity; i++)
            {
                for (int j = 0; j < _maxSeatsInRegularRows; j++)
                {
                    ISeat regularSeat = new RegularSeat(i, columnCharacters[j].ToString());
                    Seats.Add(regularSeat);
                }
            }

            return Seats;
        }

        /// <summary>
        /// Gets a list of available seats for a flight.
        /// </summary>
        /// <param name="flight">The flight to get available seats for.</param>
        /// <returns>A list of available seats for the flight.</returns>
        public static List<ISeat> GetAvailableSeatsInFlight(Flight flight)
        {
            List<ISeat> availableSeats = flight.Seats.Where(s => s.IsAvailable).ToList();

            return availableSeats;
        }
    }
}
