using UnityEngine;

public class ObjectMoving : MonoBehaviour
{
    [SerializeField] float movementAmplitude;
    [SerializeField] float speed;

    Vector3 startingPos;
    Vector3 destination;

    void Start()
    {
        startingPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        destination = new Vector3(startingPos.x, startingPos.y + movementAmplitude, startingPos.z);
    }

    void Update() { transform.position = Vector3.Lerp(startingPos, destination, Hermite(Mathf.PingPong(Time.time, 1.0f)) * speed); }

    //https://answers.unity.com/questions/504386/mathfpingpong-with-ease-in-and-out.html
    float Hermite(float t) { return -t * t * t * 2f + t * t * 3f; }
}
