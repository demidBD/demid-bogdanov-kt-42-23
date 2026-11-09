using DemidBogdanovKT_42_23.Models;
using Xunit;

namespace DemidBogdanovKT_42_23.Tests
{
    public class GroupTests
    {
        [Theory]
        [InlineData("KT-42-23")]
        [InlineData("KT-31-20")]
        public void IsValidGroupName_CorrectFormat_True(string name)
        {
            Assert.True(Group.IsValidGroupName(name));
        }

        [Theory]
        [InlineData("KT4223")]
        [InlineData("KT-4-23")]
        [InlineData("42-KT-23")]
        [InlineData("")]
        [InlineData(null)]
        public void IsValidGroupName_WrongFormat_False(string? name)
        {
            Assert.False(Group.IsValidGroupName(name));
        }
    }
}