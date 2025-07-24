$(function(){
    // 1) Инициализация: загрузить списки институтов, heads, programs, modules
    loadInstitutes();
    loadHeads();
    loadPrograms();
    loadModules();

    // 2) Обработчики кнопок “Добавить”
    $('#btnAddProgram').click(() => {
        resetProgramForm();
        $('#programModal').modal('show');
    });
    $('#btnAddModule').click(() => {
        resetModuleForm();
        $('#moduleModal').modal('show');
    });

    // 3) Отправка форм
    $('#programForm').submit(async e => {
        e.preventDefault();
        const cmd = {
            title: $('#ProgramTitle').val(),
            status: $('#ProgramStatus').val(),
            cypher: $('#ProgramCypher').val(),
            level: $('#ProgramLevel').val(),
            standard: $('#ProgramStandard').val(),
            instituteId: $('#ProgramInstituteId').val(),
            headId: $('#ProgramHeadId').val(),
            accreditationTime: $('#ProgramAccreditationTime').val(),
            moduleIds: $('#ProgramModuleIds').val() || []
        };
        const uuid = $('#ProgramUuid').val();
        if (uuid) {
            await fetch(`/api/programs/${uuid}`, { method:'PUT', headers:{'Content-Type':'application/json'}, body: JSON.stringify(cmd) });
        } else {
            await fetch('/api/programs',{ method:'POST', headers:{'Content-Type':'application/json'}, body: JSON.stringify(cmd) });
        }
        $('#programModal').modal('hide');
        loadPrograms();
    });

    $('#moduleForm').submit(async e => {
        e.preventDefault();
        const cmd = {
            title: $('#ModuleTitle').val(),
            type: $('#ModuleType').val(),
            level: $('#ModuleLevel').val(),
            standard: $('#ModuleStandard').val(),
            programId: $('#ModuleProgramId').val()
        };
        const uuid = $('#ModuleUuid').val();
        const url = uuid ? `/api/modules/${uuid}` : '/api/modules';
        const method = uuid ? 'PUT' : 'POST';
        await fetch(url, { method, headers:{'Content-Type':'application/json'}, body: JSON.stringify(cmd) });
        $('#moduleModal').modal('hide');
        loadModules();
        loadPrograms(); // потому что программы могут изменили список moduleIds
    });

    // 4) Функции загрузки
    async function loadInstitutes(){
        const data = await fetch('/api/institutes').then(r=>r.json());
        $('#ProgramInstituteId, #ModuleProgramId').empty().append(data.map(i=>`<option value="${i.uuid}">${i.title}</option>`));
    }
    async function loadHeads(){
        const data = await fetch('/api/heads').then(r=>r.json());
        $('#ProgramHeadId').empty().append(data.map(h=>`<option value="${h.uuid}">${h.fullName}</option>`));
    }
    async function loadPrograms(){
        const data = await fetch('/api/programs').then(r=>r.json());
        const $b = $('#programsTable tbody').empty();
        data.forEach(p => {
            $b.append(`<tr>
        <td>${p.title}</td>
        <td>${p.status}</td>
        <td>${p.cypher}</td>
        <td>${p.instituteTitle}</td>
        <td>${p.headFullName}</td>
        <td>${p.accreditationTime}</td>
        <td>
          <button class="btn btn-sm btn-primary edit-program" data-id="${p.uuid}">✎</button>
          <button class="btn btn-sm btn-danger delete-program" data-id="${p.uuid}">🗑</button>
        </td>
      </tr>`);
        });
        // биндим кнопки
        $('.edit-program').click(async function(){
            const id = $(this).data('id');
            const p = await fetch(`/api/programs/${id}`).then(r=>r.json());
            $('#ProgramUuid').val(p.uuid);
            $('#ProgramTitle').val(p.title);
            // ... остальные поля ...
            $('#ProgramModuleIds').val(p.modules.map(m=>m.uuid));
            $('#programModal').modal('show');
        });
        $('.delete-program').click(async function(){
            if(!confirm('Удалить программу?')) return;
            await fetch(`/api/programs/${$(this).data('id')}`,{method:'DELETE'});
            loadPrograms();
        });
    }
    async function loadModules(){
        const data = await fetch('/api/modules').then(r=>r.json());
        const $b = $('#modulesTable tbody').empty();
        data.forEach(m => {
            $b.append(`<tr>
        <td>${m.title}</td>
        <td>${m.type}</td>
        <td>${m.level}</td>
        <td>${m.standard}</td>
        <td>${m.programId}</td>
        <td>
          <button class="btn btn-sm btn-primary edit-module" data-id="${m.uuid}">✎</button>
          <button class="btn btn-sm btn-danger delete-module" data-id="${m.uuid}">🗑</button>
        </td>
      </tr>`);
        });
        $('.edit-module').click(async function(){
            const id = $(this).data('id');
            const m = await fetch(`/api/modules/${id}`).then(r=>r.json());
            $('#ModuleUuid').val(m.uuid);
            $('#ModuleTitle').val(m.title);
            // ... остальные поля ...
            $('#moduleModal').modal('show');
        });
        $('.delete-module').click(async function(){
            if(!confirm('Удалить модуль?')) return;
            await fetch(`/api/modules/${$(this).data('id')}`,{method:'DELETE'});
            loadModules();
            loadPrograms();
        });
    }

    function resetProgramForm(){
        $('#programForm')[0].reset();
        $('#ProgramUuid, #ProgramModuleIds').val('');
    }
    function resetModuleForm(){
        $('#moduleForm')[0].reset();
        $('#ModuleUuid').val('');
    }
});
