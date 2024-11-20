using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RandomFly : MonoBehaviour
{
    private Vector2 RandomDirection => Random.insideUnitCircle.normalized;
    private Vector2[] Course;
    private float interpolateAmount;
    [SerializeField] private GameObject SpawnArea;

    void Awake()
    {
        // 스폰 위치 정해놓고 그 안에서만 랜덤하게 스폰
        Bounds SpawnBounds = SpawnArea.GetComponent<Renderer>().bounds;
        float randomX = Random.Range(SpawnBounds.min.x, SpawnBounds.max.x);
        float randomY = Random.Range(SpawnBounds.min.y, SpawnBounds.max.y);
        transform.position = new Vector2(randomX, randomY);
        Course = RandomNewCourse(transform.position, 10f);
    }

    void Update()
    {
        interpolateAmount += Time.deltaTime;

        Debug.Log("interpolateAmount = " + interpolateAmount);

        if (interpolateAmount > 1f)
        {
            interpolateAmount = 0;
            Course = RandomNewCourse(transform.position, 10f);
        }

        transform.position = QuadraticLerp(Course[0], Course[1], Course[2], interpolateAmount);
    }

    // 2중 보간
    private Vector2 QuadraticLerp(Vector2 a, Vector2 b, Vector2 c, float t)
    {
        Vector2 ab = Vector2.Lerp(a, b, t);
        Vector2 bc = Vector2.Lerp(b, c, t);
        return Vector2.Lerp(ab, bc, t);
    }

    // 시작점에서 가까운 점 C를 고르고, 멀 수도 있는 점 B를 고른다.
    private Vector2[] RandomNewCourse(Vector2 start, float maxDistance)
    {
        float d_close = Random.Range(0, maxDistance);
        float d_far = Random.Range(0, maxDistance * 2);

        Vector2[] NewCourse = new Vector2[3];
        NewCourse[0] = start;
        NewCourse[1] = start + RandomDirection * d_far;
        NewCourse[2] = start + RandomDirection * d_close;

        return NewCourse;
    }
}
