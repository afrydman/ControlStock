using System;

namespace DTO
{
    public static class HelperDTO
    {

        
        public static string BEGINNING_OF_TIME
        {
            get
            {
                return "1/1/1753";
            }
        }


        public static DateTime BEGINNING_OF_TIME_DATE
        {
            get
            {
                return DateTime.Parse("01/01/1800");
            }
        }

        public static DateTime END_OF_TIME
        {
            get
            {
                return DateTime.Parse("01/01/2300");
            }
        }

        public static decimal STOCK_MINIMO_INVALIDO
        {
            get { return -9999; }
        }
        

    }
}
