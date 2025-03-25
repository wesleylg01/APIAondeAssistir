namespace APIAondeAssistir.Domain.Entities
{
    public class Transmissor
    {
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public string ImageUrl { get; set; }
        public string Url { get; set; }
        public bool Gratis {  get; set; }
    }
}
