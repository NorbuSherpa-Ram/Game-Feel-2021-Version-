using UnityEngine;
using System.Collections;
using UnityEngine.Rendering.Universal;

public class Spot_Light : MonoBehaviour
{
    [Header("Light Info")]
    [SerializeField] private Light2D myLight;
    [SerializeField] private Transform lightTop;

    [SerializeField] private Color lightColor;
   [SerializeField] private float lightIntensity;

    [Header("Rotation Info")]
    [SerializeField] private float maxRotation = 20;
    [SerializeField] private float rotationSpeed = 20;
    private float defaultRotaionSpeed, startingRotation;

    private void OnValidate()
    {
        SetIntensity();
        SetLightColor();
    }
    private void Start()
    {
        defaultRotaionSpeed = rotationSpeed;
        RandomStartingRotation();
    }
    private void Update() => PingPong();
    #region ROTATION LOGIC 
    private void RandomStartingRotation()
    {
        float ranRotaion = Random.Range(-maxRotation, maxRotation);
        lightTop.transform.localRotation = Quaternion.Euler(0, 0, ranRotaion);
        startingRotation = ranRotaion + maxRotation ;
        //if ranRotaioan =20 then it should rotate 20+maxRotaion if not added it will belike maxRot(40) - 20  lets = 20 not its rotate 0 to 20 
        //if Added now it rotate between 60 , 20 which is 40 angle  which is equal to maxRotaion 

    }
    private void PingPong()
    {
        // t = 20 it will be close to 0 like 0.265/0  and check if(t>=maxRotate ) rotate Toword maxRotation , else rotate towoard 0
        startingRotation += Time.deltaTime * rotationSpeed;
        float zRotation = Mathf.PingPong(startingRotation, maxRotation );
        lightTop.transform.localRotation = Quaternion.Euler(0, 0, zRotation);
    }
    #endregion

    #region SPEED EFFECT
    public void IncreaseRotationSpeed(float _increaseByPercentage, float _increaseFor)
    {
        rotationSpeed +=rotationSpeed*_increaseByPercentage;
       StartCoroutine(ResetRotaionSpeed(_increaseFor));
    }
    private IEnumerator ResetRotaionSpeed(float _increaseFor)
    {
        yield return new WaitForSeconds(_increaseFor);
        rotationSpeed = defaultRotaionSpeed;
    }
    #endregion

    #region LIGHT_SETTING
    private void SetLightColor() => myLight.color = lightColor;
    private void SetIntensity() => myLight.intensity = lightIntensity;
    #endregion
}
