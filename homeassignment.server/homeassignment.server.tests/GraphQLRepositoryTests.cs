using FluentAssertions;
using GraphQL.Client.Abstractions;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using homeassignment.server.GeaphQL;
using homeassignment.server.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace homeassignment.server.tests
{
    [TestClass]
    public class GraphQLRepositoryTests
    {
        IContinentRepository _graphQLRepository;
        IGraphQLClient _graphQLClient;
        public GraphQLRepositoryTests()
        {
            _graphQLClient = new GraphQLHttpClient("https://countries.trevorblades.com/", new NewtonsoftJsonSerializer());

            _graphQLRepository = new GraphQLRepository(_graphQLClient);
        }

        [TestMethod]
        public async System.Threading.Tasks.Task GetAllContinents_AllContinentsReturnsNotEmptyResultAsync()
        {
            #region Arrange 
            #endregion

            #region Act  
            var result = await _graphQLRepository.GetAllContinentsAsync();

            #endregion

            #region Assert  

            result.Should().NotBeNullOrEmpty();
            #endregion
        }


        [TestMethod]
        public async System.Threading.Tasks.Task GetContinent_ReturnSpecificContinent()
        {
            #region Arrange 

            var expectedContinent = new Continent
            {
                code = "AF",
                name = "Africa"
            };

            #endregion

            #region Act  
            var result = await _graphQLRepository.GetContinentByCodeAsync(expectedContinent.code);

            #endregion

            #region Assert  

            result.Should().BeEquivalentTo(expectedContinent, c => c.Excluding(continent => continent.countries));
            result.countries.Should().NotBeNullOrEmpty();
            #endregion

        }


        [TestMethod]
        public async System.Threading.Tasks.Task GetContinent_ContinentNotExist_ReturnNull()
        {
            #region Arrange 


            #endregion

            #region Act  
            var result = await _graphQLRepository.GetContinentByCodeAsync("false");

            #endregion

            #region Assert  

            result.Should().BeNull();
            #endregion

        }


    }
}
