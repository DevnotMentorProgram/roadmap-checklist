using RoadmapChecklist.Api.Models.Request.User;
using RoadmapChecklist.Entity.User;
using System.Security.Cryptography;
using System.Text;

namespace RoadmapChecklist.Api.Models.Mappings
{
    public class UserMapper
    {
        public static User MapUserRegisterModel(Register userRegisterModel)
        {
            return new User
            {
                Name = userRegisterModel.UserName,
                Email = userRegisterModel.Email,
                Password = MD5Hash(userRegisterModel.Password)
            };
        }

        public static string MD5Hash(string input)
        {
            using (var md5 = MD5.Create())
            {
                var result = md5.ComputeHash(Encoding.ASCII.GetBytes(input));
                return Encoding.ASCII.GetString(result);
            }
        }
    }
}