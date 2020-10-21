using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL;
using GraphQL.Client.Abstractions;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using homeassignment.server.GraphQL;
using homeassignment.server.Models;

namespace homeassignment.server.GeaphQL
{
    public class GraphQLRepository : IGraphQLRepository
    {
        IGraphQLClient _graphQLClient;
        public GraphQLRepository(IGraphQLClient graphQLClient)
        {
            _graphQLClient = graphQLClient;
        }

        /// <summary>
        /// Get all Continents from graphQL server
        /// </summary>
        /// <returns>Collection of Continents</returns>
        public async Task<ICollection<Continent>> GetAllContinentsAsync()
        {
            var request = new GraphQLRequest
            {
                Query = @"{
                        continents{
			                code,
                            name,
                        }
                    }"

            };

            var continents = await _graphQLClient.SendQueryAsync<GraphQLContinentsResponse>(request);

            return continents.Data.continents;
        }

        /// <summary>
        /// Get specific Continent by code
        /// </summary>
        /// <param name="code"> Continent code</param>
        /// <returns>Continent</returns>
        public async Task<Continent> GetContinentByCodeAsync(string code)
        {
            var request = new GraphQLRequest
            {
                Query = @"{
                            continent(code: """ + code + @""" )
                            {
                                code,
                                name,
                                countries{
                                    code,
                                    name,
                                    phone,
                                    capital,
                                    currency,
                                    languages{
                                       name
                                    },
                                    emoji
                                }
                            }
                        }"
            };

            var continents = await _graphQLClient.SendQueryAsync<GraphQLContinentResponse>(request);

            return continents.Data.continent;
        }
    }
}
