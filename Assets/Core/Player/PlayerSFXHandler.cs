using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSFXHandler : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;

    [SerializeField] SoundSO gunClip;
    [SerializeField] SoundSO grenadeClip;

    [SerializeField] SoundSO jumpClip;
    [SerializeField] SoundSO pickClip;

    [SerializeField] SoundSO hurtClip;
    [SerializeField] SoundSO deathClip;
    [SerializeField] SoundSO megaKillClip;
    [SerializeField] SoundSO partyMusic;

   [SerializeField] Health myHealth;
    private void Awake()
    {
        myHealth = GetComponent<Health>();
    }
    private void OnEnable()
    {
        myHealth.OnHurt += HurtSound;
        myHealth.OnDeath += DeathSound;

        PlayerController.OnJump += JumpSound;
        PlayerController.OnPick += PickSound;
        Gun.OnLauncherShoot += GreneadSound;
        Gun.OnGunFire += GunSound;
        UI_Score.OnMegaKill += MegaKill;
        DiscoballManager.OnDiscoballHit += PartyMusic;
    }
    private void OnDisable()
    {
        myHealth.OnHurt -= HurtSound;
        myHealth.OnHurt -= DeathSound;

        PlayerController.OnJump -= JumpSound;
        PlayerController.OnPick -= PickSound;
        Gun.OnLauncherShoot -= GreneadSound;
        Gun.OnGunFire -= GunSound;
        UI_Score.OnMegaKill -= MegaKill;
    }
    public void PlayGunFire() => GameAudioManager.instance.PlaySFX(audioSource, gunClip, gunClip.volume, gunClip.randomPitch);
    public void JumpSound() => GameAudioManager.instance.PlaySFX(audioSource, jumpClip, jumpClip.volume, jumpClip.randomPitch);
    public void PickSound(Pickable _pickable) => GameAudioManager.instance.PlaySFX(null, pickClip, pickClip.volume, pickClip.randomPitch);
    public void HurtSound() => GameAudioManager.instance.PlaySFX(audioSource, hurtClip, hurtClip.volume, hurtClip.randomPitch);
    public void DeathSound() => GameAudioManager.instance.PlaySFX(null, deathClip, deathClip.volume, deathClip.randomPitch);
    public void GreneadSound() => GameAudioManager.instance.PlaySFX(audioSource, grenadeClip, grenadeClip.volume, grenadeClip.randomPitch);
    public void GunSound() => GameAudioManager.instance.PlaySFX(audioSource, gunClip, gunClip.volume, gunClip.randomPitch);
    public void MegaKill() => GameAudioManager.instance.PlaySFX(null, megaKillClip, megaKillClip.volume, false);
    public void PartyMusic() => GameAudioManager.instance.ChangeBGM(null, partyMusic );
}
