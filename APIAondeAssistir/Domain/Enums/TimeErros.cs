namespace APIAondeAssistir.Domain.Enums
{
    public enum TimeErros
    {
        TimeNaoEncontrado = 0
    }

    public static class TimeErrosExtensions
    {
        public static string GetMessage(this TimeErros erro)
        {
            return erro switch
            {
                TimeErros.TimeNaoEncontrado => "O time especificado não foi encontrado.",
                _ => "Erro desconhecido."
            };
        }
    }
}
