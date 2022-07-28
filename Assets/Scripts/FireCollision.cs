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
        time = 1.0f;

        ParticleSystem = this.GetComponent<ParticleSystem>();
        Particle = new ParticleSystem.Particle[ParticleSystem.maxParticles];


        Player = GameObject.Find("Collider").gameObject;
        playerHp = Player.GetComponent<Player_HP>();
    }

    void Update()
    {
        int max = ParticleSystem.GetParticles(Particle);

        if(time > 0.0f)
        time -= Time.deltaTime;

        float distance = 0;

        for (int i = 0; i < max; i++)
        {
            
            distance = Mathf.Abs((Particle[i].position - Player.transform.position).magnitude);


            if (distance < 1.0f)
            {
                playerHp.change_HP(-.2f);
                break;
            }
        }

        if (max == 0 && time <= 0)
            Destroy(this.transform.root.gameObject);
    }
}
