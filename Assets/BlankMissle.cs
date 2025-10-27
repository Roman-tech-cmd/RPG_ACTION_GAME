using UnityEngine;

public class BlankMissle : MonoBehaviour
{
    private float speed;
    private float lifeTime;
    void Update()
    {
        transform.Translate(Vector2.up * -speed * Time.deltaTime);
        Destroy(gameObject, lifeTime);
    }
}
