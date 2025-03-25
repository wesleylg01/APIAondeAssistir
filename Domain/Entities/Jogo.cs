namespace APIAondeAssistir.Domain.Entities
{
    public class Jogo
    {
        public int Codigo { get; set; }
        public int? CodigoTime1 { get; set; }
        public int? CodigoTime2 { get; set; }
        public DateTime? DataDia { get; set; }
        public int? CodigoCampeonato { get; set; }
        public bool isVisible { get; set; }
        public List<int>? CodigoTransmissors { get; set; }
        public string? JogoUrl { get; set; } 
        public int? Rodada { get; set; }
    }
}
