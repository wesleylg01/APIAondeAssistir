using APIAondeAssistir.Domain.Entities;
using System.Security.Principal;

namespace APIAondeAssistir.DTOs
{
    public class JogoDetail
    {
        public int Codigo { get; set; }
        public string Mandante { get; set; }
        public string Visitante { get; set; }
        public DateTime DataDia { get; set; }
        public List<Transmissor>? Transmissors { get; set; }
    }
}
