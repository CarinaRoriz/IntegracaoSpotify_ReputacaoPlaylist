using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntegracaoSpotifyReputacaoPlaylists.Modelos
{
    public class ReputacaoPlaylists
    {
        
        public long Id { get; set; }
        public string PlaylistId { get; set; }
        public int Nota { get; set; }
    }
}
