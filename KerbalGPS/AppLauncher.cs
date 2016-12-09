/////////////////////////////////////////////////////////////////////////////////////////////
////
////   AppLauncher.cs
////
////   Kerbal Space Program AppLauncher routines
////
////   (C) Copyright 2016 Ted Thompson
////
////   This code is licensed under GPL-3.0
////
/////////////////////////////////////////////////////////////////////////////////////////////
////
////   Revision History
////
/////////////////////////////////////////////////////////////////////////////////////////////
////
////   Created December 5th, 2016
////
////
/////////////////////////////////////////////////////////////////////////////////////////////

using KSP.UI.Screens;
using UnityEngine;

namespace KerbStar.GPSToolbar
{
    public static class AppLauncherKerbalGPS
    {
        private static ApplicationLauncherButton btnLauncher;
        private static Texture2D kgps_button_off;
        private static Texture2D kgps_button_on_nosat;
        private static Texture2D kgps_button_on_sat;
        private static Texture2D kgps_button_Texture;
        private static Texture2D kgps_button_nogps;
        private static Texture2D tex2d;
        public static bool displayGUI;

        public enum rcvrStatus
        {
            OFF = 0,
            SATS = 1,
            NOSATS = 2,
            NONE = 3
        }

        public static void Awake()
        {
        }

        public static void Start()
        {
            if (!kgps_button_off && GameDatabase.Instance.ExistsTexture("KerbalGPS/Icon/GPSIconOff")) kgps_button_off = GameDatabase.Instance.GetTexture("KerbalGPS/Icon/GPSIconOff", false);
            if (!kgps_button_on_sat && GameDatabase.Instance.ExistsTexture("KerbalGPS/Icon/GPSIconSat")) kgps_button_on_sat = GameDatabase.Instance.GetTexture("KerbalGPS/Icon/GPSIconSat", false);
            if (!kgps_button_on_nosat && GameDatabase.Instance.ExistsTexture("KerbalGPS/Icon/GPSIconNoSat")) kgps_button_on_nosat = GameDatabase.Instance.GetTexture("KerbalGPS/Icon/GPSIconNoSat", false);
            if (!kgps_button_nogps && GameDatabase.Instance.ExistsTexture("KerbalGPS/Icon/GPSIconNoGPS")) kgps_button_nogps = GameDatabase.Instance.GetTexture("KerbalGPS/Icon/GPSIconNoGPS", false);
            tex2d = kgps_button_nogps;

            if (btnLauncher == null)
            {
                btnLauncher = ApplicationLauncher.Instance.AddModApplication(OnToggleTrue, OnToggleFalse,
                                                                             null, null,
                                                                             null, null,
                                                                             ApplicationLauncher.AppScenes.FLIGHT | ApplicationLauncher.AppScenes.MAPVIEW,
                                                                             tex2d);
                MonoBehaviour.print("[KerbalGPS] Adding Toolbar button");
            }
        }

        private static void OnToggleTrue()
        {
            displayGUI = true;
        }

        private static void OnToggleFalse()
        {
            displayGUI = false;
        }

        public static void setBtnState(bool state, bool click = false)
        {
            if (state)
                btnLauncher.SetTrue(click);
            else
                btnLauncher.SetFalse(click);
        }

        public static void SetAppLauncherButtonTexture(rcvrStatus status)
        {
            tex2d = null;

            switch (status)
            {
                case rcvrStatus.OFF:
                    tex2d = kgps_button_off;
                    break;
                case rcvrStatus.SATS:
                    tex2d = kgps_button_on_sat;
                    break;
                case rcvrStatus.NOSATS:
                    tex2d = kgps_button_on_nosat;
                    break;
                case rcvrStatus.NONE:
                    tex2d = kgps_button_nogps;
                    break;
            }

            // Set new Launcher Button texture
            if (btnLauncher != null)
            {
                if (tex2d != kgps_button_Texture)
                {
                    kgps_button_Texture = tex2d;
                    btnLauncher.SetTexture(tex2d);
                }
            }
        }

        public static void OnDestroy()
        {
            if (btnLauncher != null)
            {
                MonoBehaviour.print("[KerbalGPS] Removing Toolbar button");
                ApplicationLauncher.Instance.RemoveModApplication(btnLauncher);
                btnLauncher = null;
            }
        }
    }
}