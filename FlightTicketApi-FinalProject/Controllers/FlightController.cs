using FlightTicketApi_FinalProject.Business;
using FlightTicketApi_FinalProject.DataRepository;
using FlightTicketApi_FinalProject.Entities.Abstracts;
using FlightTicketApi_FinalProject.Entities.Concretes;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FlightTicketApi_FinalProject.Controllers
{
    [Route("api/[controller]")]
    public class FlightController : ControllerBase
    {
        /// <summary>
        /// Create a flight and add it to flights repository
        /// </summary>
        /// <param name="flightRequest"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<Flight> CreateFlight([FromBody] FlightRequestDTO flightRequest)
        {
            try
            {
                if (FlightsRepo.Flights.Exists(f => f.FlightNumber == flightRequest.FlightNumber))
                {
                    return BadRequest($"Flight with flight number {flightRequest.FlightNumber} already exists.");
                }

                Flight flight = new Flight(flightRequest);
                FlightService.AddFlightToRepository(flight);

                return Ok(flight);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Get all available seats in a specified flight
        /// </summary>
        /// <param name="flightNumber"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{flightNumber}")]
        public ActionResult<List<ISeat>> GetAvailableSeats(string flightNumber)
        {
            try
            {
                Flight flight = FlightsRepo.Flights.FirstOrDefault(f => f.FlightNumber.Equals(flightNumber));
                if (flight == null)
                {
                    return BadRequest("Flight not found");
                }

                return Ok(SeatService.GetAvailableSeatsInFlight(flight));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Get all flights
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("all")]
        public ActionResult<List<FlightResponseDTO>> GetAllFlights()
        {
            try
            {
                if (FlightsRepo.Flights == null)
                {
                    return BadRequest("Flights repository is not initialized");
                }

                List<FlightResponseDTO> allFlightDTO = FlightsRepo.Flights
                    .Select(f => new FlightResponseDTO(f)).ToList();

                return Ok(allFlightDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Deletes an existing flight
        /// </summary>
        /// <param name="flightNumber"></param>
        /// <returns></returns>
        
        [HttpDelete]
        [Route("delete/{flightNumber}")]
        public ActionResult<string> DeleteFlight(string flightNumber)
        {
            try
            {
                Flight flight = FlightsRepo.Flights.FirstOrDefault(f => f.FlightNumber.Equals(flightNumber));
                if (flight == null)
                {
                    return BadRequest("Flight not found");
                }
                FlightsRepo.Flights.Remove(flight);
                TicketsRepo.Tickets.RemoveAll(t => t.FlightNumber.Equals(flight.FlightNumber));
                return Ok($"Flight {flightNumber} has been deleted");

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }

    public class FlightRequestDTO
    {
        public string FlightNumber { get; set; }
        public int PlaneType { get; set; }
        public int DepartureCityId { get; set; }
        public int ArrivalCityId { get; set; }
        public DateTime FlightTime { get; set; }
        public int BusinessClassRows { get; set; }

        
    }

    public class FlightResponseDTO
    {
        public string FlightNumber { get; set; }
        public string PlaneName { get; set; }
        public string DepartureCity { get; set; }
        public string ArrivalCity { get; set; }
        public DateTime FlightTime { get; set; }
        public int BusinessClassRows { get; set; }

        public FlightResponseDTO()
        {
        }

        public FlightResponseDTO(string flightNumber, string planeName, string departureCity, string arrivalCity, DateTime flightTime, int businessClassRows)
        {
            this.FlightNumber = flightNumber;
            this.PlaneName = planeName;
            this.DepartureCity = departureCity;
            this.ArrivalCity = arrivalCity;
            this.FlightTime = flightTime;
            this.BusinessClassRows = businessClassRows;
        }

        public FlightResponseDTO(Flight flight) :
            this(flight.FlightNumber, flight.PlaneName, flight.DepartureCity, flight.ArrivalCity, flight.FlightTime, flight.BusinessClassRows)
        {
        }
    }
}
