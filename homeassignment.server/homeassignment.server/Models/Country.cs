using System.Collections.Generic;

namespace homeassignment.server.Models
{
    public class Country
    {
        public string code { get; set; }
        public string name { get; set; }
        public string native { get; set; }
        public string phone { get; set; }
        public Continent continent { get; set; }
        public string capital { get; set; }
        public string currency { get; set; }
        public ICollection<Language> languages { get; set; }
        public string emoji { get; set; }
        public string emojiU { get; set; }
        public ICollection<State> states { get; set; }

    }
}


//type Query
//{
//    continents(filter: ContinentFilterInput): [Continent!]!
//  continent(code: ID!): Continent
//  countries(filter: CountryFilterInput): [Country!]!
//  country(code: ID!): Country
//  languages(filter: LanguageFilterInput): [Language!]!
//  language(code: ID!): Language
//}
