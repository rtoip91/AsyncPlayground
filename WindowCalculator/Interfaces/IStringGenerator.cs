using System.Threading.Tasks;

namespace FileGenerator.Interfaces
{
    public interface IStringGenerator
    {
        public  Task<string> Generate();
    }
}
