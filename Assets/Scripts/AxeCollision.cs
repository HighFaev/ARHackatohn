using UnityEngine;

public class AxeCollision : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        GameObject hitObject = collision.gameObject;
        Debug.Log("Ball hit: " + hitObject.name);

        Destroy(hitObject);
        Destroy(gameObject);
    }
}