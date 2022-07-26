using UnityEngine;

public class ElevatorPowerOn : MonoBehaviour
{
    Animator ani;

    void Start()
    {
        ani = this.transform.GetComponent<Animator>();
        ani.speed = 0.0f;
    }

    public void setSpeed(float value)
    {
        ani.speed = value;
    }
}
