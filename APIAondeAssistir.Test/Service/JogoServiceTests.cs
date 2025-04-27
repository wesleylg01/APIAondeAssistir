

using APIAondeAssistir.Application.Services;
using APIAondeAssistir.Domain.Entities;
using APIAondeAssistir.Domain.Interfaces;
using APIAondeAssistir.DTOs;
using Moq;

namespace APIAondeAssistir.Test.Service
{
    public class JogoServiceTests
    {
        private readonly Mock<IJogoRepository> _jogoRepository;
        private readonly Mock<ICampeonatoRepository> _campeonatoRepository;
        private readonly Mock<ITimeRepository> _timeRepository;
        private readonly Mock<ITransmissorRepository> _transmissorRepository;
        private readonly JogoService _jogoService;

        public JogoServiceTests()
        {
            _jogoRepository = new Mock<IJogoRepository>();
            _campeonatoRepository = new Mock<ICampeonatoRepository>();
            _timeRepository = new Mock<ITimeRepository>();
            _transmissorRepository = new Mock<ITransmissorRepository>();
            _jogoService = new JogoService(_jogoRepository.Object, _campeonatoRepository.Object, _timeRepository.Object, _transmissorRepository.Object);
        }

        [Fact]
        public async Task GetAllAsync_Success()
        {
            //Arrange
            var jogos = new List<Jogo>
            {
                new Jogo{Codigo = 1, CodigoTime1 = 1, CodigoTime2 = 2, CodigoCampeonato = 1, CodigoTransmissors = new List<int>{5,18}, DataDia = DateTime.Now, isVisible = true, JogoUrl = String.Empty, Rodada = 1 },
                new Jogo{Codigo = 2, CodigoTime1 = 13, CodigoTime2 = 23, CodigoCampeonato = 1, CodigoTransmissors = new List<int>{5,18}, DataDia = DateTime.Now, isVisible = true, JogoUrl = String.Empty, Rodada = 1 },
                new Jogo{Codigo = 3, CodigoTime1 = 16, CodigoTime2 = 12, CodigoCampeonato = 1, CodigoTransmissors = new List<int>{5,18}, DataDia = DateTime.Now, isVisible = true, JogoUrl = String.Empty, Rodada = 1 }
            };

            _jogoRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(jogos);

            //Act
            var result = await _jogoService.GetAll();

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetById_Success()
        {
            //Arrange
            int id = 123;
            var jogo = new Jogo { Codigo = id, CodigoTime1 = 1, CodigoTime2 = 2, CodigoCampeonato = 1, CodigoTransmissors = new List<int> { 5, 18 }, DataDia = DateTime.Now, isVisible = true, JogoUrl = String.Empty, Rodada = 1 };

            _jogoRepository.Setup(repo => repo.GetById(id)).ReturnsAsync(jogo);

            //Act
            var result = await _jogoService.GetById(id);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(id, result.Codigo);
        }

        [Fact]
        public async Task CreateAsync_Success()
        {
            //Arrange
            var jogo = new Jogo { Codigo = 1, CodigoTime1 = 1, CodigoTime2 = 2, CodigoCampeonato = 1, CodigoTransmissors = new List<int> { 5, 18 }, DataDia = DateTime.Now, isVisible = true, JogoUrl = String.Empty, Rodada = 1 };

            _jogoRepository.Setup(repo => repo.CreateAsync(jogo)).ReturnsAsync(true);

            //Act
            var result = await _jogoService.CreateAsync(jogo);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public async Task UpdateAsync_Success()
        {
            //Arrange
            var jogo = new Jogo { Codigo = 1, CodigoTime1 = 1, CodigoTime2 = 2, CodigoCampeonato = 1, CodigoTransmissors = new List<int> { 5, 18 }, DataDia = DateTime.Now, isVisible = true, JogoUrl = String.Empty, Rodada = 1 };

            _jogoRepository.Setup(repo => repo.UpdateAsync(jogo)).ReturnsAsync(true);

            //Act
            var result = await _jogoService.UpdateAsync(jogo);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public async Task DeleteAsync_Success()
        {
            //Arrange
            int id = 123;

            _jogoRepository.Setup(repo => repo.DeleteAsync(id)).ReturnsAsync(true);

            //Act
            var result = await _jogoService.DeleteAsync(id);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public async Task GetByRodada_Success()
        {
            //Arrange
            int rodada = 10;
            int idCampeonato = 6;
            int idTime1 = 1;
            int idTime2 = 2;

            var jogos = new List<Jogo>
            {
                new Jogo{Codigo = 1, CodigoTime1 = 1, CodigoTime2 = 2, CodigoCampeonato = 6, CodigoTransmissors = new List<int>{5,18}, DataDia = DateTime.Now, isVisible = true, JogoUrl = String.Empty, Rodada = 10 },
                new Jogo{Codigo = 2, CodigoTime1 = 1, CodigoTime2 = 2, CodigoCampeonato = 6, CodigoTransmissors = new List<int>{5,18}, DataDia = DateTime.Now, isVisible = true, JogoUrl = String.Empty, Rodada = 10 },
                new Jogo{Codigo = 3, CodigoTime1 = 1, CodigoTime2 = 2, CodigoCampeonato = 6, CodigoTransmissors = new List<int>{5,18}, DataDia = DateTime.Now, isVisible = true, JogoUrl = String.Empty, Rodada = 10 }
            };
            var mandante = new Time { Codigo = 1, Nome = "São Paulo", EscudoUrl = "escudo1.png" };
            var visitante = new Time { Codigo = 2, Nome = "Boca Juniors", EscudoUrl = "escudo31.png" };
            var campeonato = new Campeonato { Codigo = 6, Nome = "Brasileirão", LogoUrl = "Brasileirao.png" };

            var response = new List<JogosRodadaDto>
            {
                new JogosRodadaDto{ Codigo = jogos[0].Codigo, Time1 = mandante.Nome, Time2 = visitante.Nome, DataJogo = DateTime.Now },
                new JogosRodadaDto{ Codigo = jogos[1].Codigo, Time1 = mandante.Nome, Time2 = visitante.Nome, DataJogo = DateTime.Now },
                new JogosRodadaDto{ Codigo = jogos[2].Codigo, Time1 = mandante.Nome, Time2 = visitante.Nome, DataJogo = DateTime.Now }
            };

            _jogoRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(jogos);
            _campeonatoRepository.Setup(repo => repo.GetById(idCampeonato)).ReturnsAsync(campeonato);
            _timeRepository.Setup(repo => repo.GetById(idTime1)).ReturnsAsync(mandante);
            _timeRepository.Setup(repo => repo.GetById(idTime2)).ReturnsAsync(visitante);

            //Act
            var result = await _jogoService.GetByRodada(rodada);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(jogos.Count, result.Count);
        }

        [Fact]
        public async Task GetByRodada_Error()
        {
            //Arrange
            int rodada = 1;

            var jogos = new List<Jogo>();

            var responseVazio = new List<JogosRodadaDto>();

            _jogoRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(jogos);

            //Act
            var result = await _jogoService.GetByRodada(rodada);

            //Assert
            Assert.Equal(result, responseVazio);
        }

        [Fact]
        public async Task GetJogosListByTime_Success()
        {
            //Arrange
            int idCampeonato = 6;
            int idTime = 1;

            var jogos = new List<Jogo>
            {
                new Jogo{Codigo = 1, CodigoTime1 = idTime, CodigoTime2 = 1, CodigoCampeonato = 6, CodigoTransmissors = new List<int>{5,18}, DataDia = DateTime.Now, isVisible = true, JogoUrl = String.Empty, Rodada = 10 },
                new Jogo{Codigo = 2, CodigoTime1 = 1, CodigoTime2 = idTime, CodigoCampeonato = 6, CodigoTransmissors = new List<int>{5,18}, DataDia = DateTime.Now, isVisible = true, JogoUrl = String.Empty, Rodada = 10 },
                new Jogo{Codigo = 3, CodigoTime1 = 1, CodigoTime2 = idTime, CodigoCampeonato = 6, CodigoTransmissors = new List<int>{5,18}, DataDia = DateTime.Now, isVisible = true, JogoUrl = String.Empty, Rodada = 10 }
            };
            var mandante = new Time { Codigo = idTime, Nome = "São Paulo", EscudoUrl = "escudo1.png" };
            var visitante = new Time { Codigo = idTime, Nome = "São Paulo", EscudoUrl = "escudo1.png" };
            var campeonato = new Campeonato { Codigo = 6, Nome = "Brasileirão", LogoUrl = "Brasileirao.png" };

            var response = new List<JogosRodadaDto>
            {
                new JogosRodadaDto{ Codigo = jogos[0].Codigo, Time1 = mandante.Nome, Time2 = visitante.Nome, DataJogo = DateTime.Now },
                new JogosRodadaDto{ Codigo = jogos[1].Codigo, Time1 = mandante.Nome, Time2 = visitante.Nome, DataJogo = DateTime.Now },
                new JogosRodadaDto{ Codigo = jogos[2].Codigo, Time1 = mandante.Nome, Time2 = visitante.Nome, DataJogo = DateTime.Now }
            };

            _jogoRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(jogos);
            _campeonatoRepository.Setup(repo => repo.GetById(idCampeonato)).ReturnsAsync(campeonato);
            _timeRepository.Setup(repo => repo.GetById(jogos[0].CodigoTime1)).ReturnsAsync(mandante);
            _timeRepository.Setup(repo => repo.GetById(jogos[0].CodigoTime2)).ReturnsAsync(visitante);

            //Act
            var result = await _jogoService.GetJogosListByTime(idTime);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(jogos.Count, result.Count);
        }

        [Fact]
        public async Task GetJogosListByTimeError()
        {
            //Arrange
            int rodada = 1;

            var jogos = new List<Jogo>();

            var responseVazio = new List<JogosRodadaDto>();

            _jogoRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(jogos);

            //Act
            var result = await _jogoService.GetJogosListByTime(rodada);

            //Assert
            Assert.Equal(result, responseVazio);
        }

        [Fact]
        public async Task GetJogoDetails_Success()
        {
            //Arrange
            int idJogo = 1;

            var jogo = new Jogo{Codigo = idJogo, CodigoTime1 = 1, CodigoTime2 = 2, CodigoCampeonato = 6, CodigoTransmissors = new List<int>{5,18}, DataDia = DateTime.Now, isVisible = true, JogoUrl = String.Empty, Rodada = 10 };

            var mandante = new Time { Codigo = 1, Nome = "São Paulo", EscudoUrl = "escudo1.png" };
            var visitante = new Time { Codigo = 2, Nome = "Boca Juniors", EscudoUrl = "escudo31.png" };
            var campeonato = new Campeonato { Codigo = 6, Nome = "Brasileirão", LogoUrl = "Brasileirao.png" };
            var transmissor = new Transmissor { Codigo = 8, Gratis = true, ImageUrl = "globo.png", Nome = "Globo", Url = "globoplay.com" };

            var response = new JogoDetail
            {
                Codigo = jogo.Codigo,
                DataDia = jogo.DataDia,
                Mandante = mandante.Nome,
                Visitante = mandante.Nome,
                Transmissors = new List<Transmissor> { transmissor }
            };

            _jogoRepository.Setup(repo => repo.GetById(idJogo)).ReturnsAsync(jogo);
            _campeonatoRepository.Setup(repo => repo.GetById(jogo.Codigo)).ReturnsAsync(campeonato);
            _timeRepository.Setup(repo => repo.GetById(jogo.CodigoTime1)).ReturnsAsync(mandante);
            _timeRepository.Setup(repo => repo.GetById(jogo.CodigoTime2)).ReturnsAsync(visitante);
            _transmissorRepository.Setup(repo => repo.GetById(jogo.CodigoTransmissors[0])).ReturnsAsync(transmissor);

            //Act
            var result = await _jogoService.GetJogoDetails(idJogo);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(jogo.Codigo, result.Codigo);
        }

        [Fact]
        public async Task GetJogoDetails_Error()
        {
            //Arrange
            int idJogo = 1;

            var jogo = new Jogo();

            var responseVazio = new JogoDetail();

            _jogoRepository.Setup(repo => repo.GetById(idJogo)).ReturnsAsync(jogo);

            //Act
            var result = await _jogoService.GetJogoDetails(idJogo);

            //Assert
            Assert.Equal(result.Codigo, responseVazio.Codigo);
        }

        [Fact]
        public async Task DeleteAnterioresAsync_Success()
        {
            //Arrange
            _jogoRepository.Setup(repo => repo.DeleteAnterioresAsync()).ReturnsAsync(true);

            //Act
            var result = await _jogoService.DeleteAnterioresAsync();

            //Assert
            Assert.True(result);
        }
    }
}
