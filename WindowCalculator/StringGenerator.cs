using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FileGenerator.Interfaces;

namespace FileGenerator
{
    public class StringGenerator : IStringGenerator
    {
        #region Private methods

        private Task<string> GenerateString()
        {
            return Task<string>.Run(() =>
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.AppendJoin('\n', new List<string>
                {
                    "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua",
                    "Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat",
                    "Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur"
                });
                return stringBuilder.ToString();
            });
        }

        #endregion

        #region IStringGenerator Members

        public async Task<string> Generate()
        {
            string a = await GenerateString();
            return a;
        }

        #endregion
    }
}