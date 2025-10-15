
namespace PotsAndPans
{

    class Program
    {
        static readonly Random random = new Random();
        static void Main()
        {
            Console.Title = "Pots and Pans";
            string answer = GenerateNumber();
            int tries = 0;
            Console.WriteLine("  ____       _                         _   ____                 \r\n |  _ \\ ___ | |_ ___    __ _ _ __   __| | |  _ \\ __ _ _ __  ___ \r\n | |_) / _ \\| __/ __|  / _` | '_ \\ / _` | | |_) / _` | '_ \\/ __|\r\n |  __/ (_) | |_\\__ \\ | (_| | | | | (_| | |  __/ (_| | | | \\__ \\\r\n |_|   \\___/ \\__|___/  \\__,_|_| |_|\\__,_| |_|   \\__,_|_| |_|___/\r\n                                                                ");
            Console.WriteLine("-By Quinn" + Environment.NewLine);
            Console.WriteLine("Welcome to POTS AND PANS!\r\n\r\nThe computer has generated a secret 4-digit number. \r\nEach digit is unique — no repeats.\r\n\r\nYour goal: guess the number in as few tries as possible.\r\n\r\nAfter each guess, you’ll see feedback:\r\n- A “pot” means a correct digit in the correct position.\r\n- A “pan” means a correct digit but in the wrong position.\r\n\r\nExample:\r\nIf the secret number is 1234 and you guess 1325,\r\nyou’ll get: 2 pots, 1 pan.\r\n\r\nType “quit” anytime to exit the game.\r\n\r\nGood luck. Try not to embarrass yourself.\r\n");
            while (true)
            {
                tries++;
                string guess = ValidateGuess();
                if (guess == "quit")
                {
                    Console.WriteLine($"The correct number was {answer}");
                    break;
                }
                int pots = Pots(guess, answer);
                int pans = Pans(guess, answer);

                if (pots != 4)
                    Console.WriteLine($"{pots}{(pots == 1 ? "pot" : "pots")}, {pans}{(pans == 1 ? "pan" : "pans")}");
                else
                {
                    Console.WriteLine($"4 pots!, You guess it in {tries} tries!");
                    break;
                }
            }
            string ans;
            while (true)
            {
                ans = Input("Try again? y/n").ToLower().Trim();
                if ((ans = YN(ans)) != "X")
                    break;
            }
            if (ans == "y")
            {
                Console.Clear();
                Main();
            }
            else
            {
                Environment.Exit(0);
            }
            Console.ReadLine();
        }

        static string YN(string ans)
        {
            string[] yes = ["yes", "y", "yeah", "yep", "yup", "sure", "ok", "okay", "alright", "fine", "affirmative", "certainly", "definitely", "absolutely", "indeed", "correct", "right", "true", "roger", "aye", "yessir", "yesss", "yah", "uh-huh", "mm-hmm", "totally", "for sure", "you bet", "of course", "gladly", "exactly", "tru", "sure thing", "no doubt", "yea", "yas", "ye", "yis", "okey", "okie", "okie-dokie", "aight", "bet", "ight", "copy", "heard", "gotcha", "confirmed", "10-4", "solid", "coo", "coool"];

            string[] no = ["no", "n", "nope", "nah", "nay", "negative", "never", "not", "no way", "no thanks", "not really", "not at all", "uh-uh", "mm-mm", "naw", "nahh", "no sir", "no ma’am", "by no means", "certainly not", "definitely not", "absolutely not", "no chance", "no shot", "no way jose", "over my dead body", "not happening", "no can do", "no deal", "forget it", "not today", "i think not", "no thank you", "no siree", "nah fam", "nah bro", "nuh-uh", "no man", "not possible", "not gonna happen", "fat chance", "zero", "denied", "refused", "rejected"];

            if (yes.Contains(ans))
            {
                return "y";
            }
            else if (no.Contains(ans))
            {
                return "n";
            }
            else
            {
                return "X";
            }
        }
        static int Pots(string guess, string answer)
        {
            int pots = 0;
            for (int i = 0; i < guess.Length; i++)
            {
                if (guess[i] == answer[i])
                    pots++;
            }
            return pots;
        }

        static int Pans(string guess, string answer)
        {
            int pans = 0;
            for (int i = 0; i < guess.Length; i++)
            {
                if (guess[i] != answer[i] && answer.Contains(guess[i]))
                    pans++;
            }
            return pans;
        }

        static string ValidateGuess()
        {
            bool invalid = false;
            string error = "";
            while (true)
            {
                if (invalid)
                    Console.WriteLine($"Invalid Guess: {error}");

                string? guess = Input("Enter guess:").Trim().ToLower();
                if (guess == "quit")
                {
                    return guess;
                }
                if (ValidGuess(guess, out error))
                {
                    return guess;
                }

                invalid = true;
            }
        }

        static bool ValidGuess(string guess, out string error)
        {
            error = "";
            if (guess.All(c => char.IsDigit(c)))
            {
                if (guess.Length == 4)
                {
                    foreach (var c in guess)
                    {
                        if (guess.Distinct().Count() != 4)
                        {
                            error = "Contains Duplicate digits";
                            return false;
                        }

                    }
                    return true;
                }
                else
                    error = "Not a four digit number";
            }
            else
                error = "Not a number";
            return false;
        }
        static string GenerateNumber()
        {
            int[] digits = new int[4];
            for (int i = 0; i < digits.Length; i++)
            {
                int num = random.Next(10);
                while (digits.Contains(num))
                {
                    num = random.Next(10);
                }
                digits[i] = num;
            }
            int fourDigitNumber = 0;
            foreach (int number in digits)
            {
                fourDigitNumber = fourDigitNumber * 10 + number;
            }
            return fourDigitNumber.ToString();
        }
        static string? Input(string message)
        {
            Console.Write(message + ' ');
            string? userInput = Console.ReadLine();
            return userInput;
        }
    }
}