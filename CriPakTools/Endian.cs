using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace CriPakTools
{
    public class EndianReader : BinaryReader
    {
        private bool isLittleEndian;
        private byte[] buffer = new byte[8];

        public EndianReader(Stream input, Encoding encoding, bool isLittleEndian)
            : base(input, encoding)
        {
            this.isLittleEndian = isLittleEndian;
        }

        public EndianReader(Stream input, bool isLittleEndian)
            : this(input, Encoding.UTF8, isLittleEndian)
        {
        }

        public bool IsLittleEndian
        {
            get { return isLittleEndian; }
            set { isLittleEndian = value; }
        }


        public override double ReadDouble()
        {
            if (isLittleEndian)
                return base.ReadDouble();
            FillMyBuffer(8);
            return BitConverter.ToDouble(buffer.Take(8).Reverse().ToArray(), 0);
        }

        public override short ReadInt16()
        {
            if (isLittleEndian)
                return base.ReadInt16();
            FillMyBuffer(2);
            return BitConverter.ToInt16(buffer.Take(2).Reverse().ToArray(), 0);

        }

        public override int ReadInt32()
        {
            if (isLittleEndian)
                return base.ReadInt32();
            FillMyBuffer(4);
            return BitConverter.ToInt32(buffer.Take(4).Reverse().ToArray(), 0);

        }

        public override long ReadInt64()
        {
            if (isLittleEndian)
                return base.ReadInt64();
            FillMyBuffer(8);
            return BitConverter.ToInt64(buffer.Take(8).Reverse().ToArray(), 0);

        }

        public override float ReadSingle()
        {
            if (isLittleEndian)
                return base.ReadSingle();
            FillMyBuffer(4);
            return BitConverter.ToSingle(buffer.Take(4).Reverse().ToArray(), 0);
        }

        public override ushort ReadUInt16()
        {
            if (isLittleEndian)
                return base.ReadUInt16();
            FillMyBuffer(2);
            return BitConverter.ToUInt16(buffer.Take(2).Reverse().ToArray(), 0);
        }


        public override uint ReadUInt32()
        {
            if (isLittleEndian)
                return base.ReadUInt32();
            FillMyBuffer(4);
            return BitConverter.ToUInt32(buffer.Take(4).Reverse().ToArray(), 0);
        }

        public override ulong ReadUInt64()
        {
            if (isLittleEndian)
                return base.ReadUInt64();
            FillMyBuffer(8);
            return BitConverter.ToUInt64(buffer.Take(8).Reverse().ToArray(), 0);
        }

        private void FillMyBuffer(int numBytes)
        {
            int offset = 0;
            int num2 = 0;
            if (numBytes == 1)
            {
                num2 = BaseStream.ReadByte();
                if (num2 == -1)
                {
                    throw new EndOfStreamException("Attempted to read past the end of the stream.");
                }
                buffer[0] = (byte)num2;
            }
            else
            {
                do
                {
                    num2 = BaseStream.Read(buffer, offset, numBytes - offset);
                    if (num2 == 0)
                    {
                        throw new EndOfStreamException("Attempted to read past the end of the stream.");
                    }
                    offset += num2;
                }
                while (offset < numBytes);
            }
        }
    }

    public class EndianWriter : BinaryWriter
    {
        private bool isLittleEndian;

        public EndianWriter(Stream input, Encoding encoding, bool isLittleEndian)
            : base(input, encoding)
        {
            this.isLittleEndian = isLittleEndian;
        }

        public EndianWriter(Stream input, bool isLittleEndian)
            : this(input, Encoding.UTF8, isLittleEndian)
        {
        }

        public bool IsLittleEndian
        {
            get { return isLittleEndian; }
            set { isLittleEndian = value; }
        }

        public void Write<T>(T value)
        {
            dynamic input = value;
            byte[] someBytes = BitConverter.GetBytes(input);
            if (!isLittleEndian)
                someBytes = someBytes.Reverse().ToArray();

            base.Write(someBytes);
        }

        public void Write(FileEntry entry)
        {
            if (entry.ExtractSizeType == typeof(Byte))
            {
                Write((Byte)entry.ExtractSize);
            }
            else if (entry.ExtractSizeType == typeof(UInt16))
            {
                Write((UInt16)entry.ExtractSize);
            }
            else if (entry.ExtractSizeType == typeof(UInt32))
            {
                Write((UInt32)entry.ExtractSize);
            }
            else if (entry.ExtractSizeType == typeof(UInt64))
            {
                Write((UInt64)entry.ExtractSize);
            }
            else if (entry.ExtractSizeType == typeof(Single))
            {
                Write((Single)entry.ExtractSize);
            }
            else
            {
                throw new Exception("Not supported type!");
            }
        }
    }
}
