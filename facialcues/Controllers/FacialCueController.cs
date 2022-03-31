using Microsoft.AspNetCore.Mvc;

namespace facialcues.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FacialCueController : ControllerBase
    {

        private readonly ILogger<FacialCueController> _logger;

        public FacialCueController(ILogger<FacialCueController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public ActionResult<FacialCue> Get()
        {
            return new FacialCue
            {
                Expression = "Happy",
                Rating = 6,
                Summary = "Droopy Dog is happy",
                Text = "I'm Happy"
            };
        }
    }
}