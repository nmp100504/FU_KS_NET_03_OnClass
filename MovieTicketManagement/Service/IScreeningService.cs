public interface IScreeningService
{
    Screening GetScreening(int id);
    IEnumerable<Screening> GetAllScreenings();
    IEnumerable<Screening> GetScreeningsByCinema(int cinemaId);
    IEnumerable<Screening> GetScreeningsByMovie(int movieId);
    Screening CreateScreening(Screening screening);
    Screening UpdateScreening(Screening screening);
    void DeleteScreening(int id);
    void CreateTicket(Ticket ticket);
    IEnumerable<Ticket> GetTicketsByPhoneNumber(string phoneNumber);
    void CancelTicket(int ticketId);

}