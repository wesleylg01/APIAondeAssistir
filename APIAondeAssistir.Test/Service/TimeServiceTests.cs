using APIAondeAssistir.Application.Services;
using APIAondeAssistir.Domain.Entities;
using APIAondeAssistir.Domain.Interfaces;
using Moq;

namespace APIAondeAssistir.Tests.Services
{
    public class TimeServiceTests
    {
        private readonly Mock<ITimeRepository> _timeRepositoryMock;
        private readonly TimeService _timeService;
        public TimeServiceTests()
        {
            _timeRepositoryMock = new Mock<ITimeRepository>();
            _timeService = new TimeService(_timeRepositoryMock.Object);
        }

        [Fact]
        public async Task GetAllAsync_Success()
        {
            //Arrange
            var fakeTimes = new List<Time>
            {
                new Time { Codigo = 1, Nome = "São Paulo", EscudoUrl = "escudo1.png" },
                new Time { Codigo = 2, Nome = "Botafogo-SP", EscudoUrl = "escudo2.png" },
                new Time { Codigo = 3, Nome = "Sampaio Corrêa", EscudoUrl = "escudo3.png" }
            };

            _timeRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(fakeTimes);

            // Act
            var result = await _timeService.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(3, result.Count);
        }

        [Fact]
        public async Task GetById_Success()
        {
            //Arrange
            int id = 1;

            var time = new Time { Codigo = 1, Nome = "São Paulo", EscudoUrl = "escudo1.png" };

            _timeRepositoryMock.Setup(repo => repo.GetById(id)).ReturnsAsync(time);

            // Act
            var result = await _timeService.GetById(id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(id, result.Codigo);
            Assert.Equal("São Paulo", result.Nome);
        }
        [Fact]
        public async Task GetById_WhenIdInvalid()
        {
            // Arrange
            _timeRepositoryMock.Setup(repo => repo.GetById(It.IsAny<int>())).ReturnsAsync((Time)null);

            // Act & Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(() => _timeService.GetById(1));
        }
        [Fact]
        public async Task GetById_WhenTimeCodigoIsZero()
        {
            // Arrange
            var fakeTime = new Time { Codigo = 0, Nome = "Time Inválido" };
            _timeRepositoryMock.Setup(repo => repo.GetById(1)).ReturnsAsync(fakeTime);

            // Act & Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(() => _timeService.GetById(1));
        }

        [Fact]
        public async Task GetById_WhenTimeNomeIsNull()
        {
            // Arrange
            var fakeTime = new Time { Codigo = 1, Nome = null };
            _timeRepositoryMock.Setup(repo => repo.GetById(1)).ReturnsAsync(fakeTime);

            // Act & Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(() => _timeService.GetById(1));
        }

        [Fact]
        public async Task CreateAsync_Success()
        {
            // Arrange
            var time = new Time { Codigo = 1, Nome = "São Paulo", EscudoUrl = "escudo1.png" };

            _timeRepositoryMock.Setup(repo => repo.CreateAsync(time)).ReturnsAsync(true);

            // Act
            var result = await _timeService.CreateAsync(time);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task CreateAsync_WhenIdInvalid()
        {
            // Arrange
            var time = new Time { Codigo = 0 };

            _timeRepositoryMock.Setup(repo => repo.CreateAsync(time)).ReturnsAsync(false);

            // Act
            var result = await _timeService.CreateAsync(time);

            // Assert
            Assert.True(!result);
        }
        [Fact]
        public async Task UpdateAsync_Success()
        {
            // Arrange
            var time = new Time { Codigo = 1, Nome = "São Paulo", EscudoUrl = "escudo1.png" };

            _timeRepositoryMock.Setup(repo => repo.UpdateAsync(time)).ReturnsAsync(true);

            // Act
            var result = await _timeService.UpdateAsync(time);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task UpdateAsync_WhenIdInvalid()
        {
            // Arrange
            var time = new Time { Codigo = 0 };

            _timeRepositoryMock.Setup(repo => repo.UpdateAsync(time)).ReturnsAsync(false);

            // Act
            var result = await _timeService.UpdateAsync(time);

            // Assert
            Assert.True(!result);
        }

        [Fact]
        public async Task Delete_Success()
        {
            //Arrange
            int id = 1;

            _timeRepositoryMock.Setup(repo => repo.DeleteAsync(id)).ReturnsAsync(true);

            //Act
            var result = await _timeService.DeleteAsync(id);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public async Task Delete_WhenIdInvalid()
        {
            //Arrange
            int id = 0;

            _timeRepositoryMock.Setup(repo => repo.DeleteAsync(id)).ReturnsAsync(false);

            //Act
            var result = await _timeService.DeleteAsync(id);

            //Assert
            Assert.True(!result);
        }
    }
}