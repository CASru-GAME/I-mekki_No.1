using Unity.VisualScripting;
using UnityEngine;

namespace App.Scripts.Game.Camera
{
    public class CameraMove : MonoBehaviour
    {
        [SerializeField] private GameObject _camera;
        [SerializeField] private float speed = 0.1f;
        Rigidbody2D rb;
        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        }
        private void FixedUpdate()
        {
            //_camera.transform.position = new Vector3(transform.position.x+speed, transform.position.y);
            rb.linearVelocity = new Vector2(speed, 0);
        }
    }
}
