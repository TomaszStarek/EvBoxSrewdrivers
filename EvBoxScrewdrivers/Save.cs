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

        public static async Task SendLogMesTisAsync(string serial, int listIndex, string NameOfStep)
        {
            var index = listIndex + 1;
            string indexString = index.ToString();
            if (index <= 9)
                indexString = "0" + indexString;

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
                    sb.Append("CEVBOX\n");
                    sb.Append($"N{System.Environment.MachineName}_{NameOfStep.Substring(NameOfStep.Length - 2)}_s{indexString}\n");
                    sb.Append($"P{NameOfStep}_s{indexString}\n");
                    sb.Append("Ooperator\n");
                    sb.Append("TP\n");
                    sb.Append("MEventCount\n");
                    sb.Append($"d{MainWindow.ScrewsList[listIndex].EventCount}\n");
                    sb.Append("MFasteningTime\n");
                    sb.Append($"d{MainWindow.ScrewsList[listIndex].FasteningTime}\n");
                    sb.Append("MPresetNumber\n");
                    sb.Append($"d{MainWindow.ScrewsList[listIndex].PresetNumber}\n");
                    sb.Append("MTargetTorque\n");
                    sb.Append($"d{MainWindow.ScrewsList[listIndex].TargetTorque}\n");
                    sb.Append("MConvertedTorque\n");
                    sb.Append($"d{MainWindow.ScrewsList[listIndex].ConvertedTorque}\n");
                    sb.Append("MTargetSpeed\n");
                    sb.Append($"d{MainWindow.ScrewsList[listIndex].TargetSpeed}\n");
                    sb.Append("MA1\n");
                    sb.Append($"d{MainWindow.ScrewsList[listIndex].A1}\n");
                    sb.Append("MA2\n");
                    sb.Append($"d{MainWindow.ScrewsList[listIndex].A2}\n");
                    sb.Append("MA3\n");
                    sb.Append($"d{MainWindow.ScrewsList[listIndex].A3}\n");
                    sb.Append("MScrewCountValue\n");
                    sb.Append($"d{MainWindow.ScrewsList[listIndex].ScrewCountValue}\n");
                    sb.Append("MError\n");
                    sb.Append($"d{MainWindow.ScrewsList[listIndex].Error}\n");
                    sb.Append("MForwardOrLoosenig\n");
                    sb.Append($"d{MainWindow.ScrewsList[listIndex].ForwardOrLoosenig}\n");
                    sb.Append("MStatus\n");
                    sb.Append($"d{MainWindow.ScrewsList[listIndex].Status}\n");
                    sb.Append("MSnugTorqueAngle\n");
                    sb.Append($"d{MainWindow.ScrewsList[listIndex].SnugTorqueAngle}\n");
                    //Preset values -\/
                    sb.Append("MTCAMorACTM\n");
                    sb.Append($"d{MainWindow.ScrewsList[listIndex].TCAMorACTM}\n");
                    sb.Append("MTorque\n");
                    sb.Append($"d{MainWindow.ScrewsList[listIndex].Torque}\n");
                    sb.Append("MTorqueMin\n");
                    sb.Append($"d{MainWindow.ScrewsList[listIndex].TorqueMin}\n");
                    sb.Append("MTorqueMax\n");
                    sb.Append($"d{MainWindow.ScrewsList[listIndex].TorqueMax}\n");
                    sb.Append("MTargetAngle\n");
                    sb.Append($"d{MainWindow.ScrewsList[listIndex].TargetAngle}\n");
                    sb.Append("MMinAngle\n");
                    sb.Append($"d{MainWindow.ScrewsList[listIndex].MinAngle}\n");
                    sb.Append("MMaxAngle\n");
                    sb.Append($"d{MainWindow.ScrewsList[listIndex].MaxAngle}\n");
                    sb.Append("MSnugTorue\n");
                    sb.Append($"d{MainWindow.ScrewsList[listIndex].SnugTorue}\n");
                    sb.Append("MSpeed\n");
                    sb.Append($"d{MainWindow.ScrewsList[listIndex].Speed}\n");
                    sb.Append("MFreeFasteningAngle\n");
                    sb.Append($"d{MainWindow.ScrewsList[listIndex].FreeFasteningAngle}\n");
                    sb.Append("MFreeFasteningSpeed\n");
                    sb.Append($"d{MainWindow.ScrewsList[listIndex].FreeFasteningSpeed}\n");
                    sb.Append("MSoftStart\n");
                    sb.Append($"d{MainWindow.ScrewsList[listIndex].SoftStart}\n");
                    sb.Append("MSeatingPoint\n");
                    sb.Append($"d{MainWindow.ScrewsList[listIndex].SeatingPoint}\n");
                    sb.Append("MTorqueRisingRate\n");
                    sb.Append($"d{MainWindow.ScrewsList[listIndex].TorqueRisingRate}\n");
                    sb.Append("MRampUpSpeed\n");
                    sb.Append($"d{MainWindow.ScrewsList[listIndex].RampUpSpeed}\n");
                    sb.Append("MTorqueCompensation\n");
                    sb.Append($"d{MainWindow.ScrewsList[listIndex].TorqueCompensation}\n");
                    sb.Append("[" + stop.ToString("yyyy-MM-dd HH:mm:ss"));
                    sb.Append("]" + stop.ToString("yyyy-MM-dd HH:mm:ss"));

                    var res = await ws.ProcessTestDataAsync(sb.ToString(), "Generic");

                        if (res != null && res.Body.ProcessTestDataResult.ToString().ToUpper() != "PASS")
                        {
                            SaveLog(serial, listIndex, NameOfStep);
                        }
                        else
                            SaveCopyLog(serial, listIndex, NameOfStep);

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        SaveLog(serial, listIndex, NameOfStep);
                    }
                    finally
                    {
                        await ws.CloseAsync();

                    }

            }
                else
                    SaveLog(serial, listIndex, NameOfStep);

            


        }

        public static void SaveLog(string serial, int listIndex, string NameOfStep)
        {
            var index = listIndex + 1;
            string indexString = index.ToString();
            if (index <= 9)
                indexString = "0" + indexString;
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

                using (StreamWriter sw = new StreamWriter("C:/tars/" + serial + "_" + $"{listIndex+1}" + "-" + "(" + stop.Day + "-" + stop.Month + "-" + stop.Year + " " + stop.Hour + "-" + stop.Minute + "-" + stop.Second + ")" + ".Tars"))
                {

                    sw.WriteLine($"S{serial}");
                    sw.WriteLine("CEVBOX");
                    sw.WriteLine($"N{System.Environment.MachineName}_{NameOfStep.Substring(NameOfStep.Length - 2)}_s{indexString}");
                    sw.WriteLine($"P{NameOfStep}_s{indexString}");
                    sw.WriteLine("Ooperator");
                    sw.WriteLine("TP");
                    sw.WriteLine("MEventCount");
                    sw.WriteLine($"d{MainWindow.ScrewsList[listIndex].EventCount}");
                    sw.WriteLine("MFasteningTime");
                    sw.WriteLine($"d{MainWindow.ScrewsList[listIndex].FasteningTime}");
                    sw.WriteLine("MPresetNumber");
                    sw.WriteLine($"d{MainWindow.ScrewsList[listIndex].PresetNumber}");
                    sw.WriteLine("MTargetTorque");
                    sw.WriteLine($"d{MainWindow.ScrewsList[listIndex].TargetTorque}");
                    sw.WriteLine("MConvertedTorque");
                    sw.WriteLine($"d{MainWindow.ScrewsList[listIndex].ConvertedTorque}");
                    sw.WriteLine("MTargetSpeed");
                    sw.WriteLine($"d{MainWindow.ScrewsList[listIndex].TargetSpeed}");
                    sw.WriteLine("MA1");
                    sw.WriteLine($"d{MainWindow.ScrewsList[listIndex].A1}");
                    sw.WriteLine("MA2");
                    sw.WriteLine($"d{MainWindow.ScrewsList[listIndex].A2}");
                    sw.WriteLine("MA3");
                    sw.WriteLine($"d{MainWindow.ScrewsList[listIndex].A3}");
                    sw.WriteLine("MScrewCountValue");
                    sw.WriteLine($"d{MainWindow.ScrewsList[listIndex].ScrewCountValue}");
                    sw.WriteLine("MError");
                    sw.WriteLine($"d{MainWindow.ScrewsList[listIndex].Error}");
                    sw.WriteLine("MForwardOrLoosenig");
                    sw.WriteLine($"d{MainWindow.ScrewsList[listIndex].ForwardOrLoosenig}");
                    sw.WriteLine("MStatus");
                    sw.WriteLine($"d{MainWindow.ScrewsList[listIndex].Status}");
                    sw.WriteLine("MSnugTorqueAngle");
                    sw.WriteLine($"d{MainWindow.ScrewsList[listIndex].SnugTorqueAngle}");
                    //Preset values -\/
                    sw.WriteLine("MTCAMorACTM");
                    sw.WriteLine($"d{MainWindow.ScrewsList[listIndex].TCAMorACTM}");
                    sw.WriteLine("MTorque");
                    sw.WriteLine($"d{MainWindow.ScrewsList[listIndex].Torque}");
                    sw.WriteLine("MTorqueMin");
                    sw.WriteLine($"d{MainWindow.ScrewsList[listIndex].TorqueMin}");
                    sw.WriteLine("MTorqueMax");
                    sw.WriteLine($"d{MainWindow.ScrewsList[listIndex].TorqueMax}");
                    sw.WriteLine("MTargetAngle");
                    sw.WriteLine($"d{MainWindow.ScrewsList[listIndex].TargetAngle}");
                    sw.WriteLine("MMinAngle");
                    sw.WriteLine($"d{MainWindow.ScrewsList[listIndex].MinAngle}");
                    sw.WriteLine("MMaxAngle");
                    sw.WriteLine($"d{MainWindow.ScrewsList[listIndex].MaxAngle}");
                    sw.WriteLine("MSnugTorue");
                    sw.WriteLine($"d{MainWindow.ScrewsList[listIndex].SnugTorue}");
                    sw.WriteLine("MSpeed");
                    sw.WriteLine($"d{MainWindow.ScrewsList[listIndex].Speed}");
                    sw.WriteLine("MFreeFasteningAngle");
                    sw.WriteLine($"d{MainWindow.ScrewsList[listIndex].FreeFasteningAngle}");
                    sw.WriteLine("MFreeFasteningSpeed");
                    sw.WriteLine($"d{MainWindow.ScrewsList[listIndex].FreeFasteningSpeed}");
                    sw.WriteLine("MSoftStart");
                    sw.WriteLine($"d{MainWindow.ScrewsList[listIndex].SoftStart}");
                    sw.WriteLine("MSeatingPoint");
                    sw.WriteLine($"d{MainWindow.ScrewsList[listIndex].SeatingPoint}");
                    sw.WriteLine("MTorqueRisingRate");
                    sw.WriteLine($"d{MainWindow.ScrewsList[listIndex].TorqueRisingRate}");
                    sw.WriteLine("MRampUpSpeed");
                    sw.WriteLine($"d{MainWindow.ScrewsList[listIndex].RampUpSpeed}");
                    sw.WriteLine("MTorqueCompensation");
                    sw.WriteLine($"d{MainWindow.ScrewsList[listIndex].TorqueCompensation}");
                    sw.WriteLine("[" + stop.ToString("yyyy-MM-dd HH:mm:ss"));
                    sw.WriteLine("]" + stop.ToString("yyyy-MM-dd HH:mm:ss"));
                    //for (int i = 0; i > 15; i++)
                    //    result[i] = string.Empty;

                }

                string sourceFile = @"C:/tars/" + serial + "_" + $"{listIndex + 1}" + @"-" + @"(" + @stop.Day + @"-" + @stop.Month + @"-" + @stop.Year + @" " + @stop.Hour + @"-" + @stop.Minute + @"-" + @stop.Second + @")" + @".Tars";
                string destinationFile = @"C:/copylogi/" + @stop.Day + @"-" + @stop.Month + @"-" + @stop.Year + @"/" + @serial + "_" + $"{listIndex + 1}" + @"-" + @"(" + @stop.Day + @"-" + @stop.Month + @"-" + @stop.Year + @" " + @stop.Hour + @"-" + @stop.Minute + @"-" + @stop.Second + @")" + @".Tars";

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

        public static void SaveCopyLog(string serial, int listIndex, string NameOfStep)
        {
            var index = listIndex + 1;
            string indexString = index.ToString();
            if (index <= 9)
                indexString = "0" + indexString;
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

                using (StreamWriter sw = new StreamWriter(sciezka + serial + "_" + $"{listIndex + 1}" + "-" + "(" + stop.Day + "-" + stop.Month + "-" + stop.Year + " " + stop.Hour + "-" + stop.Minute + "-" + stop.Second + ")" + ".Tars"))
                {

                    sw.WriteLine($"S{serial}");
                    sw.WriteLine("CEVBOX");
                    sw.WriteLine($"N{System.Environment.MachineName}_{NameOfStep.Substring(NameOfStep.Length - 2)}_s{indexString}");
                    sw.WriteLine($"P{NameOfStep}_s{indexString}");
                    sw.WriteLine("Ooperator");
                    sw.WriteLine("TP");
                    sw.WriteLine("MEventCount");
                    sw.WriteLine($"d{MainWindow.ScrewsList[listIndex].EventCount}");
                    sw.WriteLine("MFasteningTime");
                    sw.WriteLine($"d{MainWindow.ScrewsList[listIndex].FasteningTime}");
                    sw.WriteLine("MPresetNumber");
                    sw.WriteLine($"d{MainWindow.ScrewsList[listIndex].PresetNumber}");
                    sw.WriteLine("MTargetTorque");
                    sw.WriteLine($"d{MainWindow.ScrewsList[listIndex].TargetTorque}");
                    sw.WriteLine("MConvertedTorque");
                    sw.WriteLine($"d{MainWindow.ScrewsList[listIndex].ConvertedTorque}");
                    sw.WriteLine("MTargetSpeed");
                    sw.WriteLine($"d{MainWindow.ScrewsList[listIndex].TargetSpeed}");
                    sw.WriteLine("MA1");
                    sw.WriteLine($"d{MainWindow.ScrewsList[listIndex].A1}");
                    sw.WriteLine("MA2");
                    sw.WriteLine($"d{MainWindow.ScrewsList[listIndex].A2}");
                    sw.WriteLine("MA3");
                    sw.WriteLine($"d{MainWindow.ScrewsList[listIndex].A3}");
                    sw.WriteLine("MScrewCountValue");
                    sw.WriteLine($"d{MainWindow.ScrewsList[listIndex].ScrewCountValue}");
                    sw.WriteLine("MError");
                    sw.WriteLine($"d{MainWindow.ScrewsList[listIndex].Error}");
                    sw.WriteLine("MForwardOrLoosenig");
                    sw.WriteLine($"d{MainWindow.ScrewsList[listIndex].ForwardOrLoosenig}");
                    sw.WriteLine("MStatus");
                    sw.WriteLine($"d{MainWindow.ScrewsList[listIndex].Status}");
                    sw.WriteLine("MSnugTorqueAngle");
                    sw.WriteLine($"d{MainWindow.ScrewsList[listIndex].SnugTorqueAngle}");
                    //Preset values -\/
                    sw.WriteLine("MTCAMorACTM");
                    sw.WriteLine($"d{MainWindow.ScrewsList[listIndex].TCAMorACTM}");
                    sw.WriteLine("MTorque");
                    sw.WriteLine($"d{MainWindow.ScrewsList[listIndex].Torque}");
                    sw.WriteLine("MTorqueMin");
                    sw.WriteLine($"d{MainWindow.ScrewsList[listIndex].TorqueMin}");
                    sw.WriteLine("MTorqueMax");
                    sw.WriteLine($"d{MainWindow.ScrewsList[listIndex].TorqueMax}");
                    sw.WriteLine("MTargetAngle");
                    sw.WriteLine($"d{MainWindow.ScrewsList[listIndex].TargetAngle}");
                    sw.WriteLine("MMinAngle");
                    sw.WriteLine($"d{MainWindow.ScrewsList[listIndex].MinAngle}");
                    sw.WriteLine("MMaxAngle");
                    sw.WriteLine($"d{MainWindow.ScrewsList[listIndex].MaxAngle}");
                    sw.WriteLine("MSnugTorue");
                    sw.WriteLine($"d{MainWindow.ScrewsList[listIndex].SnugTorue}");
                    sw.WriteLine("MSpeed");
                    sw.WriteLine($"d{MainWindow.ScrewsList[listIndex].Speed}");
                    sw.WriteLine("MFreeFasteningAngle");
                    sw.WriteLine($"d{MainWindow.ScrewsList[listIndex].FreeFasteningAngle}");
                    sw.WriteLine("MFreeFasteningSpeed");
                    sw.WriteLine($"d{MainWindow.ScrewsList[listIndex].FreeFasteningSpeed}");
                    sw.WriteLine("MSoftStart");
                    sw.WriteLine($"d{MainWindow.ScrewsList[listIndex].SoftStart}");
                    sw.WriteLine("MSeatingPoint");
                    sw.WriteLine($"d{MainWindow.ScrewsList[listIndex].SeatingPoint}");
                    sw.WriteLine("MTorqueRisingRate");
                    sw.WriteLine($"d{MainWindow.ScrewsList[listIndex].TorqueRisingRate}");
                    sw.WriteLine("MRampUpSpeed");
                    sw.WriteLine($"d{MainWindow.ScrewsList[listIndex].RampUpSpeed}");
                    sw.WriteLine("MTorqueCompensation");
                    sw.WriteLine($"d{MainWindow.ScrewsList[listIndex].TorqueCompensation}");
                    sw.WriteLine("[" + stop.ToString("yyyy-MM-dd HH:mm:ss"));
                    sw.WriteLine("]" + stop.ToString("yyyy-MM-dd HH:mm:ss"));

                }

            }
            catch (IOException iox)
            {
                MessageBox.Show(iox.Message);
            }
        }

    }
}

