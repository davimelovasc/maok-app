using Maok.App.Modules.Shared.Navigation;
using Maok.App.Modules.Shared.Pages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Maok.App.Utils
{
    public static class Extensions
    {
        public static object GetDefaultValueEnum(this Type enumType)
        {
            var attributes =
                (DefaultValueAttribute[])enumType.GetCustomAttributes(typeof(DefaultValueAttribute), false);
            return attributes.Length > 0 ? attributes[0].Value : Activator.CreateInstance(enumType);
        }

        public static string Value(this Enum @enum)
        {
            var attr =
                @enum.GetType().GetMember(@enum.ToString()).FirstOrDefault()?.
                    GetCustomAttributes(false).OfType<EnumMemberAttribute>().
                    FirstOrDefault();
            return attr == null ? @enum.ToString() : attr.Value;
        }

        public static string Description(this Enum @enum)
        {
            var attr =
             @enum.GetType().GetMember(@enum.ToString()).FirstOrDefault()?.
                 GetCustomAttributes(false).OfType<DescriptionAttribute>().
                 FirstOrDefault();
            return attr == null ? @enum.ToString() : attr.Description;
        }

        public static T GetValueFromValueMember<T>(string valueMember)
        {
            var type = typeof(T);
            if (!type.IsEnum) throw new InvalidOperationException();
            if (string.IsNullOrEmpty(valueMember)) return default;
            foreach (var field in type.GetFields())
            {
                var attribute = Attribute.GetCustomAttribute(field,
                    typeof(EnumMemberAttribute)) as EnumMemberAttribute;
                if (attribute != null)
                {
                    if (attribute.Value == valueMember)
                        return (T)field.GetValue(null);
                }
                else
                {
                    if (field.Name == valueMember)
                        return (T)field.GetValue(null);
                }
            }
            throw new ArgumentException("Not found.", nameof(valueMember));
        }

        public static T TryGetValueFromValueMember<T>(string valueMember)
        {
            try
            {
                return GetValueFromValueMember<T>(valueMember);
            }
            catch
            {
                return default;
            }
        }

        public static string GetValue(this IDictionary<string, object> obj, string key, string defaultValue = "")
        {
            if (obj.ContainsKey(key))
                return obj[key]?.ToString() ?? defaultValue;

            return defaultValue;
        }

        public static string ZipcodeUnformatted(this string zipcode)
        {
            if (string.IsNullOrEmpty(zipcode))
                return string.Empty;

            return zipcode.Trim().Replace("-", "");
        }

        public static string ZipcodeFormatted(this string zipcode)
        {
            if (string.IsNullOrEmpty(zipcode))
                return string.Empty;

            if (zipcode.Length == 9 && zipcode.IndexOf('-') == 5)
                return zipcode;

            if (zipcode.Length == 8)
                zipcode = zipcode.Insert(5, "-");

            return zipcode;
        }

        public static string CpfUnformatted(this string cpf)
        {
            return string.IsNullOrEmpty(cpf)
                ? string.Empty
                : cpf.Trim().Replace(".", "").Replace("-", "");
        }

        public static string CpfFormatted(this string cpf)
        {
            if (string.IsNullOrEmpty(cpf))
                return string.Empty;

            if (cpf.Length == 13)
            {
                var unmasked = CpfUnformatted(cpf);
                if (IsCpf(unmasked))
                    return cpf;
            }

            var text = Regex.Replace(cpf, @"[^0-9]", "");
            text = text.PadRight(11, '0');
            text = text.Insert(3, ".").Insert(7, ".").Insert(11, "-").TrimEnd(new char[] { ' ', '.', '-' });

            return text;
        }

        public static string CpfOvershadow(this string cpf)
        {
            if (!string.IsNullOrEmpty(cpf))
            {
                cpf = cpf.Remove(4, 1).Insert(4, "*")
                        .Remove(5, 1).Insert(5, "*")
                        .Remove(6, 1).Insert(6, "*")
                        .Remove(8, 1).Insert(8, "*")
                        .Remove(9, 1).Insert(9, "*")
                        .Remove(10, 1).Insert(10, "*");
            }

            return cpf;
        }

        public static bool IsCpf(this string cpf)
        {
            if (string.IsNullOrEmpty(cpf?.Trim()))
                return false;

            switch (cpf)
            {
                case "00000000000": return false;
                case "11111111111": return false;
                case "22222222222": return false;
                case "33333333333": return false;
                case "44444444444": return false;
                case "55555555555": return false;
                case "66666666666": return false;
                case "77777777777": return false;
                case "88888888888": return false;
                case "99999999999": return false;
                default:
                    var multiplicador1 = new[] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
                    var multiplicador2 = new[] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
                    string tempCpf;
                    string digito;
                    int soma;
                    int resto;
                    cpf = cpf.Trim();
                    cpf = cpf.Replace(".", "").Replace("-", "");
                    if (cpf.Length != 11)
                        return false;
                    tempCpf = cpf.Substring(0, 9);
                    soma = 0;

                    for (var i = 0; i < 9; i++)
                        soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
                    resto = soma % 11;
                    if (resto < 2)
                        resto = 0;
                    else
                        resto = 11 - resto;
                    digito = resto.ToString();
                    tempCpf += digito;
                    soma = 0;
                    for (var i = 0; i < 10; i++)
                        soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
                    resto = soma % 11;
                    if (resto < 2)
                        resto = 0;
                    else
                        resto = 11 - resto;

                    digito += resto.ToString();

                    return cpf.EndsWith(digito, StringComparison.InvariantCulture);
            }
        }

        public static bool IsEmail(this string email) => !string.IsNullOrEmpty(email?.Trim()) && email.Contains("@") && email.Contains(".");

        public static bool IsCnpj(this string cnpj)
        {
            var multiplicador1 = new[] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            var multiplicador2 = new[] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            cnpj = cnpj.Trim();
            cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");
            if (cnpj.Length != 14)
                return false;
            var tempCnpj = cnpj.Substring(0, 12);
            var soma = 0;
            for (var i = 0; i < 12; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];
            var resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            var digito = resto.ToString();
            tempCnpj += digito;
            soma = 0;
            for (var i = 0; i < 13; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];
            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito += resto.ToString();
            return cnpj.EndsWith(digito, StringComparison.InvariantCulture);
        }

        public static string CnpjUnformatted(this string cnpj)
        {
            if (string.IsNullOrEmpty(cnpj))
            {
                return string.Empty;
            }

            return (cnpj + "").Trim().Replace(".", "").Replace("/", "").Replace("-", "");
        }

        public static string CnpjFormatted(this string cnpj)
        {
            if (string.IsNullOrEmpty(cnpj))
                return string.Empty;

            var text = Regex.Replace(cnpj, @"[^0-9]", "");

            text = text.PadRight(14);
            if (text.Length > 14)
                text = text.Remove(14);

            text = text.Insert(2, ".").Insert(6, ".").Insert(10, "/").Insert(15, "-").TrimEnd(' ', '.', '/', '-');

            return text;
        }

        public static string PhoneDddUnformatted(this string phone)
        {
            if (string.IsNullOrEmpty(phone))
                return string.Empty;

            phone = Regex.Replace(phone, @"\s", "");

            return phone.Replace("(", "").Replace(")", "").Replace("-", "").Substring(0, 2);
        }

        public static string PhoneNumberUnformatted(this string phone)
        {
            if (string.IsNullOrEmpty(phone))
                return string.Empty;

            phone = Regex.Replace(phone, @"\s", "");

            return phone.Replace("(", "").Replace(")", "").Replace("-", "").Remove(0, 2);
        }

        public static string DateFormatted(this DateTime? datetime)
        {
            return datetime?.ToString("dd/MM/yyyy") ?? "";
        }

        public static DateTime? ToDate(this string dateUnFormatted)
        {
            DateTime date;

            var success = DateTime.TryParseExact(dateUnFormatted, "dd/MM/yyyy", CultureInfo.CurrentCulture, DateTimeStyles.AssumeLocal, out date);

            if (!success)
                success = DateTime.TryParseExact(dateUnFormatted, "ddMMyyyy", CultureInfo.CurrentCulture, DateTimeStyles.AssumeLocal, out date);

            if (success)
                return date;

            return null;
        }

        public static DateTime DateApiToDate(this string dateFormatted)
        {
            DateTime.TryParse(dateFormatted, out var date);
            return date;
        }

        public static bool IsDateValid(this string date)
        {
            DateTime.TryParse(date, out var myDate);
            return myDate.Year > 1900;
        }

        public static bool IsDateValid(this DateTime date) => date.Year > 1900;

        public static bool IsNumeric(this string value)
        {
            if (string.IsNullOrEmpty(value))
                return false;

            var datachars = value.ToCharArray();
            return datachars.All(char.IsNumber);
        }

        public static string CurrencyValue(this object value, string format = "N2")
        {
            var valueChecked = value?.ToString()?.Replace(".00", "")?.Replace(".0", "") ?? "0";
            double.TryParse(valueChecked, NumberStyles.Currency, CultureInfo.GetCultureInfo("pt-BR"),
                out var valueParsed);
            return valueParsed.ToString(format);
        }

        public static bool IsNumbersEquals(this object obj) =>
            RegexCheck(@"[^0-9]|000|111|222|333|444|555|666|777|888|999|^(.{0,5}|.{9,})$", obj);

        public static bool IsSequence(this object obj) => RegexCheck(
            @"[^0-9]|012|123|234|345|456|567|678|789|890|901|987|876|765|654|543|432|321|210|109|098|^(.{0,5}|.{9,})$",
            obj);

        public static bool IsOnlyNumbers(this object obj) => RegexCheck(@"(?<=\s|^)\d+(?=\s|$)", obj);

        private static bool RegexCheck(string pattnern, object obj)
        {
            try
            {
                var txt = obj?.ToString();
                if (string.IsNullOrEmpty(txt))
                    return false;
                var rx = new Regex(pattnern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
                var matches = rx.Matches(txt);
                return matches.Count > 0;
            }
            catch
            {
                return false;
            }
        }

        public static string HtmlFormatted(this string html) => new StringBuilder()
            .AppendLine("<!DOCTYPE html>")
            .AppendLine("<html><head>")
            .AppendLine("<style> ")
            .AppendLine(" body { ")
            .AppendLine(" color: black; ")
            .AppendLine(" font-size:14px; ")
            .AppendLine(" } ")
            .AppendLine("</style>")
            .AppendLine("<meta charset=\"UTF-8\">")
            .AppendLine("</head><body>")
            .AppendLine(html)
            .AppendLine("</body></html>")
            .ToString();

        //public static T Map<T>(this object source) where T : class, new()
        //{
        //    try
        //    {
        //        return source == null ? new T() : FreshMvvm.FreshIOC.Container.Resolve<IMapper>().Map<T>(source);
        //    }
        //    catch (Exception e)
        //    {
        //        var msg = $"Houve um problema ao fazer o mapeamento de : {nameof(source)} para {nameof(T)}";
        //        AsyncErrorHandler.HandleException(e);
        //        return new T();
        //    }
        //}

        public static BasePage GetBasePage(this VisualElement element)
        {
            if (element == null) return default;

            var parent = element.Parent;
            while (parent != null)
            {
                if (parent is BasePage page)
                    return page;

                parent = parent.Parent;
            }

            return default;
        }

        public static ScrollView GetScroll(this VisualElement element)
        {
            var parent = element.Parent;
            while (parent != null)
            {
                switch (parent)
                {
                    case NavigationController _:
                        return default;

                    case ScrollView scrolled:
                        return scrolled;
                }

                if (parent.FindByName("Scroll") is ScrollView scroll)
                    return scroll;

                parent = parent.Parent;
            }

            return default;
        }

        public static Task<bool> ColorTo(this VisualElement self, string name, Color fromColor, Color toColor,
            Action<Color> callback, uint length = 250, Easing easing = null)
        {
            Color Transform(double t) =>
                Color.FromRgba(fromColor.R + t * (toColor.R - fromColor.R),
                    fromColor.G + t * (toColor.G - fromColor.G),
                    fromColor.B + t * (toColor.B - fromColor.B),
                    fromColor.A + t * (toColor.A - fromColor.A));

            easing = Easing.Linear;
            var taskCompletionSource = new TaskCompletionSource<bool>();

            self.Animate(name, Transform, callback, 16, length, easing, (v, c) => taskCompletionSource.SetResult(c));

            return taskCompletionSource.Task;
        }

        public static void CancelAnimation(this VisualElement self, string name)
        {
            self.AbortAnimation(name);
        }

        public static FormattedString AddSpans(this FormattedString item, List<Span> spans)
        {
            spans.ForEach(x => item.Spans.Add(x));
            return item;
        }

        //public static ObservableCollection<ComboBoxComponentModel> ToComboBox(this List<DomainResponseDto> items, Func<DomainResponseDto, bool> selected = null)
        //{
        //    if (items == null)
        //        return new ObservableCollection<ComboBoxComponentModel>();

        //    var itemsBoxed = items.Select(item => new ComboBoxComponentModel
        //    {
        //        Id = MapperProvider.Instance.Map<DomainModel>(item),
        //        Text = item.Name ?? item.Text ?? item.Description ?? item.Nome ?? item.Descricao,
        //        IsSelected = selected?.Invoke(item) ?? false
        //    }).ToList();

        //    return new ObservableCollection<ComboBoxComponentModel>(itemsBoxed);
        //}

        //public static ComboBoxComponentModel GetComboBoxSelected(this ObservableCollection<ComboBoxComponentModel> items)
        //{
        //    return items?.FirstOrDefault(x => x.IsSelected);
        //}

        //public static void CleanComboBoxSelected(this ObservableCollection<ComboBoxComponentModel> items)
        //{
        //    foreach (var item in items)
        //    {
        //        item.IsSelected = false;
        //    }
        //}

        //public static ObservableCollection<ComboBoxComponentModel> ToComboBox(this List<string> items)
        //{

        //    if (items == null)
        //        return new ObservableCollection<ComboBoxComponentModel>();

        //    var itemsBoxed = new ObservableCollection<ComboBoxComponentModel>();
        //    itemsBoxed.Add(new ComboBoxComponentModel() { Text = "Todos", Id = string.Empty, IsSelected = true });

        //    foreach (var item in items)
        //    {
        //        itemsBoxed.Add(new ComboBoxComponentModel() { Text = item, Id = item });
        //    }

        //    return itemsBoxed;
        //}

        public static bool IsMinor(this DateTime birthdate) => Util.CalcAge(birthdate) < 18;

        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> items) => new ObservableCollection<T>(items ?? new List<T>());

        public static string HtmlStrip(this string input)
        {
            if (input == null)
                return "";

            input = Regex.Replace(input, "<style>(.|\n)*?</style>", string.Empty);
            input = Regex.Replace(input, @"<xml>(.|\n)*?</xml>", string.Empty);
            return Regex.Replace(input, @"<(.|\n)*?>", string.Empty);
        }

        public static IEnumerable<TSource> Distinct<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            HashSet<TKey> knownKeys = new HashSet<TKey>();
            foreach (TSource element in source)
            {
                if (knownKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }

        public static string RemoveDiacritics(this string input)
        {
            var stFormD = input.Normalize(NormalizationForm.FormD);
            var len = stFormD.Length;
            var sb = new StringBuilder();
            for (var i = 0; i < len; i++)
            {
                var uc = CharUnicodeInfo.GetUnicodeCategory(stFormD[i]);
                if (uc != UnicodeCategory.NonSpacingMark)
                    sb.Append(stFormD[i]);
            }

            return (sb.ToString().Normalize(NormalizationForm.FormC));
        }

        public static string NameFormatted(this string name)
        {
            if (String.IsNullOrEmpty(name))
                return "";

            name = Regex.Replace(name, @"\s{2,}", string.Empty);
            return name;
        }

        public static bool ContainsIgnoringCaseAndDiacritics(this string source, string search)
        {
            return CultureInfo.CurrentCulture.CompareInfo.IndexOf(source, search, CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace) >= 0;
        }

        public static int ConvertToCheckVersion(string version)
        {
            string trimVersion = version.Replace(".", "");
            int converted = 0;
            Int32.TryParse(trimVersion, out converted);
            return converted;
        }
    }
}