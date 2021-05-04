using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VfxCallBack : MonoBehaviour
{
    public void OnParticleSystemStopped()
    {
        PoolManager.Instance.ReleaseObject(string.Format(Env.GENRIC_VFX_GAMEOBJECT_PATH, this.name), this.gameObject);
    }
}
