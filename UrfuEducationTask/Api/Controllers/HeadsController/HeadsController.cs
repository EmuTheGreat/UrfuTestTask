using Api.Controllers.HeadsController.Dto.Request;
using Dal.Models;
using Dal.Repositories.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[ApiController]
[Route("api/heads")]
public class HeadsController : ControllerBase
{
    private readonly IHeadRepository _repo;

    public HeadsController(IHeadRepository repo)
    {
        _repo = repo;
    }

    // Получить всех руководителей
    [HttpGet]
    public async Task<ActionResult<List<Head>>> GetAll()
    {
        var heads = await _repo.GetAllAsync();
        return Ok(heads);
    }

    // Получить руководителя по id
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<Head>> GetById(Guid id)
    {
        var head = await _repo.GetByIdAsync(id);
        if (head == null)
            return NotFound();
        return Ok(head);
    }

    // Добавить нового руководителя
    [HttpPost]
    public async Task<ActionResult<Guid>> Create([FromBody] CreateHeadRequest request)
    {
        var head = new Head
        {
            Uuid = Guid.NewGuid(),
            FullName = request.FullName
        };
        await _repo.AddAsync(head);
        await _repo.SaveChangesAsync();
        return Ok(head.Uuid);
    }
}