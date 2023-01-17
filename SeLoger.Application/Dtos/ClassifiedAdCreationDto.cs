namespace SeLoger.Api.dtos;

public class ClassifiedAdCreationDto
{
    public string Title       { get; set; }
    public string Description { get; set; }
    public double Longitude   { get; set; }
    public double Latitude    { get; set; }
    public int    Type        { get; set; }
}