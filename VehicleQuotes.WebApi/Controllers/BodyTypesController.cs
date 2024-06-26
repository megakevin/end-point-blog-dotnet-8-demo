using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VehicleQuotes.Core.Authentication.ApiKey;
using VehicleQuotes.Core.Models;
using VehicleQuotes.Core.Data;

namespace VehicleQuotes.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BodyTypesController : ControllerBase
{
    private const string AUTH_SCHEMES =
        $"{JwtBearerDefaults.AuthenticationScheme},{ApiKeyDefaults.AuthenticationScheme}";

    private readonly VehicleQuotesContext _context;

    public BodyTypesController(VehicleQuotesContext context)
    {
        _context = context;
    }

    // GET: api/BodyTypes
    // [Authorize(AuthenticationSchemes = AUTH_SCHEMES)]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<BodyType>>> GetBodyTypes()
    {
        return await _context.BodyTypes.ToListAsync();
    }
}
