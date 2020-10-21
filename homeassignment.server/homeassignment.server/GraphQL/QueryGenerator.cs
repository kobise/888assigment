using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace homeassignment.server.GraphQL
{
    public class QueryGenerator
    {
        public static string GenerateContinentsQuery()
        {
            return @"{
                        continents{
			                code,
                            name,
                        }
                    }";
        }


        public static string GenerateCountriesQuery()
        {
            return @"{
                        countries: {
                          code,
                          name,
                          native,
                          phone,
                          capital,
                          currency, " +
                          GenerateLanguagesQuery()
                   + @"
                          emoji,
                          emojiU," +
                          GenerateStatesQuery()
                   + @"
                        }
                    }";
        }

        public static string GenerateLanguagesQuery()
        {
            return @"{
                        languages: {
			                code,
                            name,
                            native,
                            rtl,
                        }
                    }";
        }
        public static string GenerateStatesQuery()
        {
            return @"{
                        states: {
			                code,
                            name
                        }
                    }";
        }
    }
}
