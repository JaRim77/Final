using System.Collections.Generic;
using UnityEngine;

public class SkillBook : MonoBehaviour
{
    public List<Skill> skillsSet = new List<Skill>();
    public GameObject[] skillEffects;

    List<Skill> DulationSkills = new List<Skill>();

    public Transform firePoint;      // ใช้กับ ProjectileSkill
    public GameObject projectilePrefab;

    public Transform firePoint2;     // ใช้กับ StationaryProjectileSkill2
    public GameObject stationaryPrefab;

    Player player;

    void Start()
    {
        player = GetComponent<Player>();

        // -------------------------
        // สกิลจากระบบเดิม (ไม่แก้)
        // -------------------------
        skillsSet.Add(new FireballSkill());
        skillsSet.Add(new HealSkill());
        skillsSet.Add(new BuffSkillMoveSpeed());

        // สกิลยิง projectile เดิม
        ProjectileSkill ps = new ProjectileSkill();
        ps.firePoint = firePoint;
        ps.projectilePrefab = projectilePrefab;
        skillsSet.Add(ps);

        // -------------------------
        // ⭐ เพิ่มสกิลสร้างวัตถุค้างที่ตำแหน่ง firePoint2 ⭐
        // -------------------------
        StationaryProjectileSkill2 sp2 = new StationaryProjectileSkill2();
        sp2.projectilePrefab = stationaryPrefab;
        sp2.firePoint2 = firePoint2;
        skillsSet.Add(sp2);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) UseSkill(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) UseSkill(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) UseSkill(2);
        if (Input.GetKeyDown(KeyCode.Alpha4)) UseSkill(3);
        if (Input.GetKeyDown(KeyCode.Alpha5)) UseSkill(4);  // ⭐ สกิลใหม่


        for (int i = DulationSkills.Count - 1; i >= 0; i--)
        {
            DulationSkills[i].UpdateSkill(player);
            if (DulationSkills[i].timer <= 0)
            {
                DulationSkills.RemoveAt(i);
            }
        }
    }

    public void UseSkill(int index)
    {
        if (index < 0 || index >= skillsSet.Count) return;

        Skill skill = skillsSet[index];

        if (!skill.IsReady(Time.time))
        {
            Debug.Log($"Skill '{skill.skillName}' cooldown...");
            return;
        }

        if (index < skillEffects.Length && skillEffects[index] != null)
        {
            GameObject fx = Instantiate(skillEffects[index], transform.position, Quaternion.identity);
            Destroy(fx, 1);
        }

        skill.Activate(player);
        skill.TimeStampSkill(Time.time);

        if (skill.timer > 0)
            DulationSkills.Add(skill);
    }
}
