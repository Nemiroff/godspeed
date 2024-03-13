/*
 * BranchExecTests.cs -- Test for converters
 * <Contribution by Louis-Michel Bergeron, on behalf of aDolus Technolog Inc., 2022>
 *
 * All data is extracted from busybox, where "ip" is the offset within the busybox file.
 */

using SharpCompress.Compressors.Filters;
using Xunit;

namespace SharpCompress.Test.Filters;

public class BranchExecTests
{
    private static byte[] x86resultData { get; } =
        new byte[]
        {
            0x12,
            0x00,
            0x00,
            0x00,
            0x02,
            0x0B,
            0x00,
            0x00,
            0xE8,
            0xBD,
            0x00,
            0x00,
            0x00,
            0x07,
            0x00,
            0x00,
            0x12,
            0x00,
            0x00,
            0x00,
            0x6D,
            0x01,
            0x00,
            0x00,
            0xF0,
            0xCA,
            0x00,
            0x00,
            0x1C,
            0x00,
            0x00,
            0x00,
            0x12,
            0x00,
            0x00,
            0x00,
            0xBC,
            0x09,
            0x00,
            0x00,
            0x14,
            0xC2,
            0x00,
            0x00,
            0xE0,
            0x01,
            0x00,
            0x00,
            0x12,
            0x00,
            0x00,
            0x00,
            0x98,
            0x0B,
            0x00,
            0x00,
            0x60,
            0x75,
            0x0A,
            0x00,
            0x00,
            0x00,
            0x00,
            0x00,
            0x10,
            0x00,
            0xF1,
            0xFF,
            0x42,
            0x01,
            0x00,
            0x00,
            0x08,
            0xC8,
            0x00,
            0x00,
            0x1C,
            0x00,
        };

    private static byte[] x86Data { get; } =
        new byte[]
        {
            0x12,
            0x00,
            0x00,
            0x00,
            0x02,
            0x0B,
            0x00,
            0x00,
            0xE8,
            0xCA,
            0x20,
            0x00,
            0x00,
            0x07,
            0x00,
            0x00,
            0x12,
            0x00,
            0x00,
            0x00,
            0x6D,
            0x01,
            0x00,
            0x00,
            0xF0,
            0xCA,
            0x00,
            0x00,
            0x1C,
            0x00,
            0x00,
            0x00,
            0x12,
            0x00,
            0x00,
            0x00,
            0xBC,
            0x09,
            0x00,
            0x00,
            0x14,
            0xC2,
            0x00,
            0x00,
            0xE0,
            0x01,
            0x00,
            0x00,
            0x12,
            0x00,
            0x00,
            0x00,
            0x98,
            0x0B,
            0x00,
            0x00,
            0x60,
            0x75,
            0x0A,
            0x00,
            0x00,
            0x00,
            0x00,
            0x00,
            0x10,
            0x00,
            0xF1,
            0xFF,
            0x42,
            0x01,
            0x00,
            0x00,
            0x08,
            0xC8,
            0x00,
            0x00,
            0x1C,
            0x00,
        };

    private static byte[] ppcResultData { get; } =
        new byte[]
        {
            0xF8,
            0x6B,
            0x2E,
            0x8C,
            0x95,
            0xC5,
            0x4B,
            0x1B,
            0x94,
            0x78,
            0x9E,
            0x7C,
            0xBD,
            0x8B,
            0xA8,
            0xAF,
            0x31,
            0x20,
            0xFE,
            0x0F,
            0xB3,
            0x15,
            0x9A,
            0x7C,
            0xD5,
            0x5C,
            0xC2,
            0xC0,
            0xEC,
            0xE9,
            0x43,
            0x2B,
            0xD0,
            0x9F,
            0x2C,
            0xFC,
            0xB8,
            0x2B,
            0x6B,
            0x15,
            0xCD,
            0x3F,
            0x0C,
            0xAF,
            0x8F,
            0x68,
            0xB0,
            0x6E,
            0x6B,
            0x30,
            0x2E,
            0x8C,
            0x3F,
            0x7E,
            0x96,
            0x7C,
            0x93,
            0xB2,
            0xA4,
            0x0E,
            0x43,
            0xEA,
            0x20,
            0x10,
            0x38,
            0x6D,
            0x37,
            0xF8,
            0x87,
            0xFE,
            0xA9,
            0x63,
            0x75,
            0xF5,
            0x56,
            0x34,
            0x4A,
            0xE3,
            0xCF,
            0x89,
            0x18,
            0x08,
            0xC2,
            0x76,
            0x74,
            0x12,
            0xEC,
            0xA7,
            0x6D,
            0xC2,
            0xB7,
            0x1B,
            0x7A,
            0xB2,
            0xD4,
            0xED
        };

