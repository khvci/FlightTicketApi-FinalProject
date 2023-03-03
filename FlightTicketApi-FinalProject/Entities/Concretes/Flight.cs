﻿using FlightTicketApi_FinalProject.Entities.Abstracts;
using FlightTicketApi_FinalProject.Helpers;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace FlightTicketApi_FinalProject.Entities.Concretes
{
    public class Flight
    {
        public string FlightNumber { get; set; }
        public PlaneConfiguration Plane { get; set; }
        public Destination Departure { get; set; }
        public Destination Arrival { get; set; }
        public DateTime FlightTime { get; set; }
        public int BusinessClassRows { get; set; }
        public List<ISeat> Seats { get; set; }
        public Flight()
        {
        }

        public Flight(string flightNumber, PlaneConfiguration planeConfiguration, Destination departure, Destination arrival, DateTime flightTime, int businessClassRows)
        {
            FlightNumber = flightNumber ?? throw new ArgumentNullException(nameof(flightNumber));
            Plane = planeConfiguration;
            Departure = departure;
            Arrival = arrival;
            FlightTime = flightTime;
            BusinessClassRows = businessClassRows;

            Seats = new List<ISeat>();
            var _columnCharacters = (ColumnCharacters[])Enum.GetValues(typeof(ColumnCharacters));
            int _planeCapacity = (int)planeConfiguration;
            int _maxSeatsInBusinessRows = 4;
            int _maxSeatsInRegularRows = 6;
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
        }
    }

    public enum Destination 
    { 
        Istanbul, London, Berlin, Madrid
    }

    public enum PlaneConfiguration 
    {
        // maximum seat rows (6 seats on each row) of planes
        AirbusA310 = 27,
        AirbusA320 = 30,
        Boeing737 = 32,
        Boeing747 = 36
}


}