using System;
using UnityEngine;

namespace UnityStandardAssets.Utility
{
    [RequireComponent(typeof (GUIText))]
    public class FPSCounter : MonoBehaviour
    {
        const float fpsMeasurePeriod = 0.1f;
        private int m_FpsAccumulator = 0;
        private float m_FpsNextPeriod = 0;
        private int m_CurrentFps;
        const string display = "FPS: {0}";
        private GUIText m_GuiText;


        private void Start()
        {
            m_FpsNextPeriod = Time.realtimeSinceStartup + fpsMeasurePeriod;
            m_GuiText = GetComponent<GUIText>();
        }


        private void Update()
        {
            // measure average frames per second
            m_FpsAccumulator++;
            if (Time.realtimeSinceStartup > m_FpsNextPeriod)
            {
                m_CurrentFps = (int) (m_FpsAccumulator/fpsMeasurePeriod);
                m_FpsAccumulator = 0;
                m_FpsNextPeriod += fpsMeasurePeriod;
                m_GuiText.text = string.Format(display, m_CurrentFps);
            }
        }
    }
}
