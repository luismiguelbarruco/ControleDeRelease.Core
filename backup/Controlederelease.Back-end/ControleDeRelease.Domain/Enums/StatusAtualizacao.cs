using System.ComponentModel;

namespace ControleDeRelease.Domain.Enums
{
    public enum StatusRelease
    {
        [Description("Atualizado")]
        Atualizado = 1,

        [Description("Mantido")]
        Mantido,

        [Description("Pendente Atualizar")]
        PendenteAtualizar,

        [Description("Não Verificado")]
        NaoVerificado
    }
}
