
using ArtistClient.Models;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ArtistClient
{
    internal class Program
    {
        static readonly HttpClient client = new HttpClient();
        static async Task Main(string[] args)
        {
            var songs = await GetSongsAsync("https://localhost:7146/songs");
            foreach (var song in songs)
            {
                Console.WriteLine($"Title: {song.Title}, Artist: {song.Artists.ArtistName}, Genre: {song.Genres.Name}");
            }
            //await GetArtistsAsync();
            //await AddArtistAsync(new Artist { ArtistName = "Reidar"});
        }

        static async Task GetArtistsAsync()
        {
            HttpResponseMessage response = await client.GetAsync("https://localhost:7146/artists");
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            Console.WriteLine(responseBody);
        }
        static async Task<List<Song>> GetSongsAsync(string url)
        {
            using (var client = new HttpClient())
            {
 
                var response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync();
                //return JsonSerializer.Deserialize<List<Song>>(json);
                //return System.Text.Json.JsonSerializer.Deserialize<List<Song>>(json);
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    ReferenceHandler = ReferenceHandler.Preserve
                };
                return JsonSerializer.Deserialize<List<Song>>(json, options);

            }
        }
        static async Task AddArtistAsync(Artist artist)
        {
            try
            {
                var json = JsonSerializer.Serialize(artist);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync("https://localhost:7096/artists", content);
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine(responseBody);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }
        }
    }
}
