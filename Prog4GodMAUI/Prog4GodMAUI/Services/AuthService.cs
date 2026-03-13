using System;
using System.Linq;
using Prog4GodMAUI.Models;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Diagnostics;

namespace Prog4GodMAUI.Services
{
    public class AuthService : BaseService
    {
        public AuthService()
        {
        }

        public bool IsUserLoggedIn
        {
            get
            {
                var token = SecureStorage.GetAsync("token").Result;
                return !string.IsNullOrEmpty(token);
            }

            set
            {
                if (!value)
                {
                    SecureStorage.Remove("token");
                    SecureStorage.Remove("userId");
                }
            }
        }
        public async Task<LoginResponse> LoginAsync(string username, string password)
        {
            var request = new LoginRequest
            {
                Username = username,
                Password = password,
            };

            var content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("auth/login", content);

            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            var loginResponse = JsonSerializer.Deserialize<LoginResponse>(responseContent);

            var usersResponse = await _httpClient.GetAsync("users");
            usersResponse.EnsureSuccessStatusCode();
            var usersResponseContent = await usersResponse.Content.ReadAsStringAsync();
            var users = JsonSerializer.Deserialize<List<User>>(usersResponseContent);

            var user = users.FirstOrDefault(u => u.Username == username);
            if (user != null)
            {
                loginResponse.UserId = user.Id;
            }

            return loginResponse;
        }
    }
}
