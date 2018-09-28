using System.Collections.Generic;

namespace GrandCircusEntry.Workflows
{
    // internal object used to log all error messages
    // and the number result.
    internal class OutNumber
    {
        public int Number { get; set; }
        public List<string> ErrorMessages { get; set; }
    }
}