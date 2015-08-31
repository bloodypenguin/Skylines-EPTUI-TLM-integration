using ColossalFramework.UI;
using UnityEngine;

namespace EptuiTlmIntegration
{
    public class AutoLineInfoCloser : MonoBehaviour
    {
        public int lineID;
        public UIPanel panel ;

        public void Update()
        {
            if (!panel.isVisible)
            {
                Destroy(gameObject); 
            }
            if (TransportManager.instance.m_lines.m_buffer[lineID].m_flags != TransportLine.Flags.None)
            {
                return;
            }
            panel.Hide();
            Destroy(gameObject);
        }

    }
}