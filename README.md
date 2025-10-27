# SurfSharkVpn.cs
Mobie-API for [SurfShark VPN](https://play.google.com/store/apps/details?id=com.surfshark.vpnclient.android) which is recognized as the best global privacy VPN proxy network in 2021, it is the best VPN service for Android devices

## Example
```cs
using SurfSharkVpnApi;

namespace Application
{
    internal class Program
    {
        static async Task Main()
        {
            var api = new SurfSharkVpn();
            string serverClusters = await api.GetServerClusters();
            Console.WriteLine(serverClusters);
        }
    }
}
```
