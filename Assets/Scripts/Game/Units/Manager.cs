using UnityEngine;

namespace Game.Units
{
    public class Manager : MonoBehaviour
    {
        public void SetActive(bool isActive)
        {
            gameObject.SetActive(isActive);
        }
    }
}