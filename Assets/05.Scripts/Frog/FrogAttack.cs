using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class FrogAttack : MonoBehaviour
{
    private Transform tongue;
    private SpriteRenderer tongueSR;
    private SpriteRenderer frogSR;
    private PlayerInput playerInput;
    private InputAction AttackAction;
    private bool isAttacking;
    private bool arrivedPoint;
    private Animator animator;

    private Vector3 tonguePos;
    private Vector3 mousePos;
    private Vector3 direction;
    private float angle;
    private float targetLength;
    [SerializeField] private float tongueSpeed = 10f;
    private CircleCollider2D tongueCollider;
    private bool canAttack;
    private GameObject catchedBug;

    void Awake()
    {
        tongue = transform.Find("tongue");
        tongueSR = tongue.GetComponent<SpriteRenderer>();
        tongueSR.size = new Vector2(0, tongueSR.size.y);

        // PlayerInput 컴포넌트에서 InputAction 가져오기
        playerInput = GetComponent<PlayerInput>();
        AttackAction = playerInput.actions["Attack"];

        frogSR = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        tongueCollider = tongue.GetComponent<CircleCollider2D>();
        // DisableAttack();
        EnableAttack();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag != "Enemy") return;
        arrivedPoint = true;
        EnemyManager.Instance.DestroyEnemy(other.gameObject);
    }

    public void EnableAttack()
    {
        tongueCollider.enabled = true;
        canAttack = true;
    }

    public void DisableAttack()
    {
        tongueCollider.enabled = false;
        canAttack = false;
    }

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
        else if (canAttack && AttackAction.triggered)
        {
            StartAttack();
        }
    }

    private void StartAttack()
    {
        SoundManager.Instance.PlayAttackSFX();

        // 클릭 당시 혀와 마우스 또는 터치의 위치를 설정하고 각도를 구한다.
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            mousePos = Camera.main.ScreenToWorldPoint(touch.position);
        }
        else
        {
            mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        }
        // Debug.Log("mousePos : " + mousePos);
        FlipCharacter();
        tonguePos = tongue.position;

        direction = mousePos - tonguePos;
        direction.z = 0; // magnitude 왜곡 방지
        targetLength = direction.magnitude;

        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        tongue.rotation = Quaternion.Euler(new Vector3(0, 0, angle)); // 각도만큼 혀를 돌린다

        // 애니메이션을 위한 변수들 설정
        isAttacking = true;
        animator.SetBool("isAttacking", true);
        arrivedPoint = false;

        // 혀로 파리 붙잡을 콜라이더 활성화
        tongueCollider.enabled = true;
    }

    private void StretchTongue()
    {
        if (tongueSR.size.x >= targetLength)
        {
            arrivedPoint = true;
            return;
        }

        tongueSR.size = new Vector2(tongueSR.size.x + tongueSpeed * targetLength * Time.deltaTime, tongueSR.size.y);
        tongueCollider.offset = new Vector2(tongueCollider.offset.x + tongueSpeed * targetLength * Time.deltaTime, 0);
    }

    private void ShrinkTongue()
    {
        if (tongueSR.size.x <= 0.1)
        {
            isAttacking = false;
            animator.SetBool("isAttacking", false);
            tongueSR.size = new Vector2(0, tongueSR.size.y);
            tongueCollider.offset = new Vector2(0, 0);
            tongueCollider.enabled = false;

            // if (catchedBug != null)
            // {
            //     Destroy(catchedBug);
            // }

            return;
        }

        tongueSR.size = new Vector2(tongueSR.size.x - tongueSpeed * targetLength * Time.deltaTime, tongueSR.size.y);
        tongueCollider.offset = new Vector2(tongueCollider.offset.x - tongueSpeed * targetLength * Time.deltaTime, 0);
    }

    private void FlipCharacter()
    {
        bool TrueIsLeft = (mousePos.x - tongue.position.x < 0) ? true : false;

        if (frogSR.flipX != TrueIsLeft)
        {
            if (TrueIsLeft)
            {
                Vector3 newPosition = tongue.position;
                newPosition.x -= 0.25f;
                tongue.position = newPosition;
            }
            else
            {
                Vector3 newPosition = tongue.position;
                newPosition.x += 0.25f;
                tongue.position = newPosition;
            }
        }

        frogSR.flipX = TrueIsLeft;
    }
}
