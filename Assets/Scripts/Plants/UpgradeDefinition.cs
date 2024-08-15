using System;

namespace Plants
{
    //todo: if UI is laggy, visit this again with Func, ask Arya
    public class UpgradeDefinition
    {
        public readonly string title;
        public readonly int currentLevel;
        public readonly int cost;

        public Action OnUpgraded;

        public UpgradeDefinition(string title, int currentLevel, int cost, Action onUpgraded)
        {
            this.title = title;
            this.currentLevel = currentLevel;
            this.cost = cost;
            OnUpgraded = onUpgraded;
        }
    }
}