using System;
using Dapper;

namespace Repository
{
    public static class Ext
    {
        /// <summary>
        /// Recorta un string indicando la máxima Cantidad posible de caracteres que puede tener.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="maxLength"></param>
        /// <returns></returns>
        public static string GetJust(this string s, int maxLength)
        {
            if (String.IsNullOrEmpty(s)) return String.Empty;

            if (s.Length > maxLength)
                return s.Substring(0, maxLength);
            return s;
        }

        /// <summary>
        /// Devuelve un DbString para pasar a la query.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="characters"></param>
        /// <returns></returns>
        public static DbString ToVarcharOf(this string text, int characters)
        {
            return new DbString() { Value = text, Length = characters };
        }
    }

    public static class Messages
    {
        public static string MethodNotImplemented { get { return "Method not supported in this object"; } }


    }
}
