using APIAondeAssistir.Application.Interfaces;
using APIAondeAssistir.Application.Services;
using APIAondeAssistir.Domain.Entities;
using APIAondeAssistir.Domain.Interfaces;
using Moq;

namespace APIAondeAssistir.Test.Service
{
    public class CampeonatoServiceTests
    {
        private readonly Mock<ICampeonatoRepository> _campeonatoRepositoryMock;
        private readonly CampeonatoService _campeonatoService;
        public CampeonatoServiceTests()
        {
            _campeonatoRepositoryMock = new Mock<ICampeonatoRepository>();
            _campeonatoService = new CampeonatoService(_campeonatoRepositoryMock.Object);
        }


        [Fact]
        public async Task GetAllAsync_Success()
        {
            //Assert
            var response = new List<Campeonato> { 
                new Campeonato { Codigo = 1, Nome = "Paulistão", LogoUrl = "url" },
                new Campeonato { Codigo = 2, Nome = "Gauchão", LogoUrl = "url" },
                new Campeonato { Codigo = 3, Nome = "Mineiro", LogoUrl = "url" }
            };

            _campeonatoRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(response);

            //Act
            var result = _campeonatoService.GetAllAsync();

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetById_Success()
        {
            //Assert
            var id = 1;
            var response = new Campeonato { Codigo = 1, Nome = "Paulistão", LogoUrl = "url" };

            _campeonatoRepositoryMock.Setup(repo => repo.GetById(id)).ReturnsAsync(response);

            //Act
            var result = _campeonatoService.GetById(id);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(id, result.Result.Codigo);
            Assert.Equal("Paulistão", result.Result.Nome);
        }

        [Fact]
        public async Task CreateAsync_Success()
        {
            // Arrange
            var campeonato = new Campeonato { Codigo = 1, Nome = "Paulistão", LogoUrl = "url" };

            _campeonatoRepositoryMock.Setup(repo => repo.CreateAsync(campeonato)).ReturnsAsync(true);

            // Act
            var result = await _campeonatoService.CreateAsync(campeonato);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task CreateAsync_WhenIdInvalid()
        {
            // Arrange
            var campeonato = new Campeonato { Codigo = 0 };

            _campeonatoRepositoryMock.Setup(repo => repo.CreateAsync(campeonato)).ReturnsAsync(false);

            // Act
            var result = await _campeonatoService.CreateAsync(campeonato);

            // Assert
            Assert.True(!result);
        }

        [Fact]
        public async Task UpdateAsync_Success()
        {
            // Arrange
            var campeonato = new Campeonato { Codigo = 1, Nome = "Paulistão", LogoUrl = "url" };

            _campeonatoRepositoryMock.Setup(repo => repo.UpdateAsync(campeonato)).ReturnsAsync(true);

            // Act
            var result = await _campeonatoService.UpdateAsync(campeonato);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task UpdateAsync_WhenIdInvalid()
        {
            // Arrange
            var campeonato = new Campeonato { Codigo = 0 };

            _campeonatoRepositoryMock.Setup(repo => repo.UpdateAsync(campeonato)).ReturnsAsync(false);

            // Act
            var result = await _campeonatoService.UpdateAsync(campeonato);

            // Assert
            Assert.True(!result);
        }

        [Fact]
        public async Task Delete_Success()
        {
            //Arrange
            int id = 1;

            _campeonatoRepositoryMock.Setup(repo => repo.DeleteAsync(id)).ReturnsAsync(true);

            //Act
            var result = await _campeonatoService.DeleteAsync(id);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public async Task Delete_WhenIdInvalid()
        {
            //Arrange
            int id = 0;

            _campeonatoRepositoryMock.Setup(repo => repo.DeleteAsync(id)).ReturnsAsync(false);

            //Act
            var result = await _campeonatoService.DeleteAsync(id);

            //Assert
            Assert.True(!result);
        }
    }
}
