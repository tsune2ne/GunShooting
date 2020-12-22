using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GunShooting
{ 
    public class Player : MonoBehaviour
    {
        [SerializeField] GameObject Hand;

        public void Rotate(float rotateX, float rotateY)
        {
            gameObject.transform.Rotate(0, -rotateX, 0);
            Hand.transform.Rotate(rotateY, 0, 0);
        }

        public void Shoot()
        {
            Debug.Log("Shoot!");
        }
    }
}