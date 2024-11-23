using System.Collections;
using UnityEngine;

public class FrogMovement : MonoBehaviour
{
    private float moveAmount = 0.05f; // 위아래로 움직일 거리
    private float duration = 1f; // 움직이는 주기 (1초)
    private Vector3 initialPosition; // 초기 위치 저장

    void Start()
    {
        initialPosition = transform.position; // 초기 위치 저장
        StartCoroutine(MoveUpDown());
    }

    private IEnumerator MoveUpDown()
    {
        while (true)
        {
            // 아래로 이동
            yield return MoveToPosition(initialPosition - new Vector3(0, moveAmount, 0));

            // 위로 이동
            yield return MoveToPosition(initialPosition);
        }
    }

    private IEnumerator MoveToPosition(Vector3 targetPosition)
    {
        Vector3 startPosition = transform.position;
        float elapsed = 0;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            transform.position = Vector3.Lerp(startPosition, targetPosition, elapsed / duration);
            yield return null;
        }

        transform.position = targetPosition; // 정확한 위치로 고정
    }
}
