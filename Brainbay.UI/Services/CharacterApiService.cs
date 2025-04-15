using Brainbay.UI.Models;
using System.Net.Http.Json;

namespace Brainbay.UI.Services
{
    public class CharacterApiService
    {
        private readonly HttpClient _http;

        public CharacterApiService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<Character>> GetCharactersAsync()
        {
            return await _http.GetFromJsonAsync<List<Character>>("https://localhost:7129/api/Character");
        }
    }
}
