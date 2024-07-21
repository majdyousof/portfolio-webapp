using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace WebAppServer.Services
{
    /// <summary>
    /// Represents the interface for the TFL (Transport for London) service.
    /// </summary>
    public interface ITflService
    {
        /// <summary>
        /// Retrieves accident data from the TFL API.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains the accident data as a string.</returns>
        Task<string> GetAccidentData();
    
        /// <summary>
        /// Retrieves bike point data from the TFL API.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains the bike point data as a string.</returns>
        Task<string> GetBikePoint();
    }

    /// <summary>
    /// Represents the implementation of the TFL (Transport for London) service.
    /// </summary>
    public class TflService : ITflService
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;
        string _apiKey;

        /// <summary>
        /// Initializes a new instance of the <see cref="TflService"/> class.
        /// </summary>
        /// <param name="configuration">The configuration object used to retrieve the API key.</param>
        /// <param name="httpClient">The HTTP client used to make API requests.</param>
        public TflService(IConfiguration configuration, HttpClient httpClient)
        {
            _configuration = configuration;
            _apiKey = _configuration["ApiKeys:Tfl"];
            _httpClient = httpClient;
        }

        /// <summary>
        /// Retrieves accident data from the TFL API.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains the accident data as a string.</returns>
        public async Task<string> GetAccidentData()
        {
            // Make API request using the HttpClient
            _httpClient.DefaultRequestHeaders.Add("app_key", _apiKey);
            HttpResponseMessage response = await _httpClient.GetAsync("https://api.tfl.gov.uk/AccidentStats/2016");

            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                return data;
            }
            else
            {
                throw new Exception("Failed to retrieve data from API.");
            }
        }

        /// <summary>
        /// Retrieves bike point data from the TFL API.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains the bike point data as a string.</returns>
        public async Task<string> GetBikePoint()
        {
            // Make API request using the HttpClient
            _httpClient.DefaultRequestHeaders.Add("app_key", _apiKey);
            HttpResponseMessage response = await _httpClient.GetAsync("https://api.tfl.gov.uk/BikePoint");

            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                return data;
            }
            else
            {
                throw new Exception("Failed to retrieve data from API.");
            }
        }
    }
}
