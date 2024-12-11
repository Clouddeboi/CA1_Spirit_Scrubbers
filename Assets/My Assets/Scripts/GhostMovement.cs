using UnityEngine;

public class GhostMovement : MonoBehaviour
{
    public Transform target; //Target object to rotate around
    public float speed = 2f; //speed of the movement
    public float radius = 1f; //radius of the path
    public float angle = 0f; //current angle of the object

    // Update is called once per frame
    void Update()
    {
        float x = target.position.x + Mathf.Cos(angle) * radius;
        float y = target.position.y;
        float z = target.position.z + Mathf.Sin(angle) * radius;

        transform.position = new Vector3(x, y, z);

        angle += speed * Time.deltaTime;
    }
}
