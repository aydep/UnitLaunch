using System;
using System.Diagnostics;

namespace UnitLaunch
{
    public class VPN
    {
        private void Connect(string adapter, string serverAddress, string userName, string passWord)
        {
#pragma warning disable CS8600 // Преобразование литерала, допускающего значение NULL или возможного значения NULL в тип, не допускающий значение NULL.
            Process p1 = Process.Start(new ProcessStartInfo
            {
                FileName = "cmd",
                Arguments = @$"/c chcp 65001 & PowerShell Add-VpnConnection -Name '{adapter}' -ServerAddres '{serverAddress}' -TunnelType 'Pptp' & echo proc1end",
                CreateNoWindow = true,
                UseShellExecute = false,
                RedirectStandardOutput = true
            });

            Process p2 = new Process();
            p2.StartInfo = new ProcessStartInfo
            {
                FileName = "cmd",
                Arguments = @$"/c rasdial {adapter} {userName} {passWord}",
                CreateNoWindow = true
            };

            p1.BeginOutputReadLine();
            p1.OutputDataReceived += (s, e) =>
            {
                if (e.Data == null)
                {
                    p1.Kill();
                    p2.Start();
                } else
                    throw new Exception("FAIL " + e.Data.ToString());
            };
        }

        private void Connect()
        {
            string adapter = "UnitLaunchVpnAdapter";
            string serverAddress = "us1.vpnbook.com";
            string userName = "vpnbook";
            string passWord = "4v3nabb";

#pragma warning disable CS8600 // Преобразование литерала, допускающего значение NULL или возможного значения NULL в тип, не допускающий значение NULL.
            Process p1 = Process.Start(new ProcessStartInfo
            {
                FileName = "cmd",
                Arguments = @$"/c chcp 65001 & PowerShell Add-VpnConnection -Name '{adapter}' -ServerAddres '{serverAddress}' -TunnelType 'Pptp' & echo proc1end",
                CreateNoWindow = true,
                UseShellExecute = false,
                RedirectStandardOutput = true
            });

            Process p2 = new Process();
            p2.StartInfo = new ProcessStartInfo
            {
                FileName = "cmd",
                Arguments = @$"/c rasdial {adapter} {userName} {passWord}",
                CreateNoWindow = true
            };

            p1.BeginOutputReadLine();
            p1.OutputDataReceived += (s, e) =>
            {
                if (e.Data == null)
                {
                    p1.Kill();
                    p2.Start();
                } else
                    throw new Exception("FAIL " + e.Data.ToString());
            };
        }
        
        private void Disconnect(string adapter)
        {
            Process p1 = Process.Start(new ProcessStartInfo
            {
                FileName = "cmd",
                Arguments = $@"/k rasdial {adapter} /disconnect & echo proc2end",
                CreateNoWindow = true,
                UseShellExecute = false,
                RedirectStandardOutput = true
            });
#pragma warning restore CS8600 // Преобразование литерала, допускающего значение NULL или возможного значения NULL в тип, не допускающий значение NULL.

            Process p2 = new Process();
            p2.StartInfo = new ProcessStartInfo
            {
                FileName = "cmd",
                Arguments = $@"/k PowerShell Remove-VpnConnection {adapter} -Force",
                CreateNoWindow = true
            };

            p1.BeginOutputReadLine();
            p1.OutputDataReceived += (s, e) =>
            {
                if (e.Data == null)
                {
                    p1.Kill();
                    p2.Start();
                }
                else
                    throw new Exception("FAIL " + e.Data.ToString());
            };
        }
        
        private void Disconnect()
        {
            string adapter = "UnitLaunchVpnAdapter";

            Process p1 = Process.Start(new ProcessStartInfo
            {
                FileName = "cmd",
                Arguments = $@"/k rasdial {adapter} /disconnect & echo proc2end",
                CreateNoWindow = true,
                UseShellExecute = false,
                RedirectStandardOutput = true
            });
#pragma warning restore CS8600 // Преобразование литерала, допускающего значение NULL или возможного значения NULL в тип, не допускающий значение NULL.

            Process p2 = new Process();
            p2.StartInfo = new ProcessStartInfo
            {
                FileName = "cmd",
                Arguments = $@"/k PowerShell Remove-VpnConnection {adapter} -Force",
                CreateNoWindow = true
            };

            p1.BeginOutputReadLine();
            p1.OutputDataReceived += (s, e) =>
            {
                if (e.Data == null)
                {
                    p1.Kill();
                    p2.Start();
                }
                else
                    throw new Exception("FAIL " + e.Data.ToString());
            };
        }
    }
}
