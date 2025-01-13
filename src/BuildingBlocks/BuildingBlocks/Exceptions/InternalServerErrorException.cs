namespace BuildingBlocks.Exceptions;

public class InternalServerErrorException  :Exception
{
    public string Details { get; set; }
    public InternalServerErrorException(string message, string details):base(message)
    {
        Details = details;
    }
}
