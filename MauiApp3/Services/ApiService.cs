using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using MauiApp3.Models;

namespace MauiApp3.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Esemeny>> GetEsemenyekAsync()
        {
            var response = await _httpClient.GetAsync("esemeny");
            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception($"Hiba történt: {response.StatusCode} - {error}");
            }

            var json = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            return JsonSerializer.Deserialize<List<Esemeny>>(json, options);
        }

        // Chat üzenetek lekérése
        public async Task<List<Chat>> GetChatMessagesAsync()
        {
            var response = await _httpClient.GetAsync("ChatMessage");
            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception($"Hiba történt: {response.StatusCode} - {error}");
            }

            var json = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            return JsonSerializer.Deserialize<List<Chat>>(json, options);
        }

        // Bejelentkezés
        public async Task<bool> LoginAsync(string username, string password)
        {
            var loginData = new { Felhasznalonev = username, Jelszo = password };
            var content = new StringContent(JsonSerializer.Serialize(loginData), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("bejelentkezes", content);

            if (!response.IsSuccessStatusCode)
            {
                return false;
            }

            return true;
        }

        // Kijelentkezés
        public async Task<bool> LogoutAsync(int felhasznaloId)
        {
            var logoutData = new { FelhasznaloId = felhasznaloId };
            var content = new StringContent(JsonSerializer.Serialize(logoutData), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("kijelentkezes", content);

            if (!response.IsSuccessStatusCode)
            {
                return false;
            }

            return true;
        }
    }
}