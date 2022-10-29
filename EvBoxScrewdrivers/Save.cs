using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;


namespace EvBoxScrewdrivers
{
    public class Save
    {

        public static async Task SendLogMesTisAsync(string serial)
        {
            DateTime stop = DateTime.Now;
            wsTis.MES_TISSoapClient ws = new wsTis.MES_TISSoapClient(wsTis.MES_TISSoapClient.EndpointConfiguration.MES_TISSoap);
          //  wsTis.MES_TISSoapClient ws = new wsTis.MES_TISSoapClient(wsTis.MES_TISSoapClient.EndpointConfiguration.MES_TISSoap);

            if (ws != null)
                {
                    try
                    {
                        //var res = await ws.GetVersionAsync();
                        //string ver = res.Body.GetVersionResult;
                        //sw.WriteLine("S{0}", serial);
                        //sw.WriteLine("CTRILLIANT");
                        //sw.WriteLine("NPLKWIM0T26B1W01");
                        //sw.WriteLine("PWEIGHT_CONTROL");
                        //sw.WriteLine("Ooperator");
                        //sw.WriteLine("TP");
                        //sw.WriteLine("MWEIGHT");
                        //sw.WriteLine("d" + WeightControl.MeasuredWeightOfCounter);
                        //sw.WriteLine("[" + stop.ToString("yyyy-MM-dd HH:mm:ss"));
                        //sw.WriteLine("]" + stop.ToString("yyyy-MM-dd HH:mm:ss"));



                        StringBuilder sb = new StringBuilder();
                        sb.Append($"S{serial}\n");
                        sb.Append("CTRILLIANT" + "\n");
                        sb.Append($"N{System.Environment.MachineName}" + "\n");
                        sb.Append("PWEIGHT_CONTROL" + "\n");
                        sb.Append("Ooperator" + "\n");
                        sb.Append("TP" + "\n");
                        sb.Append("MWEIGHT\n");
                   //     sb.Append("d" + WeightControl.MeasuredWeightOfCounter  + "\n");
                        sb.Append("[" + stop.ToString("yyyy-MM-dd HH:mm:ss") + "\n");
                        sb.Append("]" + stop.ToString("yyyy-MM-dd HH:mm:ss") + "\n");

                        var res = await ws.ProcessTestDataAsync(sb.ToString(), "Generic");

                        if (res != null && res.Body.ProcessTestDataResult.ToString().ToUpper() != "PASS")
                        {
                            SaveLog(serial);
                        }
                        else
                            SaveCopyLog(serial);

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        SaveLog(serial);
                    }
                    finally
                    {
                        await ws.CloseAsync();

                    }

            }
                else
                    SaveLog(serial);

            


        }

        public static void SaveLog(string serial)
        {
            try
            {
                string sciezka = "C:/tars/";      //definiowanieścieżki do której zapisywane logi
                DateTime stop = DateTime.Now;
                if (Directory.Exists(sciezka))       //sprawdzanie czy sciezka istnieje
                {
                    ;
                }
                else
                    System.IO.Directory.CreateDirectory(sciezka); //jeśli nie to ją tworzy
                if (serial != null)
                    serial = Regex.Replace(serial, @"\s+", string.Empty);

                using (StreamWriter sw = new StreamWriter("C:/tars/" + serial + "-" + "(" + stop.Day + "-" + stop.Month + "-" + stop.Year + " " + stop.Hour + "-" + stop.Minute + "-" + stop.Second + ")" + ".Tars"))
                {

                    sw.WriteLine("S{0}", serial);
                    sw.WriteLine("CTRILLIANT");
                    sw.WriteLine($"N{System.Environment.MachineName}");
                    sw.WriteLine($"{MainWindow.ListOfActivities[MainWindow._currentActivity].Name}");
                    sw.WriteLine("Ooperator");
                    sw.WriteLine("TP");
                    sw.WriteLine("MWEIGHT");
                //    sw.WriteLine("d" + WeightControl.MeasuredWeightOfCounter);
                    sw.WriteLine("[" + stop.ToString("yyyy-MM-dd HH:mm:ss"));
                    sw.WriteLine("]" + stop.ToString("yyyy-MM-dd HH:mm:ss"));
                    //for (int i = 0; i > 15; i++)
                    //    result[i] = string.Empty;

                }

                string sourceFile = @"C:/tars/" + serial + @"-" + @"(" + @stop.Day + @"-" + @stop.Month + @"-" + @stop.Year + @" " + @stop.Hour + @"-" + @stop.Minute + @"-" + @stop.Second + @")" + @".Tars";
                string destinationFile = @"C:/copylogi/" + @stop.Day + @"-" + @stop.Month + @"-" + @stop.Year + @"/" + @serial + @"-" + @"(" + @stop.Day + @"-" + @stop.Month + @"-" + @stop.Year + @" " + @stop.Hour + @"-" + @stop.Minute + @"-" + @stop.Second + @")" + @".Tars";

                if (Directory.Exists(@"C:/copylogi/" + @stop.Day + @"-" + @stop.Month + @"-" + @stop.Year + @"/"))       //sprawdzanie czy sciezka istnieje
                {
                    ;
                }
                else
                    System.IO.Directory.CreateDirectory(@"C:/copylogi/" + @stop.Day + @"-" + @stop.Month + @"-" + @stop.Year + @"/"); //jeśli nie to ją tworzy

                File.Copy(sourceFile, destinationFile, true);
            }
            catch (IOException iox)
            {
                MessageBox.Show(iox.Message);
            }
        }

        public static void SaveCopyLog(string serial)
        {
            try
            {
                DateTime stop = DateTime.Now;
                string sciezka = @"C:/copylogi/" + @stop.Day + @"-" + @stop.Month + @"-" + @stop.Year + @"/";      //definiowanieścieżki do której zapisywane logi
                
                if (Directory.Exists(sciezka))       //sprawdzanie czy sciezka istnieje
                {
                    ;
                }
                else
                    System.IO.Directory.CreateDirectory(sciezka); //jeśli nie to ją tworzy

                if (serial != null)
                    serial = Regex.Replace(serial, @"\s+", string.Empty);

                using (StreamWriter sw = new StreamWriter(sciezka + serial + "-" + "(" + stop.Day + "-" + stop.Month + "-" + stop.Year + " " + stop.Hour + "-" + stop.Minute + "-" + stop.Second + ")" + ".Tars"))
                {

                    sw.WriteLine("S{0}", serial);
                    sw.WriteLine("CTRILLIANT");
                    sw.WriteLine($"N{System.Environment.MachineName}");
                    sw.WriteLine("PWEIGHT_CONTROL");
                    sw.WriteLine("Ooperator");
                    sw.WriteLine("TP");
                    sw.WriteLine("MWEIGHT");
               //     sw.WriteLine("d" + WeightControl.MeasuredWeightOfCounter);
                    sw.WriteLine("[" + stop.ToString("yyyy-MM-dd HH:mm:ss"));
                    sw.WriteLine("]" + stop.ToString("yyyy-MM-dd HH:mm:ss"));
                    //for (int i = 0; i > 15; i++)
                    //    result[i] = string.Empty;

                }

            }
            catch (IOException iox)
            {
                MessageBox.Show(iox.Message);
            }
        }

    }
}

