

namespace Yurtap.Core.Auth
{
    public interface IAuthendication<EntityType>
    {
        EntityType Login(string userName, string password);
    }
}
