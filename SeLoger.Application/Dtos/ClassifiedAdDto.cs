namespace SeLoger.Application.Dtos;

public class ClassifiedAdDto
{
    public string Id          { get; set; }
    public string Title       { get; set; }
    public string Description { get; set; }
    public double Latitude    { get; set; }
    public double Longitude   { get; set; }
    public int    Type        { get; set; }

    public int Status { get; set; }

    public double? CelsiusTemperature { get; set; }
}