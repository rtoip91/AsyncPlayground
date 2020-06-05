using System.Threading.Tasks;

namespace WindowCalculator.Interfaces
{
    public interface IStringGenerator
    {
        public  Task<string> Generate();
    }
}
