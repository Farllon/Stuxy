namespace Stuxy.Bus.Messaging
{
    public abstract class Query<TResponse> : Message, IQuery<TResponse>
    {
    }
}
