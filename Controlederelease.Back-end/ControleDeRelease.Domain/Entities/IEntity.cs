namespace ControleDeRelease.Domain.Entities
{
    public interface IEntity
    {
        int Id { get; set; }
        string Nome { get; set; }
    }
}
