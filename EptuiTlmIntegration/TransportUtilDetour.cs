using System;
using System.Reflection;
using ColossalFramework.UI;
using EPTUI;
using TransportLinesManager;
using UnityEngine;

namespace EptuiTlmIntegration
{
    public class TransportUtilDetour
    {

        private static RedirectCallsState _state;

        public static void Deploy()
        {
            _state = RedirectionHelper.RedirectCalls(
                typeof(TransportUtil).GetMethod("GetFirstLineStop", BindingFlags.Public | BindingFlags.Static),
                typeof(TransportUtilDetour).GetMethod("GetFirstLineStop", BindingFlags.Public | BindingFlags.Static)
                );
        }

        public static void Revert()
        {
            RedirectionHelper.RevertRedirect(
                typeof(TransportUtil).GetMethod("GetFirstLineStop", BindingFlags.Public | BindingFlags.Static),
                _state
                );
        }



        public static Vector3 GetFirstLineStop(ushort line)
        {
            var segments = TransportManager.instance.m_lineSegments[line];
            if (segments == null || segments.Length <= 0)
            {
                HideLineInfoPanel();
                return new Vector3();
            }
            AlignLineInfoPanel("ExtendedBusPanel");
            AlignLineInfoPanel("ExtendedMetroPanel");
            AlignLineInfoPanel("ExtendedTrainPanel");
            if (TransportManager.instance.m_lines.m_buffer[line].CountStops(line) >= 32768)
            {
                HideLineInfoPanel();
                return new Vector3();
            }
            TLMController.instance.lineInfoPanel.openLineInfo(new UIButtonLineInfo()
            {
                lineID = line
            }, null);
            var autoCloser = new GameObject("AutoLineInfoCloser").AddComponent<AutoLineInfoCloser>();
            autoCloser.lineID = line;
            autoCloser.panel = GetLineInfoPanel();

            var optionsBar = UIView.Find<UIPanel>("OptionsBar");
            if (optionsBar != null)
            {
                var stationsRow = UIView.Find<UIPanel>("LineStationsLinearView");
                stationsRow.AlignTo(optionsBar, UIAlignAnchor.TopLeft);
                stationsRow.relativePosition = new Vector3(-optionsBar.absolutePosition.x, -32);
            }
            return segments[0].m_bounds.center;
        }

        private static void HideLineInfoPanel()
        {
            var lineInfoPanel = GetLineInfoPanel();
            if (lineInfoPanel != null)
            {
                lineInfoPanel.Hide();
            }
        }

        private static UIPanel GetLineInfoPanel()
        {
            var lineInfoPanelGo = GameObject.Find("LineInfoPanel");
            if (lineInfoPanelGo == null)
            {
                return null;
            }
            var lineInfoPanel = lineInfoPanelGo.GetComponent<UIPanel>();
            return lineInfoPanel;
        }


        private static void AlignLineInfoPanel(string parentName)
        {
            var extendedPanelGo = GameObject.Find(parentName);
            if (extendedPanelGo == null)
            {
                return;
            }
            var extendedPanel = extendedPanelGo.GetComponent<UITransportPanel>();
            if (extendedPanel == null || !extendedPanel.isVisible)
            {
                return;
            }
            var lineInfoPanel = GetLineInfoPanel();
            if (lineInfoPanel == null)
            {
                return;
            }
            lineInfoPanel.transform.parent = extendedPanel.transform;
            lineInfoPanel.AlignTo(extendedPanel, UIAlignAnchor.TopRight);
        }
    }
}