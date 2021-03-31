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

                Int32 port = Int32.Parse(args[1]);


                // TcpListener server = new TcpListener(port);
                server = new TcpListener(localAddr, port);

                // Start listening for client requests.
                server.Start();

                String data = null;

                // Enter the listening loop.
                while(true)
                {
                    Console.Write("Waiting for a connection... ");

                    // Perform a blocking call to accept requests.
                    // You could also use server.AcceptSocket() here.
                    TcpClient client1 = server.AcceptTcpClient();
                    Console.WriteLine("Connected!");

                    Console.Write("Waiting for a connection... ");

                    TcpClient client2 = server.AcceptTcpClient();
                    Console.WriteLine("Connected!");

                    TicTac tictac = new TicTac();
                    tictac.Play();
                    
                    data = null;

                    ///////////// Get a stream object for reading and writing
                    // NetworkStream stream = client.GetStream();
                    // StreamReader reader = new StreamReader(stream);
                    // StreamWriter writer = new StreamWriter(stream);
                    // writer.AutoFlush = true;

                    // Loop to receive all the data sent by the client.
                    // while ((data = reader.ReadLine()) != null)
                    // {

                    //     Console.WriteLine("Received: {0}", data);
                    //     // Process the data sent by the client.
                    //     String response = data.ToUpper();

                    //     byte[] msg = System.Text.Encoding.ASCII.GetBytes(data);

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