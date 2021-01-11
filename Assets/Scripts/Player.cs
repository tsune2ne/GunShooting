using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GunShooting
{ 
    public class Player : MonoBehaviour
    {
        const string HitEffect = "Prefabs/HitEffect";

        [SerializeField] GameObject Hand;
        [SerializeField] GameObject nozleFlash;

        public void Rotate(float rotateX, float rotateY)
        {
            gameObject.transform.Rotate(0, -rotateX, 0);
            Hand.transform.Rotate(rotateY, 0, 0);
        }

        public void Shoot()
        {
            // nozleFlash
            nozleFlash.SetActive(false);
            nozleFlash.SetActive(true);

            // Hit
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 1000.0f))
            {
                var obj = (GameObject)Resources.Load(HitEffect);
                Instantiate(obj, hit.point, Quaternion.identity);

                var enemy = hit.collider.gameObject.GetComponent<Enemy>();
                if (enemy)
                {
                    enemy.Hit();
                }
            }
        }
    }
}