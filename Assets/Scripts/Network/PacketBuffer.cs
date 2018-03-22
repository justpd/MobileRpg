using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class PacketBuffer : IDisposable
{
    List<byte> bufferList;
    byte[] readBuffer;
    int readPosition;
    bool bufferupdate = false;

    public PacketBuffer()
    {
        bufferList = new List<byte>();
        readPosition = 0;
    }

    public int GetReadPosition()
    {
        return readPosition;
    }

    public byte[] ToArray()
    {
        return bufferList.ToArray();
    }

    public int Count()
    {
        return bufferList.Count;
    }

    public int Lenght()
    {
        return Count() - readPosition;
    }

    public void Clear()
    {
        bufferList.Clear();
        readPosition = 0;
    }

    // Write Data
    public void WriteBytes(byte[] input)
    {
        bufferList.AddRange(input);
        bufferupdate = true;
    }
    public void WriteByte(byte input)
    {
        bufferList.Add(input);
        bufferupdate = true;
    }
    public void WriteInt(int input)
    {
        bufferList.AddRange(BitConverter.GetBytes(input));
        bufferupdate = true;
    }
    public void WriteFloat(float input)
    {
        bufferList.AddRange(BitConverter.GetBytes(input));
        bufferupdate = true;
    }
    public void WriteString(string input)
    {
        bufferList.AddRange(BitConverter.GetBytes(input.Length));
        bufferList.AddRange(Encoding.UTF8.GetBytes(input));
        bufferupdate = true;
    }

    // Read Data
    public int ReadInteger(bool peek = true)
    {
        if (bufferList.Count > readPosition)
        {
            if (bufferupdate)
            {
                readBuffer = bufferList.ToArray();
                bufferupdate = false;
            }
            int value = BitConverter.ToInt32(readBuffer, readPosition);
            if (peek & bufferList.Count > readPosition)
            {
                readPosition += 4;
            }
            return value;
        }
        else
        {
            throw new Exception("Buffer is past its Limit");
        }
    }

    public float ReadFloat(bool peek = true)
    {
        if (bufferList.Count > readPosition)
        {
            if (bufferupdate)
            {
                readBuffer = bufferList.ToArray();
                bufferupdate = false;
            }

            float value = BitConverter.ToSingle(readBuffer, readPosition);
            if (peek & bufferList.Count > readPosition)
            {
                readPosition += 4;
            }
            return value;
        }
        else
        {
            throw new Exception("Buffer is past its Limit");
        }
    }
    public byte ReadByte(bool peek = true)
    {
        if (bufferList.Count > readPosition)
        {
            if (bufferupdate)
            {
                readBuffer = bufferList.ToArray();
                bufferupdate = false;
            }

            byte value = readBuffer[readPosition];
            if (peek & bufferList.Count > readPosition)
            {
                readPosition += 1;
            }
            return value;
        }
        else
        {
            throw new Exception("Buffer is past its Limit");
        }
    }
    public byte[] ReadBytes(int length, bool peek = true)
    {
        if (bufferupdate)
        {
            readBuffer = bufferList.ToArray();
            bufferupdate = false;
        }

        byte[] value = bufferList.GetRange(readPosition, length).ToArray();
        if (peek & bufferList.Count > readPosition)
        {
            readPosition += length;
        }
        return value;
    }
    public string ReadString(bool peek = true)
    {

        int length = ReadInteger(true);
        if (bufferupdate)
        {
            readBuffer = bufferList.ToArray();
            bufferupdate = false;
        }

        string value = Encoding.UTF8.GetString(readBuffer, readPosition, length);
        if (peek & bufferList.Count > readPosition)
        {
            readPosition += length;
        }
        return value;
    }

    private bool disposedValue = false;
    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                bufferList.Clear();
            }
            readPosition = 0;
        }
        disposedValue = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
