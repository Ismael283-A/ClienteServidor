using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;
class SimpleHttpServer    {
    static async Task Main()
    {
        // 1. Crear y configurar el listener  
        var listener = new HttpListener();
        listener.Prefixes.Add("http://localhost:5000/");
        listener.Start();
       Console.WriteLine("Servidor escuchando en http://localhost:5000/ ...");
        while (true)
        {
            // 2. Esperar una petición  
            var context = await listener.GetContextAsync();
            var request = context.Request;
           var response = context.Response;
            Console.WriteLine($"→ Petición recibida: {request.HttpMethod} {request.RawUrl}");
            // 3. Procesar y construir la respuesta  
            string responseBody = $"{{ \"message\": \"¡Hola desde el servidor! Uds pidió: {request.RawUrl}\" }}";
            byte[] buffer = Encoding.UTF8.GetBytes(responseBody);
            response.ContentType = "application/json";
            response.ContentLength64 = buffer.Length;
            response.StatusCode = (int)HttpStatusCode.OK;
            // 4. Enviar la respuesta  
            await response.OutputStream.WriteAsync(buffer, 0, buffer.Length);
            response.Close();
        }
    }
}

