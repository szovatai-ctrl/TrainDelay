namespace FelevesFeladat.Model
{
    public class Journey
    {
        public int Id { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public int TrainId { get; set; }
        public List<int> PassengerIds { get; set; }
        public int Delay { get; set; }


    
    }
}