    private static byte[] ppcData { get; } =
        new byte[]
        {
            0xF8,
            0x6B,
            0x2E,
            0x8C,
            0x95,
            0xC5,
            0x4B,
            0x1B,
            0x94,
            0x78,
            0x9E,
            0x7C,
            0xBD,
            0x8B,
            0xA8,
            0xAF,
            0x31,
            0x20,
            0xFE,
            0x0F,
            0xB3,
            0x15,
            0x9A,
            0x7C,
            0xD5,
            0x5C,
            0xC2,
            0xC0,
            0xEC,
            0xE9,
            0x43,
            0x2B,
            0xD0,
            0x9F,
            0x2C,
            0xFC,
            0xB8,
            0x2B,
            0x6B,
            0x15,
            0xCD,
            0x3F,
            0x0C,
            0xAF,
            0x8F,
            0x68,
            0xB0,
            0x6E,
            0x6B,
            0x30,
            0x2E,
            0x8C,
            0x3F,
            0x7E,
            0x96,
            0x7C,
            0x93,
            0xB2,
            0xA4,
            0x0E,
            0x43,
            0xEA,
            0x20,
            0x10,
            0x38,
            0x6D,
            0x37,
            0xF8,
            0x87,
            0xFE,
            0xA9,
            0x63,
            0x75,
            0xF5,
            0x56,
            0x34,
            0x4A,
            0xE3,
            0xD6,
            0x75,
            0x18,
            0x08,
            0xC2,
            0x76,
            0x74,
            0x12,
            0xEC,
            0xA7,
            0x6D,
            0xC2,
            0xB7,
            0x1B,
            0x7A,
            0xB2,
            0xD4,
            0xED
        };

    private static byte[] armResultData { get; } =
        new byte[]
        {
            0x7C,
            0xFC,
            0x0A,
            0x00,
            0x16,
            0x42,
            0x01,
            0x00,
            0x80,
            0xFC,
            0x0A,
            0x00,
            0x16,
            0xB8,
            0x00,
            0x00,
            0x84,
            0xFC,
            0x0A,
            0x00,
            0x16,
            0x3A,
            0x00,
            0x00,
            0x04,
            0xE0,
            0x2D,
            0xE5,
            0x04,
            0xD0,
            0x4D,
            0xE2,
            0x0E,
            0x04,
            0x00,
            0xEB,
            0x04,
            0xD0,
            0x8D,
            0xE2,
            0x04,
            0xE0,
            0x9D,
            0xE4,
            0x1E,
            0xFF,
            0x2F,
            0xE1,
            0x04,
            0xE0,
            0x2D,
            0xE5,
            0x04,
            0xE0,
            0x9F,
            0xE5,
            0x0E,
            0xE0,
            0x8F,
            0xE0,
            0x08,
            0xF0,
            0xBE,
            0xE5,
            0xF0,
            0x3A,
            0x0A,
            0x00,
            0x00,
            0xC6,
            0x8F,
            0xE2,
            0xA3,
            0xCA,
            0x8C,
            0xE2,
            0xF0,
            0xFA,
            0xBC,
            0xE5,
            0x00,
            0xC6,
            0x8F,
            0xE2,
            0xA3,
            0xCA,
            0x8C,
            0xE2,
            0xE8,
            0xFA,
            0xBC,
            0xE5,
            0x00,
            0xC6,
            0x8F,
            0xE2
        };

