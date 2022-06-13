

namespace System
{
    public class RegistryNoneFindException:Exception
    {
        public RegistryNoneFindException(string message) : base(message)
        {
        }

        public RegistryNoneFindException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
