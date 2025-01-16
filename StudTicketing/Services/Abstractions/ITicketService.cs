using StudTicketing.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudTicketing.Services
{
    public interface ITicketService
    {
        Task<IEnumerable<Ticket>> GetAllTicketsAsync();
        Task<Ticket> GetTicketByIdAsync(int id);
        Task<Ticket> CreateTicketAsync(Ticket ticket);
        Task<Ticket> UpdateTicketAsync(Ticket ticket);
        Task DeleteTicketAsync(int id);
        Task<IEnumerable<Ticket>> GetTicketsByDepartmentAsync(string department);
        Task<IEnumerable<Ticket>> GetTicketsByStatusAsync(TicketStatus status);
    }

    public class TicketService : ITicketService
    {
        private readonly List<Ticket> _tickets;

        public TicketService()
        {
            _tickets = new List<Ticket>();
        }

        public async Task<IEnumerable<Ticket>> GetAllTicketsAsync()
        {
            return await Task.FromResult(_tickets);
        }

        public async Task<Ticket> GetTicketByIdAsync(int id)
        {
            return await Task.FromResult(_tickets.FirstOrDefault(t => t.Id == id));
        }

        public async Task<Ticket> CreateTicketAsync(Ticket ticket)
        {
            ticket.Id = _tickets.Count + 1;
            ticket.CreatedDate = DateTime.Now;
            ticket.Status = TicketStatus.New;
            _tickets.Add(ticket);
            return await Task.FromResult(ticket);
        }

        public async Task<Ticket> UpdateTicketAsync(Ticket ticket)
        {
            var existingTicket = _tickets.FirstOrDefault(t => t.Id == ticket.Id);
            if (existingTicket != null)
            {
                existingTicket.Title = ticket.Title;
                existingTicket.Description = ticket.Description;
                existingTicket.Department = ticket.Department;
                existingTicket.Status = ticket.Status;
                existingTicket.UpdatedDate = DateTime.Now;
            }
            return await Task.FromResult(existingTicket);
        }

        public async Task DeleteTicketAsync(int id)
        {
            var ticket = _tickets.FirstOrDefault(t => t.Id == id);
            if (ticket != null)
            {
                _tickets.Remove(ticket);
            }
            await Task.CompletedTask;
        }

        public async Task<IEnumerable<Ticket>> GetTicketsByDepartmentAsync(string department)
        {
            return await Task.FromResult(_tickets.Where(t => t.Department == department));
        }

        public async Task<IEnumerable<Ticket>> GetTicketsByStatusAsync(TicketStatus status)
        {
            return await Task.FromResult(_tickets.Where(t => t.Status == status));
        }
    }
}
