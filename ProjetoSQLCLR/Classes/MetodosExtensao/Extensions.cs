using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;

namespace ProjetoSQLCLR
{
    public static class Extensions
    {
        public static string Name<T>(this T instanciaEnum) where T : Enum
        {
            return Enum.GetName(typeof(T), instanciaEnum);
        }

        public static T With<T>(this T item, Action<T> action)
        {
            action(item);
            return item;
        }

        public static DateTime TryParseDate(this string textDate, string format = "dd/MM/yyyy")
        {
            DateTime.TryParseExact(textDate, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date);
            return date;
        }

        public static void OpenIfClosed(this SqlConnection connection)
        {
            if (connection.State == ConnectionState.Closed)
                connection.Open();
        }
    }
}
