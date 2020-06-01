using SalesSystem.Areas.Users.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesSystem.Helpers
{
    public interface IConverterHelper
    {
        TUser ToTUserModel(InputModelRegister model);
    }
}
