using AutoMapper;
using Logic.Managers.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.ProgramsController;

public class ProgramsController : ControllerBase
{
    private readonly IProgramService _programService;
    private readonly IMapper        _mapper;

    public ProgramsController(IProgramService programService, IMapper mapper)
    {
        _programService    = programService;
        _mapper = mapper;
    }

    [HttpGet("api/programs")]
    public async Task<IActionResult> Get() =>
        Ok(await _programService.GetAllAsync());
}
