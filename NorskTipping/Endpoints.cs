namespace NorskTipping
{
    public class Endpoints
    {
        // https://www.norsk-tipping.no/api-lotto/getResultInfo.json?drawID=
        private const string HOST = "https://www.norsk-tipping.no/";
        private const string PAGE = "getResultInfo.json?drawID=";
        
        public static string Lotto => GetEndpointAddress("api-lotto");
        public static string Extra => GetEndpointAddress("api-extra");
        public static string VikingLotto => GetEndpointAddress("rest-vikinglotto");
        public static string Joker => GetEndpointAddress("api-joker");
        public static string EuroJackpot => GetEndpointAddress("api-eurojackpot");

        private static string GetEndpointAddress(string type)
        {
            return $"{HOST}{type}/{PAGE}";
        }
    }
}
