using System.Reflection;
using TransportLinesManager;

namespace EptuiTlmIntegration
{
    public class TLMMainPanelDetour
    {

        private static RedirectCallsState _showState ;
        private static RedirectCallsState _hideState;

        public static void Deploy()
        {
            _showState = RedirectionHelper.RedirectCalls(
                typeof(TLMMainPanel).GetMethod("Show", BindingFlags.Public | BindingFlags.Instance),
                typeof(TLMMainPanelDetour).GetMethod("Show", BindingFlags.Public | BindingFlags.Instance)
                );
            _hideState = RedirectionHelper.RedirectCalls(
                typeof(TLMMainPanel).GetMethod("Hide", BindingFlags.Public | BindingFlags.Instance),
                typeof(TLMMainPanelDetour).GetMethod("Hide", BindingFlags.Public | BindingFlags.Instance)
                );
        }

        public static void Revert()
        {
            RedirectionHelper.RevertRedirect(
                typeof(TLMMainPanel).GetMethod("Show", BindingFlags.Public | BindingFlags.Instance),
                _showState
                );
            RedirectionHelper.RevertRedirect(
                typeof(TLMMainPanel).GetMethod("Hide", BindingFlags.Public | BindingFlags.Instance),
                _hideState
                );
        }

        public void Show()
        {
        }

        public void Hide()
        {
        }
    }
}