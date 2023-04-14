using Maok.App.Modules.Shared.Enums;
using Maok.App.Modules.Shared.Models;
using Maok.App.Providers;
using Maok.App.Utils.Handlers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace Maok.App.Utils
{
    public class Util
    {
        //public static VersionResponseDto ConfigAmazon { get; set; } = new VersionResponseDto();
        public static bool IsRunAndroid = Device.RuntimePlatform == Device.Android;

        public static T GetResource<T>(string resource)
        {
            if (Application.Current.Resources.TryGetValue(resource, out var obj))
                return (T)obj;
#if DEBUG
            throw new Exception($"Resource: {resource} não encontrado.");
#else
            return default(T);
#endif
        }

        public static string CreateCrytoPassword(string password, Dictionary<string, string[]> keyCodes)
        {
            try
            {
                if (String.IsNullOrEmpty(password) || keyCodes?.Values == null || !keyCodes.Values.Any())
                    return String.Empty;

                var codeKey = String.Empty;
                var keys = password.ToCharArray();
                return keys.Aggregate(codeKey, (current, t) => current + keyCodes[t.ToString()][0]);
            }
            catch (Exception e)
            {
                AsyncErrorHandler.HandleException(e);
                return String.Empty;
            }
        }

        public static string GetSha256(string randomString)
        {
            var crypt = new SHA256Managed();
            var hash = new StringBuilder();
            var crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(randomString));

            foreach (var theByte in crypto)
                hash.Append(theByte.ToString("x2"));
            return hash.ToString();
        }

        public static object GetDefaultValueEnum(Type enumType)
        {
            var attributes = (DefaultValueAttribute[])enumType.GetCustomAttributes(typeof(DefaultValueAttribute), false);
            return attributes.Length > 0 ? attributes[0].Value : Activator.CreateInstance(enumType);
        }

        public static int CalcAge(DateTime birthdate)
        {
            var age = DateTime.Now.Year - birthdate.Year;
            if (birthdate > DateTime.Now.AddYears(-age))
            {
                age--;
            }

            return age;
        }

        public static string Masked(string str, string mask)
        {
            if (string.IsNullOrEmpty(str?.Trim()))
                return "";

            for (var i = 0; i < str.Length; i++)
            {
                if (i < mask.Length)
                {
                    if (mask[i] != '#')
                        str = str.Insert(i, mask[i].ToString());
                }
            }

            if (str.Length > mask.Length)
                return str.Substring(0, mask.Length);

            return str;
        }

        public static string Unmask(string s)
        {
            return s.Replace(".", "").Replace("-", "")
                .Replace("/", "").Replace("(", "")
                .Replace(")", "").Trim(' ').Replace(" ", "");
        }

        public static bool VerifyInvestmentHourOpen(string HourLimitInvestment)
        {
            var investmentHourOpen = 9;

            DateTime timeUtc = DateTime.UtcNow;
            TimeZoneInfo spZone = TimeZoneInfo.FindSystemTimeZoneById("America/Sao_Paulo");
            DateTime spTime = TimeZoneInfo.ConvertTimeFromUtc(timeUtc, spZone);

            if (spTime.Hour >= investmentHourOpen && spTime.Hour < Convert.ToInt32(HourLimitInvestment.Split(':')[0]))
                return true;
            return false;
        }

        public static string VerifyTotalMonthsProduct(DateTime dateCreated)
        {
            int quantityTotalMonths = Math.Abs((DateTime.Now.Month - dateCreated.Month) + 12 * (DateTime.Now.Year - dateCreated.Year));

            if (quantityTotalMonths >= 6 && quantityTotalMonths < 12)
                return "Este fundo possui menos de 12 meses de histórico de rentabilidade";

            return "";
        }

        public static List<Color> RandomColorGenerator(int listSize, ShadeOfColor shade = ShadeOfColor.All, bool colorsInGradient = false)
        {
            var random = new Random(501);

            var colors = new List<Color>();

            int redMin = 0, greenMin = 0, blueMin = 0;
            int redMax = 255, greenMax = 255, blueMax = 255;
            int[] red = new int[listSize];
            int[] blue = new int[listSize];
            int[] green = new int[listSize];

            switch (shade)
            {
                case ShadeOfColor.Red:
                    redMin = 140;
                    blueMax = 170;
                    greenMax = 170;
                    break;

                case ShadeOfColor.Green:
                    greenMin = 140;
                    blueMax = 170;
                    redMax = 170;
                    break;

                case ShadeOfColor.Blue:
                    blueMin = 140;
                    redMax = 170;
                    greenMax = 170;
                    break;

                default:
                    break;
            }

            for (int i = 0; i < listSize; i++)
            {
                red[i] = random.Next(redMin, redMax);
                green[i] = random.Next(greenMin, greenMax);
                blue[i] = random.Next(blueMin, blueMax);
            }

            if (colorsInGradient)
            {
                red = red.OrderBy(x => x).ToArray();
                green = green.OrderBy(x => x).ToArray();
                blue = blue.OrderBy(x => x).ToArray();
            }

            for (int i = 0; i < listSize; i++)
            {
                colors.Add(Color.FromRgb(random.Next(redMin, redMax), random.Next(greenMin, greenMax), random.Next(blueMin, blueMax)));
            }

            return colors;
        }

        public static bool IsDebugOrRelease()
        {
#if DEBUG
            return true;
#else
                return false;
#endif
        }

        public static bool ValidatePassword(string password)
        {
            string patternPassword = @"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{8,16}$";
            if (!string.IsNullOrEmpty(password))
            {
                if (!Regex.IsMatch(password, patternPassword))
                    return false;

            }
            return true;
        }

        public static List<ComboBoxModel> GetGenders()
        {
            var genders = new List<ComboBoxModel>();
            genders.Add(new ComboBoxModel() { Id = 1, Value = "Masculino" });
            genders.Add(new ComboBoxModel() { Id = 2, Value = "Feminino" });

            return genders;
        }

        public static HttpClient GetClient()
        {
            var type = AuthProvider.Instance.Token.Type;
            var token = AuthProvider.Instance.Token.Token;

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://maok-api.herokuapp.com");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(type, token);

            return client;
        }
    }
}