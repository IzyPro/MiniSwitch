using System;
using MiniSwitch.Enums;
using MiniSwitch.Models;

namespace MiniSwitch.Helpers
{
    public class FeeGenerationHelper
    {
        public static decimal CalculateFee(decimal amount, Fee fee)
        {
            if (fee.FeeType == FeeTypeEnum.Flat)
                return fee.Amount;

            else if(fee.FeeType == FeeTypeEnum.Percentage)
            {
                var feeValue = (amount * fee.Percentage) / 100;
                return feeValue > fee.Maximum ? fee.Maximum : feeValue < fee.Minimum ? fee.Minimum : feeValue;
            }

            else
            {
                return 0;
            }
        }
    }
}
