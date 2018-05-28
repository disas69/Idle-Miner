using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.Commands
{
    [RequireComponent(typeof(SpriteRenderer), typeof(BoxCollider2D))]
    public class CommandHandler : MonoBehaviour, IPointerDownHandler
    {
        private ICommand _command;

        public void SetCommand(ICommand command)
        {
            _command = command;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (_command != null)
            {
                _command.Execute();
            }
            else
            {
                UnityEngine.Debug.Log(string.Format("Can't execute command for \"{0}\" because it is not assigned.", gameObject.name));
            }
        }
    }
}