namespace GrandCircusEntry.Utility
{
    class MathHelper : IMathHelper
    {
        // although this solution is not the most readable, it is faster 
        // than converting the numbers into strings.  this would 
        // be faster if I used calculus to figure this out but 
        // math is not my strong suite. 
        // this method is a little brute forcy but it's for this 
        // exersize, especially since I set the digit length limit to 5. 
        public CompareResult CompareSumDigits(int[] numbers)
        {
            // extract both numbers
            int num1 = numbers[0];
            int num2 = numbers[1];

            // find how many digits are in one of the number, we already know that 
            // each number will be of equal length.
            var digits = CountToFiveDigits(num1);

            // we will use this "first digit" as a means to 
            // compare other digit sums
            var firstDigit = 0;
            for (int i = 0; i < digits; i++)
            {
                // modulo of 10 will return the first digit
                var digit1 = num1 % 10;
                var digit2 = num2 % 10;

                // get the sum of both numbers.
                var sum = digit1 + digit2;

                // checking if this is the first time looping
                if (firstDigit == 0)
                {
                    // if this is the first time looping, we will store the 
                    // first digit in the variable outside of this loop.
                    firstDigit = sum;

                    // divide each number by 10, thus removing a "digit"
                    num1 /= 10;
                    num2 /= 10;

                    // continue with the loop.
                    continue;
                }

                // checks if the first digit equals the sum of any other "sum"
                if (firstDigit != sum)
                {

                    // return an object  containing the first digit and FALSE because the numbers
                    // did not equal the first digit.
                    return new CompareResult { Number = firstDigit, IsMatched = false };
                }

                // if the next numbers matched, then we continue to divide down
                // to reduce the digits.
                num1 /= 10;
                num2 /= 10;

            }

            // if the digits always matched just fine, then return the object containing the first digit
            // and TRUE.
            return new CompareResult { Number = firstDigit, IsMatched = true };
        }

        private int CountToFiveDigits(int number)
        {
            // fastest way to count digits without converting to string, 
            // converting tostring is a very inneficient solution
            if (number < 10) return 1;
            if (number < 100) return 2;
            if (number < 1000) return 3;
            if (number < 10000) return 4;
            return 5;
        }
    }
}
