using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;

namespace client
{
    class Program
    {
        static TcpClient client;
        static void Main(string[] args)
        {
            client = new TcpClient();

            //ipaddress of the Server
            client.Connect("127.0.0.1", 3460);
            while (client.Connected)
            {
            receivedevent:
                receive();
                goto sendevent;
            sendevent:
                send();
                goto receivedevent;
            }
        }

        static void send()
        {
            NetworkStream stream = client.GetStream();
            byte[] data = Encoding.ASCII.GetBytes(Console.ReadLine());
            stream.Write(data, 0, data.Length);
        }

        static byte[] buffer = new byte[4096];
        static void receive()
        {
            NetworkStream stream = client.GetStream();
            int data = stream.Read(buffer, 0, 4096);
            Console.WriteLine("server: " + Encoding.ASCII.GetString(buffer, 0, data));
        }
    }
}
