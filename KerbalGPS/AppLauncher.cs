/////////////////////////////////////////////////////////////////////////////////////////////
////
////   KerbalGPS_Main.cs
////
////   Kerbal Space Program GPS math library
////
////   (C) Copyright 2012-2013, Kevin Wilder (a.k.a. PakledHostage)
////
////   This code is licensed under the Attribution-NonCommercial-ShareAlike 3.0 (CC BY-NC-SA 3.0) 
////   creative commons license. See <http://creativecommons.org/licenses/by-nc-sa/3.0/legalcode> 
////   for full details.
////
////   Attribution — You are free to modify this code, so long as you mention that the resulting
////                 work is based upon or adapted from this library. This KerbalGPS_Main.cs
////                 code library is the original work of Kevin Wilder.
////
////   Non-commercial - You may not use this work for commercial purposes.
////
////   Share Alike — If you alter, transform, or build upon this work, you may distribute the 
////                 resulting work only under the same or similar license to the CC BY-NC-SA 3.0
////                 license.
////
////
/////////////////////////////////////////////////////////////////////////////////////////////
////
////   Revision History
////
/////////////////////////////////////////////////////////////////////////////////////////////
////
////   Created November 10th, 2012
////
////   Revised October 26, 2013 by Kevin Wilder to incorporate changes suggested by m4v.
////
////   Revised December 1, 2016 by Ted Thompson to incorporate remove obsolete RenderManager refs
////
////
/////////////////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using KSP.UI.Screens;

/// <summary>
/// Add a MOD(3rd party) application to the Application Launcher. Use ApplicationLauncherButton.VisibleInScenes to set where the button should be displayed.
/// </summary>
/// <param name="onTrue">Callback for when the button is toggeled on</param>
/// <param name="onFalse">Callback for when the button is toggeled off</param>
/// <param name="onHover">Callback for when the mouse is hovering over the button</param>
/// <param name="onHoverOut">Callback for when the mouse hoveris off the button</param>
/// <param name="onEnable">Callback for when the button is shown or enabled by the application launcher</param>
/// <param name="onDisable">Callback for when the button is hidden or disabled by the application launcher</param>
/// <param name="visibleInScenes">The "scenes" this button will be visible in. For example VisibleInScenes = ApplicationLauncher.AppScenes.FLIGHT | ApplicationLauncher.AppScenes.MAPVIEW;</param>
/// <param name="texture">The 38x38 Texture to use for the button icon.</param>
/// <returns></returns>
/// public ApplicationLauncherButton AddModApplication(RUIToggleButton.OnTrue onTrue, RUIToggleButton.OnFalse onFalse, RUIToggleButton.OnHover onHover, RUIToggleButton.OnHoverOut onHoverOut, RUIToggleButton.OnEnable onEnable, RUIToggleButton.OnDisable onDisable, ApplicationLauncher.AppScenes visibleInScenes, Texture texture)
/// 

namespace KerbStar.GPSToolbar
{
    public static class AppLauncherKerbalGPS
    {
        private static ApplicationLauncherButton btnLauncher;

        public static void Awake()
        {
        }

        public static void Start()
        {
            if (btnLauncher == null)
                btnLauncher =
                    ApplicationLauncher.Instance.AddModApplication(OnToggleTrue, OnToggleFalse, null, null, null, null,
                                        ApplicationLauncher.AppScenes.FLIGHT,
                                        GameDatabase.Instance.GetTexture("KerbalGPS/Icon/AppLauncherIcon", false));
        }

        private static void OnToggleTrue()
        {
            KerbalGPS.displayGUI = true;
        }

        private static void OnToggleFalse()
        {
            KerbalGPS.displayGUI = false;
        }

        public static void setBtnState(bool state, bool click = false)
        {
            if (state)
                btnLauncher.SetTrue(click);
            else
                btnLauncher.SetFalse(click);
        }

        public static void OnDestroy()
        {
            if (btnLauncher != null)
            {
                ApplicationLauncher.Instance.RemoveModApplication(btnLauncher);
                btnLauncher = null;
            }
        }
    }
}