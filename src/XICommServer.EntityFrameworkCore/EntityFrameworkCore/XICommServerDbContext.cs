using XICommServer.MailEventLogs;
using XICommServer.WebHookConfigurations;
using XICommServer.MailEvents;
using XICommServer.SendGridKeys;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.BlobStoring.Database.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.LanguageManagement.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TextTemplateManagement.EntityFrameworkCore;
using Volo.Saas.EntityFrameworkCore;
using Volo.Saas.Editions;
using Volo.Saas.Tenants;
using Volo.Abp.Gdpr;
using Volo.Abp.OpenIddict.EntityFrameworkCore;
using Cotur.Abp.ApiKeyAuthorization.EntityFrameworkCore;

namespace XICommServer.EntityFrameworkCore;

[ReplaceDbContext(typeof(IIdentityProDbContext))]
[ReplaceDbContext(typeof(ISaasDbContext))]
[ConnectionStringName("Default")]
public class XICommServerDbContext :
    AbpDbContext<XICommServerDbContext>,
    IIdentityProDbContext,
    ISaasDbContext
{
    public DbSet<MailEventLog> MailEventLogs { get; set; }
    public DbSet<WebHookConfiguration> WebHookConfigurations { get; set; }
    public DbSet<MailEvent> MailEvents { get; set; }
    public DbSet<SendGridKey> SendGridKeys { get; set; }
    /* Add DbSet properties for your Aggregate Roots / Entities here. */

    #region Entities from the modules

    /* Notice: We only implemented IIdentityProDbContext and ISaasDbContext
     * and replaced them for this DbContext. This allows you to perform JOIN
     * queries for the entities of these modules over the repositories easily. You
     * typically don't need that for other modules. But, if you need, you can
     * implement the DbContext interface of the needed module and use ReplaceDbContext
     * attribute just like IIdentityProDbContext and ISaasDbContext.
     *
     * More info: Replacing a DbContext of a module ensures that the related module
     * uses this DbContext on runtime. Otherwise, it will use its own DbContext class.
     */

    // Identity
    public DbSet<IdentityUser> Users { get; set; }
    public DbSet<IdentityRole> Roles { get; set; }
    public DbSet<IdentityClaimType> ClaimTypes { get; set; }
    public DbSet<OrganizationUnit> OrganizationUnits { get; set; }
    public DbSet<IdentitySecurityLog> SecurityLogs { get; set; }
    public DbSet<IdentityLinkUser> LinkUsers { get; set; }
    public DbSet<IdentityUserDelegation> UserDelegations { get; }

    // SaaS
    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<Edition> Editions { get; set; }
    public DbSet<TenantConnectionString> TenantConnectionStrings { get; set; }

    #endregion

    public XICommServerDbContext(DbContextOptions<XICommServerDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        /* Include modules to your migration db context */

        builder.ConfigurePermissionManagement();
        builder.ConfigureSettingManagement();
        builder.ConfigureBackgroundJobs();
        builder.ConfigureAuditLogging();
        builder.ConfigureIdentityPro();
        builder.ConfigureOpenIddictPro();
        builder.ConfigureFeatureManagement();
        builder.ConfigureLanguageManagement();
        builder.ConfigureSaas();
        builder.ConfigureTextTemplateManagement();
        builder.ConfigureBlobStoring();
        builder.ConfigureGdpr();
        builder.ConfigureApiKeyAuthorization();

        /* Configure your own tables/entities inside here */

        //builder.Entity<YourEntity>(b =>
        //{
        //    b.ToTable(XICommServerConsts.DbTablePrefix + "YourEntities", XICommServerConsts.DbSchema);
        //    b.ConfigureByConvention(); //auto configure for the base class props
        //    //...
        //});
        builder.Entity<SendGridKey>(b =>
    {
        b.ToTable(XICommServerConsts.DbTablePrefix + "SendGridKeys", XICommServerConsts.DbSchema);
        b.ConfigureByConvention();
        b.Property(x => x.TenantId).HasColumnName(nameof(SendGridKey.TenantId));
        b.Property(x => x.Name).HasColumnName(nameof(SendGridKey.Name)).IsRequired();
        b.Property(x => x.APIKey).HasColumnName(nameof(SendGridKey.APIKey)).IsRequired();
        b.Property(x => x.DisplayName).HasColumnName(nameof(SendGridKey.DisplayName));
        b.Property(x => x.EmailAddress).HasColumnName(nameof(SendGridKey.EmailAddress)).IsRequired();
        b.Property(x => x.IsDefault).HasColumnName(nameof(SendGridKey.IsDefault));
    });
        builder.Entity<MailEvent>(b =>
    {
        b.ToTable(XICommServerConsts.DbTablePrefix + "MailEvents", XICommServerConsts.DbSchema);
        b.ConfigureByConvention();
        b.Property(x => x.TenantId).HasColumnName(nameof(MailEvent.TenantId));
        b.Property(x => x.SGMessageId).HasColumnName(nameof(MailEvent.SGMessageId));
        b.Property(x => x.CreatedAt).HasColumnName(nameof(MailEvent.CreatedAt));
        b.Property(x => x.IsSuccess).HasColumnName(nameof(MailEvent.IsSuccess));
    });
        if (builder.IsHostDatabase())
        {
            builder.Entity<WebHookConfiguration>(b =>
{
    b.ToTable(XICommServerConsts.DbTablePrefix + "WebHookConfigurations", XICommServerConsts.DbSchema);
    b.ConfigureByConvention();
    b.Property(x => x.ApiSignatureVerificationKey).HasColumnName(nameof(WebHookConfiguration.ApiSignatureVerificationKey)).IsRequired();
    b.Property(x => x.ClientWebhookUrl).HasColumnName(nameof(WebHookConfiguration.ClientWebhookUrl));
    b.Property(x => x.ListenProcessed).HasColumnName(nameof(WebHookConfiguration.ListenProcessed));
    b.Property(x => x.ListenDeferred).HasColumnName(nameof(WebHookConfiguration.ListenDeferred));
    b.Property(x => x.ListenDelivered).HasColumnName(nameof(WebHookConfiguration.ListenDelivered));
    b.Property(x => x.ListenOpen).HasColumnName(nameof(WebHookConfiguration.ListenOpen));
    b.Property(x => x.ListenClick).HasColumnName(nameof(WebHookConfiguration.ListenClick));
    b.Property(x => x.ListenBounce).HasColumnName(nameof(WebHookConfiguration.ListenBounce));
    b.Property(x => x.ListenDropped).HasColumnName(nameof(WebHookConfiguration.ListenDropped));
    b.Property(x => x.ListenSpamReport).HasColumnName(nameof(WebHookConfiguration.ListenSpamReport));
    b.Property(x => x.ListenUnsubscribe).HasColumnName(nameof(WebHookConfiguration.ListenUnsubscribe));
    b.Property(x => x.ListenGroupUnsubscribe).HasColumnName(nameof(WebHookConfiguration.ListenGroupUnsubscribe));
    b.Property(x => x.ListenGroupResubscribe).HasColumnName(nameof(WebHookConfiguration.ListenGroupResubscribe));
    b.Property(x => x.IsDefault).HasColumnName(nameof(WebHookConfiguration.IsDefault));
});

        }
        builder.Entity<MailEventLog>(b =>
    {
        b.ToTable(XICommServerConsts.DbTablePrefix + "MailEventLogs", XICommServerConsts.DbSchema);
        b.ConfigureByConvention();
        b.Property(x => x.TenantId).HasColumnName(nameof(MailEventLog.TenantId));
        b.Property(x => x.Timestamp).HasColumnName(nameof(MailEventLog.Timestamp));
        b.Property(x => x.SmtpId).HasColumnName(nameof(MailEventLog.SmtpId));
        b.Property(x => x.EventType).HasColumnName(nameof(MailEventLog.EventType));
        b.Property(x => x.Category).HasColumnName(nameof(MailEventLog.Category));
        b.Property(x => x.SendGridEventId).HasColumnName(nameof(MailEventLog.SendGridEventId));
        b.Property(x => x.SendGridMessageId).HasColumnName(nameof(MailEventLog.SendGridMessageId));
        b.Property(x => x.TLS).HasColumnName(nameof(MailEventLog.TLS));
        b.Property(x => x.MarketingCampainId).HasColumnName(nameof(MailEventLog.MarketingCampainId));
        b.Property(x => x.MarketingCampainName).HasColumnName(nameof(MailEventLog.MarketingCampainName));
        b.Property(x => x.IsLogSynced).HasColumnName(nameof(MailEventLog.IsLogSynced));
    });
    }
}