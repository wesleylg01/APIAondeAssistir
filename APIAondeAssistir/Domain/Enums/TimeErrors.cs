namespace APIAondeAssistir.Domain.Enums
{
    public enum TimeErrors
    {
        TimeNaoEncontrado = 0
    }

    public static class TimeErrosExtensions
    {
        public static string GetMessage(this TimeErrors erro)
        {
            return erro switch
            {
                TimeErrors.TimeNaoEncontrado => "O time especificado não foi encontrado.",
                _ => "Erro desconhecido."
            };
        }
    }
}
