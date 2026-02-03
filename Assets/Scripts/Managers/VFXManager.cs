using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

namespace SpaceGame
{
    public class VFXManager : MonoBehaviour
    {
        [SerializeField] private VisualEffect smallExplosion;
        [SerializeField] private VisualEffect bigExplosion;

        private float visualEffectLifetime = 3f;

        private Dictionary<string, VisualEffect> vfxDict;

        private void Start()
        {
            vfxDict = new Dictionary<string, VisualEffect>
            {
                { "small_explosion", smallExplosion },
                { "big_explosion", bigExplosion },
            };
        }

        public void PlayEffect(string vfx_name, Transform new_transform)
        {
            VisualEffect curr_vfx = Instantiate(vfxDict[vfx_name], new_transform.position, Quaternion.identity);
            curr_vfx.Play();
            Destroy(curr_vfx, visualEffectLifetime);
        }


    }
}
