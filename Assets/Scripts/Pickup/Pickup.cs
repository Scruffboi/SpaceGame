using UnityEngine;

namespace SpaceGame
{
    public class Pickup : MonoBehaviour
    {
        protected SoundManager soundManager;

        protected virtual void Start()
        {
            soundManager = GameManager.GetInstance().soundManager;
        }

        public virtual void OnPicked()
        {
            Destroy(gameObject);
        }
    }
}
