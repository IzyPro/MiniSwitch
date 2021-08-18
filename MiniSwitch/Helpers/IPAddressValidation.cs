using System;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;

namespace MiniSwitch.Helpers
{
    public class IPAddressValidation
    {
        public static bool IsIPAddress(string address)
        {
            Regex validIpV4AddressRegex = new Regex(@"^(([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5]).){3}([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])$", RegexOptions.IgnoreCase);
            return validIpV4AddressRegex.IsMatch(address.Trim());
        }
    }
}
