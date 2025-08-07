using Cinemachine;
using UnityEngine;


public class BuildCamera : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera huntcam;
    [SerializeField] CinemachineVirtualCamera buildcam;
    [SerializeField] SpriteRenderer referenceBox;
    [SerializeField] float zoomSpeed = 0.1f;
    [SerializeField] float zoomSmoothness = 10f;
    private float targetOrthoSize;
    private Bounds bb;
    private float maxOrtho;
    private float minOrtho;

    void Awake()
    {
        // background bound의 가로 크기를 구한다
        // 화면 비율을 가져온 뒤, 가로 크기(최대 너비)를 이용하여
        // lens ortho size의 최대 fov를 설정한다.

        // 시작할 때 최대 fov로 설정한다
        // 두 손가락으로 줌 인 줌 아웃이 가능하다.
        // 줌 아웃은 최대 fov까지만 가능하다
        // 줌 인은 huntcam의 fov까지만 가능하다.

        minOrtho = huntcam.m_Lens.OrthographicSize;

        bb = referenceBox.bounds;
        maxOrtho = bb.size.x / Camera.main.aspect / 2f;

        buildcam.m_Lens.OrthographicSize = maxOrtho;
        targetOrthoSize = (minOrtho + maxOrtho) / 2;
    }

    void Update()
    {
        // If there are two touches on the device...
        if (Input.touchCount == 2)
        {
            // Store both touches.
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            // Find the position in the previous frame of each touch.
            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            // Find the magnitude of the vector (the distance) between the touches in each frame.
            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            // Find the difference in the distances between each frame.
            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

            targetOrthoSize += deltaMagnitudeDiff * zoomSpeed * Time.deltaTime;
            targetOrthoSize = Mathf.Clamp(targetOrthoSize, minOrtho, maxOrtho);
        }

        buildcam.m_Lens.OrthographicSize = Mathf.Lerp(
            buildcam.m_Lens.OrthographicSize,
            targetOrthoSize,
            zoomSmoothness * Time.deltaTime
        );
    }
}