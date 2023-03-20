namespace FlightTicketApi_FinalProject.Entities.Abstracts
{
    public interface ISeat
    {
        public int SeatRow { get; set; }
        public string SeatColumn { get; set; }
        public bool IsAvailable { get; set; }
        public double Price { get; set; }
    }

    /// <summary>
    /// Represents the characters used to label columns in a flight.
    /// </summary>
    public enum ColumnCharacters
    {
        A, B, C, D, E, F,
    }
}
