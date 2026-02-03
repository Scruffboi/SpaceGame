using UnityEngine;
using Unity.Cinemachine;
using System.Collections;

namespace SpaceGame
{
    public class CineMachineManager : MonoBehaviour 
    {
        [SerializeField] private CinemachineCamera cCam;

        public void SetTarget()
        {
            cCam.Follow = GameManager.GetInstance().GetPlayer().transform;
        }
    }
}
