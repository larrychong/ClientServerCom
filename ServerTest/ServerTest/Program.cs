using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;

namespace ServerTest
{

    class Program
    {
        static TcpClient connected;
        static void Main(string[] args)
        {
            TcpListener listener = new TcpListener(IPAddress.Any, 3460);
            listener.Start();

            while (true)
            {
                connected = listener.AcceptTcpClient();
            sendevent:
                send();
                goto receiveevent;

            receiveevent:
                receive();
                goto sendevent;
            }
        }
        static byte[] buffer = new byte[4096];
        static void receive()
        {
            NetworkStream read = connected.GetStream();
            int data = read.Read(buffer, 0, 4096);
            Console.WriteLine("Client: " + Encoding.ASCII.GetString(buffer, 0, data));
        }
        static void send()
        {
            byte[] data = Encoding.ASCII.GetBytes(Console.ReadLine());//
            NetworkStream stream = connected.GetStream();
            stream.Write(data, 0, data.Length);
        }
    }
}
