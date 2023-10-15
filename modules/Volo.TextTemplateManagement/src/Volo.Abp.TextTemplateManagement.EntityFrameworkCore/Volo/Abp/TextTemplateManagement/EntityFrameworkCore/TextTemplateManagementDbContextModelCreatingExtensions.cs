using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.TextTemplateManagement.TextTemplates;

namespace Volo.Abp.TextTemplateManagement.EntityFrameworkCore;

public static class TextTemplateManagementDbContextModelCreatingExtensions
{
    public static void ConfigureTextTemplateManagement(
        this ModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));

        builder.Entity<TextTemplateContent>(b =>
        {
            b.ToTable(TextTemplateManagementDbProperties.DbTablePrefix + "TextTemplateContents", TextTemplateManagementDbProperties.DbSchema);

            b.ConfigureByConvention();

            b.Property(e => e.Name).IsRequired().HasMaxLength(TextTemplateConsts.MaxNameLength);
            b.Property(e => e.CultureName).HasMaxLength(TextTemplateConsts.MaxCultureNameLength);
            b.Property(e => e.Content).IsRequired().HasMaxLength(TextTemplateConsts.MaxContentLength);

            b.ApplyObjectExtensionMappings();
        });

        builder.TryConfigureObjectExtensions<TextTemplateManagementDbContext>();
    }
}
