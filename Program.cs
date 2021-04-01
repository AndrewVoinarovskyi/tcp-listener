using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace tcp_listener
{

    class MyTcpListener
    {
        public static void Main(string[] args)
        {
            TcpListener server=null;
            try
            {
                // Set the TcpListener on port 13000.
                IPAddress localAddr = IPAddress.Parse(args[0]);

                Int32 port = Convert.ToInt32(args[1]);


                // TcpListener server = new TcpListener(port);
                server = new TcpListener(localAddr, port);

                // Start listening for client requests.
                server.Start();

                // String data = null;

                // Enter the listening loop.
                while(true)
                {
                    Console.Write("Waiting for a connection... ");
                    TcpClient client1 = server.AcceptTcpClient();
                    Console.WriteLine("Connected!");
                    NetworkStream stream1 = client1.GetStream();
                    // StreamReader reader1 = new StreamReader(stream1);
                    // StreamWriter writer1 = new StreamWriter(stream1);

                    Console.Write("Waiting for a connection... ");
                    TcpClient client2 = server.AcceptTcpClient();
                    Console.WriteLine("Connected!");
                    NetworkStream stream2 = client2.GetStream();
                    // StreamReader reader2 = new StreamReader(stream2);
                    // StreamWriter writer2 = new StreamWriter(stream2);

                    TicTac tictac = new TicTac();
                    tictac.Play(stream1, stream2);

                    // data = null;

                    ///////////// Get a stream object for reading and writing
                    // NetworkStream stream1 = client1.GetStream();
                    // StreamReader reader1 = new StreamReader(stream1);
                    // StreamWriter writer1 = new StreamWriter(stream1);
                    
                    // NetworkStream stream2 = client2.GetStream();
                    // StreamReader reader2 = new StreamReader(stream2);
                    // StreamWriter writer2 = new StreamWriter(stream2);
                    // writer.AutoFlush = true;

                    // Loop to receive all the data sent by the client.
                    // while ((data = reader.ReadLine()) != null)
                    // {

                    //     Console.WriteLine("Received: {0}", data);
                    //     // Process the data sent by the client.
                    //     String response = data.ToUpper();

                    //     // Send back a response.
                    //     writer.WriteLine(response);
                    //     writer.Flush();
                    //     Console.WriteLine("Sent: {0}", response);
                    // }

                    // Shutdown and end connection
                    client1.Close();
                }
            }
            catch(SocketException e)
            {
            Console.WriteLine("SocketException: {0}", e);
            }
            finally
            {
            // Stop listening for new clients.
            server.Stop();
            }

            Console.WriteLine("\nHit enter to continue...");
            Console.Read();
        }
    }
}