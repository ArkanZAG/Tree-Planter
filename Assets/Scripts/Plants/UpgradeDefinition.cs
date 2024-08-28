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
        public Func<int> getMaxLevel;

        public UpgradeDefinition(string title, Func<int> currentLevel, Func<int> cost, Func<int> maxLevel, Action onUpgraded)
        {
            this.title = title;
            getMaxLevel = maxLevel;
            getCurrentLevel = currentLevel;
            getCurrentCost = cost;
            OnUpgraded = onUpgraded;
        }
    }
}