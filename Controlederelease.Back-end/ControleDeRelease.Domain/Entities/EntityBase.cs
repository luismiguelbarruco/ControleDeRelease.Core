namespace ControleDeRelease.Domain.Entities
{
    public abstract class EntityBase : IEntity
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
    }
}
