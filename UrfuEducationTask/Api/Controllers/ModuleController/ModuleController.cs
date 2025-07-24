using Api.Controllers.ModuleController.Dto;
using Api.Controllers.ModuleController.Dto.Request;
using Dal.Models;
using Dal.Repositories.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.ModuleController;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[ApiController]
[Route("api/modules")]
public class ModulesController : ControllerBase
{
    private readonly IModuleRepository _repo;

    public ModulesController(IModuleRepository repo)
    {
        _repo = repo;
    }

    // Получить все модули
    [HttpGet]
    public async Task<ActionResult<List<ModuleModel>>> GetAll()
    {
        var modules = await _repo.GetAllAsync();
        return Ok(modules);
    }

    // Получить модули по образовательной программе
    [HttpGet("by-program/{programId:guid}")]
    public async Task<ActionResult<List<ModuleModel>>> GetByProgram(Guid programId)
    {
        var modules = await _repo.GetByProgramIdAsync(programId);
        return Ok(modules);
    }

    // Получить один модуль
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ModuleModel>> GetById(Guid id)
    {
        var module = await _repo.GetByIdAsync(id);
        if (module == null) return NotFound();
        return Ok(module);
    }

    // Создать модуль
    [HttpPost]
    public async Task<ActionResult<Guid>> Create([FromBody] CreateModuleRequest request)
    {
        var module = new ModuleModel
        {
            Uuid = Guid.NewGuid(),
            Title = request.Title,
            Type = request.Type,
            ProgramId = request.ProgramId
        };

        await _repo.AddAsync(module);
        await _repo.SaveChangesAsync();
        return Ok(module.Uuid);
    }

    // Обновить модуль
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateModuleRequest request)
    {
        var module = await _repo.GetByIdAsync(id);
        if (module == null) return NotFound();

        module.Title = request.Title;
        module.Type = request.Type;

        await _repo.UpdateAsync(module);
        await _repo.SaveChangesAsync();
        return NoContent();
    }

    // Удалить модуль
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var module = await _repo.GetByIdAsync(id);
        if (module == null) return NotFound();

        await _repo.DeleteAsync(module);
        await _repo.SaveChangesAsync();
        return NoContent();
    }

    // Привязать существующий модуль к другой образовательной программе
    [HttpPost("assign-to-program")]
    public async Task<IActionResult> AssignToProgram([FromBody] ChangeProgramBindingRequest request)
    {
        var module = await _repo.GetByIdAsync(request.ModuleId);
        if (module == null) return NotFound();

        module.ProgramId = request.ProgramId;

        await _repo.UpdateAsync(module);
        await _repo.SaveChangesAsync();
        return NoContent();
    }
}