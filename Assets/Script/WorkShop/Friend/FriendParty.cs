using UnityEngine;

public class FriendParty : Character
{
    protected enum State { idel, cheses, attack, death }

    [SerializeField]
    private float TimeToAttack = 1f;
    protected State currentState = State.idel;
    protected float timer = 0f;

    Animator animator;
    private void Update()
    {
        if (enemy == null)
        {
            animator.SetBool("Attack", false);
            return;
        }

        Turn(enemy.transform.position - transform.position);
        timer -= Time.deltaTime;

        if (GetDistanPlayer() < 1.5)
        {
            Attack(enemy);
        }
        else
        {
            animator.SetBool("Attack", false);
        }
    }


    protected override void Turn(Vector3 direction)
    {
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = lookRotation;
    }
    protected virtual void Attack(Enemy _enemy)
    {
        if (timer <= 0)
        {
            _enemy.TakeDamage(Damage);
            animator.SetBool("Attack", true);
            Debug.Log($"{Name} attacks {_enemy.Name} for {Damage} damage.");
            timer = TimeToAttack;
        }

    }

    public override void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
         
            Destroy(gameObject);

        }
    }
}
