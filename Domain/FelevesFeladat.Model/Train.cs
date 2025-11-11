namespace FelevesFeladat.Model;

public class Train
{
    public string Name { get; set; }
    public int Id { get; set; }
    public List<int> PassengerIds { get; set; }
    
    public double TotalDelay { get; set; }


    }