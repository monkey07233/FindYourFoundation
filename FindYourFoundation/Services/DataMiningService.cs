using FindYourFoundation.Repositoires;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace FindYourFoundation.Services
{
    public class DataMiningService
    {
        private ProductRepo _productRepo = new ProductRepo();
        public static int price = 0;
        public async Task<int> GetPrice(string name,string shop)
        {
            price = -1;
            string FileName = shop + ".py";
            await RunPython(FileName, "-u", name);
            return price;
        }
        public static async Task RunPython(string name, string args, string pro)
        {
            await Task.Run(() =>
            {
                Process process = new Process();
                //string path = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + name;
                string path = @"D:/Python/" + name;
                process.StartInfo.FileName = @"D:/Anaconda3/python.exe";
                path += " " + pro;
                process.StartInfo.Arguments = path;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardInput = true;
                process.StartInfo.RedirectStandardError = true;
                process.StartInfo.CreateNoWindow = true;

                process.OutputDataReceived += new DataReceivedEventHandler(OutputData);
                using (process)
                {
                    process.Start();
                    process.BeginOutputReadLine();
                    process.WaitForExit();
                    process.Close();
                }
            });
        }
        static void OutputData(object sender, DataReceivedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Data))
            {
                if (e.Data.ToString() == "None")
                {
                    price = -1;
                }
                else
                {
                    price = Convert.ToInt32(e.Data);
                }
                Console.WriteLine(e.Data);
            }
        }
    }
}