namespace MovieHunter.Importer.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public static class DateTimeExtensions
    {
        public static DateTime ToDateTime(this string idioticString)
        {
            if(idioticString.Length == 8)
            {
                var year = idioticString.Substring(0, 4);
                var month = idioticString.Substring(4, 2);
                var day = idioticString.Substring(6, 2);

                return new DateTime(int.Parse(year), int.Parse(month), int.Parse(day));
            }

            if(idioticString.Length == 10)
            {
                return DateTime.Parse(idioticString);
            }

            return DateTime.Now;
        }

<<<<<<< HEAD
        private static readonly DateTime SqlDateTimeMinValue = DateTime.Parse("1753/1/1");
=======
        private static readonly DateTime SqlDateTimeMinValue = DateTime.Parse("1755/1/1");
>>>>>>> f1b9c27af46085ffd280051bdb7eb342d1599317

        public static DateTime Sqlize(this DateTime datetime)
        {
            return datetime < SqlDateTimeMinValue ? SqlDateTimeMinValue : datetime;
        }
    }
}