    private static byte[] armData { get; } =
        new byte[]
        {
            0x7C,
            0xFC,
            0x0A,
            0x00,
            0x16,
            0x42,
            0x01,
            0x00,
            0x80,
            0xFC,
            0x0A,
            0x00,
            0x16,
            0xB8,
            0x00,
            0x00,
            0x84,
            0xFC,
            0x0A,
            0x00,
            0x16,
            0x3A,
            0x00,
            0x00,
            0x04,
            0xE0,
            0x2D,
            0xE5,
            0x04,
            0xD0,
            0x4D,
            0xE2,
            0x18,
            0x13,
            0x00,
            0xEB,
            0x04,
            0xD0,
            0x8D,
            0xE2,
            0x04,
            0xE0,
            0x9D,
            0xE4,
            0x1E,
            0xFF,
            0x2F,
            0xE1,
            0x04,
            0xE0,
            0x2D,
            0xE5,
            0x04,
            0xE0,
            0x9F,
            0xE5,
            0x0E,
            0xE0,
            0x8F,
            0xE0,
            0x08,
            0xF0,
            0xBE,
            0xE5,
            0xF0,
            0x3A,
            0x0A,
            0x00,
            0x00,
            0xC6,
            0x8F,
            0xE2,
            0xA3,
            0xCA,
            0x8C,
            0xE2,
            0xF0,
            0xFA,
            0xBC,
            0xE5,
            0x00,
            0xC6,
            0x8F,
            0xE2,
            0xA3,
            0xCA,
            0x8C,
            0xE2,
            0xE8,
            0xFA,
            0xBC,
            0xE5,
            0x00,
            0xC6,
            0x8F,
            0xE2
        };

    private static byte[] armtResultData { get; } =
        new byte[]
        {
            0x95,
            0x23,
            0xB6,
            0xB1,
            0xBE,
            0x60,
            0x79,
            0xF0,
            0xF6,
            0x01,
            0xD9,
            0x7F,
            0x2E,
            0x03,
            0x31,
            0x1C,
            0xFD,
            0xD3,
            0x40,
            0x0F,
            0x21,
            0x3C,
            0x06,
            0x97,
            0xE5,
            0xC3,
            0x57,
            0x11,
            0x76,
            0x6F,
            0xE3,
            0x70,
            0xED,
            0x49,
            0xCB,
            0xB5,
            0xC9,
            0x42,
            0x59,
            0x10,
            0x2F,
            0xBD,
            0xAE,
            0xB1,
            0x40,
            0x4D,
            0x9D,
            0x7C,
            0xE9,
            0xFC,
            0x48,
            0x3E,
            0xBC,
            0x7F,
            0x0B,
            0x23,
            0xB0,
            0x8A,
            0x4D,
            0x02,
            0x39,
            0xC4,
            0xFB,
            0x66,
            0x83,
            0x7F,
            0xA7,
            0xBD,
            0x12,
            0xAC,
            0xED,
            0x31,
            0x34,
            0x93,
            0x4D,
            0x8D,
            0xD7,
            0x94,
            0x93,
            0x1C,
            0x0A,
            0x50,
            0x54,
            0x4B,
            0x03,
            0x55,
            0x27,
            0xFE,
            0xCE,
            0x29,
            0x66,
            0x52,
            0x81,
            0xAE,
            0x69,
            0xA0,
            0x69,
            0xF2,
            0x3D,
            0xFF,
            0xA1,
            0x8A,
            0x5D,
            0x61,
            0x7D,
            0xC5,
            0x94,
            0x0A,
            0x7D,
            0xED,
            0x11,
            0x0F
        };

    private static byte[] armtData { get; } =
        new byte[]
        {
            0x95,
            0x23,
            0xB6,
            0xB1,
            0xBE,
            0x60,
            0x79,
            0xF0,
            0xF6,
            0x01,
            0xD9,
            0x7F,
            0x2E,
            0x03,
            0x31,
            0x1C,
            0xFD,
            0xD3,
            0x40,
            0x0F,
            0x21,
            0x3C,
            0x06,
            0x97,
            0xE5,
            0xC3,
            0x57,
            0x11,
            0x76,
            0x6F,
            0xE3,
            0x70,
            0xED,
            0x49,
            0xCB,
            0xB5,
            0xC9,
            0x42,
            0x59,
            0x10,
            0x2F,
            0xBD,
            0xAE,
            0xB1,
            0x40,
            0x4D,
            0x9D,
            0x7C,
            0xE9,
            0xFC,
            0x48,
            0x3E,
            0xBC,
            0x7F,
            0x0B,
            0x23,
            0xB0,
            0x8A,
            0x4D,
            0x02,
            0x39,
            0xC4,
            0xFB,
            0x66,
            0x83,
            0x7F,
            0xA7,
            0xBD,
            0x12,
            0xAC,
            0xED,
            0x31,
            0x34,
            0x93,
            0x4D,
            0x8D,
            0xD7,
            0x94,
            0x93,
            0x1C,
            0x0A,
            0x50,
            0x54,
            0x4B,
            0x03,
            0x55,
            0x27,
            0xFE,
            0xCE,
            0x29,
            0x66,
            0x52,
            0x81,
            0xAE,
            0x69,
            0xA0,
            0x6A,
            0xF2,
            0x6F,
            0xFC,
            0xA1,
            0x8A,
            0x5D,
            0x61,
            0x7D,
            0xC5,
            0x94,
            0x0A,
            0x7D,
            0xED,
            0x11,
            0x0F
        };

