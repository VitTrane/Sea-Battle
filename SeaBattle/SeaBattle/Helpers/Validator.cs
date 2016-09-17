using SeaBattle.ConfigSections;
using System;
using System.Configuration;
using System.Net.Mail;

namespace SeaBattle.Helpers
{
    /// <summary>
    /// Предоставляет методы для проверки значений на валидность
    /// </summary>
    public static class Validator
    {
        public static readonly int MIN_LENGTH_USERNAME = 1;
        public static readonly int MAX_LENGTH_USERNAME = 16;
        public static readonly int MIN_LENGTH_PASSWORD = 6;
        public static readonly int MAX_LENGTH_PASSWORD = 16;        

        static Validator()
        {
            var config = (ConfigurationManager.GetSection("settingsValidData") as SettingsValidDataConfigSections);
            if (config != null)
            {
                MIN_LENGTH_USERNAME = config.MinLengthUsername;
                MAX_LENGTH_USERNAME = config.MaxLengthUsername;
                MIN_LENGTH_PASSWORD = config.MinLengthPassword;
                MAX_LENGTH_PASSWORD = config.MaxLengthPassword;
            }
        }

        /// <summary>
        /// Проверяет имя пользователя на валидность
        /// </summary>
        /// <param name="username">Имя пользователя, который нужно проверить</param>
        public static bool IsValidUsername(string username)
        {
            if (String.IsNullOrWhiteSpace(username) && username.Length > MIN_LENGTH_USERNAME)
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
        public static bool IsValidPassword(string password)
        {
            if (String.IsNullOrWhiteSpace(password) || password.Length < MIN_LENGTH_PASSWORD)
                return false;

            return true;
        }
    }
}
