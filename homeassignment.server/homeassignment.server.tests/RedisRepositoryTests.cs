using FluentAssertions;
using homeassignment.server.Models;
using homeassignment.server.Redis;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Serilog;
using Serilog.Sinks.InMemory;
using StackExchange.Redis;
using System.Collections;
using System.Collections.Generic;

namespace homeassignment.server.tests
{
    [TestClass]
    public class RedisRepositoryTests
    {
        RedisRepository _redisRepository;
        IDatabase _conection;
        private ILogger _logger;

       
        public RedisRepositoryTests()
        {
            _logger = new LoggerConfiguration()
               .MinimumLevel.Verbose()
               .WriteTo.Console()
               .WriteTo.InMemory()
               .CreateLogger();
            ConnectionMultiplexer muxer = ConnectionMultiplexer.Connect("192.168.99.100:6379");
            _conection = muxer.GetDatabase();
            _redisRepository = new RedisRepository(_logger,_conection);
        }


        [TestMethod]
        public async System.Threading.Tasks.Task GetContinentsFromRedis_KeyNotExist_ReturnNull()
        {
            #region Arrange 

            #endregion


            #region Act  
            var result = await _redisRepository.GetAllContinentsAsync();

            #endregion

            #region Assert  

            result.Should().BeNull();
            #endregion


        }

        [TestMethod]
        public async System.Threading.Tasks.Task SaveContinentsAndGetContinentsInRedis_ReturnAllContinents()
        {
            #region Arrange 

            ICollection<Continent> expectedContinents = new List<Continent>
            {
                new Continent
            {
                code = "AF",
                name = "Africa"
            },
                new Continent
            {
                code = "AN",
                name = "Antarctica"
            }

            };

            #endregion

            #region Act  
            await _redisRepository.SaveAllContinentsAsync(expectedContinents);
            var result = await _redisRepository.GetAllContinentsAsync();

            #endregion

            #region Assert  

            result.Should().BeEquivalentTo(expectedContinents);
            #endregion

            #region Cleanup
            _conection.KeyDelete("continents").Should().BeTrue();
            #endregion

        }

        [TestMethod]
        public async System.Threading.Tasks.Task SaveEmptyListOfContinentsAndGetContinentsInRedis_ReturnSpecificContinent()
        {
            #region Arrange 

            ICollection<Continent> expectedContinents = new List<Continent>();

            #endregion

            #region Act  
            await _redisRepository.SaveAllContinentsAsync(expectedContinents);
            var result = await _redisRepository.GetAllContinentsAsync();

            #endregion

            #region Assert  

            result.Should().NotBeNull();
            result.Should().BeEmpty();
            #endregion

            #region Cleanup
            _conection.KeyDelete("continents").Should().BeTrue();
            #endregion

        }

        [TestMethod]
        public async System.Threading.Tasks.Task GetContinent_ContinentNotExist_ReturnNull()
        {
            #region Arrange 


            #endregion

            #region Act  
            var result = await _redisRepository.GetContinentByCodeAsync("notFound");

            #endregion

            #region Assert  

            result.Should().BeNull();
            #endregion

        }

        [TestMethod]
        public async System.Threading.Tasks.Task GetContinent_ReturnExpectedContinent()
        {
            #region Arrange 

            var expectedContinent = new Continent
            {
                code = "AF",
                name = "Africa",
                countries = new List<Country> {
                    new Country
                    {
                        code= "test",

                    }
                }
            };

            #endregion

            #region Act  
            await _redisRepository.SaveContinentByCodeAsync(expectedContinent.code, expectedContinent );
            var result = await _redisRepository.GetContinentByCodeAsync(expectedContinent.code);

            #endregion

            #region Assert  

            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(expectedContinent);
            #endregion

            #region Cleanup
            _conection.KeyDelete($"continents{expectedContinent.code}").Should().BeTrue();
            #endregion
        }


        //[TestMethod]
        //public async System.Threading.Tasks.Task GetContinent_ContinentNotExist_ReturnNull()
        //{
        //    #region Arrange 


        //    #endregion

        //    #region Act  
        //    var result = await _graphQLRepository.GetContinentByCodeAsync("false");

        //    #endregion

        //    #region Assert  

        //    result.Should().BeNull();
        //    #endregion

        //}
    }
}
