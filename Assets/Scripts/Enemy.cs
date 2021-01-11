using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GunShooting
{
    public class Enemy : MonoBehaviour
    {
        const int Score = 10;

        public Action<int> onDead;

        int hp = 3;

        bool isDead { get { return hp == 0; } }

        public void Hit()
        {
            if (isDead)
            {
                return;
            }

            hp = Math.Max(0, hp - 1);
            if (hp == 0)
            {
                Dead();
            }
        }

        void Dead()
        {
            if (onDead != null)
            {
                onDead.Invoke(Score);
            }

            gameObject.SetActive(false);
        }
    }
}
