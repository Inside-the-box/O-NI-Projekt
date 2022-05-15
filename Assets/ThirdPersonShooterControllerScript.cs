using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using StarterAssets;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ThirdPersonShooterControllerScript : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera aimVirtualCamera;
    [SerializeField] private float normalSensitivity;
    [SerializeField] private float AimSensitivity;
    [SerializeField] private LayerMask aimColliderLayerMask = new LayerMask();
    [SerializeField] private Transform debugTransform;
    [SerializeField] private Transform pfBulletProjectile;
    [SerializeField] private Transform spawnBulletPosition;

    private ThirdPersonController thirdPersonController;
    private StarterAssetsInputs starterAssetsInputs;
    private Animator animator;

    private float timeToShoot = 1f;
    public float timeToShootInterval = 1f;
    public Slider sliderForFast;

    float fastSpeedFuil = 10f;
    bool isSpeedFull = true;

    private void Awake()
    {
        timeToShoot = timeToShootInterval;
        thirdPersonController = GetComponent<ThirdPersonController>();
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        Vector3 mouseWorldPosition = Vector3.zero;
        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
        
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, aimColliderLayerMask))
        {
            debugTransform.position = raycastHit.point;
            mouseWorldPosition = raycastHit.point;
        }
        pfBulletProjectile.transform.position = mouseWorldPosition;
        if (starterAssetsInputs.aim)
        {
            aimVirtualCamera.gameObject.SetActive(true);
            thirdPersonController.SetSensitivity(AimSensitivity);
            thirdPersonController.SetRotateOnMove(false);
            animator.SetLayerWeight(1, Mathf.Lerp(animator.GetLayerWeight(1), 1f, Time.deltaTime * 10f));

            Vector3 worldAimTarget = mouseWorldPosition;
            worldAimTarget.y = transform.position.y;
            Vector3 aimDirection = (worldAimTarget - transform.position).normalized;

            transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 20f);
            if (starterAssetsInputs.shoot && timeToShoot < 0)
            {
                Vector3 aimDir = (mouseWorldPosition - spawnBulletPosition.position).normalized;
                Instantiate(pfBulletProjectile, spawnBulletPosition.position, Quaternion.LookRotation(aimDir, Vector3.up));
                starterAssetsInputs.shoot = false;
                timeToShoot = timeToShootInterval;
            }

        }
        else
        {
            aimVirtualCamera.gameObject.SetActive(false);
            thirdPersonController.SetSensitivity(normalSensitivity);
            thirdPersonController.SetRotateOnMove(true);
            animator.SetLayerWeight(1, Mathf.Lerp(animator.GetLayerWeight(1), 0f, Time.deltaTime * 10f));
        }
        timeToShoot -= Time.deltaTime;
        if (starterAssetsInputs.slide)
        {
            if(fastSpeedFuil >= 10f)
            {
                thirdPersonController.MoveSpeed += 5.0f;
                thirdPersonController.SprintSpeed += 5.0f;


                Debug.Log(thirdPersonController.MoveSpeed);
                isSpeedFull = false;
                fastSpeedFuil = 0;
                sliderForFast.maxValue = 10;
                fastSpeedFuil = 10f;
            }

            starterAssetsInputs.slide = false;
        }
        sliderForFast.value = fastSpeedFuil;
        if (!isSpeedFull)
        {
            fastSpeedFuil -= Time.deltaTime * 3.4f;
        }
        if(fastSpeedFuil <= 0 && !isSpeedFull)
        {
            isSpeedFull = true;
            thirdPersonController.MoveSpeed = 2.0f;
            thirdPersonController.SprintSpeed = 5.335f;
            fastSpeedFuil = 0;
        }
        if (isSpeedFull)
        {
            fastSpeedFuil += Time.deltaTime;
        }


        
    }
}
