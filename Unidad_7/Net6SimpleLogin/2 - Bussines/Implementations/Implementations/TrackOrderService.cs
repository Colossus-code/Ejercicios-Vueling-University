using Contracts;
using Contracts.RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Implementations
{
    public class TrackOrderService : ITrackOrderService
    {
        private readonly IRepositoryUserTrackOrder _repositoryUserTrack;

        public TrackOrderService(IRepositoryUserTrackOrder repoUserTrack)
        {
            _repositoryUserTrack = repoUserTrack;
        }

        public string GetTrack(string userName)
        {
            
        }
    }
}
