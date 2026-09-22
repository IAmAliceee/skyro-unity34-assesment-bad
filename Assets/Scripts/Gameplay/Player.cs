using System.Collections;
using Managers;
using Tools;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [Header("References")]
    [field: SerializeField] private Rigidbody2D BulletPrefab;
    [field: SerializeField] private EVar<int> Health;
    [field: SerializeField] private float speed;
    [field: SerializeField] private float bulletSpeed;
    [field: SerializeField] private float bulletCooldown;
    [field: SerializeField] private string enemyTag;

    private void Start()
    {
        Health.OnChanged.AddListener((newHealth) => GameManager.Instance.Events.OnHealthChanged.Invoke(newHealth));

        StartCoroutine(Shoot());
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.position +=
            (Vector3)GameManager.Instance.Inputs.MoveKey.action.ReadValue<Vector2>().normalized *
            speed * Time.deltaTime;
    }

    private IEnumerator Shoot()
    {
        while (true)
        {
            if (!GameManager.Instance.Inputs.ShootKey.action.IsPressed())
                yield break;

            var mousePos = Mouse.current.position.ReadValue();
            var mouseWorldPos = Camera.main.ScreenToWorldPoint(mousePos, Camera.MonoOrStereoscopicEye.Mono);
            var shootDir = (mouseWorldPos - transform.position).normalized;

            var bulletRb = Instantiate(BulletPrefab, transform.position, Quaternion.identity);
            bulletRb.linearVelocity = shootDir * bulletSpeed;

            yield return new WaitForSeconds(bulletCooldown);
        }
    }
}
