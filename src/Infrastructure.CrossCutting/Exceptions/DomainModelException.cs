namespace Infrastructure.CrossCutting.Exceptions
{
    public class DomainModelException(string msg) : Exception(msg);
}