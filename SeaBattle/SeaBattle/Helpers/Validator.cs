using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace SeaBattle.Helpers
{
    /// <summary>
    /// Предоставляет методы для проверки значений на валидность
    /// </summary>
    public static class Validator
    {
        /// <summary>
        /// Проверяет имя пользователя на валидность
        /// </summary>
        /// <param name="username">Имя пользователя, который нужно проверить</param>
        public static bool IsValidUsername(string username)
        {
            if (String.IsNullOrWhiteSpace(username))
                return false;

            return true;
        }

        /// <summary>
        /// Проверяет email на валидность
        /// </summary>
        /// <param name="emailAddress">Email, который нужно проверить</param>
        public static bool IsValidMailAddress(string emailAddress)
        {
            if (String.IsNullOrWhiteSpace(emailAddress))
                return false;

            try
            {
                MailAddress m = new MailAddress(emailAddress);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        /// <summary>
        /// Проверяет пароль на валидность
        /// </summary>
        /// <param name="password">Пароль, который нужно проверить</param>
        public static bool IsValidPassword(string password, int minLength)
        {
            if (String.IsNullOrWhiteSpace(password) || password.Length < minLength)
                return false;

            return true;
        }
    }
}
