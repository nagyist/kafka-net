﻿using System;
using System.Diagnostics.Contracts;
using System.IO;
using System.Text;

namespace KafkaNet.Common
{
    /// <summary>
    /// A BinaryWriter that stores values in BigEndian format.
    /// </summary>
    /// <remarks>
    /// Booleans, bytes and byte arrays will be written directly.
    /// All other values will be converted to a byte array in BigEndian byte order and written.
    /// Characters and Strings will all be encoded in UTF-8 (which is byte order independent).
    /// </remarks>
    /// <remarks>
    /// BigEndianBinaryWriter code provided by Zoltu
    /// https://github.com/Zoltu/Zoltu.EndianAwareBinaryReaderWriter
    /// </remarks>
    public class BigEndianBinaryWriter : BinaryWriter
    {
        public BigEndianBinaryWriter(Stream stream)
            : base(stream, Encoding.UTF8)
        {
            Contract.Requires(stream != null);
        }

        public BigEndianBinaryWriter(Stream stream, Boolean leaveOpen)
#if NET40
			: base(stream, Encoding.UTF8)
#else
            : base(stream, Encoding.UTF8, leaveOpen)
#endif
        {
            Contract.Requires(stream != null);
        }

        public override void Write(Decimal value)
        {
            var ints = Decimal.GetBits(value);
            Contract.Assume(ints != null);
            Contract.Assume(ints.Length == 4);

            if (BitConverter.IsLittleEndian)
                Array.Reverse(ints);

            for (var i = 0; i < 4; ++i)
            {
                var bytes = BitConverter.GetBytes(ints[i]);
                if (BitConverter.IsLittleEndian)
                    Array.Reverse(bytes);

                Write(bytes);
            }
        }

        public override void Write(Single value)
        {
            var bytes = BitConverter.GetBytes(value);
            WriteBigEndian(bytes);
        }

        public override void Write(Double value)
        {
            var bytes = BitConverter.GetBytes(value);
            WriteBigEndian(bytes);
        }

        public override void Write(Int16 value)
        {
            var bytes = BitConverter.GetBytes(value);
            WriteBigEndian(bytes);
        }

        public override void Write(Int32 value)
        {
            var bytes = BitConverter.GetBytes(value);
            WriteBigEndian(bytes);
        }

        public override void Write(Int64 value)
        {
            var bytes = BitConverter.GetBytes(value);
            WriteBigEndian(bytes);
        }

        public override void Write(UInt16 value)
        {
            var bytes = BitConverter.GetBytes(value);
            WriteBigEndian(bytes);
        }

        public override void Write(UInt32 value)
        {
            var bytes = BitConverter.GetBytes(value);
            WriteBigEndian(bytes);
        }

        public override void Write(UInt64 value)
        {
            var bytes = BitConverter.GetBytes(value);
            WriteBigEndian(bytes);
        }

        private void WriteBigEndian(Byte[] bytes)
        {
            Contract.Requires(bytes != null);

            if (BitConverter.IsLittleEndian)
                Array.Reverse(bytes);

            Write(bytes);
        }
    }
}