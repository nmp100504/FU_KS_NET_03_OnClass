using System;
using System.Collections.Generic;
using System.Linq;

public class ScreeningService : IScreeningService
{
    private List<Screening> screenings = new List<Screening>();
    private List<Ticket> tickets = new List<Ticket>();
    private int nextScreeningId = 1;
    private int nextTicketId = 1;

    public Screening GetScreening(int id)
    {
        return screenings.FirstOrDefault(s => s.Id == id);
    }

    public IEnumerable<Screening> GetAllScreenings()
    {
        return screenings;
    }

    public IEnumerable<Screening> GetScreeningsByCinema(int cinemaId)
    {
        return screenings.Where(s => s.ScreeningRoom != null && s.ScreeningRoom.CinemaId == cinemaId);
    }

    public IEnumerable<Screening> GetScreeningsByMovie(int movieId)
    {
        return screenings.Where(s => s.MovieId == movieId);
    }

    public Screening CreateScreening(Screening screening)
    {
        screening.Id = nextScreeningId++;
        screenings.Add(screening);
        return screening;
    }

    public Screening UpdateScreening(Screening screening)
    {
        var existingScreening = screenings.FirstOrDefault(s => s.Id == screening.Id);
        if (existingScreening != null)
        {
            existingScreening.MovieId = screening.MovieId;
            existingScreening.ScreeningRoom = screening.ScreeningRoom;
            existingScreening.ShowDateTime = screening.ShowDateTime;
        }
        return existingScreening;
    }

    public void DeleteScreening(int id)
    {
        screenings.RemoveAll(s => s.Id == id);
    }

    public void CreateTicket(Ticket ticket)
    {
        ticket.Id = nextTicketId++;
        tickets.Add(ticket);
    }

    public IEnumerable<Ticket> GetTicketsByPhoneNumber(string phoneNumber)
    {
        return tickets.Where(t => t.PhoneNumber == phoneNumber);
    }

    public void CancelTicket(int ticketId)
    {
        tickets.RemoveAll(t => t.Id == ticketId);
    }
}