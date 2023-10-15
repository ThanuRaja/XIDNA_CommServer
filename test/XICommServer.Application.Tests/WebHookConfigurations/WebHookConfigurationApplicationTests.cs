using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace XICommServer.WebHookConfigurations
{
    public class WebHookConfigurationsAppServiceTests : XICommServerApplicationTestBase
    {
        private readonly IWebHookConfigurationsAppService _webHookConfigurationsAppService;
        private readonly IRepository<WebHookConfiguration, Guid> _webHookConfigurationRepository;

        public WebHookConfigurationsAppServiceTests()
        {
            _webHookConfigurationsAppService = GetRequiredService<IWebHookConfigurationsAppService>();
            _webHookConfigurationRepository = GetRequiredService<IRepository<WebHookConfiguration, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _webHookConfigurationsAppService.GetListAsync(new GetWebHookConfigurationsInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.Id == Guid.Parse("64b97695-f20b-4d1f-9875-d57047b0c635")).ShouldBe(true);
            result.Items.Any(x => x.Id == Guid.Parse("c5d6265f-37c9-43e5-b26d-ba3d8eec6d1a")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _webHookConfigurationsAppService.GetAsync(Guid.Parse("64b97695-f20b-4d1f-9875-d57047b0c635"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("64b97695-f20b-4d1f-9875-d57047b0c635"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new WebHookConfigurationCreateDto
            {
                ApiSignatureVerificationKey = "dc86c32e82d",
                ClientWebhookUrl = "0ed2648b97bb4b8b9d9b445168c0e0c71b90e289288649b39ffd928927502d21c9b036398fa54",
                ListenProcessed = true,
                ListenDeferred = true,
                ListenDelivered = true,
                ListenOpen = true,
                ListenClick = true,
                ListenBounce = true,
                ListenDropped = true,
                ListenSpamReport = true,
                ListenUnsubscribe = true,
                ListenGroupUnsubscribe = true,
                ListenGroupResubscribe = true,
                IsDefault = true
            };

            // Act
            var serviceResult = await _webHookConfigurationsAppService.CreateAsync(input);

            // Assert
            var result = await _webHookConfigurationRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.ApiSignatureVerificationKey.ShouldBe("dc86c32e82d");
            result.ClientWebhookUrl.ShouldBe("0ed2648b97bb4b8b9d9b445168c0e0c71b90e289288649b39ffd928927502d21c9b036398fa54");
            result.ListenProcessed.ShouldBe(true);
            result.ListenDeferred.ShouldBe(true);
            result.ListenDelivered.ShouldBe(true);
            result.ListenOpen.ShouldBe(true);
            result.ListenClick.ShouldBe(true);
            result.ListenBounce.ShouldBe(true);
            result.ListenDropped.ShouldBe(true);
            result.ListenSpamReport.ShouldBe(true);
            result.ListenUnsubscribe.ShouldBe(true);
            result.ListenGroupUnsubscribe.ShouldBe(true);
            result.ListenGroupResubscribe.ShouldBe(true);
            result.IsDefault.ShouldBe(true);
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new WebHookConfigurationUpdateDto()
            {
                ApiSignatureVerificationKey = "4fb23ec76c2c4d21979a7fccbcbcbf16c0d6efe59a9a43cabe3a7e662af65ff97f78fff31da1427a8c29dc52c8c5a301",
                ClientWebhookUrl = "5237f210572f4af78524397457ce1f0280002a41b1f948",
                ListenProcessed = true,
                ListenDeferred = true,
                ListenDelivered = true,
                ListenOpen = true,
                ListenClick = true,
                ListenBounce = true,
                ListenDropped = true,
                ListenSpamReport = true,
                ListenUnsubscribe = true,
                ListenGroupUnsubscribe = true,
                ListenGroupResubscribe = true,
                IsDefault = true
            };

            // Act
            var serviceResult = await _webHookConfigurationsAppService.UpdateAsync(Guid.Parse("64b97695-f20b-4d1f-9875-d57047b0c635"), input);

            // Assert
            var result = await _webHookConfigurationRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.ApiSignatureVerificationKey.ShouldBe("4fb23ec76c2c4d21979a7fccbcbcbf16c0d6efe59a9a43cabe3a7e662af65ff97f78fff31da1427a8c29dc52c8c5a301");
            result.ClientWebhookUrl.ShouldBe("5237f210572f4af78524397457ce1f0280002a41b1f948");
            result.ListenProcessed.ShouldBe(true);
            result.ListenDeferred.ShouldBe(true);
            result.ListenDelivered.ShouldBe(true);
            result.ListenOpen.ShouldBe(true);
            result.ListenClick.ShouldBe(true);
            result.ListenBounce.ShouldBe(true);
            result.ListenDropped.ShouldBe(true);
            result.ListenSpamReport.ShouldBe(true);
            result.ListenUnsubscribe.ShouldBe(true);
            result.ListenGroupUnsubscribe.ShouldBe(true);
            result.ListenGroupResubscribe.ShouldBe(true);
            result.IsDefault.ShouldBe(true);
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _webHookConfigurationsAppService.DeleteAsync(Guid.Parse("64b97695-f20b-4d1f-9875-d57047b0c635"));

            // Assert
            var result = await _webHookConfigurationRepository.FindAsync(c => c.Id == Guid.Parse("64b97695-f20b-4d1f-9875-d57047b0c635"));

            result.ShouldBeNull();
        }
    }
}