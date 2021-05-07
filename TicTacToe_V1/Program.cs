using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace TicTacToe
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Tic Tac Toe Game");
            MainGame();
        }
        private static void MainGame()
        {
            string[,] arrayPosition = new string[3, 3];
            string[,] arrayGame = new string[3, 3];
            ShowDefaultSuntetagmenesTouUser(arrayPosition);
            DefaultActiveGame(arrayGame);
            Console.WriteLine("");
            StartGaming();
        }
        private static void DefaultActiveGame(string[,] arrayGame)
        {
            Console.WriteLine("\n\nActive Game");
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    arrayGame[i, j] = "-";
                }
            }
            for (int i = 0; i < 3; i++)
            {
                Console.Write("\n");
                for (int j = 0; j < 3; j++)
                {
                    Console.Write("{0}\t", arrayGame[i, j]);
                }
            }
        }
        public static void ShowDefaultSuntetagmenesTouUser(string[,] arrayPosition)
        {
            Console.WriteLine("Suntetagmenes gia ton xrhsth");
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    arrayPosition[i, j] = $"{i} {j}";
                }
            }
            for (int i = 0; i < 3; i++)
            {
                Console.Write("\n");
                for (int j = 0; j < 3; j++)
                {
                    Console.Write("{0}\t", arrayPosition[i, j]);
                }
            }
        }
        public static string XorO()
        {
            Console.WriteLine("Pick X or O");
            string pick = Console.ReadLine();
            while (!(pick == "X" || pick == "O"))
            {
                Console.WriteLine("Pick X or O");
                string pickNew = Console.ReadLine();
                pick = pickNew;
            }
            return pick;
        }
        public static string ComputerPick(string pick)
        {
            return pick == "X" ? "O" : "X";
        }
        public static string GetUserMove(string[,] array)
        {
            Console.WriteLine("What is your move?");
            string userMove = Console.ReadLine();
            var a = AvailableMoves(array).Contains(userMove);
            while (!ValidMoves().OfType<string>().ToList().Contains(userMove) || !AvailableMoves(array).Contains(userMove))
            {
                Console.WriteLine("What is your move?");
                string userMoveNew = Console.ReadLine();
                userMove = userMoveNew;
            }
            return userMove;
        }
        public static string ComputerMove(string[,] array)
        {
            Random random = new Random();
            var computerMove = $"{random.Next(0, 3)} {random.Next(0, 3)}";
            while (!AvailableMoves(array).Contains(computerMove))
            {
                var computerMoveNew = $"{random.Next(0, 3)} {random.Next(0, 3)}";
                computerMove = computerMoveNew;
            }
            return computerMove;
        }
        public static void StartGaming()
        {
            var winner = "";
            var userPick = XorO();
            var userMove = GetUserMove(DefaultGame());
            Console.WriteLine("\nYour Move Coming Up");
            Thread.Sleep(1000);
            var array = StartRound(userMove, userPick);
            RoundDisplay(array);
            var computerPick = ComputerPick(userPick);
            do
            {
                Console.WriteLine("\nComputer Move Coming Up");
                Thread.Sleep(2000);
                var computerMove = ComputerMove(array);
                var arrayNext = NextRound(computerMove, computerPick, array);
                Console.WriteLine("");
                RoundDisplay(arrayNext);
                if (Winner(arrayNext))
                {
                    winner = "Computer";
                    break;
                }
                var userMoveNew = GetUserMove(arrayNext);
                Console.WriteLine("\nYour Move Coming Up");
                Thread.Sleep(2000);
                var arrayNext1 = NextRound(userMoveNew, userPick, arrayNext);
                Console.WriteLine("");
                RoundDisplay(arrayNext1);
                if (Winner(arrayNext))
                {
                    winner = "User";
                    break;
                }
                array = arrayNext1;
            } while (AvailableMoves(array).Count != 0);
            // na balw ta available moves
            //RoundDisplay(array);
            //return array;
            // var a = AvailableMoves(array1);
            Console.WriteLine(ShowWinner(winner));
        }
        public static string ShowWinner(string winner)
        {
            if (winner == "Computer")
            {
                return "Computer Won!";
            }
            if (winner == "User")
            {
                return "You Won!";
            }
            return "Nobody won!";
        }
        public static void RoundDisplay(string[,] arrayGame)
        {
            for (int i = 0; i < 3; i++)
            {
                Console.Write("\n");
                for (int j = 0; j < 3; j++)
                {
                    Console.Write("{0}\t", arrayGame[i, j]);
                }
            }
        }
        public static string[,] StartRound(string move, string pick)
        {
            // var a2 = Convert.ToInt32(move.First().ToString()); //H metablhth move einai string kai pairnw thn prwth timh tou string opou einai ari8mos
            var arrayGame = DefaultGame();
            arrayGame[Convert.ToInt32(move.First().ToString()), Convert.ToInt32(move.Last().ToString())] = pick;
            return arrayGame;
        }
        public static string[,] NextRound(string move, string pick, string[,] array)
        {
            array[Convert.ToInt32(move.First().ToString()), Convert.ToInt32(move.Last().ToString())] = pick;
            return array;
        }
        public static string[,] DefaultGame()
        {
            string[,] arrayGame = new string[3, 3];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    arrayGame[i, j] = "-";
                }
            }
            return arrayGame;
        }
        public static string[,] ValidMoves()
        {
            string[,] arrayPosition = new string[3, 3];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    arrayPosition[i, j] = $"{i} {j}";
                }
            }
            return arrayPosition;
        }
        //mallon dn xreiazetai
        public static bool EmptyCell(string[,] array)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (array[i, j] == "-" || array[i, j] == "X" || array[i, j] == "O")
                        return true;
                }
            }
            return false;
        }
        public static List<string> AvailableMoves(string[,] array)
        {
            List<string> availableMovesToList = new List<string>();
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (array[i, j] != "X" && array[i, j] != "O")
                        availableMovesToList.Add($"{i} {j}");
                }
            }
            return availableMovesToList;
        }
        public static bool Winner(string[,] array)
        {
            if ((array[0, 0].Equals("X") && array[0, 1].Equals("X") && array[0, 2].Equals("X")) || (array[0, 0].Equals("O") && array[0, 1].Equals("O") && array[0, 2].Equals("O")))
                return true;
            if ((array[1, 0].Equals("X") && array[1, 1].Equals("X") && array[1, 2].Equals("X")) || (array[1, 0].Equals("O") && array[1, 1].Equals("O") && array[1, 2].Equals("O")))
                return true;
            if ((array[2, 0].Equals("X") && array[2, 1].Equals("X") && array[2, 2].Equals("X")) || (array[2, 0].Equals("O") && array[2, 1].Equals("O") && array[2, 2].Equals("O")))
                return true;
            if ((array[0, 0].Equals("X") && array[1, 0].Equals("X") && array[2, 0].Equals("X")) || (array[0, 0].Equals("O") && array[1, 0].Equals("O") && array[2, 0].Equals("O")))
                return true;
            if ((array[0, 1].Equals("X") && array[1, 1].Equals("X") && array[2, 1].Equals("X")) || (array[0, 1].Equals("O") && array[1, 1].Equals("O") && array[2, 1].Equals("O")))
                return true;
            if ((array[0, 2].Equals("X") && array[1, 2].Equals("X") && array[2, 2].Equals("X")) || (array[0, 2].Equals("O") && array[1, 2].Equals("O") && array[2, 2].Equals("O")))
                return true;
            if ((array[0, 0].Equals("X") && array[1, 1].Equals("X") && array[2, 2].Equals("X")) || (array[0, 0].Equals("O") && array[1, 1].Equals("O") && array[2, 2].Equals("O")))
                return true;
            if ((array[2, 0].Equals("X") && array[1, 1].Equals("X") && array[0, 2].Equals("X")) || (array[2, 0].Equals("O") && array[1, 1].Equals("O") && array[0, 2].Equals("O")))
                return true;
            return false;
        }
    }
}