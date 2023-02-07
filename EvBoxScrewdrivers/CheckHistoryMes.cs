using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
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

        public static string GetBoardData(string SerialTxt, string client)
        {
            using (MESwebservice.BoardsSoapClient wsMES = new MESwebservice.BoardsSoapClient(MESwebservice.BoardsSoapClient.EndpointConfiguration.BoardsSoap12))
            {
                try
                {
                    var boardData = wsMES.GetBoardData(@client, SerialTxt);

                    boardData[1] = boardData[1].Remove(0, 7);

                    return boardData[1];
                }
                catch
                {
                    return "Błąd połączenia";
                }
            }
        }

        private static string GetBoardAssemblyID(string SerialTxt, string client)
        {
            using (MESwebservice.BoardsSoapClient wsMES = new MESwebservice.BoardsSoapClient(MESwebservice.BoardsSoapClient.EndpointConfiguration.BoardsSoap12))
            {
                try
                {
                    var boardData = wsMES.GetBoardData(@client, SerialTxt);

                    boardData[0] = boardData[0].Remove(0, 12);

                    return boardData[0];
                }
                catch
                {
                    return "Błąd połączenia";
                }
            }
        }

        public static string VerifySetupSheetDS(string AssemblyID, string RouteId)
        {

            using (MESwebservice.BoardsSoapClient wsMES = new MESwebservice.BoardsSoapClient(MESwebservice.BoardsSoapClient.EndpointConfiguration.BoardsSoap))
            {

                try
                {

                    var res = wsMES.VerifySetupSheetDS(@AssemblyID, @RouteId, "", "1");  //24671 top // 24670 bottom

                    if (res is null)
                        return "";
                    else
                        ;


                    var tableName = res.Nodes.Last().Value.ToString().ToUpper();
                    //.Tables[0].TableName;
                    //     var tableName2 = res.Nodes.Last().FirstNode.ToString();

                    //    var fdg = res.Nodes[1].FirstAttribute.Parent.Name;//.Rows[0].Field<string>("Column1");
                    //if (tableName.Equals("Table1"))
                    //    return res.Tables[0].Rows[0].Field<string>("Column1");
                    //else
                    //    return tableName;
                    return tableName;
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }
                return "";
            }
        }
        public static bool CheckSetupSheet(string SerialNumber, string RouteId)
        {
            var AssemblyID = CheckHistoryMes.GetBoardAssemblyID(SerialNumber, "evbox");
            string? VerifySetupSheetDSResult;
            if (AssemblyID.Length >= 5)
                VerifySetupSheetDSResult = CheckHistoryMes.VerifySetupSheetDS(@AssemblyID, @RouteId);
            else
                return false;

            if (VerifySetupSheetDSResult.ToUpper().Equals("VERIFIED"))
                return true;
            else
                return false;
        }



        //public static int CheckTestResultFVT(string SerialTxt)
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
        public static double CheckTackTime(string SerialTxt)
        {
            using (wsTis.MES_TISSoapClient ws = new wsTis.MES_TISSoapClient(wsTis.MES_TISSoapClient.EndpointConfiguration.MES_TISSoap))
            {
                try
                {
                    var res = ws.GetLastTestResult(SerialTxt, @"evbox", "", "DISPLAY_ASSEMBLY");

                    var resd = res.Split('\n');

                    double minutesDiff = 0;

                    foreach (string line in resd)
                    {
                        if (line.Contains("<StartTime>"))
                        {
                            try
                            {
                                DateTime ts = DateTime.Parse(line.Substring(15, 25));
                                minutesDiff = (DateTime.Now - ts).TotalMinutes;

                             //   MessageBox.Show($"{line} --> {ts.ToString("c")}");

                            }
                            catch (FormatException)
                            {
                                MessageBox.Show("{0}: Bad Format", line);
                            }
                            catch (OverflowException)
                            {
                                MessageBox.Show("{0}: Overflow", line);
                            }
                        }
                    }


                        return minutesDiff;



                }
                catch (Exception ex)
                {
                    return 0;
                }

            }
        }

    }
}
