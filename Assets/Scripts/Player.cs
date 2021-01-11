using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GunShooting
{ 
    public class Player : MonoBehaviour
    {
        [SerializeField] GameObject Hand;
        [SerializeField] GameObject nozleFlash;

        public void Rotate(float rotateX, float rotateY)
        {
            gameObject.transform.Rotate(0, -rotateX, 0);
            Hand.transform.Rotate(rotateY, 0, 0);
        }

        public void Shoot()
        {
            nozleFlash.SetActive(false);
            nozleFlash.SetActive(true);
            // TODO Hit
        }
    }
}