    private static byte[] ia64ResultData { get; } =
        new byte[]
        {
            0x4D,
            0xF8,
            0xF2,
            0x0D,
            0x06,
            0x2F,
            0x74,
            0x0F,
            0xF0,
            0x91,
            0x06,
            0x0B,
            0x19,
            0x22,
            0x91,
            0x5A,
            0x66,
            0x56,
            0xA7,
            0x15,
            0x77,
            0x1E,
            0x2F,
            0xA3,
            0xE4,
            0xDE,
            0x93,
            0x1C,
            0xD5,
            0xCE,
            0x6E,
            0x45,
            0x36,
            0x15,
            0x15,
            0x65,
            0x4E,
            0xC5,
            0xA3,
            0x8C,
            0x5A,
            0x8B,
            0x8A,
            0x1C,
            0x12,
            0x5B,
            0x39,
            0x1F,
            0xA0,
            0xF2,
            0x93,
            0x7C,
            0x7F,
            0x5D,
            0xD9,
            0x30,
            0x1F,
            0xF6,
            0x5C,
            0x10,
            0x62,
            0x3E,
            0xB4,
            0x64,
            0x56,
            0x48,
            0xB2,
            0x20,
            0x39,
            0xE8,
            0x44,
            0x10,
            0x87,
            0x9E,
            0x2C,
            0xFC,
            0x29,
            0x0E,
            0x20,
            0x76,
            0xCE,
            0xDA,
            0x93,
            0x1C,
            0xED,
            0x54,
            0x0D,
            0xAF,
            0xEC,
            0xDE,
            0x93,
            0x1C,
            0x2B,
            0x72,
            0xD5,
            0x0D
        };

    private static byte[] ia64Data { get; } =
        new byte[]
        {
            0x4D,
            0xF8,
            0xF2,
            0x0D,
            0x06,
            0x2F,
            0x74,
            0x0F,
            0xF0,
            0x91,
            0x06,
            0x0B,
            0x19,
            0x22,
            0x91,
            0x5A,
            0x66,
            0x56,
            0xA7,
            0x15,
            0x77,
            0x1E,
            0x2F,
            0xA3,
            0xE4,
            0xDE,
            0x93,
            0x1C,
            0xD5,
            0xCE,
            0x6E,
            0x45,
            0x36,
            0x15,
            0x15,
            0x65,
            0x4E,
            0xC5,
            0xA3,
            0x8C,
            0x5A,
            0x8B,
            0x8A,
            0x1C,
            0x12,
            0x5B,
            0x39,
            0x1F,
            0xA0,
            0xF2,
            0x93,
            0x7C,
            0x7F,
            0x5D,
            0xD9,
            0x30,
            0x1F,
            0xF6,
            0x5C,
            0x10,
            0x62,
            0x3E,
            0xB4,
            0x64,
            0x56,
            0x48,
            0xB2,
            0x20,
            0x39,
            0xE8,
            0x44,
            0x80,
            0x8C,
            0x9E,
            0x2C,
            0xFC,
            0x29,
            0x0E,
            0x20,
            0x76,
            0xCE,
            0xDA,
            0x93,
            0x1C,
            0xED,
            0x54,
            0x0D,
            0xAF,
            0xEC,
            0xDE,
            0x93,
            0x1C,
            0x2B,
            0x72,
            0xD5,
            0x0D
        };

