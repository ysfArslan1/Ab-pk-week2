using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;


namespace Ab_pk_week2.Middleware
{
    public class HttpLoggingMiddleware
    {
        private readonly ILogger _logger;
        private readonly RequestDelegate _next;
        private readonly string _logFilePath; // Log dosyasının yolu

        string projectPath;


        public HttpLoggingMiddleware(RequestDelegate next, ILogger<HttpLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;

            // log dosyası path ataması
            projectPath =  Directory.GetCurrentDirectory();
            _logFilePath = projectPath+"\\LoggingMiddleware\\log.txt";
        }

        public async Task Invoke(HttpContext context)
        {
            var originalBodyStream = context.Response.Body;

            var request = await GetRequestAsTextAsync(context.Request);
            LogToFile(request); // Request'i LogToFile fonksiyonu ile dosyaya loglama

            await using var responseBody = new MemoryStream();
            context.Response.Body = responseBody;

            await _next(context);

            var response = await GetResponseAsTextAsync(context.Response);
            LogToFile(response); // Response'u LogToFile fonksiyonu ile dosyaya loglama

            await responseBody.CopyToAsync(originalBodyStream);
        }

        // Request Text haline getirilir
        private async Task<string> GetRequestAsTextAsync(HttpRequest request)
        {
            var body = request.Body;

            //Set the reader for the request back at the beginning of its stream.
            request.EnableBuffering();

            //Read request stream
            var buffer = new byte[Convert.ToInt32(request.ContentLength)];

            //Copy into  buffer.
            await request.Body.ReadAsync(buffer, 0, buffer.Length);

            //Convert the byte[] into a string using UTF8 encoding...
            var bodyAsText = Encoding.UTF8.GetString(buffer);

            //Assign the read body back to the request body
            request.Body = body;

            return $"{request.Scheme} {request.Host}{request.Path} {request.QueryString} {bodyAsText}";
        }

        // Response text haline getirilir
        private async Task<string> GetResponseAsTextAsync(HttpResponse response)
        {
            response.Body.Seek(0, SeekOrigin.Begin);
            //Create stream reader to write entire stream
            var text = await new StreamReader(response.Body).ReadToEndAsync();
            response.Body.Seek(0, SeekOrigin.Begin);

            return text;
        }
        private void LogToFile(string message)
        {
            try
            {
                // Log dosyasına loglar eklenir
                using (StreamWriter writer = new StreamWriter(_logFilePath, true))
                {
                    writer.WriteLine($"{DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffZ")} - #####{projectPath}#####- {message}");
                }
            }
            catch (Exception ex)
            {
                // Loglama sırasında oluşan hatalarıkonsola yazdırır
                Console.WriteLine($"Loglama sırasında hata oluştu: {ex.Message}");
            }
        }
    }
}