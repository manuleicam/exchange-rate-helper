namespace Infrastructure.CrossCutting.Exceptions
{

    public class ResourceNotFound(string msg) : Exception(msg);
}