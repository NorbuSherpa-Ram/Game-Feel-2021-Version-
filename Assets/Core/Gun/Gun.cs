using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Gun : MonoBehaviour
{
    public static event Action OnGunFire;
    public static event Action OnLauncherShoot;

    private Animator myAnim;

    [Header("Gun Info ")]
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject muzzleFalsh;
    private CinemachineImpulseSource impulseSource;

    [Header("Machine Gun Info")]
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] float bulletSpeed;
    [SerializeField] private float gunCd;
    private float lastFire;


    [Header("Launcher Gun Info")]
    [SerializeField] private int grenadeAmount = 3;
    [SerializeField] private int maxGrenade; // How manay it can hold 
    [SerializeField] Grenade grenadePrefabs;
    [SerializeField] float grenadeSpeed;
    [SerializeField] private float launcherCd;
    private float lastLauncherFire;

    private Vector2 mousePos;
    private ObjectPooler objectPooler;
    private PlayerController player;

    private Coroutine muzzleFlashRoutine;


    [SerializeField] private UI_Grenade uI_Grenade;
    private readonly int FireHash = Animator.StringToHash("Gun_Fire");


    private void OnEnable()
    {
        OnGunFire += ShootBullet;
        OnGunFire += ResrtLastFire;
        OnGunFire += FireAnimation;
        OnGunFire += MuzzleFlash;
        OnGunFire += ScreenShake;

        OnLauncherShoot += ShootGrenad;
        OnLauncherShoot += ScreenShake;
        OnLauncherShoot += ResrtLauncherLastFire;
        OnLauncherShoot += FireAnimation;
        PlayerController.OnPick += RefillGrenade;
    }
    private void OnDisable()
    {
        OnGunFire -= ShootBullet;
        OnGunFire -= ResrtLastFire;
        OnGunFire -= FireAnimation;
        OnGunFire -= MuzzleFlash;
        OnGunFire -= ScreenShake;

        OnLauncherShoot -= ShootGrenad;
        OnLauncherShoot -= ScreenShake;
        OnLauncherShoot -= ResrtLauncherLastFire;
        OnLauncherShoot -= FireAnimation;
        PlayerController.OnPick -= RefillGrenade;
    }

    private void Start()
    {
        player = PlayerController.Instance;
        objectPooler = ObjectPooler.instance;
        impulseSource = GetComponent<CinemachineImpulseSource>();
        myAnim = GetComponentInChildren<Animator>();
        uI_Grenade.UpdateGrenadeUI(grenadeAmount);
    }


    private void Update()
    {
        Shoot();
        GunRotation();
    }

    private void GunRotation()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 localDirection = player.transform.InverseTransformPoint(mousePos);     // Calculate the direction from the object to the mouse in local space
        float angle = Mathf.Atan2(localDirection.y, localDirection.x) * Mathf.Rad2Deg;
        transform.localRotation = Quaternion.Euler(0, 0, angle);
    }
    private void Shoot()
    {

        uI_Grenade.UpdateGrenadeUI(grenadeAmount);

        if (Input.GetMouseButton(0))
        {
            if (lastFire <= Time.time)
            {
                OnGunFire?.Invoke();
            }
        }
        if (PlayerController.Instance.frameInput.Jetpack)
        {
            if (grenadeAmount < 1) return;
            if (lastLauncherFire <= Time.time)
            {
                OnLauncherShoot?.Invoke();
            }
        }
    }
    private void ResrtLastFire() => lastFire = Time.time + gunCd;
    private void ResrtLauncherLastFire() => lastLauncherFire = Time.time + launcherCd;
    private void FireAnimation() => myAnim.Play(FireHash, 0, 0);
    private void ScreenShake() => impulseSource.GenerateImpulse();

    private void ShootBullet()
    {
        GameObject newBullet = objectPooler.GetObjectFormPool(_bulletPrefab, firePoint.position);
        if (newBullet != null)
            newBullet?.GetComponent<Bullet>().SetUp(player.transform.position, mousePos, bulletSpeed);
    }
    private void ShootGrenad()
    {
        grenadeAmount--;
        Grenade newGrenade = Instantiate(grenadePrefabs, firePoint.position, Quaternion.identity);
        newGrenade.LaunchGrenade(player.transform.position, mousePos, grenadeSpeed);
    }
    private void RefillGrenade(Pickable _pickable)
    {
        grenadeAmount += _pickable.PickAmount();
        grenadeAmount = Mathf.Clamp(grenadeAmount, 0, maxGrenade);
    }
    private void MuzzleFlash()
    {
        if (muzzleFlashRoutine != null)
            StopCoroutine(muzzleFlashRoutine);

        muzzleFlashRoutine = StartCoroutine(MuzzleFlashDelay());
    }
    IEnumerator MuzzleFlashDelay()
    {
        muzzleFalsh.SetActive(true);
        yield return new WaitForSeconds(.08f);
        muzzleFalsh.SetActive(false);

    }
}
