using UnityEngine;

public class ProjectileSkill : Skill
{
    public GameObject projectilePrefab;
    public Transform firePoint;

    public float speed = 10f;
    public float maxDistance = 10f;
    public int damage = 30;

    public ProjectileSkill()
    {
        this.skillName = "Projectile";
        this.cooldownTime = 10f;
    }

    public override void Activate(Character character)
    {
        if (projectilePrefab == null || firePoint == null)
        {
            Debug.LogError("ProjectileSkill ต้องการ projectilePrefab และ firePoint แต่ยังไม่ถูกใส่ค่า!");
            return;
        }

        // ยิงกระสุนออกจากจุดยิง
        GameObject p = Object.Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

        // ส่ง damage ให้กระสุนรู้
        Projectile proj = p.GetComponent<Projectile>();
        proj.Init(damage, Vector3.down, speed, maxDistance, character);

        Debug.Log("ยิง Projectile ออกไป!");
    }

    public override void Deactivate(Character character) { }
    public override void UpdateSkill(Character character) { }
}
