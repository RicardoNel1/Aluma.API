using DataService.Enum;
using System;

namespace Aluma.API.Extensions
{
    public class EnumConvertions
    {
        public static double RiskExpectations(string risk)
        {
            RiskRatingsEnum? riskRating = null;
            if (Enum.IsDefined(typeof(RiskRatingsEnum), risk))
                riskRating = ((RiskRatingsEnum)Enum.Parse(typeof(RiskRatingsEnum), risk));

            switch (riskRating)
            {
                case RiskRatingsEnum.Conservative:
                    return 7;
                case RiskRatingsEnum.ModeratelyConservative:
                    return 8;
                case RiskRatingsEnum.Moderate:
                    return 9;
                case RiskRatingsEnum.ModeratelyAggressive:
                    return 9.5d;
                case RiskRatingsEnum.Aggressive:
                    return 10;
                default:
                    return 0;
            }
        }
    }
}
