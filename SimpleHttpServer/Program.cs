using System;
using System.Net.Http;
using System.Threading.Tasks;
class SimpleHttpClient
{
    static async Task Main()
    {
        var client = new HttpClient();
        try
        {
            // 1. Enviar GET a la URL del servidor  
            var url = "http://localhost:5000/saludo?usuario=SEB";
            Console.WriteLine($"→ Enviando petición GET a {url}");
            var response = await client.GetAsync(url);
            // 2. Leer el contenido de la respuesta  
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            // 3. Mostrarlo por consola  
            Console.WriteLine("← Respuesta del servidor:");
            Console.WriteLine(content);
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"¡Error en la petición! {ex.Message}");
        }
    }
}