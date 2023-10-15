using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using XICommServer.WebHookConfigurations;
using XICommServer.EntityFrameworkCore;
using Xunit;

namespace XICommServer.WebHookConfigurations
{
    public class WebHookConfigurationRepositoryTests : XICommServerEntityFrameworkCoreTestBase
    {
        private readonly IWebHookConfigurationRepository _webHookConfigurationRepository;

        public WebHookConfigurationRepositoryTests()
        {
            _webHookConfigurationRepository = GetRequiredService<IWebHookConfigurationRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _webHookConfigurationRepository.GetListAsync(
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
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("64b97695-f20b-4d1f-9875-d57047b0c635"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _webHookConfigurationRepository.GetCountAsync(
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
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}