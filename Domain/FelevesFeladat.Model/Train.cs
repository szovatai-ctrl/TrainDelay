namespace FelevesFeladat.Model;

public class Train
{
    public string Name { get; set; }
    public int Id { get; set; }
    public List<int> PassengerIds { get; set; } = new List<int>();

    public double TotalDelay { get; set; }


    }