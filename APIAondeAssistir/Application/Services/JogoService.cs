using APIAondeAssistir.Application.Interfaces;
using APIAondeAssistir.Domain.Entities;
using APIAondeAssistir.Domain.Interfaces;
using APIAondeAssistir.DTOs;

namespace APIAondeAssistir.Application.Services
{
    public class JogoService : IJogoService
    {
        private readonly IJogoRepository _jogoRepository;
        private readonly ICampeonatoRepository _campeonatoRepository;
        private readonly ITimeRepository _timeRepository;
        private readonly ITransmissorRepository _transmissorRepository;

        public JogoService(IJogoRepository jogoRepository, ICampeonatoRepository campeonatoRepository,
            ITimeRepository timeRepository, ITransmissorRepository transmissorRepository)
        {
            _jogoRepository = jogoRepository;
            _campeonatoRepository = campeonatoRepository;
            _timeRepository = timeRepository;
            _transmissorRepository = transmissorRepository;
        }

        public async Task<List<Jogo>> GetAll()
        {
            return await _jogoRepository.GetAllAsync();
        }

        public async Task<Jogo> GetById(int id)
        {
            return await _jogoRepository.GetById(id);
        }

        public async Task<bool> CreateAsync(Jogo jogo)
        {
            if (jogo.Codigo < 1)
                jogo.Codigo = await _jogoRepository.GetNewId();

            return await _jogoRepository.CreateAsync(jogo);
        }

        public async Task<bool> UpdateAsync(Jogo jogo)
        {
            return await _jogoRepository.UpdateAsync(jogo);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _jogoRepository.DeleteAsync(id);
        }

        public async Task<List<JogosRodadaDto>> GetByRodada(int rodada)
        {
            var response = new List<JogosRodadaDto>();

            var jogos = await _jogoRepository.GetAllAsync();
            if (jogos.Count() > 1)
            {
                var jogosRodada = jogos.Where(r => r.Rodada == rodada).ToList();

                foreach (var jogo in jogosRodada)
                {
                    var campeonato = await _campeonatoRepository.GetById(jogo.CodigoCampeonato);
                    var mandante = await _timeRepository.GetById(jogo.CodigoTime1);
                    var visitante = await _timeRepository.GetById(jogo.CodigoTime2);

                    response.Add(new JogosRodadaDto
                    {
                        Codigo = jogo.Codigo,
                        Time1 = mandante.EscudoUrl,
                        Time2 = visitante.EscudoUrl,
                        DataJogo = jogo.DataDia
                    });
                }

                return response.OrderBy(x => x.DataJogo).ToList();
            }

            return response;
        }
        public async Task<List<JogosRodadaDto>> GetJogosListByTime(int id)
        {
            var response = new List<JogosRodadaDto>();

            var jogos = await _jogoRepository.GetAllAsync();
            if (jogos.Count() > 1)
            {
                var jogosRodada = jogos.Where(r => (r.CodigoTime1 == id) || (r.CodigoTime2 == id)).ToList();

                foreach (var jogo in jogosRodada)
                {
                    var campeonato = await _campeonatoRepository.GetById(jogo.CodigoCampeonato);
                    var mandante = await _timeRepository.GetById(jogo.CodigoTime1);
                    var visitante = await _timeRepository.GetById(jogo.CodigoTime2);

                    response.Add(new JogosRodadaDto
                    {
                        Codigo = jogo.Codigo,
                        Time1 = mandante.EscudoUrl,
                        Time2 = visitante.EscudoUrl,
                        DataJogo = jogo.DataDia
                    });
                }

                return response.OrderBy(x => x.DataJogo).ToList();
            }

            return response;
        }
        public async Task<JogoDetail> GetJogoDetails(int id)
        {
            var response = new JogoDetail();

            var jogo = await _jogoRepository.GetById(id);
            var mandante = await _timeRepository.GetById(jogo.CodigoTime1);
            var visitante= await _timeRepository.GetById(jogo.CodigoTime2);

            if ((jogo is not null) && (jogo.CodigoTransmissors is not null))
            {
                var transmissores = new List<Transmissor>();
                if (jogo.CodigoTransmissors.Count > 0)
                {
                    var transmissoresList = jogo.CodigoTransmissors;

                    foreach (var transmissor in transmissoresList)
                    {
                        transmissores.Add(await _transmissorRepository.GetById(transmissor));
                    }
                }

                response = new JogoDetail
                {
                    Codigo = jogo.Codigo,
                    Mandante = mandante.EscudoUrl,
                    Visitante = visitante.EscudoUrl,
                    DataDia = jogo.DataDia,
                    Transmissors = transmissores
                };

                return response;
            }

            return response;
        }

        public async Task<bool> DeleteAnterioresAsync()
        {
            return await _jogoRepository.DeleteAnterioresAsync();
        }
    }
}
