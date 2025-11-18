namespace Domain.Shared.Entities
{
    public class Entity
    {
        public Entity()
        {
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
            IsActive = true;
            IsDeleted = false;
        }

        public Guid Id { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }
        public DateTime? DeletedAt { get; private set; }
        public bool IsActive { get; private set; }
        public bool IsDeleted { get; private set; }

        public void SetActive(bool isActive)
        {
            IsActive = isActive;
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetDeleted()
        {
            IsDeleted = true;
            UpdatedAt = DateTime.UtcNow;
        }

        public void InitializeAdd()
        {
            IsActive = true;
            IsDeleted = false;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

        public void InitializeAdd(Guid id)
        {
            Id = id;
            IsActive = true;
            IsDeleted = false;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
