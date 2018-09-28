using GrandCircusEntry.Workflows;
using System.Collections.Generic;
using System.Linq;

namespace GrandCircusEntry.Utility
{
    // if you notice, there is NO console logging in this method, this is on purpose, I wanted to 
    // seperate the validation logic from the workflow or "View" of the console application.
    class Validation : IValidation
    {
        // using the try get pattern to return a boolean so the consumer of this
        // method will check if we can use the number or not, if we can't we have
        // error messages stating why we cannot.
        public bool TryGetOutputNumber(string input, out OutNumber output)
        {
            // start a new list of potential error messages.
            var errorMessages = new List<string>();

            if (!int.TryParse(input, out int result))
            {
                // in each of these if statements, we will start adding errors 
                // into the list.
                errorMessages.Add("Not a valid number.");
            }

            if (!CheckLessThanSix(result))
            {
                errorMessages.Add("Number contains too many digits.");
            }

            if (!CheckIsNegative(result))
            {
                errorMessages.Add("Number contains too many digits.");
            }

            // if the list has ANY elements inside of it, then we know theres errors
            if (errorMessages.Any())
            {

                // return object containing the error message.
                output = new OutNumber { ErrorMessages = errorMessages };
                return false;
            }
            else
            {
                // else, theres no errors, so return the resulting input number.
                output = new OutNumber { Number = result };
                return true;
            }
        }

        // checks if the inputted number is less than six
        private bool CheckLessThanSix(int number)
        {
            if (number < 100000)
            {
                return true;
            }
            return false;
        }

        // checks if the inputted number is a negative number
        private bool CheckIsNegative(int number)
        {
            if (number > 0)
            {
                return true;
            }
            return false;
        }
    }
}
