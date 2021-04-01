﻿using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace tcp_listener
{
    // class Program
    // {       
    //     static void Main(string[] args)
    //     {
    //         TicTac tictac = new TicTac();
            
    //         tictac.Play();

    //     }
    // }
    class TicTac
    {
        const char EMPTY = ' ';
        const string HORIZ = "-+-+-";
        const string VERT = "|";
        const char X = 'X';
        const char O = 'O';

        string first, second;
        int line;
        int column;
        int player;
        bool flag;
        bool isFinished = false;
        char[,] field;
        int turn = 0;
        bool isGameContinue = false;
        StreamReader reader1;
        StreamReader reader2;
        StreamWriter writer1;
        StreamWriter writer2;
        private bool step;

        void DrawGameField(StreamWriter writer)
        { 
            writer.WriteLine($"{field[0, 0]}{VERT}{field[0, 1]}{VERT}{field[0, 2]}");
            writer.WriteLine(HORIZ);
            writer.WriteLine($"{field[1, 0]}{VERT}{field[1, 1]}{VERT}{field[1, 2]}");
            writer.WriteLine(HORIZ);
            writer.WriteLine($"{field[2, 0]}{VERT}{field[2, 1]}{VERT}{field[2, 2]}");
        }
        bool MakeStep(StreamReader reader, StreamWriter writer)
        {
            
            flag = true;
            do
            {
                try
                {
                    reader.DiscardBufferedData();
                    string chooseLine = $"Player {player} choose line (1-3):";
                    writer.WriteLine(chooseLine);
                    first = reader.ReadLine();
                    line = Convert.ToInt32(first) - 1;

                    string chooseColumn = $"Player {player} choose column (1-3):";
                    writer.WriteLine(chooseColumn);
                    second = reader.ReadLine();
                    column = Convert.ToInt32(second) - 1;

                    if ((first.Length ==  1) && (second.Length == 1))
                    {
                        if ((line >= 0 && line < 3) && (column >= 0 && column <3))
                        {
                            if (field[line,column] == EMPTY)
                            {
                                if (player == 1)
                                {
                                    field[line,column] = X;
                                }
                                else 
                                {
                                    field[line,column] = O;
                                }

                                flag = false;
                                
                            }
                            else
                            {
                                writer.WriteLine("Changed row is not empty. Choose another one.");
                            }
                        }
                        else
                        {
                            writer.WriteLine("Error. Input 1, 2 or 3.");
                        }
                    }
                    else
                    {
                        writer.WriteLine("Error. Input only ONE number to choose line or column");
                    }
                }
                catch (FormatException)
                {
                    writer.WriteLine("Error. Input correct data.");
                }
            }
            while(flag);
            
            return DetermineWinner();
        }
        bool DetermineWinner()
        {
            isGameContinue = 
                (EMPTY != field[0,0]) && (field[0,0] == field[0,1]) && (field[0,1] == field[0,2]) ||
                (EMPTY != field[1,0]) && (field[1,0] == field[1,1]) && (field[1,1] == field[1,2]) ||
                (EMPTY != field[2,0]) && (field[2,0] == field[2,1]) && (field[2,1] == field[2,2]) ||
                (EMPTY != field[0,0]) && (field[0,0] == field[1,0]) && (field[1,0] == field[2,0]) ||
                (EMPTY != field[0,1]) && (field[0,1] == field[1,1]) && (field[1,1] == field[2,1]) ||
                (EMPTY != field[0,2]) && (field[0,2] == field[1,2]) && (field[1,2] == field[2,2]) ||
                (EMPTY != field[0,0]) && (field[0,0] == field[1,1]) && (field[1,1] == field[2,2]) ||
                (EMPTY != field[0,2]) && (field[0,2] == field[1,1]) && (field[1,1] == field[2,0]);

            DrawGameField(writer1);
            DrawGameField(writer2);
            if (isGameContinue)
            {
                Console.WriteLine($"Player {player} is winner!");
                writer1.WriteLine($"Player {player} is winner!");
                writer2.WriteLine($"Player {player} is winner!");
            }
            else if (turn == 8)
            {
                isGameContinue = true;
                Console.WriteLine("Draw!");
                writer1.WriteLine("Draw!");
                writer2.WriteLine("Draw!");
            }
            return isGameContinue;
        }

        public void Play(NetworkStream stream1, NetworkStream stream2)
        {

            reader1 = new StreamReader(stream1);
            reader2 = new StreamReader(stream2);

            writer1 = new StreamWriter(stream1);
            writer1.AutoFlush = true;
            writer2 = new StreamWriter(stream2);
            writer2.AutoFlush = true;



            field = new char[3, 3];

            for(var i=0; i<3; i++)
            {
                for(int j=0; j<3; j++)
                {
                    field[i,j] = EMPTY;
                }
            }

            DrawGameField(writer1);
            DrawGameField(writer2);
            writer1.WriteLine("Press Enter to start the game.");
            
            while (!isFinished)
            {


                player = (turn % 2) + 1;
                if (player == 1)
                {
                    while(stream1.Length >= 0)
                    {
                        // Char[] buffer = new Char[(int)reader1.BaseStream.Length];
                        // Span<char> c = new Span<char>();
                        reader1.ReadLine();
                        // c.Clear();
                        // buffer = null;
                    }
                    step = MakeStep(reader1, writer1);
                }
                else
                {
                    while(stream2.Length >= 0)
                    {
                        // Char[] buffer = new Char[(int)reader2.BaseStream.Length];
                        // Span<char> c = new Span<char>();
                        reader2.ReadLine();
                        // c.Clear();
                        // buffer = null;
                    }
                    step = MakeStep(reader2, writer2);
                }
                isFinished = step;
                turn += 1;
            }
        }
    }
}