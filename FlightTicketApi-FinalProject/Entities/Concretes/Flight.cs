﻿using FlightTicketApi_FinalProject.Business;
using FlightTicketApi_FinalProject.Controllers;
using FlightTicketApi_FinalProject.Entities.Abstracts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace FlightTicketApi_FinalProject.Entities.Concretes
{
    public class Flight
    {
        public string FlightNumber { get; set; }
        public string PlaneName { get; set; }
        public string DepartureCity { get; set; }
        public string ArrivalCity { get; set; }
        public DateTime FlightTime { get; set; }
        public int BusinessClassRows { get; set; }
        public List<ISeat> Seats { get; set; }
        public int PlaneType { get; set; }
        public int DepartureCityId { get; set; }
        public int ArrivalCityId { get; set; }
        [JsonIgnore]
        PlaneConfiguration[] configurations;
        [JsonIgnore]
        Destination[] destinations;
        [JsonIgnore]
        ColumnCharacters[] columnCharacters;

        public Flight()
        {
        }

        public Flight(string flightNumber, int planeType, int departureCityId, int arrivalCityId, DateTime flightTime, int businessClassRows)
        {

            configurations = (PlaneConfiguration[])Enum.GetValues(typeof(PlaneConfiguration));
            destinations = (Destination[])Enum.GetValues(typeof(Destination));
            columnCharacters = (ColumnCharacters[])Enum.GetValues(typeof(ColumnCharacters));

            FlightNumber = flightNumber;
            PlaneType = planeType;
            PlaneName = configurations[planeType].ToString();
            DepartureCityId = departureCityId;
            DepartureCity = destinations[departureCityId].ToString();
            ArrivalCityId = arrivalCityId;
            ArrivalCity = destinations[arrivalCityId].ToString();
            FlightTime = flightTime;
            BusinessClassRows = businessClassRows;

            Seats = SeatService.CreateSeatsInFlight(planeType, businessClassRows, configurations, columnCharacters);
        }

        public Flight(FlightRequestDTO flightRequest) : this(
            flightRequest.FlightNumber, flightRequest.PlaneType,
            flightRequest.DepartureCityId, flightRequest.ArrivalCityId,
            flightRequest.FlightTime, flightRequest.BusinessClassRows)
        {
        }
    }

    /// <summary>
    /// Represents the destinations for a flight.
    /// </summary>
    public enum Destination 
    { 
        Istanbul, London, Berlin, Madrid, Moscow, Dubai, Washington
    }

    /// <summary>
    /// Represents the maximum seat rows for different plane configurations.
    /// </summary>
    public enum PlaneConfiguration 
    {
        // maximum seat rows (6 seats on each row) of planes
        AirbusA310 = 27,
        AirbusA320 = 30,
        Boeing737 = 32,
        Boeing747 = 36
    }
}
