// Assets/Scripts/Camera/ParallaxLayer.cs
using UnityEngine;

public class ParallaxLayer : MonoBehaviour
{
    public Transform cameraTransform;     // 보통 MainCamera의 Transform
    public Vector2 parallaxMultiplier = new Vector2(0.2f, 0.1f); // x,y 배율

    private Vector3 prevCamPos;

    void Start()
    {
        if (cameraTransform == null)
        {
            if (Camera.main != null)
            {
                cameraTransform = Camera.main.transform;
            }
        }

        if (cameraTransform != null)
        {
            prevCamPos = cameraTransform.position;
        }
    }

    void LateUpdate()
    {
        if (cameraTransform == null)
        {
            return;
        }

        Vector3 camPos = cameraTransform.position;
        Vector3 delta = camPos - prevCamPos;

        // 배경 이동: z는 고정
        transform.position = new Vector3(
            transform.position.x + delta.x * parallaxMultiplier.x,
            transform.position.y + delta.y * parallaxMultiplier.y,
            transform.position.z
        );

        prevCamPos = camPos;
    }
}
