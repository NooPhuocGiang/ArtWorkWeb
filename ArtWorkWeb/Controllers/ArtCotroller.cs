using DataTier.Models;
using Eatera.Helpers;
using Eatera.Models;
using Login_Signup.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

namespace Login_Signup.Controllers
{
    [ApiController]
    [Route("api/art")]
    [DisableCors]
    public class ArtController : Controller
    {
        private MainDataContext _mainDataContext;
        public ArtController(MainDataContext dataContext)
        {
            _mainDataContext = dataContext;
        }
        [HttpGet("images")]
        public IActionResult getImages([FromQuery(Name = "id")] int id)
        {
            try
            {
                Artwork currentArt = _mainDataContext.Arts.Where(x => x.IdArtwork == id).FirstOrDefault();
                if (currentArt != null)
                {
                    Console.WriteLine(Convert.ToBase64String(currentArt.Image));
                    return Ok(new { data = new { Image = Convert.ToBase64String(currentArt.Image) } });
                }
            }
            catch (Exception ex)
            {
                return Ok(new { error = ex.Message });
            }
            return Ok(new { data = new { Image = "" } });
        }
        [HttpGet]
        public async Task<IActionResult> GetArt([FromQuery] int? id, [FromQuery] int? page, [FromQuery] bool? unsold)
        {
            var query = _mainDataContext.Arts
                .Select(t1 => new
                {
                    t1.IdArtwork,
                    t1.Name,
                    t1.CompletionDate,
                    t1.Width,
                    t1.Height,
                    t1.Price,
                    t1.Description,
                    Purchased = _mainDataContext.Orders.Any(t2 => t2.IdArtwork == t1.IdArtwork)
                })
                .OrderByDescending(t1 => t1.IdArtwork).ToList();

            if (id.HasValue)
            {
                if (query!=null) query = query.Where(t1 => t1.IdArtwork == id).ToList();
            }
            else
            {
                if (unsold.HasValue && unsold.Value)
                {
                    query = query.Where(t1 => !_mainDataContext.Orders.Any(t2 => t2.IdArtwork == t1.IdArtwork)).ToList();
                }

                if (page.HasValue && page.Value > 0)
                {
                    int offset = (page.Value - 1) * 12;
                    query = query.Skip(offset).Take(12).ToList();
                }
            }

            try
            {
                var data = query;
                return Ok(new { data });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }
        [HttpPost]
        public async Task<IActionResult> CreateArt([FromBody] ArtworkInputModel model)
        {
            try
            {

                var newArt = new Artwork
                {
                    Name = model.ArtworkName,
                    CompletionDate = model.ArtworkCompletionDate,
                    Width = model.ArtworkWidth,
                    Height = model.ArtworkHeight,
                    Price = model.ArtworkPrice,
                    Description = model.ArtworkDescription,

                };
                if (model.ArtworkImage!=null) newArt.Image  = Convert.FromBase64String(model.ArtworkImage.Split(',')[1]);
                _mainDataContext.Arts.Add(newArt);
                await _mainDataContext.SaveChangesAsync();

                return Ok(new { data = newArt });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

    }
}
