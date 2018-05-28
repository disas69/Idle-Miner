using Game.Core.Resources;

namespace Game.Utils
{
    public static class FormatHelper
    {
        private const string IdleMiningTemplate = "{0}/s";
        private const string CostTemplate = "{0} {1}";

        public static string FormatIdleMining(int value)
        {
            return string.Format(IdleMiningTemplate, FormatResourceAmount(value));
        }

        public static string FormatResourceAmount(int value)
        {
            if (value < 1000)
            {
                return string.Format("{0}", value);
            }
            else if (value < 100000)
            {
                return string.Format("{0}.{1}K", value / 1000, value % 1000 / 100);
            }
            else if (value < 1000000)
            {
                return string.Format("{0}K", value / 1000);
            }
            else if (value < 100000000)
            {
                return string.Format("{0}.{1}M", value / 1000000, value % 1000000 / 100000);
            }
            else
            {
                return string.Format("{0}M", value / 1000000);
            }
        }

        public static string FormatCost(int value, ResourceType resourceType)
        {
            return string.Format(CostTemplate, FormatResourceAmount(value), resourceType.ToString());
        }
    }
}