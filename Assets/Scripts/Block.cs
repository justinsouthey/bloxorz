using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _forceFactor = 5;
    [SerializeField] private float _torqueForceFactor = 5;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            _rigidbody.AddForce(Vector3.forward * _forceFactor);
            _rigidbody.AddTorque(Vector3.right * _torqueForceFactor, ForceMode.Impulse);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            _rigidbody.AddForce(Vector3.back * _forceFactor);
            _rigidbody.AddTorque(Vector3.left * _torqueForceFactor, ForceMode.Impulse);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            _rigidbody.AddForce(Vector3.left * _forceFactor);
            _rigidbody.AddTorque(Vector3.forward * _torqueForceFactor, ForceMode.Impulse);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            _rigidbody.AddForce(Vector3.right * _forceFactor);
            _rigidbody.AddTorque(Vector3.back * _torqueForceFactor, ForceMode.Impulse);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;
            transform.position = Vector3.zero;
            transform.rotation = Quaternion.identity;
        }
    }
}
