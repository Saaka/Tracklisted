using System;
using System.Collections.Generic;
using System.Text;

namespace Tracklisted.Integration.Lastfm.Base
{
    public interface IPeriodMapper
    {
        string GetPeriod(PeriodType periodType);
    }
    internal class PeriodMapper : IPeriodMapper
    {
        protected readonly Dictionary<PeriodType, string> PeriodDictionary = new Dictionary<PeriodType, string>
        {
            { PeriodType.Overall, "overall" },
            { PeriodType.Day7, "7day" },
            { PeriodType.Month1, "1month" },
            { PeriodType.Month3, "3month" },
            { PeriodType.Month6, "6month" },
            { PeriodType.Month12, "12month" }
        };

        public string GetPeriod(PeriodType periodType)
        {
            return PeriodDictionary.GetValueOrDefault(periodType, "overall");
        }
    }
}
