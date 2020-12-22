using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GunShooting
{
    public class Controller : MonoBehaviour
    {
        [SerializeField] Player Player;

        void Start()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Player.Shoot();
            }

            var rotateX = Input.GetAxis("Mouse X");
            var rotateY = Input.GetAxis("Mouse Y");
            if (Mathf.Abs(rotateX + rotateY) > 0)
            {
                Player.Rotate(rotateX, rotateY);
            }
        }
    }
}
