using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace XICommServer.Pages;

public class Index_Tests : XICommServerWebTestBase
{
    [Fact]
    public async Task Welcome_Page()
    {
        var response = await GetResponseAsStringAsync("/");
        response.ShouldNotBeNull();
    }
}
