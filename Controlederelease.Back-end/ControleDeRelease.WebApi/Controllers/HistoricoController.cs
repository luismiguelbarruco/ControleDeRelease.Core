using ControleDeRelease.Domain.Commands;
using ControleDeRelease.Domain.Data;
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

        [HttpGet]
        public ICommandResult Get()
        {
            return new CommandResult();
        }
    }
}
