using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomFlying : MonoBehaviour
{
    private Vector3 fall_v;
    private Vector3 moving_v;
    [SerializeField] private Vector3 Center;
    [SerializeField] private float speed = 20f;
    [SerializeField] private float grav = 10f;
    private float sec_count;
    private GameObject flyingArea;
    private Bounds flyingBounds;

    // Start is called before the first frame update
    void Start()
    {
        sec_count = 0;
        flyingArea = GameObject.FindWithTag("flyingArea");
        flyingBounds = flyingArea.GetComponent<SpriteRenderer>().bounds;
        speed = 5 * GameManager.Instance.difficulty;
        speed = RandomChoice((int)speed, -(int)speed);
        Center = RandomCenter();
    }

    void Update()
    {
        sec_count += Time.deltaTime;
        float sqrDistance = (Center - transform.position).sqrMagnitude;

        if (sec_count > 0.4f || sqrDistance < 0.1f)
        {
            sec_count = 0;
            Center = RandomCenter();
            speed = 5 * GameManager.Instance.difficulty;
            speed = RandomChoice((int)speed, -(int)speed);
            // Debug.Log("New Center : " + Center);
        }

        // 중심에 대해 원운동 시키기
        fall_v = (Center - transform.position).normalized;
        moving_v = new Vector3(-fall_v.y, fall_v.x) * speed;
        fall_v *= Mathf.Abs(speed / 2);
        transform.position += (moving_v + fall_v) * Time.deltaTime;
    }

    private Vector3 RandomCenter()
    {
        float randomX = Random.Range(flyingBounds.min.x, flyingBounds.max.x);
        float randomY = Random.Range(flyingBounds.min.y, flyingBounds.max.y);
        return new Vector3(randomX, randomY, 0);
    }

    int RandomChoice(int value1, int value2)
    {
        return Random.Range(0, 2) == 0 ? value1 : value2;
    }

    private void OnTriggerEnter2D(Collider2D tongue)
    {
        if (tongue.gameObject.name == "tongue")
            Destroy(gameObject);
    }
}
