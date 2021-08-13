using System;
using System.Net;

namespace MiniSwitch.Helpers
{
    public class IPAddressValidation
    {
        public bool IsIPAddress(string address)
        {
            return IPAddress.TryParse(address, out IPAddress ip); 
        }
    }
}
