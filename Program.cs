using System;
using static System.Console;

namespace NumberGuess2
{
    class Program
    {
        static void Main(string[] args)
        {
            int maxNumber = 0;
            int theLowerBound = 1;
            int theUpperBound = 0;
            int myGuess = 0;
            bool endGame = false;
            int numberOfGuesses = 1;
            int secretNumber = 0;
            int playerGuess = 0;

            maxNumber = GetMaxNumber();
            theUpperBound = maxNumber;
            myGuess = (theLowerBound + theUpperBound) / 2;

            Write("Now that we've agreed upon a max number, who shall go first?" +
                "\nPress 1 if you want to go first. ");
            if (ReadLine() == "1")
            {
                PlayerGuess(maxNumber, ref endGame,
                    ref numberOfGuesses, out secretNumber, out playerGuess);

                // Resets for next game
                endGame = false;
                numberOfGuesses = 1;
                WriteLine("Good job! Now it's my turn.");

                ComputerGuess(maxNumber, ref theLowerBound, ref theUpperBound,
                ref myGuess, ref endGame, ref numberOfGuesses);
            }
            else
            {
                ComputerGuess(maxNumber, ref theLowerBound, ref theUpperBound,
                ref myGuess, ref endGame, ref numberOfGuesses);

                // Resets for next game
                endGame = false;
                numberOfGuesses = 1;
                WriteLine("Awesome! Now it's your turn.");

                PlayerGuess(maxNumber, ref endGame,
                    ref numberOfGuesses, out secretNumber, out playerGuess);
            }

            WriteLine("\nWell, that was fun. Thank you for playing!");
        }

        // The player guesses the computer's number
        private static void PlayerGuess(int maxNumber, ref bool endGame,
            ref int numberOfGuesses, out int secretNumber, out int playerGuess)
        {
            WriteLine("\nI will now pick a number between 1 and {0}." +
                " Try to guess what it is, and I will tell you if it's high, low, or perfect.", (maxNumber - 1));

            // Randomly generates secret number for player to guess
            Random numberPicker = new Random();
            secretNumber = numberPicker.Next(maxNumber - 1) + 1;

            // Gameplay loop
            do
            {
                Write("Make your guess: ");
                playerGuess = int.Parse(ReadLine());

                if (playerGuess == secretNumber)
                {
                    WriteLine("You got the number!");
                    if (numberOfGuesses < 2)
                    {
                        // Only to be displayed if it takes one guess.
                        WriteLine("And you did it in {0} guess!", numberOfGuesses);
                    }
                    else
                    {
                        WriteLine("It took you only {0} guesses.", numberOfGuesses);
                    }
                    endGame = true;
                }
                else
                {
                    if (playerGuess > secretNumber)
                    {
                        WriteLine("Your guess is too high.");
                    }
                    else
                    {
                        WriteLine("Your guess is too low.");
                    }
                    numberOfGuesses++;
                }

            } while (!endGame);
        }

        // The computer opponent guesses the player's number
        private static void ComputerGuess(int maxNumber, ref int theLowerBound,
            ref int theUpperBound, ref int myGuess, ref bool endGame, ref int numberOfGuesses)
        {
            WriteLine("\nPlease pick a number between 1 and {0}, and I will try to guess what it is.\n" +
                            "Just type \"Y\" for yes, and \"N\" for no.\n" +
                            "If the number is higher, type \"H,\" and \"L\" for lower.\n\n", (maxNumber - 1));

            // Gameplay loop
            do
            {
                Write("Is your number {0}? ", myGuess);

                if (ReadLine() == "Y")
                {
                    WriteLine("I got your number!");
                    if (numberOfGuesses < 2)
                    {
                        // Only to be displayed if it takes one guess.
                        WriteLine("And I did it in {0} guess!", numberOfGuesses);
                    }
                    else
                    {
                        WriteLine("It only took me {0} guesses.", numberOfGuesses);
                    }
                    endGame = true;
                }
                else
                {
                    Write("Is the number (H)igher or (L)ower? ");
                    if (ReadLine() == "H")
                    {
                        theLowerBound = myGuess;
                    }
                    else
                    {
                        theUpperBound = myGuess;
                    }
                    myGuess = (theLowerBound + theUpperBound) / 2;
                    numberOfGuesses++;
                }
            } while (!endGame);
        }

        // Prompts the player to enter in a max number for the game.
        private static int GetMaxNumber()
        {
            string input = "";
            int max = 0;
            while (max < 1)
            {
                Write("What is the maximum number? ");
                input = ReadLine();
                if (!int.TryParse(input, out max))
                {
                    WriteLine("Sorry, but \"{0}\" is not a valid number. Please try again.", input);
                }
                else if (input == "0")
                {
                    WriteLine("Sorry, but you cannot pick 0 as a maximum number. Please try again.");
                }
            }
            return max + 1;
            
        }
    }
}