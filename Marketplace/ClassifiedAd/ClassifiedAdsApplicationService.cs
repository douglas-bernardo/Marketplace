using Marketplace.Framework;
using Marketplace.Domain.ClassifiedAd;
using Marketplace.Domain.Shared;
using static Marketplace.ClassifiedAd.Commands;

namespace Marketplace.ClassifiedAd
{
    public class ClassifiedAdsApplicationService(
        IClassifiedAdRepository repository,
        IUnitOfWork unitOfWork,
        ICurrencyLookup currencyLookup) : IApplicationService
    {
        public Task Handle(object command) => command switch
        {
            V1.Create cmd => 
                HandleCreate(cmd),
            V1.SetTitle cmd => HandleUpdate(
                cmd.Id, c => c.SetTitle(ClassifiedAdTitle.FromString(
                    cmd.Title ?? throw new ArgumentNullException(nameof(cmd.Title), "Title cannot be null")))),

            V1.UpdateText cmd => HandleUpdate(
                cmd.Id, c => c.UpdateText(ClassifiedAdText.FromString(
                    cmd.Text ?? throw new ArgumentNullException(nameof(cmd.Text), "Text cannot be null")))),

            V1.UpdatePrice cmd => HandleUpdate(
                cmd.Id,
                c => c.UpdatePrice(
                    Price.FromDecimal(cmd.Price,
                        cmd.Currency ?? throw new ArgumentNullException(nameof(cmd.Currency), "Currency cannot be null"),
                        currencyLookup))),

            V1.RequestToPublish cmd => HandleUpdate(
                cmd.Id, c => c.RequestToPublish()),

            _ => Task.CompletedTask
        };

        private async Task HandleCreate(V1.Create cmd)
        {
            if (await repository.Exists(new ClassifiedAdId(cmd.Id)))
                throw new InvalidOperationException($"Entity with id {cmd.Id} already exists");

            var classifiedAd = new Domain.ClassifiedAd.ClassifiedAd(
                new ClassifiedAdId(cmd.Id),
                new UserId(cmd.OwnerId));

            await repository.Add(classifiedAd);
            await unitOfWork.Commit();
        }

        private async Task HandleUpdate(Guid classifiedAdId, Action<Domain.ClassifiedAd.ClassifiedAd> operation)
        {
            var classifiedAd = await repository.Load(new ClassifiedAdId(classifiedAdId));
            if (classifiedAd == null)
                throw new InvalidOperationException($"Entity with id {classifiedAdId} cannot be found");

            operation(classifiedAd);

            await unitOfWork.Commit();
        }
    }
}
