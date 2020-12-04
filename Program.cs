using System;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace Example
{
    class Program
    {

        static void CreateZipArchive(string filename, Func<Stream, Stream> GetWriteStreamFromFileStream)
        {
            Console.WriteLine("creating zip archive " + filename);
            File.Delete(filename);
            using (var fileStream = File.Open(filename, FileMode.OpenOrCreate, FileAccess.Write))
            {
                using (var zip = new ZipArchive(GetWriteStreamFromFileStream(fileStream), ZipArchiveMode.Create))
                {
                    using (var s = zip.CreateEntry("file.txt", CompressionLevel.NoCompression).Open())
                    {
                        var data = new MemoryStream(Encoding.UTF8.GetBytes("hello world"));
                        data.CopyTo(s);
                    }
                }
            }
        }
        static void TestZipArchive(string filename)
        {
            Console.WriteLine("testing zip archive " + filename);
            try
            {
                using (var file = File.OpenRead(filename))
                using (var stream = new ICSharpCode.SharpZipLib.Zip.ZipInputStream(file))
                {
                    for (var entry = stream.GetNextEntry(); entry != null; entry = stream.GetNextEntry())
                    {
                        Console.WriteLine(entry.Name + ":");
                        stream.CopyTo(Console.OpenStandardOutput());
                        Console.WriteLine("");
                    }
                }
            }
            catch (Exception e)
            {
                Console.Write(e.Message + e.StackTrace);
            }
        }

        static void Main(string[] args)
        {
            File.Delete("./stream.zip");
            CreateZipArchive("./seekable.zip", s => s);
            TestZipArchive("./seekable.zip");
            CreateZipArchive("./unseekable.zip", s => new UnseekableStream(s));
            TestZipArchive("./unseekable.zip");
        }
    }
}
