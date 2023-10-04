using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
public class Respawn_Player : MonoBehaviour
{
    [SerializeField] GameObject playerPrefab;
    [SerializeField] Image fadeImage;

    //private void Start()
    //{
    //    float startValue = fadeImage.color.a;

    //    float fadeTime = 1;
    //    float startTime = 0;
    //    while (startTime < fadeTime)
    //    {
    //        startTime += Time.deltaTime;
    //        float a = Mathf.Lerp(startValue, 1, startTime / fadeTime);
    //        fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, a);
    //    }
    //}
    public void RespawnPlayer()
    {
        StartCoroutine(Wait());
    }
    private IEnumerator Wait()
    {
        //float startValue = fadeImage.color.a;

        //float fadeTime = 1;
        //float startTime = 0;
        //while (startTime < fadeTime)
        //{
        //    startTime += Time.deltaTime;
        //    float a = Mathf.Lerp(startValue, 1, 1);
        //    fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, a);
        //}
        yield return new WaitForSeconds(.5f);
        GameObject player = Instantiate(playerPrefab, transform.position, Quaternion.identity);
    }


}
