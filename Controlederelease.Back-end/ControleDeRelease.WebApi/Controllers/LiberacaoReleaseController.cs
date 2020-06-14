using ControleDeRelease.Domain.Commands;
using ControleDeRelease.Domain.Commands.AnaliseRelease;
using ControleDeRelease.Domain.Data;
using ControleDeRelease.Domain.Data.UnitOfWork;
using ControleDeRelease.Domain.Handlers;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeRelease.WebApi.Controllers
{
    [EnableCors("AllowSpecificOrigin")]
    [Route("api/[controller]")]
    [ApiController]
    public class LiberacaoReleaseController : ControllerBase
    {
        private readonly DataBaseContext _dataBaseContext;

        public LiberacaoReleaseController(DataBaseContext dataBaseContext)
        {
            _dataBaseContext = dataBaseContext;
        }

        [HttpGet("{id}")]
        public ICommandResult Get(int id)
        {
            var command = new AnalisarReleasesCommand{ Id = id };

            var unitOfWork = new UnitOfWork(_dataBaseContext);

            var versaoRepository = unitOfWork.GetVersaoProjetoRepository();
            var projetoRepository = unitOfWork.GetProjetoRepository();
            var liberacaoReleaseRepository = unitOfWork.GetLiberacaoReleaseRepository();

            var handle = new AnaliseReleaseHandle(versaoRepository, projetoRepository, liberacaoReleaseRepository);

            var result = handle.Handler(command);

            return result; 
        }

        [HttpPost]
        public ICommandResult Post([FromBody] InserirLiberacaoReleaseCommand command)
        {
            var unitOfWork = new UnitOfWork(_dataBaseContext);

            var versaoRepository = unitOfWork.GetVersaoProjetoRepository();
            var projetoRepository = unitOfWork.GetProjetoRepository();
            var liberacaoReleaseRepository = unitOfWork.GetLiberacaoReleaseRepository();

            var handle = new AnaliseReleaseHandle(versaoRepository, projetoRepository, liberacaoReleaseRepository);

            var result = handle.Handler(command);

            unitOfWork.Commit();

            return new CommandResult();
        }
    }
}
