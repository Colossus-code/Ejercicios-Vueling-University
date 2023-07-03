namespace DomainEntities
{
    public class Country
    {
        public string ShortName { get; set; }

        public string ShortCoinName { get; set; }

        public int DeliverTaxes { get; set; }

        public void SetCoinAndTaxes()
        {

            switch (ShortName)
            {

                case "EUR":

                    ShortCoinName = "EUR";
                    DeliverTaxes = 6;
                    break;

                case "CHN":

                    ShortCoinName = "CNY";
                    DeliverTaxes = 9;
                    break;

                case "AMR":

                    ShortCoinName = "USD";
                    DeliverTaxes = 2;
                    break;

                case "CAN":

                    ShortCoinName = "CAD";
                    DeliverTaxes = 5;
                    break;


            }
        }

    }
}