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
    }
}
