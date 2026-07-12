namespace Marketplace.Api
{
    public interface IApplicationService
    {
        Task Handle(object command);
    }
}