using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialNetwork.DAL.Interfaces;
using SocialNetwork.BLL.DataTransferObjects;
using SocialNetwork.BLL.Infrastructure;

namespace SocialNetwork.BLL.Interfaces
{
    public interface IAccountService
    {
        ServiceResult<RegistrationDTO> RegisterUser(RegistrationDTO regDTO);
        ServiceResult<LoginDTO> LoginUser(LoginDTO logDTO);
    }
}
