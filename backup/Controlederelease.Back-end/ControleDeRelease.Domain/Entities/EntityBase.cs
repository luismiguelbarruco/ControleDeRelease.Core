namespace ControleDeRelease.Domain.Entities
{
    public abstract class EntityBase : IEntity
    {
        public int Id { get; set; } = 0;
        public string Nome { get; set; } = string.Empty;
    }
}
