using FindYourFoundation.Repositoires;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace FindYourFoundation.Services
{
    public class FaceService
    {
        private ProductRepo _productRepo = new ProductRepo();
        public static string skin = "";
        private static string AzureKey = "b7171b1bb51b462da557ceedebfd67fa"; 
        public async Task<string> GetSkin(string image)
        {
            var ticket = _productRepo.GetProTicket();
            string color = "";
            foreach(var tic in ticket)
            {
                color += tic + ";";
            }
            await RunPython("skin.py", "-u", image , color);
            return skin;
        }
        public static async Task RunPython(string name, string args, string img,string color)
        {
            await Task.Run(() =>
            {
                Process process = new Process();
                //string path = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + name;
                string path = @"D:/Python/" + name;
                process.StartInfo.FileName = @"D:/Anaconda3/python.exe";
                path +=" " + AzureKey + " " + img + " " + color ;
                //path += " " + img;
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
                skin = e.Data.ToString();
                Console.WriteLine(e.Data);
            }
        }
    }
}