using Api.Controllers.InstituteController.Dto.Request;
using Dal.Models;
using Dal.Repositories.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace Api.Controllers.InstituteController;

using Microsoft.AspNetCore.Mvc;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[ApiController]
[Route("api/institutes")]
public class InstitutesController : ControllerBase
{
    private readonly IInstituteRepository _repo;

    public InstitutesController(IInstituteRepository repo)
    {
        _repo = repo;
    }

    // Получить все институты
    [HttpGet]
    public async Task<ActionResult<List<Institute>>> GetAll()
    {
        var institutes = await _repo.GetAllAsync();
        return Ok(institutes);
    }

    // Получить институт по id
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<Institute>> GetById(Guid id)
    {
        var institute = await _repo.GetByIdAsync(id);
        if (institute == null)
            return NotFound();
        return Ok(institute);
    }

    // Добавить новый институт
    [HttpPost]
    public async Task<ActionResult<Guid>> Create([FromBody] CreateInstituteRequest request)
    {
        var institute = new Institute
        {
            Uuid = Guid.NewGuid(),
            Title = request.Title
        };
        await _repo.AddAsync(institute);
        await _repo.SaveChangesAsync();
        return Ok(institute.Uuid);
    }
}
