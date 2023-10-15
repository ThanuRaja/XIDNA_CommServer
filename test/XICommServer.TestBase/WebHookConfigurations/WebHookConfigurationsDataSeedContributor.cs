using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using XICommServer.WebHookConfigurations;

namespace XICommServer.WebHookConfigurations
{
    public class WebHookConfigurationsDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly IWebHookConfigurationRepository _webHookConfigurationRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public WebHookConfigurationsDataSeedContributor(IWebHookConfigurationRepository webHookConfigurationRepository, IUnitOfWorkManager unitOfWorkManager)
        {
            _webHookConfigurationRepository = webHookConfigurationRepository;
            _unitOfWorkManager = unitOfWorkManager;

        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _webHookConfigurationRepository.InsertAsync(new WebHookConfiguration
            (
                id: Guid.Parse("64b97695-f20b-4d1f-9875-d57047b0c635"),
                apiSignatureVerificationKey: "27687cc2a64b471a80b4c1dc07c002a443e1fa65333d4f71928eddc152fa726c2",
                clientWebhookUrl: "b7eb9f755dfe44eebcb485b9b0bbf499698f257b35704f",
                listenProcessed: true,
                listenDeferred: true,
                listenDelivered: true,
                listenOpen: true,
                listenClick: true,
                listenBounce: true,
                listenDropped: true,
                listenSpamReport: true,
                listenUnsubscribe: true,
                listenGroupUnsubscribe: true,
                listenGroupResubscribe: true,
                isDefault: true
            ));

            await _webHookConfigurationRepository.InsertAsync(new WebHookConfiguration
            (
                id: Guid.Parse("c5d6265f-37c9-43e5-b26d-ba3d8eec6d1a"),
                apiSignatureVerificationKey: "252fb612a2f94831b8c352d5e0b8418d2dc849670ac449be80d3f8c54448b237f",
                clientWebhookUrl: "999b12c47ffc4c2a95c2bb",
                listenProcessed: true,
                listenDeferred: true,
                listenDelivered: true,
                listenOpen: true,
                listenClick: true,
                listenBounce: true,
                listenDropped: true,
                listenSpamReport: true,
                listenUnsubscribe: true,
                listenGroupUnsubscribe: true,
                listenGroupResubscribe: true,
                isDefault: true
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}