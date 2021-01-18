using UnityEngine;

namespace GunShooting
{
    public class MoveAI : MonoBehaviour
    {
        enum State
        {
            Moving,
            Waiting
        }

        class TargetPointManager
        {
            Transform[] points;
            int currentIndex = 0;

            public TargetPointManager(Transform[] points)
            {
                this.points = points;
            }

            public Transform Current { get { return points[currentIndex]; } }

            public void Next()
            {
                currentIndex++;
                currentIndex = currentIndex >= points.Length ? 0 : currentIndex;
            }
        }

        [SerializeField, Header("移動座標リスト")] Transform[] points;
        [SerializeField, Header("座標での待機時間")] float MaxWaitingTime = 3f;
        [SerializeField, Header("座標への移動速度")] float MoveSpeed = 1f;

        State currentState;
        TargetPointManager targetPointManager;
        float waitingTime = 0f;

        void Start()
        {
            currentState = State.Moving;

            targetPointManager = new TargetPointManager(points);
        }

        void Update()
        {
            if (currentState == State.Moving)
            {
                float step = MoveSpeed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, targetPointManager.Current.position, step);
                if (transform.position == targetPointManager.Current.position)
                {
                    currentState = State.Waiting;
                    waitingTime = 0f;
                }
            }
            else
            {
                waitingTime += Time.deltaTime;
                if (waitingTime > MaxWaitingTime)
                {
                    targetPointManager.Next();
                    currentState = State.Moving;
                }
            }
        }
    }
}
