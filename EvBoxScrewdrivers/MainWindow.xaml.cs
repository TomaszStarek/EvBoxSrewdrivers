using EasyModbus;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace EvBoxScrewdrivers
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {
        private MDC26 _mdc26;
        public static int CurrentActivity;
        public static ModbusClient modbusClient { get; set; }
        public static bool Model2Selection { get; set; } = false;

        public static int NumberOfScrews = 0;

        public static List<Tightening>  ScrewsList = new List<Tightening>();
        public static List<Activity> ListOfActivities;
        public static string Barcode { get; private set; } = "";

        public static MainWindow MyWindow { get; private set; }

        public static string CheckpointToCheck;

        public static bool CheckTackTime = false;

        private void AddActivities()
        {

            ListOfActivities = new List<Activity>();

            ////////var a = new Activity() { Name = "Przykręcanie radiatora", CountOfScrews = 6 };
            ////////var b = new Activity() { Name = "Przykręcanie śruby PE", CountOfScrews = 2 };
            ////////var c = new Activity() { Name = "kokokokokoko", CountOfScrews = 4 };

            ////////ListOfActivities.Add(a);
            ////////ListOfActivities.Add(b);
            ////////ListOfActivities.Add(c);


            //using (TextWriter tw = new StreamWriter("SavedList.txt"))
            //{
            //    foreach (String s in ListOfActivities.verblist)
            //        tw.WriteLine(s);
            //}

            switch (System.Environment.MachineName)
            {
                case "PLKWIM0T21B4S01":
                    ListOfActivities = new List<Activity>()
                    {
                        new Activity() { Name = "SCREW_BB1_P1", CountOfScrews = 6},
                        new Activity() { Name = "SCREW_BB1_P2", CountOfScrews = 1 }
                    };
                    CheckpointToCheck = "SCREW_BB1_1";
                    break;
                case "PLKWIM0T21B4S02":
                    ListOfActivities = new List<Activity>()
                    {
                        new Activity() { Name = "SCREW_BB2_P1", CountOfScrews = 2},
                        new Activity() { Name = "SCREW_BB2_P2", CountOfScrews = 1 }
                    };
                    CheckpointToCheck = "SCREW_BB2_1";
                    break;
                case "PLKWIM0T21B4S03":
                    ListOfActivities = new List<Activity>()
                    {
                        new Activity() { Name = "SCREW_BB3_P1", CountOfScrews = 2},
                        new Activity() { Name = "SCREW_BB3_P2", CountOfScrews = 2 },
                        new Activity() { Name = "SCREW_BB3_P3", CountOfScrews = 4 }
                    };
                    CheckpointToCheck = "SCREW_BB3_1";
                    break;
                case "PLKWIM0T21B4S04":
                    ListOfActivities = new List<Activity>()
                    {
                        new Activity() { Name = "SCREW_BB4_P1", CountOfScrews = 2},
                        new Activity() { Name = "SCREW_BB4_P2", CountOfScrews = 1 }
                    };
                    CheckpointToCheck = "SCREW_BB4_1";
                    break;
                case "PLKWIM0T21B4S05":
                    ListOfActivities = new List<Activity>()
                    {
                        new Activity() { Name = "SCREW_BB5_P1", CountOfScrews = 14},
                        new Activity() { Name = "SCREW_BB5_P2", CountOfScrews = 2 }
                    };
                    CheckpointToCheck = "SCREW_BB5_1";
                    break;
                case "PLKWIM0T21EVB06":
                    ListOfActivities = new List<Activity>()
                    {
                        new Activity() { Name = "SCREW_DISPLAY_P1", CountOfScrews = 8},
                    };
                    CheckpointToCheck = "DISPLAY_SCREW_1";
                    CheckTackTime = true;
                    break;
                case "PLKWIM0T21EVB02":
                    ListOfActivities = new List<Activity>()
                    {
                        new Activity() { Name = "SCREW_SOCKET_P1", CountOfScrews = 4},
                        new Activity() { Name = "SCREW_SOCKET_P2", CountOfScrews = 2},
                        new Activity() { Name = "SCREW_SOCKET_P2", CountOfScrews = 1}
                    };
                    CheckpointToCheck = "SCREW_SOCKET_1";
                    break;
                default:
                    ListOfActivities = new List<Activity>()
                    {
                        new Activity() { Name = "unknown", CountOfScrews = 14},
                        new Activity() { Name = "unknown", CountOfScrews = 0 }
                    };
                    CheckpointToCheck = "SCREW_BB1_1";
                    break;
            }


            //try
            //{
            //    List<string> lines = new List<string>();

            //    if (File.Exists(@"tasks.txt"))       //sprawdzanie czy sciezka istnieje
            //    {
            //        lines = System.IO.File.ReadLines("tasks.txt").ToList();
            //    }
            //    else
            //    {
            //        MessageBox.Show("Nie znaleziono pliku z zadaniami: tasks.txt\nPodaj w nim nazwę zadania i w nowej linii ilość śrub do wkręcenia",
            //            "Błąd odczytu pliku", MessageBoxButton.OK, MessageBoxImage.Exclamation, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            //        Environment.Exit(0);
            //    }



            //    for (int i = 0; i < lines.Count; i += 2)
            //    {
            //        int c = 0;
            //        Int32.TryParse(lines[i + 1], out c);

            //        if(c >0 && lines[i].Length > 3)
            //        {
            //            var a = new Activity() { Name = lines[i], CountOfScrews = c };
            //            ListOfActivities.Add(a);
            //        }

            //    }
            //}
            //catch (Exception ex)
            //{

            //    MessageBox.Show("Błąd odzczytu listy zadań\n" + ex, "Błąd odczytu pliku", MessageBoxButton.OK, MessageBoxImage.Exclamation, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            //}




        }


        public MainWindow()
        {
            InitializeComponent();

            AddActivities();

            CurrentActivity = 0;
            //NumberOfScrews = ListOfActivities[_currentActivity].CountOfScrews;

            MyWindow = this;

            //NumberOfScrews = 12;
            //for (int i = 0; i < NumberOfScrews; i++)
            //{
            //    ScrewsList.Add(new Tightening());
            //}

            StartSetLabels();
         //   Dispatcher.Invoke(new Action(() => ___a2fiflavrt4f9amsalsy_png.Opacity = 0.15));

            try
            {
                modbusClient = new ModbusClient("192.168.1.100", 5000);
                modbusClient.ReceiveDataChanged += new EasyModbus.ModbusClient.ReceiveDataChangedHandler(UpdateReceiveData);
                modbusClient.Connect();
                modbusClient.WriteSingleRegister(4001, 1);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd podczas nawiązywania połączenia ModbusTCP: " + ex, "Błąd ModbusTcp", MessageBoxButton.OK, MessageBoxImage.Exclamation, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }


            _mdc26 = new MDC26("COM3", textBoxRS);


        }

        private static string _receiveData = null;

        void UpdateReceiveData(object sender)
        {
            _receiveData = BitConverter.ToString(modbusClient.receiveData) + System.Environment.NewLine; //.Replace("-", " ")
            Dispatcher.Invoke(new Action(() => textBoxTCP.Text = _receiveData));
            //Thread thread = new Thread(updateReceiveTextBox);
            //thread.Start();

        }

        private void OnKeyDownHandlerBarcode(object sender, KeyEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (e.Key == Key.Return)
            {
                if (CheckTackTime)
                {
                    var buforTimeInMinutes = CheckHistoryMes.CheckTackTime(textBox.Text);

                    var IsDataSheetVerified = CheckHistoryMes.CheckTackTime(textBox.Text);

                    if (buforTimeInMinutes > 48 * 60)  // DIS230200059  
                    {
                        if (!CheckHistoryMes.CheckSetupSheet(textBox.Text, "14935"))
                        {
                            Dispatcher.Invoke(new Action(() => labelStatusInfo.Content = $"Błąd weryfikacji SetupSheet"));
                            Dispatcher.Invoke(new Action(() => labelStatusInfo.Background = System.Windows.Media.Brushes.IndianRed));
                            Dispatcher.Invoke(new Action(() => textBox.Text = String.Empty));
                            return;
                        }
                        else
                            Save.SaveGrn(textBox.Text);

                    }
                    else if (buforTimeInMinutes == 0)
                    {
                        Dispatcher.Invoke(new Action(() => labelStatusInfo.Content = $"Nieprawidłowy czasu buforu!\n Błąd zapytania"));
                        Dispatcher.Invoke(new Action(() => labelStatusInfo.Background = System.Windows.Media.Brushes.IndianRed));
                        Dispatcher.Invoke(new Action(() => textBox.Text = String.Empty));
                        return;
                    }
                    else
                    {
                        Dispatcher.Invoke(new Action(() => labelStatusInfo.Content = $"Nieprawidłowy czasu buforu!\n Pozostało do odczekania: {Math.Round((2880 - buforTimeInMinutes), 1)} minut"));
                        Dispatcher.Invoke(new Action(() => labelStatusInfo.Background = System.Windows.Media.Brushes.IndianRed));
                        Dispatcher.Invoke(new Action(() => textBox.Text = String.Empty));
                        return;
                    }

                }
            



                
                   

                             //   if(textBox.Text.Length > 5)
                             //  if (CheckHistoryMes.CheckSerialNumberByCheckpointEPS(textBox.Text, "evbox", CheckpointToCheck).ToUpper().Equals("TRUE"))
                if (textBox.Text.Length > 5)
                {
                    
                   
                    textBox.Text = Regex.Replace(textBox.Text, @"\s+", string.Empty);
                    Barcode = textBox.Text;

                    GetActivities.GetBoardDataAndChooseActivitiesToDo(Barcode, ChargerImage);
                    ScrewsList.Clear();
                    //if(NumberOfScrews > 0)
                    //    Dispatcher.Invoke(new Action(() => ___a2fiflavrt4f9amsalsy_png.Opacity = 0.03));
                    //else
                    //    Dispatcher.Invoke(new Action(() => ___a2fiflavrt4f9amsalsy_png.Opacity = 0.15));

                    for (int i = 0; i < NumberOfScrews; i++)
                    {
                        ScrewsList.Add(new Tightening());
                    }

                    StartSetLabels();
                    try
                    {
                          modbusClient.WriteSingleRegister(4001, 0);
                        //                  modbusClient.WriteSingleRegister(4004, 1);
                        modbusClient.WriteSingleRegister(4007, 1);

                        if(!Model2Selection)
                        {
                            modbusClient.WriteSingleRegister(4006, 63);
                            modbusClient.WriteSingleRegister(4008, 2);
                            Thread.Sleep(500);
                            modbusClient.WriteSingleRegister(4008, 1);
                        }
                        else
                        {
                            modbusClient.WriteSingleRegister(4006, 62 + 64);
                            modbusClient.WriteSingleRegister(4008, 1);
                            Thread.Sleep(500);
                            modbusClient.WriteSingleRegister(4008, 2);
                        }

                        modbusClient.WriteSingleRegister(4007, 0);


                    }
                    catch (Exception ex)
                    {
                        Dispatcher.Invoke(new Action(() => textBox.Text = String.Empty));
                        Dispatcher.Invoke(new Action(() => labelStatusInfo.Content = "Błąd przy wysłaniu komendy odblokowania wkrętaka! \n Zeskanuj produkt jeszcze raz!"));
                        return;
                    }

                    Dispatcher.Invoke(new Action(() => labelStatusInfo.Content = $"{ListOfActivities[CurrentActivity].Name}\nWkręć śruby!"));
                    Dispatcher.Invoke(new Action(() => labelStatusInfo.Background = System.Windows.Media.Brushes.Yellow));
                    Dispatcher.Invoke(new Action(() => textBox.IsEnabled = false));
                }
                else
                {
                    Dispatcher.Invoke(new Action(() => labelStatusInfo.Content = "Podaj poprawny barkod!"));
                    Dispatcher.Invoke(new Action(() => labelStatusInfo.Background = System.Windows.Media.Brushes.IndianRed));
                    Dispatcher.Invoke(new Action(() => textBox.Text = String.Empty));
                }

            }
        }

        private void StartSetLabels()
        {

            for (int i = 1; i <= 14; i++)
            {
                string labelName = "";
                Label label = null;
                Dispatcher.Invoke(new Action(() => labelName = string.Format("labelScrew{0}", i) ));
                Dispatcher.Invoke(new Action(() => label = (Label)this.FindName(labelName) ));

                if (NumberOfScrews >= i )
                {
                    Dispatcher.Invoke(new Action(() => label.Visibility = Visibility.Visible));
                    Dispatcher.Invoke(new Action(() => label.Content = $"Śruba {i} czeka na wkręcenie"));
                    Dispatcher.Invoke(new Action(() => label.Background = Brushes.LightGray));
                    
                }
                else
                {
                    Dispatcher.Invoke(new Action(() => label.Visibility = Visibility.Hidden));
                }

            }
            Dispatcher.Invoke(new Action(() => textBoxBarcode.Focus()));

        }
        public void ChangeLabelOnScrew(int screwNumber)
        {

            switch (screwNumber)
            {
                case 1:
                    Dispatcher.Invoke(new Action(() => labelScrew1.Content = "Śruba 1 OK"));
                    Dispatcher.Invoke(new Action(() => labelScrew1.Background = Brushes.LawnGreen));
                    break;
                case 2:
                    Dispatcher.Invoke(new Action(() => labelScrew2.Content = "Śruba 2 OK"));
                    Dispatcher.Invoke(new Action(() => labelScrew2.Background = Brushes.LawnGreen));
                    break;
                case 3:
                    Dispatcher.Invoke(new Action(() => labelScrew3.Content = "Śruba 3 OK"));
                    Dispatcher.Invoke(new Action(() => labelScrew3.Background = Brushes.LawnGreen)); ;
                    break;
                case 4:
                    Dispatcher.Invoke(new Action(() => labelScrew4.Content = "Śruba 4 OK"));
                    Dispatcher.Invoke(new Action(() => labelScrew4.Background = Brushes.LawnGreen));
                    break;
                case 5:
                    Dispatcher.Invoke(new Action(() => labelScrew5.Content = "Śruba 5 OK"));
                    Dispatcher.Invoke(new Action(() => labelScrew5.Background = Brushes.LawnGreen));
                    break;
                case 6:
                    Dispatcher.Invoke(new Action(() => labelScrew6.Content = "Śruba 6 OK"));
                    Dispatcher.Invoke(new Action(() => labelScrew6.Background = Brushes.LawnGreen));
                    break;
                case 7:
                    Dispatcher.Invoke(new Action(() => labelScrew7.Content = "Śruba 7 OK"));
                    Dispatcher.Invoke(new Action(() => labelScrew7.Background = Brushes.LawnGreen));
                    break;
                case 8:
                    Dispatcher.Invoke(new Action(() => labelScrew8.Content = "Śruba 8 OK"));
                    Dispatcher.Invoke(new Action(() => labelScrew8.Background = Brushes.LawnGreen));
                    break;
                case 9:
                    Dispatcher.Invoke(new Action(() => labelScrew9.Content = "Śruba 9 OK"));
                    Dispatcher.Invoke(new Action(() => labelScrew9.Background = Brushes.LawnGreen));
                    break;
                case 10:
                    Dispatcher.Invoke(new Action(() => labelScrew10.Content = "Śruba 10 OK"));
                    Dispatcher.Invoke(new Action(() => labelScrew10.Background = Brushes.LawnGreen));
                    break;
                case 11:
                    Dispatcher.Invoke(new Action(() => labelScrew11.Content = "Śruba 11 OK"));
                    Dispatcher.Invoke(new Action(() => labelScrew11.Background = Brushes.LawnGreen));
                    break;
                case 12:
                    Dispatcher.Invoke(new Action(() => labelScrew12.Content = "Śruba 12 OK"));
                    Dispatcher.Invoke(new Action(() => labelScrew12.Background = Brushes.LawnGreen));
                    break;
                case 13:
                    Dispatcher.Invoke(new Action(() => labelScrew13.Content = "Śruba 13 OK"));
                    Dispatcher.Invoke(new Action(() => labelScrew13.Background = Brushes.LawnGreen));
                    break;
                case 14:
                    Dispatcher.Invoke(new Action(() => labelScrew14.Content = "Śruba 14 OK"));
                    Dispatcher.Invoke(new Action(() => labelScrew14.Background = Brushes.LawnGreen));
                    break;
                default:
                    break;

            }
                       
        }
        public void CheckJobCompleted()
        {
            int i = 0;
            foreach (var item in ScrewsList)
            {
                if (item.Status == 1)
                    i++;
            }
            if (i == NumberOfScrews)
                ScrewingComplete();
        }
        public void ScrewingComplete()
        {

            CurrentActivity++;

            if(ListOfActivities.Count > CurrentActivity) 
            {
                NumberOfScrews = ListOfActivities[CurrentActivity].CountOfScrews;

                ScrewsList.Clear();
                for (int i = 0; i < NumberOfScrews; i++)
                {
                    ScrewsList.Add(new Tightening());
                }

                Dispatcher.Invoke(new Action(() => labelStatusInfo.Content = $"{ListOfActivities[CurrentActivity].Name}\nWkręć śruby!"));             
                //  Dispatcher.Invoke(new Action(() => labelStatusInfo.Background = System.Windows.Media.Brushes.LawnGreen));
                
                StartSetLabels();
            }
            else
            {
                if (CheckTackTime)
                    Save.SaveQC2(Barcode);
                if (modbusClient.Connected)
                    modbusClient.WriteSingleRegister(4001, 1);
                CurrentActivity = 0;
                NumberOfScrews = ListOfActivities[CurrentActivity].CountOfScrews;
                StartSetLabels();
                Dispatcher.Invoke(new Action(() => labelStatusInfo.Content = "Wkręcanie zakończone pomyślnie\nZeskanuj kolejny produkt!"));
                Dispatcher.Invoke(new Action(() => labelStatusInfo.Background = System.Windows.Media.Brushes.LawnGreen));
                Barcode = "";
                Dispatcher.Invoke(new Action(() => textBoxBarcode.IsEnabled = true));
                Dispatcher.Invoke(new Action(() => textBoxBarcode.Text = string.Empty));
                Dispatcher.Invoke(new Action(() => textBoxBarcode.Focus()));
            }



        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                if (modbusClient.Connected)
                    modbusClient.Disconnect();
            }
            catch (Exception)
            {

                ;
            }

        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if(modbusClient.Connected)
            {
                modbusClient.WriteSingleRegister(4001, 0);

                //modbusClient.WriteSingleRegister(4005, 1);
                //Thread.Sleep(1000);
                //modbusClient.WriteSingleRegister(4005, 0);
                modbusClient.WriteSingleRegister(4007, 1);
                if (!Model2Selection)
                {
                    modbusClient.WriteSingleRegister(4006, 63);
                    modbusClient.WriteSingleRegister(4008, 2);
                    Thread.Sleep(500);
                    modbusClient.WriteSingleRegister(4008, 1);
                }
                else
                {
                    modbusClient.WriteSingleRegister(4006, 62 + 64);
                    modbusClient.WriteSingleRegister(4008, 1);
                    Thread.Sleep(500);
                    modbusClient.WriteSingleRegister(4008, 2);
                }

                modbusClient.WriteSingleRegister(4007, 0);

            }
            else
                MessageBox.Show("Brak połączenia ze sterownikiem, ZAMKNIJ APLIKACJĘ I OTWÓRZ JĄ PONOWNIE! ", "Błąd ModbusTcp", MessageBoxButton.OK, MessageBoxImage.Exclamation, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);


            ScrewsList.Clear();
            for (int i = 0; i < NumberOfScrews; i++)
            {
                ScrewsList.Add(new Tightening());
            }
            Barcode = String.Empty;
            
            Dispatcher.Invoke(new Action(() => labelStatusInfo.Content = "Podaj barkod!"));
            Dispatcher.Invoke(new Action(() => labelStatusInfo.Background = System.Windows.Media.Brushes.LightSteelBlue));
            Dispatcher.Invoke(new Action(() => textBoxBarcode.Text = String.Empty));
            Dispatcher.Invoke(new Action(() => textBoxBarcode.IsEnabled = true));
            CurrentActivity = 0;
            NumberOfScrews = ListOfActivities[CurrentActivity].CountOfScrews;
            StartSetLabels();

            try
            {
                if (modbusClient.Connected)
                    modbusClient.Disconnect();
                Thread.Sleep(250);
                modbusClient = new ModbusClient("192.168.1.100", 5000);
                modbusClient.ReceiveDataChanged += new EasyModbus.ModbusClient.ReceiveDataChangedHandler(UpdateReceiveData);
                modbusClient.Connect();
                modbusClient.WriteSingleRegister(4001, 1);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd podczas nawiązywania połączenia ModbusTCP: " + ex, "Błąd ModbusTcp", MessageBoxButton.OK, MessageBoxImage.Exclamation, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }

        }

        private void labelScrew_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

            Label label = (Label)sender;

            if(label.Background == Brushes.LawnGreen)
            {
                var labelNumber = label.Name.Substring(10);

                int indexOfListToDisplay = 0;

                bool success = int.TryParse(labelNumber, out indexOfListToDisplay);

                if(success && indexOfListToDisplay > 0)
                {
                    ViewElementOfList win2 = new ViewElementOfList(indexOfListToDisplay - 1);
                    win2.Show();
                }


            }

        }
    }
}
