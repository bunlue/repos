using BlazorMovieWA.Shared.DTOs;
using BlazorMovieWA.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorMovieWA.Client.Repository
{
    public interface IPersonRepository
    {
        Task CreatePerson(Person person);
        Task<List<Person>>GetPeopleByName(string name);
        Task<PaginatedResponse<List<Person>>> GetPeople(PaginationDTO paginationDTO);
        Task<Person> GetPersonById(int id);
        Task UpdatePerson(Person person);
        Task DeletePerson(int Id);
    }
}
