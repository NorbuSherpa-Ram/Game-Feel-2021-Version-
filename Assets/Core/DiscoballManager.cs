using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Rendering.Universal;
public class DiscoballManager : MonoBehaviour
{
    public static DiscoballManager instance;

    [Tooltip("Event Info")]
    public static event Action OnDiscoballHit;


    [Header("Spot_Light  Info")]
    [SerializeField] private Spot_Light[] lights;
    [SerializeField] private float partyDuration = 1f;
    [Range(0, 1)] [SerializeField] private float speedModifire = .2f;

    [SerializeField] private Light2D globalLight;
    [SerializeField] private float globalLightIntensity = 1f;

    private Coroutine discoRutine;
    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    private void OnEnable()
    {
        OnDiscoballHit += DiscoParty;
    }
    private void OnDisable()
    {
        OnDiscoballHit -= DiscoParty;
    }
    void Start()
    {
        lights = FindObjectsByType<Spot_Light>(FindObjectsSortMode.None);
    }
    public void TriggerDiscoParty()
    {
        if (discoRutine != null) return;
        OnDiscoballHit?.Invoke();
    }
    private void DiscoParty()
    {
        discoRutine = StartCoroutine(DimGlobalLight());
        foreach (var item in lights)
        {
            item.IncreaseRotationSpeed(speedModifire, partyDuration);
        }
    }

    private IEnumerator DimGlobalLight()
    {
        float defaultIntensity = globalLight.intensity;
        globalLight.intensity = globalLightIntensity;
        yield return new WaitForSeconds(partyDuration);
        globalLight.intensity = defaultIntensity;
        discoRutine = null;
    }
}
