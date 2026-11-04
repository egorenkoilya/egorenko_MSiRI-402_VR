using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Столкновение с: " + collision.gameObject.name);
        GetComponent<Renderer>().material.color = Color.red;
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Вошел в триггер: " + other.gameObject.name);
        GetComponent<Renderer>().material.color = Color.green;
    }

    void OnTriggerExit(Collider other)
    {
        Debug.Log("Вышел из триггера: " + other.gameObject.name);
    }
}