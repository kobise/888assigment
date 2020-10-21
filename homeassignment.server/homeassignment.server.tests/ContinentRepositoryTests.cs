using FluentAssertions;
using FluentAssertions.Specialized;
using homeassignment.server.GeaphQL;
using homeassignment.server.Models;
using homeassignment.server.Redis;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Serilog;
using Serilog.Core;
using Serilog.Sinks.InMemory;
using Serilog.Sinks.InMemory.Assertions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace homeassignment.server.tests
{
    [TestClass]
    public class ContinentRepositoryTests
    {
        private ILogger _logger;

        public ContinentRepositoryTests()
        {
            _logger = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .WriteTo.Console()
                .WriteTo.InMemory()
                .CreateLogger();
        }

        private static Mock<IGraphQLRepository> GenerateGraphQLMock(ICollection<Continent> continents = null, Continent continent = null)
        {
            var graphQLMock = new Mock<IGraphQLRepository>();
            graphQLMock.Setup(g => g.GetAllContinentsAsync())
                .ReturnsAsync(continents);
            if (continent != null)
            {
                graphQLMock.Setup(g => g.GetContinentByCodeAsync(It.Is<string>(s => s == continent.code)))
                    .ReturnsAsync(continent);
            }
            return graphQLMock;
        }

        private static Mock<ICacheContinentRepository> GenerateCacheRepositoryMock(ICollection<Continent> continents = null, Continent continent = null, bool isContinentInCache = true)
        {
            var cacheRepositoryMock = new Mock<ICacheContinentRepository>();
            cacheRepositoryMock.Setup(c => c.GetAllContinentsAsync())
                .ReturnsAsync(continents);
            if (isContinentInCache)
            {
                cacheRepositoryMock.Setup(c => c.GetContinentByCodeAsync(It.Is<string>(s => s == continent.code)))
                .ReturnsAsync(continent);
            }

            cacheRepositoryMock.Setup(c => c.SaveAllContinentsAsync(It.IsAny<ICollection<Continent>>()));
            cacheRepositoryMock.Setup(c => c.SaveContinentByCodeAsync(It.Is<string>(s => s == continent.code), It.IsAny<Continent>()));
            return cacheRepositoryMock;
        }



        #region Get All Continents Tests


        [TestMethod]
        public async Task GetAllContinentsAsync_ContinentsNotInCache_SaveInCache()
        {
            #region Arrange 
            var continents = new List<Continent>
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
            Mock<ICacheContinentRepository> cacheRepositoryMock = GenerateCacheRepositoryMock();
            Mock<IGraphQLRepository> graphQLMock = GenerateGraphQLMock(continents);
            var continentRepository = new ContinentRepository(_logger, cacheRepositoryMock.Object, graphQLMock.Object);
            #endregion


            #region Act  
            var result = await continentRepository.GetAllContinentsAsync();
            #endregion


            #region Assert  
            cacheRepositoryMock.Verify(c => c.SaveAllContinentsAsync(It.IsAny<ICollection<Continent>>()));
            #endregion
        }
        [TestMethod]
        public async Task GetAllContinentsAsync_ContinentsNotInCache_TryGetContinentsFromCache()
        {
            #region Arrange 
            var continents = new List<Continent>
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
            Mock<ICacheContinentRepository> cacheRepositoryMock = GenerateCacheRepositoryMock();
            Mock<IGraphQLRepository> graphQLMock = GenerateGraphQLMock(continents);
            var continentRepository = new ContinentRepository(_logger, cacheRepositoryMock.Object, graphQLMock.Object);
            #endregion


            #region Act  
            var result = await continentRepository.GetAllContinentsAsync();
            #endregion


            #region Assert  
            cacheRepositoryMock.Verify(c => c.GetAllContinentsAsync());
            #endregion
        }


        [TestMethod]
        public async Task GetAllContinentsAsync_ContinentsNotInCache_GetContinentsCollectionFromGraphQL()
        {
            #region Arrange 
            var continents = new List<Continent>
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
            Mock<ICacheContinentRepository> cacheRepositoryMock = GenerateCacheRepositoryMock();
            Mock<IGraphQLRepository> graphQLMock = GenerateGraphQLMock(continents);
            var continentRepository = new ContinentRepository(_logger, cacheRepositoryMock.Object, graphQLMock.Object);
            #endregion


            #region Act  
            var result = await continentRepository.GetAllContinentsAsync();
            #endregion


            #region Assert  
            graphQLMock.Verify(g => g.GetAllContinentsAsync());
            #endregion
        }


        [TestMethod]
        public async Task GetAllContinentsAsync_ContinentsNotInCache_ReturnContinentsCollectionFromGraphQL()
        {
            #region Arrange 
            var continents = new List<Continent>
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
            Mock<ICacheContinentRepository> cacheRepositoryMock = GenerateCacheRepositoryMock();
            Mock<IGraphQLRepository> graphQLMock = GenerateGraphQLMock(continents);
            var continentRepository = new ContinentRepository(_logger, cacheRepositoryMock.Object, graphQLMock.Object);
            #endregion


            #region Act  
            var result = await continentRepository.GetAllContinentsAsync();
            #endregion


            #region Assert  
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(continents);
            #endregion
        }


        [TestMethod]
        public async Task GetAllContinentsAsync_CacheThrowException_LogErrorAndReturnContinentsCollectionFromGraphQL()
        {
            #region Arrange 
            var continents = new List<Continent>
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
            Mock<ICacheContinentRepository> cacheRepositoryMock = new Mock<ICacheContinentRepository>();
            cacheRepositoryMock.Setup(c => c.GetAllContinentsAsync())
                .ThrowsAsync(new Exception("some exceptio on get"));
            cacheRepositoryMock.Setup(c => c.SaveAllContinentsAsync(It.IsAny<ICollection<Continent>>()))
                .ThrowsAsync(new Exception("some exceptio on save"));
            Mock<IGraphQLRepository> graphQLMock = GenerateGraphQLMock(continents);
            var continentRepository = new ContinentRepository(_logger, cacheRepositoryMock.Object, graphQLMock.Object);
            #endregion


            #region Act  
            var result = await continentRepository.GetAllContinentsAsync();
            #endregion


            #region Assert  
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(continents);
            InMemorySink.Instance.Should().HaveMessage(ContinentRepository.GetDataFromCacheError)
                .WithLevel(Serilog.Events.LogEventLevel.Error);
            InMemorySink.Instance.Should().HaveMessage(ContinentRepository.SaveDataInCacheError)
                .WithLevel(Serilog.Events.LogEventLevel.Error);
            #endregion
        }
        [TestMethod]
        public async Task GetAllContinentsAsync_NotExistInRedis_GraphQLThrowException_LogErrorAndThrowException()
        {
            #region Arrange 
            var continents = new List<Continent>
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
            Mock<ICacheContinentRepository> cacheRepositoryMock = GenerateCacheRepositoryMock();
            Mock<IGraphQLRepository> graphQLMock = new Mock<IGraphQLRepository>();
            graphQLMock.Setup(g => g.GetAllContinentsAsync())
                .ThrowsAsync(new Exception("test exception"));
            var continentRepository = new ContinentRepository(_logger, cacheRepositoryMock.Object, graphQLMock.Object);
            #endregion

            #region Act  
            bool isExceptionThrow = false;
            ICollection<Continent> result = null;
            try
            {
                result = await continentRepository.GetAllContinentsAsync();

            }
            catch (Exception e)
            {
                isExceptionThrow = e.Message == ContinentRepository.GetDataFromGraphQLError;
            }
            #endregion


            #region Assert  
            result.Should().BeNull();
            isExceptionThrow.Should().BeTrue();
            InMemorySink.Instance.Should().HaveMessage(ContinentRepository.GetDataFromGraphQLError)
                .WithLevel(Serilog.Events.LogEventLevel.Error);
            #endregion
        }

        [TestMethod]
        public async Task GetAllContinentsAsync_ContinentsExistInCache_ReturnContinentsCollectionFromCache()
        {
            #region Arrange 
            var continents = new List<Continent>
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
            Mock<ICacheContinentRepository> cacheRepositoryMock = GenerateCacheRepositoryMock(continents);
            Mock<IGraphQLRepository> graphQLMock = GenerateGraphQLMock(continents);
            var continentRepository = new ContinentRepository(_logger, cacheRepositoryMock.Object, graphQLMock.Object);
            #endregion


            #region Act  
            var result = await continentRepository.GetAllContinentsAsync();
            #endregion


            #region Assert  
            cacheRepositoryMock.Verify(c => c.GetAllContinentsAsync());
            graphQLMock.VerifyNoOtherCalls();
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(continents);

            #endregion
        }

        #endregion

        #region Get Sepecific Continent Tests

        [TestMethod]
        public async Task GetContinentAsync_ContinentNotInCache_SaveInCacheAndReturnContinentFromCache()
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
            Mock<ICacheContinentRepository> cacheRepositoryMock = GenerateCacheRepositoryMock(continent: expectedContinent, isContinentInCache: false);
            Mock<IGraphQLRepository> graphQLMock = GenerateGraphQLMock(continent: expectedContinent);
            var continentRepository = new ContinentRepository(_logger, cacheRepositoryMock.Object, graphQLMock.Object);
            #endregion


            #region Act  
            var result = await continentRepository.GetContinentByCodeAsync(expectedContinent.code);
            #endregion


            #region Assert  
            cacheRepositoryMock.Verify(c => c.GetContinentByCodeAsync(It.Is<string>(s => s == expectedContinent.code)));
            cacheRepositoryMock.Verify(c => c.SaveContinentByCodeAsync(It.Is<string>(s => s == expectedContinent.code), It.IsAny<Continent>()));
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(expectedContinent);
            #endregion
        }


        [TestMethod]
        public async Task GetContinentAsync_ContinentNotInCache_ReturnContinentFromGraphQL()
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
            Mock<ICacheContinentRepository> cacheRepositoryMock = GenerateCacheRepositoryMock(continent: expectedContinent, isContinentInCache: false);
            Mock<IGraphQLRepository> graphQLMock = GenerateGraphQLMock(continent: expectedContinent);
            var continentRepository = new ContinentRepository(_logger, cacheRepositoryMock.Object, graphQLMock.Object);
            #endregion


            #region Act  
            var result = await continentRepository.GetContinentByCodeAsync(expectedContinent.code);
            #endregion


            #region Assert  
            graphQLMock.Verify(g => g.GetContinentByCodeAsync(It.Is<string>(s => s == expectedContinent.code)));
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(expectedContinent);
            #endregion
        }


        [TestMethod]
        public async Task GetContinentAsync_ContinentExistInCache_ReturnContinentFromCache()
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
            Mock<ICacheContinentRepository> cacheRepositoryMock = GenerateCacheRepositoryMock(continent: expectedContinent, isContinentInCache: true);
            Mock<IGraphQLRepository> graphQLMock = GenerateGraphQLMock(continent: expectedContinent);
            var continentRepository = new ContinentRepository(_logger, cacheRepositoryMock.Object, graphQLMock.Object);
            #endregion


            #region Act  
            var result = await continentRepository.GetContinentByCodeAsync(expectedContinent.code);
            #endregion


            #region Assert  
            cacheRepositoryMock.Verify(c => c.GetContinentByCodeAsync(It.Is<string>(s => s == expectedContinent.code)));
            cacheRepositoryMock.Verify(c => c.SaveContinentByCodeAsync(It.Is<string>(s => s == expectedContinent.code),It.IsAny<Continent>()));
            graphQLMock.VerifyNoOtherCalls();
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(expectedContinent);
            #endregion
        }


        [TestMethod]
        public async Task GetContinentAsync_CacheThrowException_LogErrorAndReturnContinentFromGraphQL()
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
            Mock<ICacheContinentRepository> cacheRepositoryMock = new Mock<ICacheContinentRepository>();
            cacheRepositoryMock.Setup(c => c.GetContinentByCodeAsync(It.IsAny<string>()))
                .ThrowsAsync(new Exception("some exceptio"));
            cacheRepositoryMock.Setup(c => c.SaveContinentByCodeAsync(It.IsAny<string>(), It.IsAny<Continent>()))
                .ThrowsAsync(new Exception("some exceptio")); Mock<IGraphQLRepository> graphQLMock = GenerateGraphQLMock(continent: expectedContinent);
            var continentRepository = new ContinentRepository(_logger, cacheRepositoryMock.Object, graphQLMock.Object);
            #endregion


            #region Act  
            var result = await continentRepository.GetContinentByCodeAsync(expectedContinent.code);
            #endregion


            #region Assert  
            graphQLMock.Verify(g => g.GetContinentByCodeAsync(It.Is<string>(s => s == expectedContinent.code)));
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(expectedContinent);

            InMemorySink.Instance.Should().HaveMessage(ContinentRepository.GetDataFromCacheError)
                .WithLevel(Serilog.Events.LogEventLevel.Error);
            InMemorySink.Instance.Should().HaveMessage(ContinentRepository.SaveDataInCacheError)
                .WithLevel(Serilog.Events.LogEventLevel.Error);
            #endregion
        }
        [TestMethod]
        public async Task GetContinentAsync_ContinentNotExistInCache_GraphQLThrowException_LogErrorAndThrowException()
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
            Mock<ICacheContinentRepository> cacheRepositoryMock = GenerateCacheRepositoryMock(continent: expectedContinent, isContinentInCache:false);
            Mock<IGraphQLRepository> graphQLMock = new Mock<IGraphQLRepository>();
            graphQLMock.Setup(g => g.GetContinentByCodeAsync(It.Is<string>(s => s == expectedContinent.code)))
            .ThrowsAsync(new Exception("test exception"));
            var continentRepository = new ContinentRepository(_logger, cacheRepositoryMock.Object, graphQLMock.Object);
            #endregion


            #region Act  
            bool isExceptionThrow = false;
            Continent result = null;
            try
            {
                result = await continentRepository.GetContinentByCodeAsync(expectedContinent.code);

            }
            catch (Exception e)
            {
                isExceptionThrow = e.Message == ContinentRepository.GetDataFromGraphQLError;
            }
            #endregion


            #region Assert  
            result.Should().BeNull();
            isExceptionThrow.Should().BeTrue();
            InMemorySink.Instance.Should().HaveMessage(ContinentRepository.GetDataFromGraphQLError)
                .WithLevel(Serilog.Events.LogEventLevel.Error);
            #endregion
        }

        #endregion

    }
}
