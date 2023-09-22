namespace api.organisations.domain.Model;

public class Entity<T>
{
    public T Identity { get; protected set; }

    protected Entity(T identity = default)
    {
        Identity = identity;
    }
}