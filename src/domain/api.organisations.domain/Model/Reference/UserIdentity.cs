
using System.ComponentModel;

namespace api.organisations.domain.Model.Reference;

public record UserIdentity(string Identity)
{
    internal static UserIdentity From(string userIdentity)
    {
        return new UserIdentity(userIdentity);
    }
}
