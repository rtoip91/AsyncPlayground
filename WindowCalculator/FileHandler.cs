using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using WindowCalculator.Interfaces;

namespace WindowCalculator
{
    public class FileHandler : IFileHandler
    {
        private readonly string filePath;
        private readonly ulong amount;

        public FileHandler(string path, ulong repetetionAmount)
        {
            filePath = path;
            amount = repetetionAmount;
        }

        public async Task WriteToFile (string content)
        {
            if(string.IsNullOrEmpty(filePath))
            {
                throw new Exception("File path cannot be empty");
            }

            if(File.Exists(filePath))
            {
                File.Delete(filePath);
            }
              
            using (FileStream fileStream = File.Create(filePath))
            {
                for (ulong i = 0; i < amount; i++)
                {
                    await AddText(fileStream, content);                    
                }
            }
        }

        public async Task WriteToFileWithSeparateTask(string content)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                throw new Exception("File path cannot be empty");
            }

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            using (FileStream fileStream = File.Create(filePath))
            {
                 IList<Task> tasks = new List<Task>();              

                for (ulong i = 0; i < amount; i++)
                {
                    Task tasak = AddText(fileStream, content);
                    tasks.Add(tasak);
                }

                await Task.WhenAll(tasks);
            }
        }

        private async Task AddText(FileStream fileStream, string value)
        {
            byte[] info = new UTF8Encoding(true).GetBytes(value);
            await fileStream.WriteAsync(info, 0, info.Length);
        }
    }
}
