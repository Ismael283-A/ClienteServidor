# Cliente Servidor
Servidor y cliente HTTP en C#  

Este proyecto consiste en una aplicación de consola en C# que implementa un servidor HTTP básico utilizando HttpListenery un cliente HTTP que se comunica con el servidor usando HttpClient. El servidor escucha solicitudes http://localhost:5000/y responde con un mensaje en formato JSON.  

Estructura del Proyecto  

El proyecto está dividido en dos partes principales:  

Servidor HTTP: Implementado en la clase SimpleHttpServer.  

Cliente HTTP: Implementado en la clase SimpleHttpClient.  

Cómo ejecutar el servidor  

Abre una terminal y navega al directorio del proyecto.  

Compila y ejecuta el servidor con el siguiente comando:  

    dotnet run --project SimpleHttpServer  
    
El servidor comenzará a escuchar en http://localhost:5000/.  
 
Cómo ejecutar al cliente  

Abre otra terminal y navega al directorio del proyecto.  

Compila y ejecuta el cliente con el siguiente comando:  

    dotnet run --project SimpleHttpClient  
   
#Código del servidor  

    using System;
    using System.Net;
    using System.Text;
    using System.Threading.Tasks;

    class SimpleHttpServer
    {
     static async Task Main()
    {
        var listener = new HttpListener();
        listener.Prefixes.Add("http://localhost:5000/");
        listener.Start();
        Console.WriteLine("Servidor escuchando en http://localhost:5000/ ...");

        while (true)
        {
            var context = await listener.GetContextAsync();
            var request = context.Request;
            var response = context.Response;
            Console.WriteLine($"→ Petición recibida: {request.HttpMethod} {request.RawUrl}");

            string responseBody = $"{{ \"message\": \"¡Hola desde el servidor! Uds pidió: {request.RawUrl}\" }}";
            byte[] buffer = Encoding.UTF8.GetBytes(responseBody);
            response.ContentType = "application/json";
            response.ContentLength64 = buffer.Length;
            response.StatusCode = (int)HttpStatusCode.OK;

            await response.OutputStream.WriteAsync(buffer, 0, buffer.Length);
            response.Close();
        }
     }
    }

#Código del cliente

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
            var url = "http://localhost:5000/saludo?usuario=SEB";
            Console.WriteLine($"→ Enviando petición GET a {url}");
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            Console.WriteLine("← Respuesta del servidor:");
            Console.WriteLine(content);
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"¡Error en la petición! {ex.Message}");
        }
    }
   }
#Conclusión
Este proyecto es un ejemplo básico de cómo crear un servidor y un cliente HTTP en C#.
