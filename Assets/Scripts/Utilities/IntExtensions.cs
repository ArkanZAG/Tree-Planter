namespace Utilities
{
    public static class IntExtensions
    {
        public static int FloorTo(this int number, int multiple)
        {
            return multiple * (number / multiple);
        }
    }
}