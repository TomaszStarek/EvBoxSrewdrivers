using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EvBoxScrewdrivers
{
    public static class CheckHistoryMes
    {
        private enum MesResult
        {
            Pass,
            STEP_MISS,
            FAIL,
            CONNECTION_FAIL
        }

        public static string CheckSerialNumberByCheckpointEPS(string SerialTxt, string client, string step)
        {
            using (MESwebservice.BoardsSoapClient wsMES = new MESwebservice.BoardsSoapClient(MESwebservice.BoardsSoapClient.EndpointConfiguration.BoardsSoap12))
            {
                try
                {
                    return wsMES.CheckSerialNumberByCheckpointEPS(@client, @step, SerialTxt);
                }
                catch
                {
                    return "Błąd połączenia";
                }
            }
        }



        //public static int CheckTackTime(string SerialTxt)
        //{
        //    using (wsTis.MES_TISSoapClient ws = new wsTis.MES_TISSoapClient(wsTis.MES_TISSoapClient.EndpointConfiguration.MES_TISSoap))
        //    {
        //        try
        //        {
        //            var res = ws.GetLastTestResult(SerialTxt, @"TRILLIANT", "", "FVT");

        //            if (res.Contains("<TestStatus>P</TestStatus>"))
        //                return (int)MesResult.Pass;
        //            else if (res.Contains("<TestStatus>F</TestStatus>"))
        //                return (int)MesResult.FAIL;
        //            else
        //                return (int)MesResult.STEP_MISS;
        //        }
        //        catch (Exception ex)
        //        {
        //            return (int)MesResult.CONNECTION_FAIL;
        //        }

        //    }
        //}
        //public static int CheckTestResultFVT(string SerialTxt)
        //{
        //    using (wsTis.MES_TISSoapClient ws = new wsTis.MES_TISSoapClient(wsTis.MES_TISSoapClient.EndpointConfiguration.MES_TISSoap))
        //    {
        //        try
        //        {
        //            var res = ws.GetLastTestResult(SerialTxt, @"TRILLIANT", "", "FVT");

        //            var resd = res.Split('\n');

        //            foreach (string line in resd)
        //            {
        //                if(line.Contains("<StartTime>"))
        //                {
        //                    try
        //                    {
        //                        DateTime ts = DateTime.Parse(line.Substring(15,25));
        //                        var minutesDiff = (DateTime.Now - ts).TotalMinutes;

        //                        MessageBox.Show($"{line} --> {ts.ToString("c")}");
        //                    }
        //                    catch (FormatException)
        //                    {
        //                        MessageBox.Show("{0}: Bad Format", line);
        //                    }
        //                    catch (OverflowException)
        //                    {
        //                        MessageBox.Show("{0}: Overflow", line);
        //                    }
        //                }
        //            }

        //            if (res.Contains("<TestStatus>P</TestStatus>"))
        //                return (int)MesResult.Pass;
        //            else if (res.Contains("<TestStatus>F</TestStatus>"))
        //                return (int)MesResult.FAIL;
        //            else
        //                return (int)MesResult.STEP_MISS;
        //        }
        //        catch (Exception ex)
        //        {
        //            return (int)MesResult.CONNECTION_FAIL;
        //        }

        //    }
        //}

    }
}
