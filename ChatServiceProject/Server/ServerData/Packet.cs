using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
namespace ServerData
{
    [Serializable]
    public class Packet
    {
        public List<string> Gdata;
        public int packetInt;
        public bool packetBool;
        public string senderId;
        public PacketType packetType;

        public Packet(PacketType type, string _senderId)
        {
            Gdata = new List<string>();
            senderId = _senderId;
            packetType = type;
        }
        public Packet(byte[] packetBytes)
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            MemoryStream memoryStream = new MemoryStream(packetBytes);

            Packet packet = (Packet)binaryFormatter.Deserialize(memoryStream);
            memoryStream.Close();
            this.Gdata = packet.Gdata;
            this.packetInt = packet.packetInt;
            this.packetBool = packet.packetBool;
            this.senderId = packet.senderId;
            this.packetType = packet.packetType;
        }
        public byte[] ToBytes()
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            MemoryStream memoryStream = new MemoryStream();
            binaryFormatter.Serialize(memoryStream, this);
            byte[] bytes = memoryStream.ToArray();
            memoryStream.Close();
            return bytes;
        }

        public static string GetIpAddress()
        {
            // Try to find the ip of internetwork. If the ip doesn't have, define default ip "127.0.0.1".
            IPAddress[] ips = Dns.GetHostAddresses(Dns.GetHostName());
            foreach (IPAddress item in ips)
            {
                if (item.AddressFamily == AddressFamily.InterNetwork)
                    return item.ToString();
            }
            return "127.0.0.1";
        }
    }
    public enum PacketType
    {

    }
}
