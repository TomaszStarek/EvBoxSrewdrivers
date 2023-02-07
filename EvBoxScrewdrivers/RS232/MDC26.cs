using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace EvBoxScrewdrivers
{
    public class MDC26 : ScrewdriverComPort
    {
        public MDC26(string com, TextBox textBox) : base(com, textBox)
        {
        }

        public override void port_DataReceived(object sender, SerialDataReceivedEventArgs rcvdData)
        {

            if (!Port.IsOpen) return;
            Thread.Sleep(125);
            int bytes = Port.BytesToRead;
            byte[] buffer = new byte[bytes];
            Port.Read(buffer, 0, bytes);


            var receiveData2 = BitConverter.ToString(buffer) + System.Environment.NewLine;
            displayTextReadIn(receiveData2);

            ReadFromModbusTcpTighteningParameter();
        }

        private bool ReadFromModbusTcpTighteningParameter()
        {
            int[]? monitoringData = { 0}, presetData = { 0};
            int screwIndex = 0;
            try
            {
                monitoringData = MainWindow.modbusClient.ReadInputRegisters(3200, 14);
                presetData = MainWindow.modbusClient.ReadHoldingRegisters(1, 15);

                screwIndex = monitoringData[9];
                if (screwIndex > MainWindow.ScrewsList.Count)
                {
                    MessageBox.Show($"Błąd zakresu śrub, próbujesz nadać śrubę numer: {screwIndex.ToString()} \n " +
                        $"a zdefiniowana w aplikacji liczba śrub to: {MainWindow.ScrewsList.Count.ToString()}", "Błąd programu", MessageBoxButton.OK, MessageBoxImage.Exclamation, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);

                    return false;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Błąd podczas odczytu zmiennych po ModbusTCP: " + ex, "Błąd ModbusTcp", MessageBoxButton.OK, MessageBoxImage.Exclamation, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
                return false;
            }


            try
            {
                if (MainWindow.NumberOfScrews > screwIndex)
                    screwIndex = MainWindow.NumberOfScrews - screwIndex;

                MainWindow.ScrewsList[screwIndex - 1].EventCount = monitoringData[0];
                MainWindow.ScrewsList[screwIndex - 1].FasteningTime = monitoringData[1];
                MainWindow.ScrewsList[screwIndex - 1].PresetNumber = monitoringData[2];
                MainWindow.ScrewsList[screwIndex - 1].TargetTorque = monitoringData[3];
                MainWindow.ScrewsList[screwIndex - 1].ConvertedTorque = monitoringData[4];
                MainWindow.ScrewsList[screwIndex - 1].TargetSpeed = monitoringData[5];
                MainWindow.ScrewsList[screwIndex - 1].A1 = monitoringData[6];
                MainWindow.ScrewsList[screwIndex - 1].A2 = monitoringData[7];
                MainWindow.ScrewsList[screwIndex - 1].A3 = monitoringData[8];
                MainWindow.ScrewsList[screwIndex - 1].ScrewCountValue = monitoringData[9];
                MainWindow.ScrewsList[screwIndex - 1].Error = monitoringData[10];
                MainWindow.ScrewsList[screwIndex - 1].ForwardOrLoosenig = monitoringData[11];
                MainWindow.ScrewsList[screwIndex - 1].Status = monitoringData[12];
                MainWindow.ScrewsList[screwIndex - 1].SnugTorqueAngle = monitoringData[13];


                MainWindow.ScrewsList[screwIndex - 1].TCAMorACTM = presetData[0];
                MainWindow.ScrewsList[screwIndex - 1].Torque = presetData[1];
                MainWindow.ScrewsList[screwIndex - 1].TorqueMin = presetData[2];
        //        MainWindow.ScrewsList[screwIndex - 1].TorqueMax = presetData[1];
                MainWindow.ScrewsList[screwIndex - 1].TargetAngle = presetData[3];
                MainWindow.ScrewsList[screwIndex - 1].MinAngle = presetData[4];
                MainWindow.ScrewsList[screwIndex - 1].MaxAngle = presetData[5];
                MainWindow.ScrewsList[screwIndex - 1].SnugTorue = presetData[6];
                MainWindow.ScrewsList[screwIndex - 1].Speed = presetData[7];
                MainWindow.ScrewsList[screwIndex - 1].FreeFasteningAngle = presetData[8];
                MainWindow.ScrewsList[screwIndex - 1].FreeFasteningSpeed = presetData[9];
                MainWindow.ScrewsList[screwIndex - 1].SoftStart = presetData[10];
                MainWindow.ScrewsList[screwIndex - 1].SeatingPoint = presetData[11];
                MainWindow.ScrewsList[screwIndex - 1].TorqueRisingRate = presetData[12];
                MainWindow.ScrewsList[screwIndex - 1].RampUpSpeed = presetData[13];
                MainWindow.ScrewsList[screwIndex - 1].TorqueCompensation = presetData[14];

                if(MainWindow.Barcode.Length > 0)
                {
                    var regexString = Regex.Replace(MainWindow.ListOfActivities[MainWindow.CurrentActivity].Name, @"\s+", string.Empty);
                    Save.SaveLog(MainWindow.Barcode, screwIndex - 1, regexString);
                     
                    //Task task2 = Task.Run(() => Save.SendLogMesTisAsync(MainWindow.Barcode, screwIndex - 1, regexString) );
                    //task2.Wait();

                    MainWindow.MyWindow.ChangeLabelOnScrew(screwIndex);
                    MainWindow.MyWindow.CheckJobCompleted();

                }

                return true;

            }
            catch (Exception ex)
            {

                MessageBox.Show("Błąd podczas przypisania zmiennych: " + ex, "Błąd zapisu danych", MessageBoxButton.OK, MessageBoxImage.Exclamation, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
                return false;
            }






        }


    }
}
