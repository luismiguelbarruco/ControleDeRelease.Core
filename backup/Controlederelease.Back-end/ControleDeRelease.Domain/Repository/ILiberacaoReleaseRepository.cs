using ControleDeRelease.Domain.Entities;

namespace ControleDeRelease.Domain.Repository
{
    public interface ILiberacaoReleaseRepository
    {
        bool Cadastar(LiberacaoRelease liberacaoRelease);
    }
}
