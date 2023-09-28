using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateObject : MonoBehaviour
{
    [SerializeField] float lifeTime=1;
    private float defaultLifetime;
    #region UNITY_METHODS
    private void Awake()
    {
        defaultLifetime = lifeTime;
    }
    private void OnEnable()
    {
        Invoke(nameof(Deactivate),lifeTime );
    }
    private void OnDisable()
    {
        lifeTime = defaultLifetime;
        CancelInvoke(nameof(Deactivate));
    }
    #endregion
    public void SetLifeTime(float _life) => lifeTime = _life;
    private void Deactivate() => gameObject.SetActive(false);

}
