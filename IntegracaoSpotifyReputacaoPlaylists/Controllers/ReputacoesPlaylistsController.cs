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

        public ReputacoesPlaylistsController(ApiContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarReputacao(ReputacaoPlaylists reputacaoPlaylists)
        {
            var isNotaValida = IsNotaValida(reputacaoPlaylists.Nota);

            if (isNotaValida)
            {
                _context.ReputacoesPlaylists.Add(reputacaoPlaylists);
                _context.SaveChanges();

                return Ok();
            }
            else
            {
                return NotFound("Nota Inválida");
            }
        }

        [HttpPut]
        public async Task<IActionResult> EditarReputacao(ReputacaoPlaylists reputacaoPlaylists)
        {
            var isNotaValida = IsNotaValida(reputacaoPlaylists.Nota);

            if (isNotaValida)
            {
                var reputacao = _context.ReputacoesPlaylists.Where(r => r.PlaylistId == reputacaoPlaylists.PlaylistId).FirstOrDefault();

                if (reputacao != null)
                {
                    reputacao.Nota = reputacaoPlaylists.Nota;
                }

                _context.ReputacoesPlaylists.Update(reputacao);
                _context.SaveChanges();

                return Ok();
            }
            else
            {
                return NotFound("Nota Inválida");
            }
        }

        [HttpGet]
        public async Task<IActionResult> ListarReputacoes()
        {
            var resposta = _context.ReputacoesPlaylists.Select(r => new
            {
                Id = r.Id,
                PlaylistId = r.PlaylistId,
                Nota = r.Nota
            });

            return Ok(resposta);
        }

        [HttpGet("{playlistId}")]
        public async Task<IActionResult> ListarReputacoes(string playlistId)
        {
            var resposta = _context.ReputacoesPlaylists.Select(r => new
            {
                Id = r.Id,
                PlaylistId = r.PlaylistId,
                Nota = r.Nota
            }).Where(r => r.PlaylistId.Equals(playlistId));

            return Ok(resposta);
        }

        private bool IsNotaValida(int nota)
        {
            if (nota >= 1 && nota <= 5)
                return true;

            return false;
        }
    }
}