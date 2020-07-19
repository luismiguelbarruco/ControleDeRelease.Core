using ControleDeRelease.Domain.Commands;
using ControleDeRelease.Domain.Data;
using ControleDeRelease.Domain.Data.UnitOfWork;
using ControleDeRelease.Domain.Queries;
using ControleDeRelease.Domain.ViewModel;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeRelease.WebApi.Controllers
{
    [EnableCors("AllowSpecificOrigin")]
    [Route("api/[controller]")]
    [ApiController]
    public class HistoricoController : ControllerBase
    {
        private readonly DataBaseContext _dataBaseContext;

        public HistoricoController(DataBaseContext dataBaseContext) => _dataBaseContext = dataBaseContext;

        [HttpGet("{id}")]
        public ICommandResult Get(int id)
        {
            var unitOfWork = new UnitOfWork(_dataBaseContext);

            var query = VersaoQueries.Selecionar(id);

            var versaoRepository = unitOfWork.GetVersaoProjetoRepository();

            var versao = versaoRepository.Selecionar(query);

            if(versao == null)
                return new CommandResult(false, "Versão não encontrada.");

            var liberacaoReleaseRepository = unitOfWork.GetLiberacaoReleaseRepository();

            var liberacaoReleaseQuery = LiberacaoReleaseQueries.Selecionar(versao.Id);

            var liberacaoRelease = liberacaoReleaseRepository.Seleionar(liberacaoReleaseQuery);

            if (liberacaoRelease == null)
                return new CommandResult(false, "Não há dados de liberação de release");

            var liberacaoReleaseViewModel = new LibercaoReleaseViewModel().Parse(liberacaoRelease);

            return new CommandResult(true, liberacaoReleaseViewModel);
        }
    }
}
