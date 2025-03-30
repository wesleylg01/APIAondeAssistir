namespace APIAondeAssistir.Domain.Enums
{
    public enum TimeErros
    {
        NenhumTimeEncontrado = 0,
        TimeNaoEncontrado = 1,
        ErroAoCriarTime = 2,
        ErroAoAtualizarTime = 3,
        ErroAoDeletarTime = 4,
        TimeExistente = 5
    }

    public static class TimeErrosExtensions
    {
        public static string GetMessage(this TimeErros erro)
        {
            return erro switch
            {
                TimeErros.NenhumTimeEncontrado => "Nenhum time foi encontrado.",
                TimeErros.TimeNaoEncontrado => "O time especificado não foi encontrado.",
                TimeErros.ErroAoCriarTime => "Ocorreu um erro ao tentar criar o time.",
                TimeErros.ErroAoAtualizarTime => "Ocorreu um erro ao tentar atualizar o time.",
                TimeErros.ErroAoDeletarTime => "Ocorreu um erro ao tentar deletar o time.",
                TimeErros.TimeExistente => "O time informado já existe",
                _ => "Erro desconhecido."
            };
        }
    }
}
