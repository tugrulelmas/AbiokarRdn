using AbiokaRdn.ApplicationService.Abstractions;
using AbiokaRdn.ApplicationService.Messaging;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AbiokaRdn.ApplicationService.Implementations
{
    public class FileReader : IFileReader
    {
        public byte[] ReadAllBytes(FilePathRequest filePathRequest) => File.ReadAllBytes(filePathRequest.Path);

        public string[] ReadAllLines(FilePathRequest filePathRequest) => File.ReadAllLines(filePathRequest.Path, Encoding.UTF8);

        public string ReadAllText(FilePathRequest filePathRequest) => File.ReadAllText(filePathRequest.Path, Encoding.UTF8);

        public IEnumerable<string> ReadLines(FilePathRequest filePathRequest) => File.ReadLines(filePathRequest.Path, Encoding.UTF8);
    }
}
