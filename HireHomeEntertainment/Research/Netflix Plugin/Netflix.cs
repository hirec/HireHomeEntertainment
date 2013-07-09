namespace NetflixAlphaPlugin.Config
{
    using System;

    public static class Netflix
    {
        private static string consumerKey = "5rydjvyd2ygzsdjuqv6d4tpq";
        private static string consumerSecret = "EewyCMRzyX";
        public static int defaultItemsCount = 30;
        public static string timeFormatStr = "{0} min";

        public static string ConsumerKey
        {
            get
            {
                return consumerKey;
            }
        }

        public static string ConsumerSecret
        {
            get
            {
                return consumerSecret;
            }
        }
    }
}

