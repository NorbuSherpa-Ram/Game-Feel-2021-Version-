using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Twee
using DG.Tweening;


public class Blood_Splatter : MonoBehaviour
{
    [SerializeField] LayerMask layerToCheck;
    [SerializeField] SpriteRenderer mySr;
    [SerializeField] float raylength;
    [SerializeField] Transform rightCheck;
    [SerializeField] Transform leftCheck;
    bool startFade;

    void Start()
    {
        mySr.color = GetComponent<SpriteRenderer>().color;
        RaycastHit2D hitGround = Physics2D.Raycast(transform.position, Vector2.down, raylength, layerToCheck);
        if (hitGround)
        {
            transform.position = hitGround.point;
        }
        Invoke(nameof(FadeOut), 1);
    }
    void FadeOut() => startFade = true;

    private void Update()
    {
        if (startFade)
            mySr.color = Color.Lerp(mySr.color, new Color(1, 1, 1, 0), .3f * Time.deltaTime);


        if (Physics2D.Raycast(rightCheck.position, Vector2.right, 1, layerToCheck))
        {
            Vector2 pos = transform.position;
            pos.x-= .1f;
            transform.position = pos;
        }  
        if (Physics2D.Raycast(leftCheck.position, Vector2.left, 1, layerToCheck))
        {
            Vector2 pos = transform.position;
            pos.x+= .1f;
            transform.position = pos;
        }
    }

}
