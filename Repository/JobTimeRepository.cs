
namespace api_guardian.Repository
{
    public class JobTimeRepository
    {
        private readonly ILogger<JobTimeRepository> _logger;
        public JobTimeRepository(
            ILogger<JobTimeRepository> logger
        )
        {
            _logger = logger;
        }
     
    }
}