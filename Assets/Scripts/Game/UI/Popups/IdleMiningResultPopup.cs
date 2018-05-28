using Framework.UI.Structure.Base.View;
using Game.Core;
using Game.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.Popups
{
    public class IdleMiningResultPopup : Popup
    {
        [SerializeField] private Text _messageText;

        public override void OnEnter()
        {
            base.OnEnter();
            _messageText.text = string.Format("Your miners made {0} Gold while you were away.",
                FormatHelper.FormatResourceAmount(GameContext.Session.IdleMiningResul));
        }
    }
}