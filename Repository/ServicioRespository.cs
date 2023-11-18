
namespace api_guardian.Repository
{
    public class ServicioRespository
    {
        private readonly ILogger<ServicioRespository> _logger;

        public ServicioRespository(
            ILogger<ServicioRespository> logger
           
        )
        {
            _logger = logger;
        }
      
    }
}