using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseStatsEffect : MonoBehaviour
{
    public CarController.StatPowerup boostStats = new CarController.StatPowerup
    {
        MaxTime = 5
    };

    public bool needToDissapear;

    public bool isCoolingDown { get; private set; }
    public float lastActivatedTimestamp { get; private set; }

    public float cooldown;

    private void Awake()
    {
        lastActivatedTimestamp = -9999f;
    }


    public void Activate(CarController kart) {
        boostStats.ElapsedTime = 0;
        if (kart) {
            lastActivatedTimestamp = Time.time;
            kart.AddPowerup(this.boostStats);
            isCoolingDown = true;
        }

        if (needToDissapear) {
            PoolManager.Instance.ReleaseObject(string.Format(Env.GENRIC_GAMEOBJECT_PATH, this.name), this.gameObject);
        }
    }

    private void Update()
    {
        if (isCoolingDown) {
            if (Time.time - lastActivatedTimestamp > cooldown) {
                isCoolingDown = false;
            }
        }
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        if (isCoolingDown) return;

        Rigidbody rb = other.attachedRigidbody;
        if (rb) {
            CarController kart = rb.GetComponent<CarController>();
            Activate(kart);
        }

        if (needToDissapear) {
            PoolManager.Instance.ReleaseObject(string.Format(Env.GENRIC_GAMEOBJECT_PATH, this.name),this.gameObject);
        }

    }
}
