﻿using Contracts.RequestService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IPokemonFinderService
    {

        Task<string> IntroduceMovesByTypeAndLng(RequestPokeApiModel requesApiModel);
    }
}
