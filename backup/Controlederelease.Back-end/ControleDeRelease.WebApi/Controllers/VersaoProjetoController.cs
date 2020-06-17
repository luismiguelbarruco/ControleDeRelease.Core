using ControleDeRelease.Domain.Commands;
using ControleDeRelease.Domain.Commands.VersaoProjeto;
using ControleDeRelease.Domain.Data;
using ControleDeRelease.Domain.Data.UnitOfWork;
using ControleDeRelease.Domain.Handlers;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ControleDeRelease.WebApi.Controllers
{
    [EnableCors("AllowSpecificOrigin")]
    [Route("api/[controller]")]
    [ApiController]
    public class VersaoProjetoController : ControllerBase
    {
        private readonly DataBaseContext _dataBaseContext;

        public VersaoProjetoController(DataBaseContext dataBaseContext) => _dataBaseContext = dataBaseContext;

        [HttpGet]
        public ICommandResult Get()
        {
            using var unitOfWork = new UnitOfWork(_dataBaseContext);

            var versaoProjetoRepository = unitOfWork.GetVersaoProjetoRepository();

            var versaoProjetos = versaoProjetoRepository.Selecionar().ToList();

            if (!versaoProjetos.Any())
                return new CommandResult(false, "Não há diretorios cadastrados");

            return new CommandResult(true, versaoProjetos);
        }

        [HttpPost]
        public ICommandResult Post([FromBody] InserirVersaoProjetoCommand command)
        {
            using var unitOfWork = new UnitOfWork(_dataBaseContext);

            var versaoProjetoRepository = unitOfWork.GetVersaoProjetoRepository();

            var handle = new VersaoProjetoHandler(versaoProjetoRepository);

            var result = handle.Handler(command);

            unitOfWork.Commit();

            return result;
        }

        [HttpPut("{id}")]
        public ICommandResult Put(int id, [FromBody] AtualizarVersaoProjetoCommand command)
        {
            using var unitOfWork = new UnitOfWork(_dataBaseContext);

            var versaoProjetoRepository = unitOfWork.GetVersaoProjetoRepository();

            var handler = new VersaoProjetoHandler(versaoProjetoRepository);

            command.Id = id;

            var result = handler.Handler(command);

            unitOfWork.Commit();

            return result;
        }

        [HttpDelete("{id}")]
        public ICommandResult Delete(int id)
        {
            using var unitOfWork = new UnitOfWork(_dataBaseContext);

            var versaoProjetoRepository = unitOfWork.GetVersaoProjetoRepository();

            var command = new DeletarVersaoProjetoCommand { Id = id };

            var handler = new VersaoProjetoHandler(versaoProjetoRepository);

            var result = handler.Handler(command);

            unitOfWork.Commit();

            return result;
        }
    }
}
