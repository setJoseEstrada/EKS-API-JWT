using APIEKS.Models.Request;
using APIEKS.Models.Response;

namespace APIEKS.Service
{
    public interface IUserService
    {
        UserResponse Auth(AuthRequest model);
    }
}
