using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using FileGenerator.Interfaces;

namespace FileGenerator
{
    public class FileHandler : IFileHandler
    {
       

        public FileHandler()
        {
          
        }

        public async Task WriteToFile (string content,string path, ulong amount)
        {
            if(string.IsNullOrEmpty(path))
            {
                throw new Exception("File path cannot be empty");
            }

            if(File.Exists(path))
            {
                File.Delete(path);
            }

            await using FileStream fileStream = File.Create(path);
            for (ulong i = 0; i < amount; i++)
            {
                await AddText(fileStream, content);
            }
        }

        public async Task WriteToFileWithSeparateTask(string content, string path, ulong amount)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new Exception("File path cannot be empty");
            }

            if (File.Exists(path))
            {
                File.Delete(path);
            }

            await using FileStream fileStream = File.Create(path);
            IList<Task> tasks = new List<Task>();

            for (ulong i = 0; i < amount; i++)
            {
                Task task = AddText(fileStream, content);
                tasks.Add(task);
            }

            await Task.WhenAll(tasks);
        }

        private async Task AddText(FileStream fileStream, string value)
        {
            byte[] info = new UTF8Encoding(true).GetBytes(value);
            await fileStream.WriteAsync(info, 0, info.Length);
        }
    }
}
