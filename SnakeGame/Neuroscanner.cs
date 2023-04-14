using System.IO.Ports;

namespace SnakeGame
{
    public class Neuroscanner
    {
        private byte generatedChecksum = 0;
    private byte checksum = 0;
    private int payloadLength = 0;
    private byte[] payloadData = new byte[64];
    private byte poorQuality = 0;
    private byte attention = 0;
    private byte meditation = 0;

    public byte GetAttention()
    {
        return this.attention;
    }

    public byte GetMeditation()
    {
        return this.meditation;
    }

    private byte ReadOneByte(SerialPort serialPort)
    {
        return (byte)serialPort.ReadByte();
    }

    public void Scan(SerialPort serialPort)
{
    while (serialPort.BytesToRead > 0)
    {
        if (ReadOneByte(serialPort) == 0xAA)
    {
        if (ReadOneByte(serialPort) == 0xAA)
        {
            payloadLength = ReadOneByte(serialPort);
            if (payloadLength > payloadData.Length || payloadLength <= 0) // Payload length can not be greater than payloadData.Length or less than 1
                return;

            generatedChecksum = 0;
            for (int i = 0; i < payloadLength; i++)
            {
                payloadData[i] = ReadOneByte(serialPort); // Read payload into memory
                generatedChecksum += payloadData[i];
            }

            checksum = ReadOneByte(serialPort); // Read checksum byte from stream
            generatedChecksum = (byte)(0xFF - generatedChecksum); // Take one's compliment of generated checksum

            if (checksum == generatedChecksum)
            {
                poorQuality = 200;

                for (int i = 0; i < payloadLength; i++) // Parse the payload
                {
                    if (i >= payloadData.Length) break; // Avoid index out of range

                    switch (payloadData[i])
                    {
                        case 2:
                            i++;
                            if (i >= payloadData.Length) break; // Avoid index out of range
                            poorQuality = payloadData[i];
                            break;
                        case 4:
                            i++;
                            if (i >= payloadData.Length) break; // Avoid index out of range
                            attention = payloadData[i];
                            break;
                        case 5:
                            i++;
                            if (i >= payloadData.Length) break; // Avoid index out of range
                            meditation = payloadData[i];
                            break;
                        case 0x80:
                            i = i + 3;
                            break;
                        case 0x83:
                            i = i + 25;
                            break;
                        default:
                            break;
                    }
                }
            }
            else
            {
                // ChecksumFalse
            }
        }
    }
    
    }
}
    }
}