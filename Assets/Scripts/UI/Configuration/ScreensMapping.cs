using System;
using System.Collections.Generic;
using UI.Structure.Base.View;
using UnityEngine;
using Screen = UI.Structure.Base.Screen;

namespace UI.Configuration
{
    [CreateAssetMenu(fileName = "ScreensMapping", menuName = "UI/ScreensMapping")]
    public class ScreensMapping : ScriptableObject
    {
        public List<ScreenSetting> ScreenSettings = new List<ScreenSetting>();
        public List<PopupSettings> PopupSettings = new List<PopupSettings>();
    }

    [Serializable]
    public class ScreenSetting
    {
        public Screen Screen;
        public bool IsCached;
    }

    [Serializable]
    public class PopupSettings
    {
        public Popup Popup;
    }
}