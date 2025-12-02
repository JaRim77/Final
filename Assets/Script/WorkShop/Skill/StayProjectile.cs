using UnityEngine;

public class StayProjectile : MonoBehaviour
{
    public int damage = 50;     // ดาเมจที่ต้องการ
    public float lifeTime = 3f; // อยู่กี่วินาที

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        MonsterGO m = other.GetComponent<MonsterGO>();
        if (m != null)
        {
            m.TakeDamage(damage);
            Debug.Log($"🔥 {m.Name} took {damage} damage from StayProjectile!");
        }
    }
}
