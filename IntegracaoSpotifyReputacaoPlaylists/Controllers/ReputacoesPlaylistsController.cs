using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IntegracaoSpotifyReputacaoPlaylists.Modelos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IntegracaoSpotifyReputacaoPlaylists.Controllers
{
    [Route("v1/reputacoesplaylists")]
    [ApiController]
    public class ReputacoesPlaylistsController : ControllerBase
    {
        private readonly ApiContext _context;

        public ReputacoesPlaylistsController()
        {
            var options = new DbContextOptionsBuilder<ApiContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .EnableSensitiveDataLogging()
                .Options;
            _context = new ApiContext(options);
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarReputacao(ReputacaoPlaylists reputacaoPlaylists)
        {
            _context.ReputacoesPlaylists.Add(reputacaoPlaylists);
            _context.SaveChanges();

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> ListarReputacoes()
        {
            var resposta = _context.ReputacoesPlaylists;

            return Ok(resposta);
        }
    }
}