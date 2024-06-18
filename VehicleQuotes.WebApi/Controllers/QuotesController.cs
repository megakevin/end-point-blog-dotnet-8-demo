using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VehicleQuotes.Core.Authentication.ApiKey;
using VehicleQuotes.Core.Services;

namespace VehicleQuotes.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuotesController : ControllerBase
    {
        private readonly QuoteService _service;

        public QuotesController(QuoteService service)
        {
            _service = service;
        }

        // GET: api/Quotes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SubmittedQuoteRequest>>> GetAll()
        {
            return await _service.GetAllQuotes();
        }

        private const string AUTH_SCHEMES =
            $"{JwtBearerDefaults.AuthenticationScheme},{ApiKeyDefaults.AuthenticationScheme}";

        // GET: api/Quotes/Secure
        [HttpGet("Secure")]
        [Authorize(AuthenticationSchemes = AUTH_SCHEMES)]
        public async Task<ActionResult<IEnumerable<SubmittedQuoteRequest>>> GetAllSecure()
        {
            return await _service.GetAllQuotes();
        }

        // POST: api/Quotes
        [HttpPost]
        public async Task<ActionResult<SubmittedQuoteRequest>> Post(QuoteRequest request)
        {
            return await _service.CalculateQuote(request);
        }
    }
}
