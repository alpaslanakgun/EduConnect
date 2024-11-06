using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduConnect.Services.Common
{
    public class UserAuthenticationMessageConstant
    {
        public const string EmailOrPasswordWrong = "Email veya şifre yanlış.";
        public const string RefreshTokenNotFound = "Refresh token bulunamadı.";
        public const string UserIdNotFound = "Kullanıcı ID bulunamadı.";
        public const string UserNotFound = "Kullanıcı Bulunamadı ";
        public const string UserSuccessful = "Kullanıcı Başarıyla Oluşturuldu";
        public const string TokenCreationSuccessful = "Token başarıyla oluşturuldu.";
        public const string TokenUpdateSuccessful = "Token başarıyla güncellendi.";
        public const string ClientIdOrSecretNotFound = "ClientId veya Client Secret bulunamadı.";
        public const string RoleAdded = "Rol Başarıyla Eklendi";

    }

}
