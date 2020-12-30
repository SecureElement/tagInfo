using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Runtime.InteropServices;


namespace TagVerify
{
    public class Program
    {
        // Insert correct filePath
        [DllImport("NTAGLib.dll", EntryPoint = "NTAG_Verify_CMAC", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool NTAG_Verify_CMAC(int[] a, int[] b, short c, int[] d, short e, int[] f, short g, int[] h);



        public static void Main(string[] args)
        {
            Console.WriteLine("start");
            int[] k_SDMFileReadKey = { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
            int[] uID = { 0x04, 0xA0, 0x46, 0x2A, 0xAA, 0x61, 0x80 };
            int[] uReadCtr = { 0x04, 0x01, 0x00 };
            int[] uCMACValue = { 0x07, 0xF9, 0xC8, 0x6A, 0x97, 0x20, 0xC2, 0x3C };
            int[] cCMACData = { 0x07, 0xF9, 0xC8, 0x6A, 0x97, 0x20, 0xC2, 0x3C };
            int[] uCMACValuePass = { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };

            bool bRetValue = NTAG_Verify_CMAC(k_SDMFileReadKey, uID, (short)7, uReadCtr, (short)3, cCMACData, (short)cCMACData.Length, uCMACValuePass);
            Console.WriteLine("NTAG_Verify_CMAC: " + bRetValue);
            CreateHostBuilder(args).Build().Run();


        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
