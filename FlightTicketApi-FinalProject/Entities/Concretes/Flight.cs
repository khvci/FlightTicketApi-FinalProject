using FlightTicketApi_FinalProject.Business;
using FlightTicketApi_FinalProject.Entities.Abstracts;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

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
        public Flight()
        {
        }


        public Flight(string flightNumber, int planeType, int departureCityId, int arrivalCityId, DateTime flightTime, int businessClassRows)
        {

            PlaneConfiguration[] _configurations = (PlaneConfiguration[])Enum.GetValues(typeof(PlaneConfiguration));
            Destination[] _destinations = (Destination[])Enum.GetValues(typeof(Destination));
            ColumnCharacters[] _columnCharacters = (ColumnCharacters[])Enum.GetValues(typeof(ColumnCharacters));

            FlightNumber = flightNumber;
            PlaneType = planeType;
            PlaneName = _configurations[planeType].ToString();
            DepartureCityId = departureCityId;
            DepartureCity = _destinations[departureCityId].ToString();
            ArrivalCityId = arrivalCityId;
            ArrivalCity = _destinations[arrivalCityId].ToString();
            FlightTime = flightTime;
            BusinessClassRows = businessClassRows;

            Seats = SeatService.CreateSeatsInFlight(planeType, businessClassRows, _configurations, _columnCharacters);
        }

        
    }

    public enum Destination 
    { 
        Istanbul, London, Berlin, Madrid, Moscow, Dubai, Washington
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
