using UnityEngine;

public class TitleTextMove : MonoBehaviour
{
    [SerializeField] private float zoomSpeed = 0.05f;

    private float cos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        cos += 0.01f * zoomSpeed;

        gameObject.transform.localScale = new Vector3(
            gameObject.transform.localScale.x + Mathf.Cos(cos) * 0.01f,
            gameObject.transform.localScale.y + Mathf.Cos(cos) * 0.01f,
            gameObject.transform.localScale.z);

        if (cos > 3.14f)
        {
            cos = 0;
        }
    }
}
