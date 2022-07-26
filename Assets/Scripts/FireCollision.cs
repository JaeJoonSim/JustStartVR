using UnityEngine;

public class FireCollision : MonoBehaviour
{
    ParticleSystem ParticleSystem;
    ParticleSystem.Particle[] Particle;

    GameObject Player;
    Player_HP playerHp;

    void Start()
    {
        ParticleSystem = this.GetComponent<ParticleSystem>();
        Particle = new ParticleSystem.Particle[ParticleSystem.maxParticles];


        Player = GameObject.Find("Collider").gameObject;
        playerHp = Player.GetComponent<Player_HP>();
    }

    void FixedUpdate()
    {
        int max = ParticleSystem.GetParticles(Particle);

        float distance = 0;

        for (int i = 0; i < max; i++)
        {
            
            distance = Mathf.Abs((Particle[i].position - Player.transform.position).magnitude);


            if (distance < 1.5f)
            {
                playerHp.change_HP(-.2f);
                break;
            }
        }
    }
}
