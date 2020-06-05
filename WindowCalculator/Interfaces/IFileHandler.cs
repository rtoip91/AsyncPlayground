using System.Threading.Tasks;

namespace FileGenerator.Interfaces
{
    public interface IFileHandler
    {
        public Task WriteToFile(string content, ulong amount);
        public Task WriteToFileWithSeparateTask(string content, ulong amount);
    }
}
