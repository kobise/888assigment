﻿namespace homeassignment.server.Models
{
    public class Language
    {
        public string code { get; set; }
        public string name { get; set; }
        public string native { get; set; }
        public bool rtl { get; set; }

        
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
