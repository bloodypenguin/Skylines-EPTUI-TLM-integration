using System;
using System.Reflection;
using EPTUI;
using ICities;
using TransportLinesManager;

namespace EptuiTlmIntegration
{
    public class LoadingExtension : LoadingExtensionBase
    {
        public override void OnLevelLoaded(LoadMode mode)
        {
            try
            {
                TLMMainPanelDetour.Deploy();
                TransportUtilDetour.Deploy();
            }
            catch (Exception e)
            {
                UnityEngine.Debug.LogException(e);
            }

        }

        public override void OnReleased()
        {
            try
            {
                TLMMainPanelDetour.Revert();
                TransportUtilDetour.Revert();
            }
            catch (Exception e)
            {
                UnityEngine.Debug.LogException(e);
            }
        }
    }
}