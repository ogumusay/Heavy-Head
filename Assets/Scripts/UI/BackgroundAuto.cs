using UnityEngine;

public class BackgroundAuto : MonoBehaviour
{
    [SerializeField] Material material;
    [SerializeField] float speed = 0.01f;
    Vector2 offset;


    private void Start()
    {
        offset = new Vector2(Time.deltaTime * speed, 0f);
    }
    private void Update()
    {

        material.mainTextureOffset += offset;
        
    }
}
