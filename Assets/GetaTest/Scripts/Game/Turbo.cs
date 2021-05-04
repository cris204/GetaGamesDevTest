using UnityEngine;
using UnityEngine.Events;

public class Turbo : MonoBehaviour
{

    public CarController.StatPowerup boostStats = new CarController.StatPowerup
    {
        PowerUpID = "Turbo",
        MaxTime = 5
    };

    public bool isCoolingDown { get; private set; }
    public float lastActivatedTimestamp { get; private set; }

    public float cooldown;

    private void Awake()
    {
        lastActivatedTimestamp = -9999f;
    }


    private void Update()
    {
        if (isCoolingDown) {

            if (Time.time - lastActivatedTimestamp > cooldown) {
                isCoolingDown = false;
            }

        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (isCoolingDown) return;

        var rb = other.attachedRigidbody;
        if (rb) {

            var kart = rb.GetComponent<CarController>();

            if (kart) {
                lastActivatedTimestamp = Time.time;
                kart.AddPowerup(this.boostStats);
                isCoolingDown = true;
            }
        }
    }

}
