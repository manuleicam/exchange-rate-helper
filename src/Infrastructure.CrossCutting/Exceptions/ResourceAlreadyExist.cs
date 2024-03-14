namespace Infrastructure.CrossCutting.Exceptions
{
    public class ResourceAlreadyExist(string msg) : Exception(msg);
}