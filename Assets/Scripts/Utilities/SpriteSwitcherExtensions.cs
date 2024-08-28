namespace Utilities
{
    public static class SpriteSwitcherExtensions
    {
        public static void Set(this BoolSpriteSwitcher[] switchers, bool state)
        {
            foreach (var switcher in switchers)
                switcher.Set(state);
        }
    }
}