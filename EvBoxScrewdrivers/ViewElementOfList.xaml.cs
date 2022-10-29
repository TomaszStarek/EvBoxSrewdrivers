using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace EvBoxScrewdrivers
{
    /// <summary>
    /// Interaction logic for ViewElementOfList.xaml
    /// </summary>
    public partial class ViewElementOfList : Window
    {

  //      public static List<Tightening> list;
        public ViewElementOfList(int numberOfScrew)
        {
            InitializeComponent();

            int i = numberOfScrew;

            this.Title = $"Parametry śruby{numberOfScrew}";

            ListView.Items.Add("Event Count:");
            ListView.Items.Add(MainWindow.ScrewsList[i].EventCount);
            ListView.Items.Add("Fastening Time:");
            ListView.Items.Add(MainWindow.ScrewsList[i].FasteningTime);
            ListView.Items.Add("Preset Number:");
            ListView.Items.Add(MainWindow.ScrewsList[i].PresetNumber);

            ListView.Items.Add("Target Torque:");
            ListView.Items.Add(MainWindow.ScrewsList[i].TargetTorque);
            ListView.Items.Add("Converted Torque:");
            ListView.Items.Add(MainWindow.ScrewsList[i].ConvertedTorque);
            ListView.Items.Add("Fastening Time:");
            ListView.Items.Add(MainWindow.ScrewsList[i].TargetSpeed);

            ListView.Items.Add("A1:");
            ListView.Items.Add(MainWindow.ScrewsList[i].A1);
            ListView.Items.Add("A2:");
            ListView.Items.Add(MainWindow.ScrewsList[i].A2);
            ListView.Items.Add("A3:");
            ListView.Items.Add(MainWindow.ScrewsList[i].A3);

            ListView.Items.Add("Screw Count Value:");
            ListView.Items.Add(MainWindow.ScrewsList[i].ScrewCountValue);
            ListView.Items.Add("Error:");
            ListView.Items.Add(MainWindow.ScrewsList[i].Error);
            ListView.Items.Add("Forward Or Loosenig:");
            ListView.Items.Add(MainWindow.ScrewsList[i].ForwardOrLoosenig);
            ListView.Items.Add("Status:");
            ListView.Items.Add(MainWindow.ScrewsList[i].Status);
            ListView.Items.Add("Snug Torque Angle:");
            ListView.Items.Add(MainWindow.ScrewsList[i].SnugTorqueAngle);

            ListView.Items.Add("TCAMorACTM:");
            ListView.Items.Add(MainWindow.ScrewsList[i].TCAMorACTM);
            ListView.Items.Add("Torque:");
            ListView.Items.Add(MainWindow.ScrewsList[i].Torque);
            ListView.Items.Add("TorqueMin:");
            ListView.Items.Add(MainWindow.ScrewsList[i].TorqueMin);

            ListView.Items.Add("TorqueMax:");
            ListView.Items.Add(MainWindow.ScrewsList[i].TorqueMax);
            ListView.Items.Add("TargetAngle:");
            ListView.Items.Add(MainWindow.ScrewsList[i].TargetAngle);
            ListView.Items.Add("MinAngle:");
            ListView.Items.Add(MainWindow.ScrewsList[i].MinAngle);

            ListView.Items.Add("MaxAngle:");
            ListView.Items.Add(MainWindow.ScrewsList[i].MaxAngle);
            ListView.Items.Add("SnugTorue:");
            ListView.Items.Add(MainWindow.ScrewsList[i].SnugTorue);
            ListView.Items.Add("Speed:");
            ListView.Items.Add(MainWindow.ScrewsList[i].Speed);

            ListView.Items.Add("FreeFasteningAngle:");
            ListView.Items.Add(MainWindow.ScrewsList[i].FreeFasteningAngle);
            ListView.Items.Add("FreeFasteningSpeed:");
            ListView.Items.Add(MainWindow.ScrewsList[i].FreeFasteningSpeed);
            ListView.Items.Add("SoftStart:");
            ListView.Items.Add(MainWindow.ScrewsList[i].SoftStart);
            ListView.Items.Add("SeatingPoint:");
            ListView.Items.Add(MainWindow.ScrewsList[i].SeatingPoint);
            ListView.Items.Add("TorqueRisingRate:");
            ListView.Items.Add(MainWindow.ScrewsList[i].TorqueRisingRate);
            ListView.Items.Add("RampUpSpeed:");
            ListView.Items.Add(MainWindow.ScrewsList[i].RampUpSpeed);
            ListView.Items.Add("TorqueCompensation:");
            ListView.Items.Add(MainWindow.ScrewsList[i].TorqueCompensation);
        }
    }
}