    private static byte[] sparcResultData { get; } =
        new byte[]
        {
            0x78,
            0x2E,
            0x73,
            0x6F,
            0x2E,
            0x33,
            0x00,
            0x00,
            0x07,
            0x01,
            0x00,
            0x00,
            0x09,
            0x00,
            0x00,
            0x00,
            0x40,
            0x00,
            0x00,
            0x00,
            0x0B,
            0x00,
            0x00,
            0x00,
            0xA2,
            0x0D,
            0x10,
            0x12,
            0x30,
            0x03,
            0xC9,
            0x37,
            0x40,
            0x1A,
            0x85,
            0xD9,
            0x44,
            0x34,
            0x40,
            0x32,
            0x85,
            0xA0,
            0x30,
            0x40,
            0x00,
            0x70,
            0x00,
            0x40,
            0x84,
            0x80,
            0x04,
            0x00,
            0xE4,
            0xAC,
            0x07,
            0x04,
            0x21,
            0x44,
            0x02,
            0x20,
            0x10,
            0x00,
            0x40,
            0xC2,
            0x89,
            0x98,
            0x85,
            0x00,
            0x58,
            0x6A,
            0x41,
            0x8E,
            0x18,
            0xA1,
            0x91,
            0x00,
            0x10,
            0x00,
        };

    private static byte[] sparcData { get; } =
        new byte[]
        {
            0x78,
            0x2E,
            0x73,
            0x6F,
            0x2E,
            0x33,
            0x00,
            0x00,
            0x07,
            0x01,
            0x00,
            0x00,
            0x09,
            0x00,
            0x00,
            0x00,
            0x40,
            0x00,
            0x00,
            0x44,
            0x0B,
            0x00,
            0x00,
            0x00,
            0xA2,
            0x0D,
            0x10,
            0x12,
            0x30,
            0x03,
            0xC9,
            0x37,
            0x40,
            0x1A,
            0x86,
            0x21,
            0x44,
            0x34,
            0x40,
            0x32,
            0x85,
            0xA0,
            0x30,
            0x40,
            0x00,
            0x70,
            0x00,
            0x40,
            0x84,
            0x80,
            0x04,
            0x00,
            0xE4,
            0xAC,
            0x07,
            0x04,
            0x21,
            0x44,
            0x02,
            0x20,
            0x10,
            0x00,
            0x40,
            0xC2,
            0x89,
            0x98,
            0x85,
            0x00,
            0x58,
            0x6A,
            0x41,
            0x8E,
            0x18,
            0xA1,
            0x91,
            0x00,
            0x10,
            0x00,
        };

    private void CompareBuffer(byte[] testBuffer, byte[] targetBuffer) =>
        Assert.Equal(testBuffer, targetBuffer);

    [Fact]
    public void X86ConverterDecodeTest()
    {
        uint state = 0;
        uint ip = 0x2000;
        var testData = x86Data;
        BranchExecFilter.X86Converter(testData, ip, ref state);
        CompareBuffer(testData, x86resultData);
    }

    [Fact]
    public void PowerPCConverterDecodeTest()
    {
        uint ip = 0x6A0;
        var testData = ppcData;
        BranchExecFilter.PowerPCConverter(testData, ip);
        CompareBuffer(testData, ppcResultData);
    }

    [Fact]
    public void ARMConverteDecoderTest()
    {
        uint ip = 0x3C00;
        var testData = armData;
        BranchExecFilter.ARMConverter(testData, ip);
        CompareBuffer(testData, armResultData);
    }

    [Fact]
    public void ARMTConverterDecodeTest()
    {
        uint ip = 0xA00;
        var testData = armtData;
        BranchExecFilter.ARMTConverter(testData, ip);
        CompareBuffer(testData, armtResultData);
    }

    [Fact]
    public void IA64ConverterDecodeTest()
    {
        uint ip = 0xAA0;
        var testData = ia64Data;
        BranchExecFilter.IA64Converter(testData, ip);
        CompareBuffer(testData, ia64ResultData);
    }

    [Fact]
    public void SPARCConverterDecodeTest()
    {
        uint ip = 0x100;
        var testData = sparcData;
        BranchExecFilter.SPARCConverter(testData, ip);
        CompareBuffer(testData, sparcResultData);
    }
}
