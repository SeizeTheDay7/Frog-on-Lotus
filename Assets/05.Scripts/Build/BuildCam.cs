using Cinemachine;
using UnityEngine;
using UnityEngine.Tilemaps;


public class BuildCam : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera huntcam;
    [SerializeField] CinemachineVirtualCamera buildcam;
    [SerializeField] SpriteRenderer referenceBox;
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

        bb = referenceBox.bounds;
        maxOrtho = bb.size.x / Camera.main.aspect / 2f;

        buildcam.m_Lens.OrthographicSize = maxOrtho;
    }
}