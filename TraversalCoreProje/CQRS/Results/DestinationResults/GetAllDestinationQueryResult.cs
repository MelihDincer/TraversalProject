namespace TraversalCoreProje.CQRS.Results.DestinationResults
{
    //Entitymize karşılık gelen property leri tutacak kısım.
    public class GetAllDestinationQueryResult
    {
        public int id { get; set; }
        public string city { get; set; }
        public string daynight { get; set; }
        public double price { get; set; }
        public int capacity { get; set; }
    }
}
