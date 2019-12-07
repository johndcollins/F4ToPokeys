﻿using System;
using System.Runtime.InteropServices;

namespace F4SharedMem.Headers
{
    [ComVisible(true)]
    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public struct FlightData2
    {
        // changelog:
        // 1: initial BMS 4.33 version
        // 2: added AltCalReading, altBits, BupUhfPreset, powerBits, blinkBits, cmdsMode
        // 3: added VersionNum, hydPressureA/B, cabinAlt, BupUhfFreq, currentTime, vehicleACD
        // 4: added fuelflow2
        // 5: added RwrInfo, lefPos, tefPos
        // 6: added vtolPos
        // 7: bit fields are now unsigned instead of signed
        // 8: increased RwrInfo size to 512
        // 9: added human pilot names and their status in a session

        public const int RWRINFO_SIZE = 512;
        public const int MAX_CALLSIGNS = 32;

        // VERSION 1
        public float nozzlePos2;   // Ownship engine nozzle2 percent open (0-100)
        public float rpm2;         // Ownship engine rpm2 (Percent 0-103)
        public float ftit2;        // Ownship Forward Turbine Inlet Temp2 (Degrees C)
        public float oilPressure2; // Ownship Oil Pressure2 (Percent 0-100)
        public byte navMode;  // current mode selected for HSI/eHSI (added in BMS4)
        public float aauz; // Ownship barometric altitude given by AAU (depends on calibration)
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = (int)TacanSources.NUMBER_OF_SOURCES)]
        public byte[] tacanInfo;      // Tacan band/mode settings for UFC and AUX COMM

        // VERSION 2
        public int AltCalReading;	// barometric altitude calibration (depends on CalType)
        public int altBits;		// various altimeter bits, see AltBits enum for details
        public int powerBits;		// Ownship power bus / generator states, see PowerBits enum for details
        public int blinkBits;		// Cockpit indicator lights blink status, see BlinkBits enum for details
        // NOTE: these bits indicate only *if* a lamp is blinking, in addition to the
        // existing on/off bits. It's up to the external program to implement the
        // *actual* blinking.
        public int cmdsMode;		// Ownship CMDS mode state, see CmdsModes enum for details
        public int BupUhfPreset;	// BUP UHF channel preset

        // VERSION 3
        public int BupUhfFreq;		// BUP UHF channel frequency
        public float cabinAlt;		// Ownship cabin altitude
        public float hydPressureA;	// Ownship Hydraulic Pressure A
        public float hydPressureB;	// Ownship Hydraulic Pressure B
        public int currentTime;	// Current time in seconds (max 60 * 60 * 24)
        public short vehicleACD;	// Ownship ACD index number, i.e. which aircraft type are we flying.
        public int VersionNum2;		// Version of FlightData2 mem area

        // VERSION 4
        public float fuelFlow2;    // Ownship fuel flow2 (Lbs/Hour)

        // VERSION 5 / 8
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = RWRINFO_SIZE)]
        public byte[] RwrInfo;     //[512] New RWR Info
        public float lefPos;       // Ownship LEF position
        public float tefPos;       // Ownship TEF position

        // VERSION 6
        public float vtolPos;      // Ownship VTOL exhaust angle

        // VERSION 9
        public byte pilotsOnline;                  // Number of pilots in an MP session

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_CALLSIGNS)]
        public Callsign_LineOfText[] pilotsCallsign;        // [MAX_CALLSIGNS][CALLSIGN_LEN] List of pilots callsign connected to an MP session

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_CALLSIGNS)]
        public byte[] pilotsStatus;                // [MAX_CALLSIGNS] Status of the MP pilots, see enum FlyStates
    }

}
