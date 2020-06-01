using SalesSystem.Areas.Users.Models;

namespace SalesSystem.Helpers
{
    public class ConverterHelper : IConverterHelper
    {
        public TUser ToTUserModel(InputModelRegister model)
        {
            return new TUser
            {
                DNI = model.DNI,
                Email = model.Email,
                LastName = model.LastName,
                Name = model.Name
            };
        }
    }
}
