using UnityEngine;

public class Projectile : MonoBehaviour
{
    private int damage;
    private float speed;
    private float maxDistance;
    private Vector3 direction;
    private Character owner;

    private Vector3 startPos;

    public void Init(int dmg, Vector3 dir, float spd, float dist, Character caster)
    {
        damage = dmg;
        direction = dir.normalized;
        speed = spd;
        maxDistance = dist;
        owner = caster;

        startPos = transform.position;
    }

    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;

        // ถ้าวิ่งไกลเกิน → ทำลาย
        if (Vector3.Distance(startPos, transform.position) >= maxDistance)
            Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        // ห้ามโดนตัวที่ยิง
        if (other.GetComponent<Character>() == owner) return;

        MonsterGO m = other.GetComponent<MonsterGO>();
        if (m != null)
        {
            m.TakeDamage(damage);
           
        }
    }
}
