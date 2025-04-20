namespace APIAondeAssistir.Domain.Enums
{
    public enum CampeonatoErrors
    {
        CampeonatoNaoEncontrado = 1
    }
    public static class CampeonatoErrorsExtensions
    {
        public static string GetMessage(this CampeonatoErrors erro)
        {
            return erro switch
            {
                CampeonatoErrors.CampeonatoNaoEncontrado => "O Campeonato especificado não foi encontrado.",
                _ => "Erro desconhecido."
            };
        }
    }
}
