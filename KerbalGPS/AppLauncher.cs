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