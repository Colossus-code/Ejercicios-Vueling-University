namespace Crosscuting
{
    public class DateValidator
    {
        public static DateTime? ValidateDataFotmat(string dateAsString)
        {
            if (dateAsString.Length == 8 && dateAsString.All(c => Char.IsDigit(c)))
            {
                DateTime dateTime;

                if (DateTime.TryParseExact(dateAsString, "yyyyMMdd", null, System.Globalization.DateTimeStyles.None, out dateTime))
                {
                    return dateTime;
                }

            }
            return null;
        }

        private bool ValidateDay(int day)
        {
            if (day >= 1 && day <= 31) return true;

            return false;
        }

        private bool ValidateMonth(int month)
        {
            if (month >= 1 && month <= 12) return true;

            return false;
        }
    }
}