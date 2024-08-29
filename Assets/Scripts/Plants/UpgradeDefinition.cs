using System;
using System.Collections.Generic;

namespace Plants
{
    public class UpgradeDefinition
    {
        public readonly string title;

        public Action<int> OnUpgraded;
        public Func<int> getCurrentLevel;
        public Func<int, int> getCost;
        public Func<int> getMaxLevel;

        public UpgradeDefinition(string title, Func<int> currentLevel, Func<int, int> cost, Func<int> maxLevel, Action<int> onUpgraded)
        {
            this.title = title;
            getMaxLevel = maxLevel;
            getCurrentLevel = currentLevel;
            getCost = cost;
            OnUpgraded = onUpgraded;
        }
    }
}