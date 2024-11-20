using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class FrogAttack : MonoBehaviour
{
    private Transform tongue;
    private SpriteRenderer tongueSR;
    private PlayerInput playerInput;
    private InputAction AttackAction;
    private bool isAttacking;
    private bool arrivedPoint;

    private Vector3 tonguePos;
    private Vector3 mousePos;
    private Vector3 direction;
    private float angle;
    private float targetLength;
    [SerializeField] private float tongueSpeed = 10f;

    void Awake()
    {
        tongue = transform.Find("tongue"); // TODO :: tongue 따로 할당해서 위치 설정해야 함
        tongueSR = tongue.GetComponent<SpriteRenderer>();
        tongueSR.size = new Vector2(0, tongueSR.size.y);

        // PlayerInput 컴포넌트에서 InputAction 가져오기
        playerInput = GetComponent<PlayerInput>();
        AttackAction = playerInput.actions["Attack"];
    }

    // 혀의 위치로부터 마우스 클릭 위치까지 각도를 구한 뒤에,
    // 각도만큼 돌린 후에 거리만큼 늘리면 됨.

    // 혀 공격 중이라면 점에 닿을 때까지 늘려야 하고
    // 혀 공격이 달성됐다면 다시 줄여야 함
    // 늘리거나 줄이는 건 Time.deltaTime만큼

    void Update()
    {
        // 혀 공격은 혀를 늘리는 동작과 줄이는 동작으로 나뉜다
        if (isAttacking)
        {
            // 점에 도착하지 못했다면 혀를 늘리고
            if (!arrivedPoint)
                StretchTongue();
            // 점에 도착했다면 혀를 줄인다
            else
                ShrinkTongue();

        }
        else if (AttackAction.triggered)
        {
            // 클릭 당시 혀와 마우스의 위치를 설정하고 각도를 구한다.
            tonguePos = tongue.position;
            mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            direction = mousePos - tonguePos;
            direction.z = 0; // magnitude 왜곡 방지
            targetLength = direction.magnitude;
            angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // Debug.Log("tongePos : " + tonguePos);
            // Debug.Log("direction : " + direction);
            // Debug.Log("targetLength : " + targetLength);

            // 각도만큼 혀를 돌린다
            tongue.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

            isAttacking = true;
            arrivedPoint = false;
        }
    }

    private void StretchTongue()
    {
        if (tongueSR.size.x >= targetLength)
        {
            arrivedPoint = true;
            return;
        }

        tongueSR.size = new Vector2(tongueSR.size.x + tongueSpeed * targetLength * Time.deltaTime, tongueSR.size.y);
    }

    private void ShrinkTongue()
    {
        if (tongueSR.size.x <= 0)
        {
            isAttacking = false;
            tongueSR.size = new Vector2(0, tongueSR.size.y);
            return;
        }

        tongueSR.size = new Vector2(tongueSR.size.x - tongueSpeed * targetLength * Time.deltaTime, tongueSR.size.y);
    }
}
