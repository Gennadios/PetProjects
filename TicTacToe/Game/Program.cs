using System;

namespace Game
{
    sealed class Program
    {
        static char[,] gameField = new char[,]
            {
                { '1', '2', '3' },
                { '4', '5', '6' },
                { '7', '8', '9' }
            };
 
        static byte availableTurns = 9;
        static char p1Mark = 'X';
        static char p2Mark = 'O';

        static bool p1Turn = true;
        static bool isDraw = false;
        static bool p1Wins = false;
        static bool p2Wins = false;

        static string p1TurnMessage = "Player 1 turn! Choose your field number.";
        static string p2TurnMessage = "Player 2 turn! Choose your field number.";
        static string p1WinMessage = "PLAYER 1 WINS!!!";
        static string p2WinMessage = "PLAYER 2 WINS!!!";
        static string drawMessage = "It's a DRAW!";
        static string resetMessage = "Press any key to reset the game.";

        private static void DrawField()
        {
            Console.Clear();
            for (int i = 0; i < gameField.GetLength(0); i++)
            {
                Console.Write("     |     |     |\n");
                for (int j = 0; j < gameField.GetLength(0); j++)
                {
                    if(gameField[i, j].Equals('X'))
                        Console.ForegroundColor = ConsoleColor.Red;
                    else if(gameField[i, j].Equals('O'))
                        Console.ForegroundColor = ConsoleColor.Green;

                    Console.Write($"  {gameField[i, j]}  ");
                    Console.ResetColor();
                    Console.Write("|");
                }
                Console.Write("\n_____|_____|_____|\n");
            }
            Console.WriteLine();
        }

        private static void DisplayTurnInfo()
        {
            if(p1Turn)
                Console.WriteLine(p1TurnMessage);
            else
                Console.WriteLine(p2TurnMessage);
        }

        private static void TakeTurn()
        {
            char currentMark;
            if (p1Turn)
                currentMark = p1Mark;
            else
                currentMark = p2Mark;   

            char fieldNumber = ValidCharInput();

            switch (fieldNumber)
            {
                case '1':
                    gameField[0, 0] = currentMark; break;
                case '2':
                    gameField[0, 1] = currentMark; break;
                case '3':
                    gameField[0, 2] = currentMark; break;
                case '4':
                    gameField[1, 0] = currentMark; break;
                case '5':
                    gameField[1, 1] = currentMark; break;
                case '6':
                    gameField[1, 2] = currentMark; break;
                case '7':
                    gameField[2, 0] = currentMark; break;
                case '8':
                    gameField[2, 1] = currentMark; break;
                case '9':
                    gameField[2, 2] = currentMark; break;
                default:
                    break;
            }

            availableTurns--;
        }

        static void SwitchPlayers()
        {
            if (p1Turn)
                p1Turn = false;
            else
                p1Turn = true;
        }

        static void CheckForDraw()
        {
            if (availableTurns == 0 && !p1Wins && !p2Wins)
            {
                isDraw = true;
                Console.WriteLine(drawMessage);
            }
        }

        static void CheckForWinner()
        {
            char currentMark;
            if (p1Turn)
                currentMark = p1Mark;
            else
                currentMark = p2Mark;

            bool currentPlayerIsWinner = ThereIsAWinner(currentMark);
            if (currentPlayerIsWinner && p1Turn)
            {
                p1Wins = true;
                Console.WriteLine(p1WinMessage);
            }
            if (currentPlayerIsWinner && !p1Turn)
            {
                p2Wins = true;
                Console.WriteLine(p2WinMessage);
            }
        }    

        static void ResetGame()
        {
            if(p1Wins || p2Wins || isDraw)
            {
                Console.WriteLine(resetMessage);
                Console.ReadKey();

                gameField[0, 0] = '1';
                gameField[0, 1] = '2';
                gameField[0, 2] = '3';
                gameField[1, 0] = '4';
                gameField[1, 1] = '5';
                gameField[1, 2] = '6';
                gameField[2, 0] = '7';
                gameField[2, 1] = '8';
                gameField[2, 2] = '9';

                p1Turn = true;
                p1Wins = false;
                p2Wins = false;
                isDraw = false;
                availableTurns = 9;
            }
        }

        static bool ThereIsAWinner(char playerMark)
        {
            char cell1 = gameField[0, 0];
            char cell2 = gameField[0, 1];
            char cell3 = gameField[0, 2];
            char cell4 = gameField[1, 0];
            char cell5 = gameField[1, 1];
            char cell6 = gameField[1, 2];
            char cell7 = gameField[2, 0];
            char cell8 = gameField[2, 1];
            char cell9 = gameField[2, 2];

            bool winCon1 = cell1 == playerMark && cell1 == cell2 && cell2 == cell3;
            bool winCon2 = cell4 == playerMark && cell4 == cell5 && cell5 == cell6;
            bool winCon3 = cell7 == playerMark && cell7 == cell8 && cell8 == cell9;
            bool winCon4 = cell1 == playerMark && cell1 == cell4 && cell4 == cell7;
            bool winCon5 = cell2 == playerMark && cell2 == cell5 && cell5 == cell8;
            bool winCon6 = cell3 == playerMark && cell3 == cell6 && cell6 == cell9;
            bool winCon7 = cell1 == playerMark && cell1 == cell5 && cell5 == cell9;
            bool winCon8 = cell3 == playerMark && cell3 == cell5 && cell5 == cell7;

            if (winCon1 || winCon2 || winCon3 || winCon4 || winCon5 || winCon6 || winCon7 || winCon8)
                return true;
            return false;
        }

        static char ValidCharInput()
        {
            string input;
            char fieldNumber;

            while (true)
            {
                input = Console.ReadLine();
                bool inputIsValid = char.TryParse(input, out fieldNumber);
                
                if (inputIsValid && FieldNotTaken(fieldNumber))
                    break;

                Console.WriteLine("Sorry, input a number from 1 to 9. Also the field should not be occupied!");
            }
            return fieldNumber;
        }

        static bool FieldNotTaken(char c)
        {
            char cell = default;

            switch(c) 
            {
                case '1':
                    cell = gameField[0, 0]; break;
                case '2':
                    cell = gameField[0, 1]; break;
                case '3':
                    cell = gameField[0, 2]; break;
                case '4':
                    cell = gameField[1, 0]; break;
                case '5':
                    cell = gameField[1, 1]; break;
                case '6':
                    cell = gameField[1, 2]; break;
                case '7':
                    cell = gameField[2, 0]; break;
                case '8':
                    cell = gameField[2, 1]; break;
                case '9':
                    cell = gameField[2, 2]; break;
                default:
                    break;
            }

            if (c == cell)
                return true;
            return false;
        }

        static void Main(string[] args)
        {
            while (true)
            {
                DrawField();
                DisplayTurnInfo();
                TakeTurn();
                DrawField();
                CheckForWinner();
                CheckForDraw();
                SwitchPlayers();
                ResetGame();
            }
        }
    }
}
