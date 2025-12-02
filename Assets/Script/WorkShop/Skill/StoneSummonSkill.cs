using UnityEngine;

[CreateAssetMenu(fileName = "StationaryProjectileSkill2", menuName = "Skills/Stationary Projectile 2")]
public class StationaryProjectileSkill2 : Skill
{
    public GameObject projectilePrefab;
    public Transform firePoint2;

    public StationaryProjectileSkill2()
    {
        skillName = "Stationary Spawn Skill";
        cooldownTime = 10f;
    }

    public override void Activate(Character character)
    {
        if (projectilePrefab == null || firePoint2 == null)
        {
            Debug.LogWarning("❌ StationaryProjectileSkill2 missing prefab or firePoint2 !");
            return;
        }

        // ✨ สร้าง prefab ตรงจุดที่กำหนด เหมือนเสกของขึ้นจากพื้น
        GameObject fx = Object.Instantiate(projectilePrefab, firePoint2.position, firePoint2.rotation);

        // ❌ ห้ามทำลายตรงนี้ ปล่อยให้ prefab ควบคุมเอง
        // Destroy(fx, 3f);  <-- ลบออก

        TimeStampSkill(Time.time);
    }

    public override void Deactivate(Character character) { }

    public override void UpdateSkill(Character character) { }
}
