using System;
using System.Collections.Generic;

namespace Plants
{
    public class UpgradeDefinition
    {
        public readonly string title;

        public Action OnUpgraded;
        public Func<int> getCurrentLevel;
        public Func<int> getCurrentCost;

        public UpgradeDefinition(string title, Func<int> currentLevel, Func<int> cost, Action onUpgraded)
        {
            this.title = title;
            getCurrentLevel = currentLevel;
            getCurrentCost = cost;
            OnUpgraded = onUpgraded;
        }
    }
}