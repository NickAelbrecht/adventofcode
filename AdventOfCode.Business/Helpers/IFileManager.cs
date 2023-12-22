using System.IO;

namespace AdventOfCode.Business.Helpers
{
    // Added for easier unit test mocking purposes
    public interface IFileManager
    {
        StreamReader StreamReader(string path);
    }
}
