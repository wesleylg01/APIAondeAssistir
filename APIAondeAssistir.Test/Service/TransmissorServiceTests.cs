using APIAondeAssistir.Application.Services;
using APIAondeAssistir.Domain.Entities;
using APIAondeAssistir.Domain.Interfaces;
using Moq;

namespace APIAondeAssistir.Test.Service
{
    public class TransmissorServiceTests
    {
        private readonly Mock<ITransmissorRepository> _transmissorRepositoryMock;
        private readonly TransmissorService _transmissorService;

        public TransmissorServiceTests()
        {
            _transmissorRepositoryMock = new Mock<ITransmissorRepository>();
            _transmissorService = new TransmissorService(_transmissorRepositoryMock.Object);
        }

        [Fact]
        public async Task GetAllAsync_Success()
        {
            // Arrange
            var transmissorList = new List<Transmissor>
                {
                    new Transmissor(){ Codigo = 1, Nome = "ESPN", ImageUrl = "http://example.com/espn.png",Url = "http://example.com", Gratis = false},
                    new Transmissor(){ Codigo = 2, Nome = "Cazé TV", ImageUrl = "http://example.com/caze.png",Url = "http://example.com", Gratis = true}
                };

            _transmissorRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(transmissorList);
            // Act
            var result = await _transmissorService.GetAllAsync();
            // Assert
            Assert.NotNull(result);
            Assert.Equal(transmissorList.Count, result.Count); 
        }

        [Fact]
        public async Task GetById_Success()
        {
            // Arrange
            int id = 1;
            var transmissor = new Transmissor(){ Codigo = 1, Nome = "ESPN", ImageUrl = "http://example.com/espn.png",Url = "http://example.com", Gratis = false};

            _transmissorRepositoryMock.Setup(repo => repo.GetById(id)).ReturnsAsync(transmissor);
            // Act
            var result = await _transmissorService.GetById(id);
            // Assert
            Assert.NotNull(result);
            Assert.Equal(transmissor.Nome, result.Nome);
        }

        [Fact]
        public async Task GetById_Fail()
        {
            // Arrange
            int id = 0;

            _transmissorRepositoryMock.Setup(repo => repo.GetById(id))
                .ThrowsAsync(new KeyNotFoundException("Transmissor não encontrado"));

            // Act
            var exception = await Assert.ThrowsAsync<KeyNotFoundException>(() => _transmissorService.GetById(id));

            //Assert
            Assert.Equal("Transmissor não encontrado", exception.Message);
        }


        [Fact]
        public async Task Create_Success()
        {
            // Arrange
            var transmissor = new Transmissor() { Codigo = 1, Nome = "ESPN", ImageUrl = "http://example.com/espn.png", Url = "http://example.com", Gratis = false };

            _transmissorRepositoryMock.Setup(repo => repo.CreateAsync(transmissor)).ReturnsAsync(true);
            
            // Act
            var result = await _transmissorService.CreateAsync(transmissor);

            // Assert
            Assert.True(result);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        public async Task Create_Fail(int codigo)
        {
            // Arrange
            var transmissor = new Transmissor() { Codigo = codigo, Nome = "ESPN", ImageUrl = "http://example.com/espn.png", Url = "http://example.com", Gratis = false };

            _transmissorRepositoryMock.Setup(repo => repo.CreateAsync(transmissor)).ReturnsAsync(false);

            // Act
            var result = await _transmissorService.CreateAsync(transmissor);

            // Assert
            Assert.False(result);
        }
        [Fact]
        public async Task Update_Success()
        {
            // Arrange
            var transmissor = new Transmissor() { Codigo = 1, Nome = "ESPN", ImageUrl = "http://example.com/espn.png", Url = "http://example.com", Gratis = false };

            _transmissorRepositoryMock.Setup(repo => repo.UpdateAsync(transmissor)).ReturnsAsync(true);

            // Act
            var result = await _transmissorService.UpdateAsync(transmissor);

            // Assert
            Assert.True(result);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        public async Task Update_Fail(int codigo)
        {
            // Arrange
            var transmissor = new Transmissor() { Codigo = codigo, Nome = "ESPN", ImageUrl = "http://example.com/espn.png", Url = "http://example.com", Gratis = false };

            _transmissorRepositoryMock.Setup(repo => repo.UpdateAsync(transmissor)).ReturnsAsync(false);

            // Act
            var result = await _transmissorService.UpdateAsync(transmissor);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task Delete_Success()
        {
            //Arrange
            int codigo = 1;

            _transmissorRepositoryMock.Setup(repo => repo.DeleteAsync(codigo)).ReturnsAsync(true);

            // Act
            var result = await _transmissorService.DeleteAsync(codigo);
            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task Delete_Fail()
        {
            //Arrange
            int codigo = 1;

            _transmissorRepositoryMock.Setup(repo => repo.DeleteAsync(codigo)).ReturnsAsync(false);

            // Act
            var result = await _transmissorService.DeleteAsync(codigo);
            // Assert
            Assert.False(result);
        }
    }
}
