
namespace api.organisations.domain.Model.Reference;

public record UserReference(UserIdentity UserIdentity, string Name)
{
    internal static UserReference From(string userIdentity, string userName)
    {
        return new UserReference(UserIdentity.From(userIdentity), userName);
    }

    internal static UserReference Empty => new UserReference(UserIdentity.From("Not Set"), "Not Set");
}
