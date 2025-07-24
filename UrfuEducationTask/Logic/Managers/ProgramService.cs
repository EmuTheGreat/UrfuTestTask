using AutoMapper;
using Dal;
using Dal.Models;
using Dal.Repositories.Interfaces;
using Logic.Infrastructure.Results;
using Logic.Managers.Interfaces;
using Logic.Model;
using Logic.Model.Command;

namespace Logic.Managers
{
    public class ProgramService : IProgramService
    {
        private readonly IProgramRepository   _programRepo;
        private readonly IInstituteRepository _instRepo;
        private readonly IHeadRepository      _headRepo;
        private readonly IMapper              _mapper;

        public ProgramService(
            IProgramRepository programRepo,
            IInstituteRepository instRepo,
            IHeadRepository headRepo,
            IMapper mapper)
        {
            _programRepo = programRepo;
            _instRepo    = instRepo;
            _headRepo    = headRepo;
            _mapper      = mapper;
        }

        public async Task<IEnumerable<ProgramLogicModel>> GetAllAsync()
        {
            var programs  = await _programRepo.GetAllAsync();
            var institutes = (await _instRepo.GetAllAsync())
                               .ToDictionary(i => i.Uuid, i => i.Title);
            var heads      = (await _headRepo.GetAllAsync())
                               .ToDictionary(h => h.Uuid, h => h.FullName);

            return programs.Select(p =>
            {
                var dto = _mapper.Map<ProgramLogicModel>(p);
                dto.InstituteTitle = institutes.GetValueOrDefault(p.InstituteId, "");
                dto.HeadFullName   = heads.GetValueOrDefault(p.HeadId, "");
                return dto;
            });
        }

        public async Task<ProgramLogicModel?> GetByIdAsync(Guid id)
        {
            var p = await _programRepo.GetByIdAsync(id);
            if (p == null) return null;

            var inst = await _instRepo.GetByIdAsync(p.InstituteId);
            var head = await _headRepo.GetByIdAsync(p.HeadId);

            var dto = _mapper.Map<ProgramLogicModel>(p);
            dto.InstituteTitle = inst?.Title ?? "";
            dto.HeadFullName   = head?.FullName ?? "";
            return dto;
        }

        public async Task<OperationResult<ProgramLogicModel>> CreateAsync(CreateProgramCommand cmd)
        {
            var model = _mapper.Map<ProgramModel>(cmd);
            model.Uuid = Guid.NewGuid();

            await _programRepo.AddAsync(model);
            await _programRepo.SaveChangesAsync();

            var inst = await _instRepo.GetByIdAsync(model.InstituteId);
            var head = await _headRepo.GetByIdAsync(model.HeadId);

            var dto = _mapper.Map<ProgramLogicModel>(model);
            dto.InstituteTitle = inst?.Title ?? "";
            dto.HeadFullName   = head?.FullName ?? "";
            return OperationResult<ProgramLogicModel>.Ok(dto);
        }

        /*public async Task<OperationResult<ProgramLogicModel>> UpdateAsync(UpdateProgramCommand cmd)
        {
            var model = await _programRepo.GetByIdAsync(cmd.Uuid);
            if (model == null)
                return OperationResult<ProgramLogicModel>.Fail("Программа не найдена.");

            _mapper.Map(cmd, model);
            await _programRepo.UpdateAsync(model);
            await _programRepo.SaveChangesAsync();

            var inst = await _instRepo.GetByIdAsync(model.InstituteId);
            var head = await _headRepo.GetByIdAsync(model.HeadId);

            var dto = _mapper.Map<ProgramLogicModel>(model);
            dto.InstituteTitle = inst?.Title ?? "";
            dto.HeadFullName   = head?.FullName ?? "";
            return OperationResult<ProgramLogicModel>.Ok(dto);
        }*/

        public async Task<OperationResult> DeleteAsync(Guid id)
        {
            var model = await _programRepo.GetByIdAsync(id);
            if (model == null)
                return OperationResult.Fail("Программа не найдена.");

            await _programRepo.DeleteAsync(model);
            await _programRepo.SaveChangesAsync();
            return OperationResult.Ok();
        }

        /*public async Task<IEnumerable<ProgramLogicModel>> GetModulesByProgramIdAsync(Guid programId)
        {
            var mods = await _programRepo.GetModulesByProgramIdAsync(programId);
            return _mapper.Map<IEnumerable<ProgramLogicModel>>(mods);
        }

        public async Task<OperationResult> AddModuleToProgramAsync(Guid programId, Guid moduleId)
        {
            await _programRepo.AddModuleAsync(programId, moduleId);
            await _programRepo.SaveChangesAsync();
            return OperationResult.Ok();
        }

        public async Task<OperationResult> RemoveModuleFromProgramAsync(Guid programId, Guid moduleId)
        {
            await _programRepo.RemoveModuleAsync(programId, moduleId);
            await _programRepo.SaveChangesAsync();
            return OperationResult.Ok();
        }*/
    }
}