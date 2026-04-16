using Aplicacion_ReservasStyle.Interfaces;
using System.Security.Claims;

namespace Api_ReservasStyle.Middlewares
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<LoggingMiddleware> _logger;

        public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, ILogService logService)
        {
            var startTime = DateTime.Now;
            var method = context.Request.Method;
            var path = context.Request.Path.Value;
            var ipAddress = context.Connection.RemoteIpAddress?.ToString();
            var userAgent = context.Request.Headers["User-Agent"].ToString();
            
            var userIdClaim = context.User.FindFirst(ClaimTypes.NameIdentifier);
            var userNameClaim = context.User.FindFirst(ClaimTypes.Name);
            
            int? idUsuario = userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId) ? userId : null;
            string nombreUsuario = userNameClaim?.Value ?? "Anónimo";

            try
            {
                await _next(context);

                var duration = DateTime.Now.Subtract(startTime).TotalMilliseconds;
                
                // Log automático para operaciones POST, PUT, DELETE
                if (method != "GET" && context.Response.StatusCode < 400)
                {
                    await logService.RegistrarAccionAsync(
                        accion: method,
                        entidad: ExtractEntityName(path),
                        idUsuario: idUsuario,
                        direccionIP: ipAddress,
                        userAgent: userAgent,
                        exitoso: true);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error en middleware de logging para {method} {path}");
                
                // Log de error
                if (method != "GET")
                {
                    await logService.RegistrarAccionAsync(
                        accion: method,
                        entidad: ExtractEntityName(path),
                        idUsuario: idUsuario,
                        direccionIP: ipAddress,
                        userAgent: userAgent,
                        exitoso: false,
                        mensajeError: ex.Message);
                }
                
                throw;
            }
        }

        private static string ExtractEntityName(string path)
        {
            var parts = path?.Split('/') ?? Array.Empty<string>();
            return parts.Length > 2 ? parts[2] : "Sistema";
        }
    }
}
