using GrandCircusEntry.Utility;
using System;

namespace GrandCircusEntry.Workflows
{
    class NumberMatchingWorkflow : IWorkflow
    {
        private readonly IMathHelper _math;
        private readonly IValidation _validate;
        private readonly IConsoleHelper _console;

        public NumberMatchingWorkflow(IConsoleHelper console, IValidation validate, IMathHelper math)
        {
            // Dependency Injection through the DI container.
            _math = math;
            _validate = validate;
            _console = console;
        }

        public void Run()
        {
            while (true)
            {
                // First behavior of the workflow, which is just to write a message to the console.
                LogIntroMessage();

                // setup variables to help continue the flow
                bool isValid;

                // these will be the numbers provided by the user.
                int[] numbers;

                do
                {
                    // second main behavior, log instructions of the app.
                    LogInstructions();

                    // get the user input, includes tons of validation, if its not valid,
                    // we print the instructions again and try to get the correct user input again.
                    isValid = ReadInputs(out numbers);
                } while (!isValid);

                // the final behavior of the workflow, logs to the console if the users 
                // number matched up correctly or not, then returns if we want to try
                // again.
                bool shouldCloseProgram = LogConclusion(numbers);

                if (shouldCloseProgram)
                {
                    // if the user wants to try again, we start the workflow all over again.
                    PressToContinue();
                    continue;
                }

                // if the user does not want to check anymore, we stop the workflow and terminate the program.
                break;
            }
        }

        private bool LogConclusion(int[] numbers)
        {
            // the math to figure out is injected as a dependancy, this returns an object that contains a boolean
            // for if the matching... matched, and the number of the first digit.
            var result = _math.CompareSumDigits(numbers);
            Console.WriteLine($"First Digit sum = {result.Number}");
            if (result.IsMatched)
            {
                // log if this is correct
                _console.GreenLine("Correct, each digit summed equaled the result of the first digits summed!");
            }
            else
            {
                // log if this is NOT correct
                _console.RedLine("Incorrect, each digit was not equal to first digit!");
            }

            // simply asking if the user wants to try again.
            Console.WriteLine();
            Console.WriteLine("Do you want to try again?");
            Console.WriteLine("press [Y]es to try again, any other key to close program...");
            return Console.ReadKey().Key == ConsoleKey.Y ?
                true :
                false;
        }

        private bool ReadInputs(out int[] numbers)
        {
            // validation is an object injected through the constructor, confirms that the 
            // user is entering the right number.

            // this is the process of taking the users first input.
            Console.WriteLine("Please enter the first number");
            if (!_validate.TryGetOutputNumber(Console.ReadLine(), out OutNumber output1))
            {
                Console.WriteLine("Invalid Enteries");
                foreach (var message in output1.ErrorMessages)
                {
                    // writing out the reasons it failed, so the user
                    // hopefully does not repeat his/her mistake.
                    _console.RedLine($"\t{message}");
                    PressToContinue();
                }
                numbers = new int[0];
                return false;
            }

            // this is the process of taking the users second input.
            Console.WriteLine("Please enter the second number");
            if (!_validate.TryGetOutputNumber(Console.ReadLine(), out OutNumber output2))
            {
                Console.WriteLine("Invalid Enteries");
                foreach (var message in output1.ErrorMessages)
                {
                    _console.RedLine($"\t{message}");
                    PressToContinue();
                }
                numbers = new int[0];
                return false;
            }

            numbers = new int[2] { output1.Number, output2.Number };

            return true;
        }

        private void LogInstructions()
        {

            // the _console is a console helper injected through the constructor, basically 
            // just some quick repeatable console commands that allows color changing and borders.
            _console.BlueBorderMessage('#', 50, "INSTRUCTIONS");
            Console.WriteLine("Enter two seperate numbers that when you add each single digit of both numbers, they will sum to the same number.");
            Console.Write("Both numbers must be ");
            _console.Yellow("POSITIVE ");
            Console.Write("and must be ");
            _console.Yellow("SHORTER ");
            Console.Write("than ");
            _console.Yellow("SIX ");
            Console.Write("digits in length");
            Console.WriteLine();
            _console.BlueBorderMessage('#', 53, "EXAMPLE");
            _console.RedLine("INCORRECT: 4456, 412 - Digit Length Missmatch");
            _console.RedLine("INCORRECT: -2233, -33245 - Digit Length Missmatch, Negative Numbers");
            _console.RedLine("INCORRECT: 1234521,  65432123 - Both Digits too long");
            Console.WriteLine();
            _console.GreenLine("Correct: 12345, 54321 - Both Positive, both correct length");
            _console.BlueBorder('#', 116);
            Console.WriteLine();
        }

        private void LogIntroMessage()
        {
            // simple introduction
            _console.CyanLine("Welcome to the Grand Circus Number Matching App!");
        }

        private void PressToContinue()
        {
            // a press to continue "macro method" if you will.
            Console.WriteLine();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            Console.Clear();
        }
    }
}
