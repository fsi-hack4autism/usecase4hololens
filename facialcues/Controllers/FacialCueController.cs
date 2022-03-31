using facialcues.Entity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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

        [HttpGet("{imageName}", Name = "GetFacialExpression")]
        public async Task<string> Get(string imageName)
        {
            var ed = new ExpressionDetector();
            var imgUrl = Constants.FacesBlobUri + "/" + imageName;
            var exp = await ed.GetExression(imgUrl);
            return JsonConvert.SerializeObject(exp);
        }
        //[HttpGet("url/{imageUrl}", Name = "GetFacialExpression")]
        //public ActionResult<FacialCue> GetByUrl(string imageUrl)
        //{
        //    var ed = new ExpressionDetector();
        //    var exp = ed.GetExression(imageUrl);
        //    return new FacialCue
        //    {
        //        FaceExpression = new Expression(),
        //        Rating = 7,
        //        Summary = "Droopy Dog is happy",
        //        Text = "I'm Happy"
        //    };
        //}
    }
}