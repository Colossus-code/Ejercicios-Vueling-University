using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiPokemonMoves.Models
{
    public class PokemonFinderModel
    {
        public string QuantityOfResults { get; set; }   
        public string TypeOfPokemon { get; set; }   
        public string LanguageToFind { get; set; }

        public PokemonFinderModel(string quantity = "10", string type = "fire", string lang = "es")
        {
            QuantityOfResults = quantity;
            TypeOfPokemon = type;    
            LanguageToFind = lang;    
        }
    }
}