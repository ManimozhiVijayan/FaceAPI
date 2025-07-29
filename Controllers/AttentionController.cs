using Microsoft.AspNetCore.Mvc;

namespace FaceAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AttentionController : ControllerBase
    {
        [HttpPost("attention")]
        public IActionResult CheckAttention([FromForm] IFormFile image)
        {
            if (image == null || image.Length == 0)
                return BadRequest("Image is required.");

            try
            {
                var filePath = Path.GetTempFileName();
                using (var stream = System.IO.File.Create(filePath))
                {
                    image.CopyTo(stream);
                }

                var attentive = new AttentionDetector().IsPersonAttentive(filePath);
                return Ok(new { Attentive = attentive });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal error: {ex.Message}");
            }
        }
    }
}
