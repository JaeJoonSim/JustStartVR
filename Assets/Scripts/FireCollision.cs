using UnityEngine;

public class FireCollision : MonoBehaviour
{
    ParticleSystem ParticleSystem;
    ParticleSystem.Particle[] Particle;

    GameObject Player;
    Player_HP playerHp;

    float time;

    void Start()
    {
        time = 0f;

        ParticleSystem = this.GetComponent<ParticleSystem>();
        Particle = new ParticleSystem.Particle[ParticleSystem.maxParticles];


        Player = GameObject.Find("Collider").gameObject;
        playerHp = Player.GetComponent<Player_HP>();
    }

    void Update()
    {

        int max = ParticleSystem.GetParticles(Particle);
        time += Time.deltaTime;
        if (time > 0.5f)
        {
            time = 0;
            float distance = 0;

            for (int i = 0; i < max; i++)
            {
                distance = Mathf.Abs((Particle[i].position - Player.transform.position).magnitude);

                if (distance < 1.0f)
                {
                    playerHp.change_HP(-5f);
                    break;
                }
            }
            for (int i = 0; i < max; i++)
            {
                Collider[] closeZombies = Physics.OverlapSphere(Particle[i].position, 1, 1 << 16);
                for (int j = 0; j < closeZombies.Length; ++j)
                {
                    EnemyBaseFSMMgr temp = closeZombies[j].GetComponent<EnemyBaseFSMMgr>();
                    if (temp != null && !temp.isBurning)
                    {

                        temp.isBurning = true;
                        temp.Damaged(10);

                    }
                }
            }


            if (max == 0)
                Destroy(this.transform.root.gameObject);
        }
        //if(time > 0.0f)
        //time -= Time.deltaTime;

        
    }
}
