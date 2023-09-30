using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public enum GunType
{
    Machingun,
    Launcher
}
public class Gun : MonoBehaviour
{
    public static event Action OnShoot;
    public static event Action OnLauncherShoot;

    private Animator myAnim;

    [Header("Gun Info ")]
    public GunType gunType;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject muzzleFalsh;
    private CinemachineImpulseSource impulseSource;

    [Header("Machine Gun Info")]
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] float bulletSpeed;
    [SerializeField] private float gunCd;
    private float lastFire;


    [Header("Launcher Gun Info")]
    [SerializeField] Grenade grenadePrefabs;
    [SerializeField] float grenadeSpeed;
    [SerializeField] private float launcherCd;
    private float lastLauncherFire;


    private Vector2 mousePos;
    private ObjectPooler objectPooler;
    private PlayerController player;

    private Coroutine muzzleFlashRoutine;

    private readonly int Fire_Has = Animator.StringToHash("Gun_Fire");


    private void OnEnable()
    {
        OnShoot += ShootBullet;
        OnShoot += ResrtLastFire;
        OnShoot += FireAnimation;
        OnShoot += MuzzleFlash;
        OnShoot += ScreenShake;

        OnLauncherShoot += ShootGrenad;
        OnLauncherShoot += ScreenShake;
        OnLauncherShoot += ResrtLauncherLastFire;
        OnLauncherShoot += FireAnimation;
    }
    private void OnDisable()
    {
        OnShoot -= ShootBullet;
        OnShoot -= ResrtLastFire;
        OnShoot -= FireAnimation;
        OnShoot -= MuzzleFlash;
        OnShoot -= ScreenShake;

        OnLauncherShoot -= ShootGrenad;
        OnLauncherShoot -= ScreenShake;
        OnLauncherShoot -= ResrtLauncherLastFire;
        OnLauncherShoot -= FireAnimation;

    }

    private void Start()
    {
        player = PlayerController.Instance;
        objectPooler = ObjectPooler.instance;
        impulseSource = GetComponent<CinemachineImpulseSource>();
        myAnim = GetComponentInChildren<Animator>();
    }


    private void Update()
    {
        Shoot();
        GunRotation();
        ChangeGunMode();
    }

    private void ChangeGunMode()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (gunType == GunType.Machingun)
            {
                gunType = GunType.Launcher;
            }
       else      if (gunType == GunType.Launcher)
            {
                gunType = GunType.Machingun;
            }
        }
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
        if (Input.GetMouseButton(0))
        {
            if (gunType == GunType.Machingun)
            {

                if (lastFire <= Time.time)
                {
                    OnShoot?.Invoke();
                }
            }
            else if (gunType == GunType.Launcher)
            {
                if (lastLauncherFire <= Time.time)
                {
                    OnLauncherShoot?.Invoke();
                }
            }
        }
    }
        private void ResrtLastFire() => lastFire = Time.time + gunCd;
        private void ResrtLauncherLastFire() => lastLauncherFire = Time.time + launcherCd;
        private void FireAnimation() => myAnim.Play(Fire_Has, 0, 0);
        private void ScreenShake() => impulseSource.GenerateImpulse();

        private void ShootBullet()
        {
            GameObject newBullet = objectPooler.GetObjectFormPool(_bulletPrefab, firePoint.position);
            if (newBullet != null)
                newBullet?.GetComponent<Bullet>().SetUp(player.transform.position, mousePos, bulletSpeed);
        }
        private void ShootGrenad()
        {
            Grenade newGrenade = Instantiate(grenadePrefabs, firePoint.position, Quaternion.identity);
            newGrenade.LaunchGrenade(player.transform.position, mousePos, grenadeSpeed);
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
