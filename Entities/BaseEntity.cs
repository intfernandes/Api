
namespace Api.Entities
{
    public abstract class BaseEntity
    {
        public  Guid Id {get; set;} = new Guid();
        public  virtual DateTime CreatedAt { get; set; } = DateTime.UtcNow; 
        public virtual DateTime UpdatedAt { get; set; }
        public virtual bool IsDeleted { get; set; } = false; 
        public virtual void OnCreating() {  }
        public virtual void OnUpdating() { }
        public override string ToString()
        {
            return $"{GetType().Name} - CreatedAt: {CreatedAt}, UpdatedAt: {UpdatedAt}";
        }
    }
}