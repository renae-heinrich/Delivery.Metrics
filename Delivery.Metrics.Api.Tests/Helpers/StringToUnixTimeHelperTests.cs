using Delivery.Metrics.Helpers;
using Xunit;

namespace Delivery.Metrics.Api.Tests.Helpers
{
    public class StringToUnixTimeHelperTests
    {
        
        [Fact]
        public void GetUnixTimeStringReturnUtcUnixTime_GivenLocalStringDate()
        {
            var actual = "01/04/2021".GetUnixTime();
            
            Assert.Equal(1617195600, actual);
        }
    }
}