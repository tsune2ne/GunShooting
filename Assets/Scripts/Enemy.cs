using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GunShooting
{
    public class Enemy : MonoBehaviour
    {
        const string EnemyExplodePrefab = "Prefabs/EnemyExplode";
        const int Score = 10;

        [SerializeField] GameObject model;

        public Action<int> onDead;

        int hp = 3;
        Material material;

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

            material.SetColor("_Color", Color.red);
        }

        private void Start()
        {
            var renderer = model.GetComponent<Renderer>();
            if (renderer)
            {
                material = renderer.material;
            }
        }

        private void Update()
        {
            if (material)
            {
                var color = material.GetColor("_Color");
                var newColor = new Color(
                    Mathf.Min(1f, color.r + 0.01f),
                    Mathf.Min(1f, color.g + 0.01f),
                    Mathf.Min(1f, color.b + 0.01f),
                    color.a);
                material.SetColor("_Color", newColor);
            }
        }

        void Dead()
        {
            if (onDead != null)
            {
                onDead.Invoke(Score);
            }

            gameObject.SetActive(false);
            var obj = (GameObject)Resources.Load(EnemyExplodePrefab);
            Instantiate(obj, gameObject.transform.position, Quaternion.identity);
        }
    }
}
