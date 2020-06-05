using System.Threading.Tasks;

namespace FileGenerator.Interfaces
{
    public interface IFileHandler
    {
        public Task WriteToFile(string content,string path, ulong amount);
        public Task WriteToFileWithSeparateTask(string content, string path, ulong amount);
    }
}
