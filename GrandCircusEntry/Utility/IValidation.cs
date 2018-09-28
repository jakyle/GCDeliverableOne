using GrandCircusEntry.Workflows;

namespace GrandCircusEntry.Utility
{
    // contract for validation
    internal interface IValidation
    {
        bool TryGetOutputNumber(string input, out OutNumber output);
    }
}