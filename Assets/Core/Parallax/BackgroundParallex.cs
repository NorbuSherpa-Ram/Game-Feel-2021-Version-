using UnityEngine;

namespace Core.Parallex
{
    public class BackgroundParallex : MonoBehaviour
    {
        [Range(-1, 1)]
        [SerializeField] private float parallexMultiplier;
        private float xPosition;
        void Start()
        {
            xPosition = transform.position.x;
        }

        void FixedUpdate()
        {
            float distanceToMove = Camera.main.transform.position.x * parallexMultiplier * Time.fixedDeltaTime ;
            transform.position = new Vector2(distanceToMove + xPosition, transform.position.y);
        }
    }
}
