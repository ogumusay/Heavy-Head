using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    [SerializeField] Material material;
    [SerializeField] float speed;

    void Update()
    {
        material.mainTextureOffset = new Vector2(transform.position.x * speed, 0f);
    }
}
