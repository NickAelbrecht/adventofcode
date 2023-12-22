using System.IO;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Business.Helpers;
using AdventOfCode.Business.Managers;
using Moq;
using Xunit;
using Xunit.Abstractions;

namespace AdventOfCode.Business.UnitTests
{
    public class AdventOfCodeManagerTests : BaseUnitTest<AdventOfCodeManager>
    {
        public AdventOfCodeManagerTests(
            ITestOutputHelper output)
            : base(output)
        {
        }

        [Theory]
        [DefaultInlineAutoData(" ")]
        public async Task HandleCalibrationCount_ReturnsException_WithEmptyLine(
            string fileValue)
        {
            // Arrange
            var fakeFileBytes = Encoding.UTF8.GetBytes(fileValue);
            var fakeMemoryStream = new MemoryStream(fakeFileBytes);

            Mock<IFileManager>()
                .Setup(fileManager => fileManager.StreamReader(It.IsAny<string>()))
                .Returns(() => new StreamReader(fakeMemoryStream));

            // Act
            var result = Sut.HandleCalibrationCount();

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.ExceptionCode);
            Assert.Equal("AdventOfCodeManager_HandleCalibrationCount_EmptyLine", result.ExceptionCode);
        }

        [Fact]
        public async Task HandleCalibrationCount_ExecutesExpectedLogic()
        {
            // Arrange
            var fakeFileContents = "9aa2aa3";
            var fakeFileBytes = Encoding.UTF8.GetBytes(fakeFileContents);
            var fakeMemoryStream = new MemoryStream(fakeFileBytes);

            Mock<IFileManager>()
                .Setup(fileManager => fileManager.StreamReader(It.IsAny<string>()))
                .Returns(() => new StreamReader(fakeMemoryStream));

            // Act
            var result = Sut.HandleCalibrationCount();

            // Assert
            Assert.NotNull(result);
            Assert.Null(result.ExceptionCode);
            Assert.Equal(93, result.Calibration);
        }
    }
}