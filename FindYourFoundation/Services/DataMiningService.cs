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
        public static string shopTmp = "";
        public static ArrayList books = new ArrayList();
        public static ArrayList cosmed = new ArrayList();
        public static ArrayList momo = new ArrayList();
        public static ArrayList wastons = new ArrayList();
        public static ArrayList yahoobuy = new ArrayList();
        public static ArrayList yahoomall = new ArrayList();
        public async Task<int> GetPrice()
        {
            string[] ShopList = { "books", "cosmed", "momo", "wastons", "yahoobuy", "yahoomall" };
            var NameList = _productRepo.GetProductName();
            foreach (var shop in ShopList)
            {
                shopTmp = shop;
                foreach (var name in NameList)
                {
                    //string FileName = @"yahoomall.py";
                    string FileName = shop + ".py";
                    await RunPython(FileName, "-u", name);
                }
            }
            //string FileName = @"yahoomall.py";
            //await RunPython(FileName, "-u", "夢幻奇蹟無瑕粉底液");
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
                    switch (shopTmp)
                    {
                        case "books":
                            books.Add(null);
                            break;
                        case "cosmed":
                            cosmed.Add(null);
                            break;
                        case "momo":
                            momo.Add(null);
                            break;
                        case "wastons":
                            wastons.Add(null);
                            break;
                        case "yahoobuy":
                            yahoobuy.Add(null);
                            break;
                        case "yahoomall":
                            yahoomall.Add(null);
                            break;
                        default:
                            break;
                    }                   
                }
                else
                {
                    switch (shopTmp)
                    {
                        case "books":
                            books.Add(Convert.ToInt32(e.Data));
                            break;
                        case "cosmed":
                            cosmed.Add(Convert.ToInt32(e.Data));
                            break;
                        case "momo":
                            momo.Add(Convert.ToInt32(e.Data));
                            break;
                        case "wastons":
                            wastons.Add(Convert.ToInt32(e.Data));
                            break;
                        case "yahoobuy":
                            yahoobuy.Add(Convert.ToInt32(e.Data));
                            break;
                        case "yahoomall":
                            yahoomall.Add(Convert.ToInt32(e.Data));
                            break;
                        default:
                            break;
                    }
                    price = Convert.ToInt32(e.Data);
                }
                Console.WriteLine(e.Data);
            }
        }
    }
}