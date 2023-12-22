using System.IO;

namespace AdventOfCode.Business.Helpers
{
    public class FileManager : IFileManager
    {
        public StreamReader StreamReader(string path)
        {
            return new StreamReader(path);
        }
    }
}
