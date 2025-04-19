namespace APIAondeAssistir.Domain.Enums
{
    public enum TransmissorErrors
    {
        TransmissorNaoEncontrado = 0
    }
    public static class TransmissorErrorsExtensions
    {
        public static string GetMessage(this TransmissorErrors erro)
        {
            return erro switch
            {
                TransmissorErrors.TransmissorNaoEncontrado => "O transmissor especificado não foi encontrado.",
                _ => "Erro desconhecido."
            };
        }
    }
}
