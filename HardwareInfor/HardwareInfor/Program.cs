using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace HardwareInfor
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("\n------------------------------ GPU Information ------------------------------");
			ManagementObjectSearcher myVideoObject = new ManagementObjectSearcher("select * from Win32_VideoController");

			foreach (ManagementObject obj in myVideoObject.Get())
			{
				Console.WriteLine("Name  -  " + obj["Name"]);
				Console.WriteLine("Status  -  " + obj["Status"]);
				Console.WriteLine("Caption  -  " + obj["Caption"]);
				Console.WriteLine("DeviceID  -  " + obj["DeviceID"]);
				Console.WriteLine("AdapterRAM  -  " + obj["AdapterRAM"]);
				Console.WriteLine("AdapterDACType  -  " + obj["AdapterDACType"]);
				Console.WriteLine("Monochrome  -  " + obj["Monochrome"]);
				Console.WriteLine("InstalledDisplayDrivers  -  " + obj["InstalledDisplayDrivers"]);
				Console.WriteLine("DriverVersion  -  " + obj["DriverVersion"]);
				Console.WriteLine("VideoProcessor  -  " + obj["VideoProcessor"]);
				Console.WriteLine("VideoArchitecture  -  " + obj["VideoArchitecture"]);
				Console.WriteLine("VideoMemoryType  -  " + obj["VideoMemoryType"]);
			}

			Console.WriteLine("\n------------------------------ Hard drives Information ------------------------------");
			DriveInfo[] allDrives = DriveInfo.GetDrives();

			foreach (DriveInfo d in allDrives)
			{
				Console.WriteLine("Drive {0}", d.Name);
				Console.WriteLine("  Drive type: {0}", d.DriveType);
				if (d.IsReady == true)
				{
					Console.WriteLine("  Volume label: {0}", d.VolumeLabel);
					Console.WriteLine("  File system: {0}", d.DriveFormat);

					Console.WriteLine("  Available space to current user:{0, 15}", SizeSuffix(d.AvailableFreeSpace));
					Console.WriteLine("  Total available space:          {0, 15}", SizeSuffix(d.TotalFreeSpace));
					Console.WriteLine("  Total size of drive:            {0, 15} ", SizeSuffix(d.TotalSize));

					Console.WriteLine("  Root directory:            {0, 12}", d.RootDirectory);
				}
			}

			Console.WriteLine("\n------------------------------ Processor Information ------------------------------");
			ManagementObjectSearcher myProcessorObject = new ManagementObjectSearcher("select * from Win32_Processor");

			foreach (ManagementObject obj in myProcessorObject.Get())
			{
				Console.WriteLine("Name  -  " + obj["Name"]);
				Console.WriteLine("DeviceID  -  " + obj["DeviceID"]);
				Console.WriteLine("Manufacturer  -  " + obj["Manufacturer"]);
				Console.WriteLine("CurrentClockSpeed  -  " + obj["CurrentClockSpeed"]);
				Console.WriteLine("Caption  -  " + obj["Caption"]);
				Console.WriteLine("NumberOfCores  -  " + obj["NumberOfCores"]);
				Console.WriteLine("NumberOfEnabledCore  -  " + obj["NumberOfEnabledCore"]);
				Console.WriteLine("NumberOfLogicalProcessors  -  " + obj["NumberOfLogicalProcessors"]);
				Console.WriteLine("Architecture  -  " + obj["Architecture"]);
				Console.WriteLine("Family  -  " + obj["Family"]);
				Console.WriteLine("ProcessorType  -  " + obj["ProcessorType"]);
				Console.WriteLine("Characteristics  -  " + obj["Characteristics"]);
				Console.WriteLine("AddressWidth  -  " + obj["AddressWidth"]);
			}

			Console.WriteLine("\n------------------------------ Operative Information ------------------------------");
			ManagementObjectSearcher myOperativeSystemObject = new ManagementObjectSearcher("select * from Win32_OperatingSystem");

			foreach (ManagementObject obj in myOperativeSystemObject.Get())
			{
				Console.WriteLine("Caption  -  " + obj["Caption"]);
				Console.WriteLine("WindowsDirectory  -  " + obj["WindowsDirectory"]);
				Console.WriteLine("ProductType  -  " + obj["ProductType"]);
				Console.WriteLine("SerialNumber  -  " + obj["SerialNumber"]);
				Console.WriteLine("SystemDirectory  -  " + obj["SystemDirectory"]);
				Console.WriteLine("CountryCode  -  " + obj["CountryCode"]);
				Console.WriteLine("CurrentTimeZone  -  " + obj["CurrentTimeZone"]);
				Console.WriteLine("EncryptionLevel  -  " + obj["EncryptionLevel"]);
				Console.WriteLine("OSType  -  " + obj["OSType"]);
				Console.WriteLine("Version  -  " + obj["Version"]);
			}

			Console.WriteLine("\n------------------------------ Network Interface Information ------------------------------");
			NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();

			if (nics == null || nics.Length < 1)
			{
				Console.WriteLine("  No network interfaces found.");
			}
			else
			{
				foreach (NetworkInterface adapter in nics)
				{
					IPInterfaceProperties properties = adapter.GetIPProperties();
					Console.WriteLine();
					Console.WriteLine(adapter.Description);
					Console.WriteLine(String.Empty.PadLeft(adapter.Description.Length, '='));
					Console.WriteLine("  Interface type .......................... : {0}", adapter.NetworkInterfaceType);
					Console.WriteLine("  Physical Address ........................ : {0}", adapter.GetPhysicalAddress().ToString());
					Console.WriteLine("  Operational status ...................... : {0}", adapter.OperationalStatus);
				}
			}

			Console.WriteLine("\n------------------------------ Sound Card Information ------------------------------");
			ManagementObjectSearcher myAudioObject = new ManagementObjectSearcher("select * from Win32_SoundDevice");

			foreach (ManagementObject obj in myAudioObject.Get())
			{
				Console.WriteLine("Name  -  " + obj["Name"]);
				Console.WriteLine("ProductName  -  " + obj["ProductName"]);
				Console.WriteLine("Availability  -  " + obj["Availability"]);
				Console.WriteLine("DeviceID  -  " + obj["DeviceID"]);
				Console.WriteLine("PowerManagementSupported  -  " + obj["PowerManagementSupported"]);
				Console.WriteLine("Status  -  " + obj["Status"]);
				Console.WriteLine("StatusInfo  -  " + obj["StatusInfo"]);
				Console.WriteLine(String.Empty.PadLeft(obj["ProductName"].ToString().Length, '='));
			}

			Console.WriteLine("\n------------------------------ Printers Information ------------------------------");
			ManagementObjectSearcher myPrinterObject = new ManagementObjectSearcher("select * from Win32_Printer");

			foreach (ManagementObject obj in myPrinterObject.Get())
			{
				Console.WriteLine("Name  -  " + obj["Name"]);
				Console.WriteLine("Network  -  " + obj["Network"]);
				Console.WriteLine("Availability  -  " + obj["Availability"]);
				Console.WriteLine("Is default printer  -  " + obj["Default"]);
				Console.WriteLine("DeviceID  -  " + obj["DeviceID"]);
				Console.WriteLine("Status  -  " + obj["Status"]);

				Console.WriteLine(String.Empty.PadLeft(obj["Name"].ToString().Length, '='));
			}

			Console.ReadKey();
		}

		static readonly string[] SizeSuffixes = { "bytes", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" };

		static string SizeSuffix(Int64 value)
		{
			if (value < 0) { return "-" + SizeSuffix(-value); }
			if (value == 0) { return "0.0 bytes"; }

			int mag = (int)Math.Log(value, 1024);
			decimal adjustedSize = (decimal)value / (1L << (mag * 10));

			return string.Format("{0:n1} {1}", adjustedSize, SizeSuffixes[mag]);
		}
	}
}
