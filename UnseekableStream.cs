using System;
using System.IO;
namespace Example
{
    public class UnseekableStream : Stream
    {
        private Stream stream;
        public UnseekableStream(Stream stream)
        {
            this.stream = stream;
        }
        public override void Write(byte[] buffer, int offset, int count)
        {
            stream.Write(buffer, offset, count);
        }
        public override bool CanRead => false;
        public override bool CanSeek => false;
        public override bool CanWrite => true;
        public override long Length => stream.Length;
        public override long Position
        {
            get => stream.Position;
            set => throw new NotImplementedException();
        }
        public override void Flush()
        {
            stream.Flush();
        }
        public override int Read(byte[] buffer, int offset, int count)
        {
            throw new NotImplementedException();
        }
        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotImplementedException();
        }
        public override void SetLength(long value)
        {
            throw new NotImplementedException();
        }
    }
}