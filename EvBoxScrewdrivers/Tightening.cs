using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvBoxScrewdrivers
{
    public class Tightening 
    {
        public int EventCount { get; set; }
        public int FasteningTime { get; set; }
        public int PresetNumber { get; set; }
        public int TargetTorque { get; set; }
        public int ConvertedTorque { get; set; }
        public int TargetSpeed { get; set; }
        public int A1 { get; set; }
        public int A2 { get; set; }
        public int A3 { get; set; }
        public int ScrewCountValue { get; set; }
        public int Error { get; set; }
        public int ForwardOrLoosenig { get; set; }
        public int Status { get; set; }
        public int SnugTorqueAngle { get; set; }

        #region PresetValues
        public int TCAMorACTM  { get; set; }
        public int Torque { get; set; }
        public int TorqueMin { get; set; }
        public int TorqueMax { get; set; }
        public int TargetAngle { get; set; }
        public int MinAngle { get; set; }
        public int MaxAngle { get; set; }
        public int SnugTorue { get; set; }
        public int Speed { get; set; }
        public int FreeFasteningAngle { get; set; }
        public int FreeFasteningSpeed { get; set; }
        public int SoftStart { get; set; }
        public int SeatingPoint { get; set; }
        public int TorqueRisingRate { get; set; }
        public int RampUpSpeed { get; set; }
        public int TorqueCompensation { get; set; }
        #endregion
    }
}
