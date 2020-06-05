using System.Threading.Tasks;

namespace WindowCalculator.Interfaces
{
    public interface IFileHandler
    {
        public Task WriteToFile(string content);
        public Task WriteToFileWithSeparateTask(string content);
    }
}
