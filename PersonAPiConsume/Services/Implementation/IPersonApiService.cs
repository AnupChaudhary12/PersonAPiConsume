
using Microsoft.Extensions.Options;
using PersonAPiConsume.Models;


namespace PersonApiConsume.Services.Implementation
{
    public interface IPersonApiService
    {
        Task<List<Person>> GetPersons();
        Task<Person> GetPersonById(int id);
        Task<bool>AddPerson(Person person);
        Task<bool> UpdatePerson(/*int id,*/Person person);
        Task<bool> DeletePerson(int id);
    }
}
