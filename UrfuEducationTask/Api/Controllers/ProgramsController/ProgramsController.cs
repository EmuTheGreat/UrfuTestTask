using Api.Controllers.ProgramsController.Dto.Request;
using Api.Controllers.ProgramsController.Dto.Response;
using AutoMapper;
using Logic.Managers.Interfaces;
using Logic.Model.Command;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.ProgramsController;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[ApiController]
[Route("api/[controller]")]
public class ProgramsController : ControllerBase
{
    private readonly IProgramService _svc;
    private readonly IMapper _mapper;

    public ProgramsController(IProgramService svc, IMapper mapper)
    {
        _svc = svc;
        _mapper = mapper;
    }

    // GET api/programs
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var dtos = await _svc.GetAllAsync();
        var resp = _mapper.Map<IEnumerable<ProgramResponse>>(dtos);
        return Ok(resp);
    }

    // GET api/programs/{id}
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var dto = await _svc.GetByIdAsync(id);
        if (dto is null) return NotFound();
        return Ok(_mapper.Map<ProgramResponse>(dto));
    }

    // POST api/programs
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ProgramCreateRequest req)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var cmd = _mapper.Map<CreateProgramCommand>(req);
        var result = await _svc.CreateAsync(cmd);

        if (!result.Success)
            return BadRequest(new { Message = result.Error });

        var response = _mapper.Map<ProgramResponse>(result.Data!);
        return CreatedAtAction(nameof(GetById),
            new { id = response.Uuid },
            response);
    }

    // DELETE api/programs/{id}
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _svc.DeleteAsync(id);
        if (!result.Success)
            return BadRequest(new { Message = result.Error });
        return NoContent();
    }
}