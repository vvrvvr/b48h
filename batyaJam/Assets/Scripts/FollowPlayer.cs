using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Vector3 offset = new Vector3();
    private float smoothTime = 0.5f;
    private Vector3 _velocity = Vector3.zero;

    private void Awake()
    {
        //if (player != null)
        //{
        //    transform.position = player.position + offset;
        //}
    }

    void FixedUpdate()
    {
        if (player != null)
        {
            var follow = player.position + offset;
            transform.position = Vector3.SmoothDamp(transform.position, follow, ref _velocity, smoothTime);

        }
    }
}
