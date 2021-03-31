using System;

namespace tiktak
{
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

        void DrawGameField()
        { 
            Console.WriteLine($"{field[0, 0]}{VERT}{field[0, 1]}{VERT}{field[0, 2]}");
            Console.WriteLine(HORIZ);
            Console.WriteLine($"{field[1, 0]}{VERT}{field[1, 1]}{VERT}{field[1, 2]}");
            Console.WriteLine(HORIZ);
            Console.WriteLine($"{field[2, 0]}{VERT}{field[2, 1]}{VERT}{field[2, 2]}");
        }
        bool MakeStep()
        {
            player = (turn % 2) + 1;
            flag = true;
            do
            {
                try
                {   
                    Console.WriteLine($"Player {player} change line (1-3):");
                    first = Console.ReadLine();
                    line = Convert.ToInt32(first) - 1;
                    Console.WriteLine($"Player {player} change column (1-3):");
                    second = Console.ReadLine();
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
                                Console.WriteLine("Changed row is not empty. Choose another one.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Error. Input 1, 2 or 3.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Error. Input only ONE number to choose line or column");
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Error. Input correct data.");
                }
            }
            while(flag);
            
            return DetermineWinner();
        }
        bool DetermineWinner()
        {
            isGameContinue = (EMPTY != field[0,0]) && (field[0,0] == field[0,1]) && (field[0,1] == field[0,2]) ||
                (EMPTY != field[1,0]) && (field[1,0] == field[1,1]) && (field[1,1] == field[1,2]) ||
                (EMPTY != field[2,0]) && (field[2,0] == field[2,1]) && (field[2,1] == field[2,2]) ||
                (EMPTY != field[0,0]) && (field[0,0] == field[1,0]) && (field[1,0] == field[2,0]) ||
                (EMPTY != field[0,1]) && (field[0,1] == field[1,1]) && (field[1,1] == field[2,1]) ||
                (EMPTY != field[0,2]) && (field[0,2] == field[1,2]) && (field[1,2] == field[2,2]) ||
                (EMPTY != field[0,0]) && (field[0,0] == field[1,1]) && (field[1,1] == field[2,2]) ||
                (EMPTY != field[0,2]) && (field[0,2] == field[1,1]) && (field[1,1] == field[2,0]);

            DrawGameField();
            if (isGameContinue)
                Console.WriteLine($"Player {player} is winner!");
            else if (turn == 8)
            {
                isGameContinue = true;
                Console.WriteLine("Draw!");
            }
            return isGameContinue;
        }

        public void Play()
        {
            field = new char[3, 3];

            for(var i=0; i<3; i++)
            {
                for(int j=0; j<3; j++)
                {
                    field[i,j] = EMPTY;
                }
            }

            DrawGameField();
            
            while (!isFinished)
            {
                isFinished = MakeStep();
                turn += 1;
            }
        }
    }
}
