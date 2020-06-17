using ControleDeRelease.Domain.Commands;
using ControleDeRelease.Domain.Commands.Projeto;
using ControleDeRelease.Domain.Data;
using ControleDeRelease.Domain.Data.UnitOfWork;
using ControleDeRelease.Domain.Handlers;
using ControleDeRelease.Domain.Queries;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ControleDeRelease.WebApi.Controllers
{
    [EnableCors("AllowSpecificOrigin")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProjetoController : ControllerBase
    {
        private readonly DataBaseContext _dataBaseContext;

        public ProjetoController(DataBaseContext dataBaseContext) => _dataBaseContext = dataBaseContext;

        [HttpGet]
        public ICommandResult Get()
        {
            using var unitOfWork = new UnitOfWork(_dataBaseContext);

            var projetoRepository = unitOfWork.GetProjetoRepository();

            var projetos = projetoRepository.Selecionar();

            if (!projetos.Any())
                return new CommandResult(false, "Não há projetos cadastrados");

            return new CommandResult(true, projetos);
        }

        [HttpGet("{id}")]
        public ICommandResult Get(int id)
        {
            using var unitOfWork = new UnitOfWork(_dataBaseContext);

            var projetoRepository = unitOfWork.GetProjetoRepository();
            
            var query = ProjetoQueries.Selecionar(id);

            var projeto = projetoRepository.Selecionar(query);

            if (projeto == null)
                return new CommandResult(false, "Projeto não encontrado");

            return new CommandResult(true, projeto);
        }

        [HttpPost]
        public ICommandResult Post([FromBody] InserirProjetoCommand command)
        {
            using var unitOfWork = new UnitOfWork(_dataBaseContext);

            var projetoRepository = unitOfWork.GetProjetoRepository();

            var handle = new ProjetoHandler(projetoRepository);

            var result = handle.Handler(command);

            unitOfWork.Commit();

            return result;
        }

        [HttpPut("{id}")]
        public ICommandResult Put(int id, [FromBody] AtualizarProjetoCommand command)
        {
            using var unitOfWork = new UnitOfWork(_dataBaseContext);
            var projetoRepository = unitOfWork.GetProjetoRepository();

            var handler = new ProjetoHandler(projetoRepository);

            command.Id = id;

            var result = handler.Handler(command);

            unitOfWork.Commit();

            return result;
        }

        [HttpDelete("{id}")]
        public ICommandResult Delete(int id)
        {
            using var unitOfWork = new UnitOfWork(_dataBaseContext);

            var projetoRepository = unitOfWork.GetProjetoRepository();

            var command = new DeletarProjetoCommand { Id = id };

            var handler = new ProjetoHandler(projetoRepository);

            var result = handler.Handler(command);

            unitOfWork.Commit();

            return result;
        }
    }
}
