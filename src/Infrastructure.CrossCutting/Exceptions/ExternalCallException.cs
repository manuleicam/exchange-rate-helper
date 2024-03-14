namespace Infrastructure.CrossCutting.Exceptions
{
    public class ExternalCallException(string msg) : Exception(msg);
}