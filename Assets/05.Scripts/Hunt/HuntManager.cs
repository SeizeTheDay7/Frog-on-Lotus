using UnityEngine;

public class HuntManager : MonoBehaviour
{
    public static HuntManager Instance;
    [SerializeField] GameObject frog;

    // 건물 건설하면 해당 적 등장 확률 높아짐
    // hunt mode로 들어가야 적 스폰
    // build mode로 들어가면 공격만 안되고 적은 계속 날아다님

    void Awake()
    {
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(gameObject);
    }

    void Start()
    {
        // BuildManager에서 세이브 파일 보고 스폰하면서 리스트에 저장해놓는데
        // 거기서 건물들 효과 하나하나 GetComponent<interface>() 호출하면서 
    }

    public void ChangeToHuntMode()
    {
        UIManager.Instance.SetHuntUI();
        frog.GetComponent<FrogAttack>().EnableAttack();
    }
}