using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Pickable : MonoBehaviour
{
    [Range(0, 100)] public int dropChnage;
    [SerializeField] private float lifeTime = 5;
    [Range(0, 4)] [SerializeField] private int amount;

    [SerializeField] float groundRange = 1;
    [SerializeField] LayerMask groundLayer;

    Vector2 initialPosition;
    Vector3 finalPosition; // Change this to your desired final position
    private void Start()
    {
        if (Physics2D.Raycast(transform.position, Vector2.down, groundRange, groundLayer))
            transform.position += new Vector3(0, .5f);

        initialPosition = transform.position;
        finalPosition = new Vector3(initialPosition.x, 1f+initialPosition.y);

        EaseInOut();
        Invoke(nameof(Deactivate), lifeTime);
    }
    private void EaseInOut()
    {
        // Use DOTween to animate the position with ease-in and ease-out
        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOMove(finalPosition, .5f).SetEase(Ease.InOutQuad));
        seq.Append(transform.DOMove(initialPosition, .5f).SetEase(Ease.InOutQuad));
        seq.SetLoops(-1);


    }
    public int PickAmount() => Random.Range(1, amount);
    public void Deactivate() => this.gameObject.SetActive(false);

}
