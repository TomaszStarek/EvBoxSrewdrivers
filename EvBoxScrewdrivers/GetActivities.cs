using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace EvBoxScrewdrivers
{
    public static class GetActivities
    {
        public static string LastProductNumber { get; set; } = "";

        private static void SelectModel1orModel2(string productNumber)
        {
            switch (System.Environment.MachineName)
            {
                case "PLKWIM0T21B4S03":
                    if(productNumber.ToUpper().Equals("Q332S2DEC8M-MBS") ||
                        productNumber.ToUpper().Equals("Q332S2EUC80-CHS") ||
                            productNumber.ToUpper().Equals("Q332S2DEC00-CHS") ||
                                productNumber.ToUpper().Equals("Q332S2EUC00-CHS") ||
                                    productNumber.ToUpper().Equals("Q316S2EUC80-CHS")
                       )
                    {
                        MainWindow.Model2Selection = true;
                    }
                    else
                        MainWindow.Model2Selection = false;
                    break;
                case "PLKWIM0T21B4S05":
                    if (productNumber.ToUpper().Equals("Q31662EUC00-CHS") ||
                            productNumber.ToUpper().Equals("Q33262EUC00-CHS") ||      //Q332S2DEC8M-MBS
                                productNumber.ToUpper().Equals("Q332S2DEC00-CHS") ||
                                productNumber.ToUpper().Equals("Q13262EUC00-CHS")
                        )
                    {
                        MainWindow.Model2Selection = true;
                    }
                    else
                        MainWindow.Model2Selection = false;
                    break;

                default:
                    break;
            }
        }
        private static void ChooseChargerImage(string productNumber, System.Windows.Controls.Image image)
        {
            var nameOfImage = "";
            switch (productNumber)
            {//Q332S2EUC80-CHS               //Q332S2DEC8M-MBS
                case "Q332S2DEC8M-MBS":
                    nameOfImage = "ładowarka2_bez kabla.png";
                    break;
                case "Q332S2EUC80-CHS":
                    nameOfImage = "ładowarka2_bez kabla.png";
                    break;
                case "Q332S2DEC00-CHS":
                    nameOfImage = "ładowarka2_bez kabla.png";
                    break;
                case "Q332S2EUC00-CHS":
                    nameOfImage = "ładowarka2_bez kabla.png";
                    break;
                case "Q316S2EUC80-CHS":
                    nameOfImage = "ładowarka2_bez kabla.png";
                    break;
                default:
                    nameOfImage = "ładowarka1.png";
                    break;
            }
            try
            {
                Application.Current.Dispatcher.BeginInvoke(
                    DispatcherPriority.Background,
                    new Action(() => {
                    image.Source = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory
                    + nameOfImage, UriKind.Absolute));
                    }));
            }
            catch (Exception)
            {
                ;
            }

        }
        public static void GetBoardDataAndChooseActivitiesToDo(string serialNumber, System.Windows.Controls.Image image)
        {
            var productNumber = CheckHistoryMes.GetBoardData(serialNumber, "evbox");

            if(productNumber.ToUpper().Equals(LastProductNumber.ToUpper()))
                return;
            else
                LastProductNumber = productNumber;

            ChooseChargerImage(productNumber, image);
            SelectModel1orModel2(productNumber);
            //Dispatcher.Invoke(new Action(() => image.Source = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "ładowarka2.png", UriKind.Absolute))));

            MainWindow.ListOfActivities = new List<Activity>();

            switch (System.Environment.MachineName)
            {
                case "PLKWIM0T21B4S01":
                    if (productNumber.ToUpper().Equals("Q31662EUC00-CHS"))
                    {
                        MainWindow.ListOfActivities = new List<Activity>()
                    {
                        new Activity() { Name = "SCREW_BB1_P1", CountOfScrews = 6},
                        new Activity() { Name = "SCREW_BB1_P2", CountOfScrews = 1 }
                    };
                        MainWindow.CheckpointToCheck = "SCREW_BB1_1";
                    }
                    else
                    {
                        MainWindow.ListOfActivities = new List<Activity>()
                    {
                        new Activity() { Name = "SCREW_BB1_P1", CountOfScrews = 6},
                        new Activity() { Name = "SCREW_BB1_P2", CountOfScrews = 1 }
                    };
                        MainWindow.CheckpointToCheck = "SCREW_BB1_1";
                    }

                    break;
                case "PLKWIM0T21B4S02":
                    MainWindow.ListOfActivities = new List<Activity>()
                    {
                        new Activity() { Name = "SCREW_BB2_P1", CountOfScrews = 2},
                        new Activity() { Name = "SCREW_BB2_P2", CountOfScrews = 1 }
                    };
                    MainWindow.CheckpointToCheck = "SCREW_BB2_1";
                    break;
                case "PLKWIM0T21B4S03":
                    if (productNumber.ToUpper().Equals("Q332S2DEC8M-MBS") ||
                            productNumber.ToUpper().Equals("Q332S2EUC80-CHS") || //Q332S2DEC00-CHS
                                productNumber.ToUpper().Equals("Q332S2DEC00-CHS") ||
                                    productNumber.ToUpper().Equals("Q332S2EUC00-CHS") ||
                                        productNumber.ToUpper().Equals("Q316S2EUC80-CHS"))
                    {
                        MainWindow.ListOfActivities = new List<Activity>()
                        {
                            new Activity() { Name = "SCREW_BB3_P1", CountOfScrews = 4},
                        };
                    }
                    else
                    {
                        MainWindow.ListOfActivities = new List<Activity>()
                        {
                            new Activity() { Name = "SCREW_BB3_P1", CountOfScrews = 2},
                            new Activity() { Name = "SCREW_BB3_P2", CountOfScrews = 2 },
                            new Activity() { Name = "SCREW_BB3_P3", CountOfScrews = 4 }
                        };
                    }
                    MainWindow.CheckpointToCheck = "SCREW_BB3_1";
                    break;
                case "PLKWIM0T21B4S04":
                    MainWindow.ListOfActivities = new List<Activity>()
                    {
                        new Activity() { Name = "SCREW_BB4_P1", CountOfScrews = 2},
                        new Activity() { Name = "SCREW_BB4_P2", CountOfScrews = 1 }
                    };
                    MainWindow.CheckpointToCheck = "SCREW_BB4_1";
                    break;
                case "PLKWIM0T21B4S05":
                    if (productNumber.ToUpper().Equals("Q31662EUC00-CHS") ||
                            productNumber.ToUpper().Equals("Q33262EUC00-CHS") || //Q332S2DEC00-CHS
                                productNumber.ToUpper().Equals("Q332S2DEC00-CHS") ||  //Q13262EUC00-CHS
                                productNumber.ToUpper().Equals("Q13262EUC00-CHS")
                                )
                    {
                        MainWindow.ListOfActivities = new List<Activity>()
                        {
                            new Activity() { Name = "SCREW_BB5_P1", CountOfScrews = 12},
                            new Activity() { Name = "SCREW_BB5_P2", CountOfScrews = 2 }
                        };
                        MainWindow.CheckpointToCheck = "SCREW_BB5_1";
                    }
                    else //                        productNumber.ToUpper().Equals("Q332S2EUC80-CHS"))
                    {
                        MainWindow.ListOfActivities = new List<Activity>()
                        {
                            new Activity() { Name = "SCREW_BB5_P1", CountOfScrews = 14},
                            new Activity() { Name = "SCREW_BB5_P2", CountOfScrews = 2 }
                        };
                        MainWindow.CheckpointToCheck = "SCREW_BB5_1";
                    }
                    break;
                case "PLKWIM0T21EVB06":
                    MainWindow.ListOfActivities = new List<Activity>()
                    {
                        new Activity() { Name = "SCREW_DISPLAY_P1", CountOfScrews = 8},
                    };
                    MainWindow.CheckpointToCheck = "DISPLAY_SCREW_1";
                    MainWindow.CheckTackTime = true;
                    break;
                case "PLKWIM0T21EVB02":
                    MainWindow.ListOfActivities = new List<Activity>()
                    {
                        new Activity() { Name = "SCREW_SOCKET_P1", CountOfScrews = 4},
                        new Activity() { Name = "SCREW_SOCKET_P2", CountOfScrews = 2},
                        new Activity() { Name = "SCREW_SOCKET_P2", CountOfScrews = 1}
                    };
                    MainWindow.CheckpointToCheck = "SCREW_SOCKET_1";
                    break;
                default:
                    MainWindow.ListOfActivities = new List<Activity>()
                    {
                        new Activity() { Name = "unknown", CountOfScrews = 0},
                        new Activity() { Name = "unknown", CountOfScrews = 0 }
                    };
                    MainWindow.CheckpointToCheck = "SCREW_BB1_1";
                    break;
            }

            MainWindow.NumberOfScrews = MainWindow.ListOfActivities[MainWindow.CurrentActivity].CountOfScrews;
            //for (int i = 0; i < MainWindow.NumberOfScrews; i++)
            //{
            //    MainWindow.ScrewsList.Add(new Tightening());
            //}
        }

    }
}
