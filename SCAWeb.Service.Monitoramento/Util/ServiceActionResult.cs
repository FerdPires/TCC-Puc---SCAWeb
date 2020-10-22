using SCAWeb.Service.Monitoramento.Util.Interfaces;

namespace SCAWeb.Service.Monitoramento.Util
{
    public class ServiceActionResult : IServiceActionResult
    {
        public ServiceActionResult() { }

        public ServiceActionResult(bool success, string message, object data)
        {
            Success = success;
            Message = message;
            Data = data;
        }

        public bool Success { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }
}
