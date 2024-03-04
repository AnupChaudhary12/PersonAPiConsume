
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Options;
using PersonApiConsume.Models;
using PersonApiConsume.Services.Implementation;
using PersonAPiConsume.Models;
using System.Net;
using System.Text.Json;

namespace PersonApiConsume.Services.Interface
{
    public class PersonApiService : IPersonApiService
    {
        private readonly HttpClient _httpClient;
        private readonly PersonApiOptions _personApiOptions;
        private readonly IConfiguration _configuration;
        //private readonly IMapper _mapper;

        public PersonApiService(HttpClient httpClient, IOptions<PersonApiOptions> options, IConfiguration configuration
            /*,IMapper mapper*/)
        {
            _httpClient = httpClient;
            _personApiOptions = options.Value;
            //_configuration = configuration;
            //_mapper = mapper;
        }

        public async Task<List<Person>> GetPersons()
        {
            string url = _personApiOptions.BaseUrl+ "GetAllPerson";
            //string url = _configuration.GetSection("PersonApiOptions")["BaseUrl"];
            var result = new List<Person>();
            var response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var stringResponse = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Response Content: " + stringResponse);
                try
                {
                    result = JsonSerializer.Deserialize<List<Person>>(stringResponse);
                }
                catch (JsonException ex)
                {
                    throw new JsonException("Error deserializing response content.", ex);
                }

            }
            else
            {
                Console.WriteLine("HTTP Status Code: " + response.StatusCode);
                throw new HttpRequestException(response.ReasonPhrase);
            }
            return result;
        }

        public async Task<Person> GetPersonById(int id)
        {

            string url = _personApiOptions.BaseUrl +"GetPersonById"+"/"+ id;
            var result = new Person();
            var response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var stringResponse = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Response Content: " + stringResponse);
                try
                {
                    result = JsonSerializer.Deserialize<Person>(stringResponse);
                }
                catch (JsonException ex)
                {
                    throw new JsonException("Error deserializing response content.", ex);
                }
            }
            else if (response.StatusCode == HttpStatusCode.NotFound)
            {
                //Console.WriteLine("HTTP Status Code: " + response.StatusCode);
                //throw new HttpRequestException("Person not found");

                Console.WriteLine("Person with provided id not found");
                return null;
            }
            else
            {
                Console.WriteLine("HTTP Status Code: " + response.StatusCode);
                throw new HttpRequestException(response.ReasonPhrase);
            }
            return result;
        }

        public async Task<bool> AddPerson(Person person)
        {
            try
            {
                string url = _personApiOptions.BaseUrl + "AddPerson";
                var response = await _httpClient.PostAsJsonAsync(url, person);

                if (response.IsSuccessStatusCode)
                {
                    var stringResponse = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("Response Content: " + stringResponse);
                    return true;
                }
                else
                {
                    Console.WriteLine("HTTP Status Code: " + response.StatusCode);
                    throw new HttpRequestException(response.ReasonPhrase);
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine("HTTP Request Exception: " + ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                throw;
            }
        }

        public async Task<bool> DeletePerson(int id)
        {
            string url = _personApiOptions.BaseUrl+"DeletePerson"+"/"+ id;
            var response = await _httpClient.DeleteAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var stringResponse = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Response Content: " + stringResponse);
                return true;
                
            }
            else if (response.StatusCode == HttpStatusCode.NotFound)
            {
                Console.WriteLine("Person with provided id not found");
                return false;
            }
            else
            {
                Console.WriteLine("HTTP Status Code: " + response.StatusCode);
                throw new HttpRequestException(response.ReasonPhrase);
            }
        }

        public async Task<bool> UpdatePerson(/*int id,*/ Person person)
        {
            try
            {
                string url = $"{_personApiOptions.BaseUrl}UpdatePerson";
                var response = await _httpClient.PutAsJsonAsync(url,person);

                if (response.IsSuccessStatusCode)
                {
                    var stringResponse = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("Response Content: " + stringResponse);
                    return true;
                }
                else
                {
                    Console.WriteLine("HTTP Status Code: " + response.StatusCode);
                    throw new HttpRequestException(response.ReasonPhrase);
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine("HTTP Request Exception: " + ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                throw;
            }
        }
    }
}
