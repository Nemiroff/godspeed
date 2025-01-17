using System;
using System.IO;

namespace SharpCompress.Test.Mocks;

// This is a simplified version of CryptoStream that always flushes the inner stream on Dispose to trigger an error in EntryStream
// CryptoStream doesn't always trigger the Flush, so this class is used instead
// See https://referencesource.microsoft.com/#mscorlib/system/security/cryptography/cryptostream.cs,141

public class FlushOnDisposeStream : Stream, IDisposable
{
    private Stream inner;

    public FlushOnDisposeStream(Stream innerStream) => inner = innerStream;

    public override bool CanRead => inner.CanRead;

    public override bool CanSeek => false;

    public override bool CanWrite => false;

    public override long Length => inner.Length;

    public override long Position
    {
        get => inner.Position;
        set => inner.Position = value;
    }

    public override void Flush() { }

    public override int Read(byte[] buffer, int offset, int count) =>
        inner.Read(buffer, offset, count);

    public override long Seek(long offset, SeekOrigin origin) =>
        throw new NotImplementedException();

    public override void SetLength(long value) => throw new NotImplementedException();

    public override void Write(byte[] buffer, int offset, int count) =>
        throw new NotImplementedException();

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            inner.Flush();
            inner.Close();
        }

        base.Dispose(disposing);
    }
}
