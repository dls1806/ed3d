using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]
public class CharacterAnimBasedMovement : MonoBehaviour
{
    public float rotationSpeed = 4f;
    public float rotationTreshold = 0.3f;
    [Range(0, 180f)]
    public float degreesToTurn = 160f;

    [Header("Animator Parameters")]
    public string motionParam = "motion";
    public string mirrorIddleParam = "mirrorIddle";
    public string turnParam = "turn180";

    [Header("Animation Smoothing")]
    [Range(0, 1f)]
    public float StartAnimTime = 0.3f;
    [Range(0, 1f)]
    public float StopAnimTime = 0.15f;

    private Ray wallRay = new Ray();
    private float speed;
    private Vector3 desiredMoveDirection;
    private CharacterController characterController;
    private Animator animator;
    private bool mirrorIddle;
    private bool turn180;
    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            animator.SetBool("ataque", true);
        }
        else
        {
            animator.SetBool("ataque", false);
        }
        if (Input.GetKey(KeyCode.Space))
        {
            animator.SetBool("salto", true);
        }
        else
        {
            animator.SetBool("salto", false);
        }
    }
    public void moveCharacter (float hInput, float vInput, Camera cam, bool jump, bool dash)
    {
        speed = new Vector2(hInput, vInput).normalized.sqrMagnitude;

        if(speed >= speed - rotationTreshold && dash)
        {
            speed = 1.5f;
        }

        if(speed > rotationTreshold)
        {
            animator.SetFloat(motionParam, speed, StartAnimTime, Time.deltaTime);
            Vector3 forward = cam.transform.forward;
            Vector3 right = cam.transform.right;

            forward.y = 0f;
            right.y = 0f;

            forward.Normalize();
            right.Normalize();

            desiredMoveDirection = forward * vInput + right * hInput;
            if (Vector3.Angle(transform.forward, desiredMoveDirection) >= degreesToTurn)
            {
                turn180 = true;
            }
            else
            {
                turn180 = false;
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(desiredMoveDirection), rotationSpeed * Time.deltaTime);
            }
            animator.SetBool(turnParam, turn180);
        }
        else if (speed < rotationTreshold)
        {
            animator.SetBool(mirrorIddleParam, mirrorIddle);
            animator.SetFloat(motionParam, speed, StopAnimTime, Time.deltaTime);
           
        }
    }
    private void OnAnimatorIK(int layerIndex)
    {
        if (speed < rotationTreshold) return;
        float distanceToLeftFoot = Vector3.Distance(transform.position, animator.GetIKPosition(AvatarIKGoal.LeftFoot));
        float distanceToRightFoot = Vector3.Distance(transform.position, animator.GetIKPosition(AvatarIKGoal.RightFoot));
        if(distanceToRightFoot > distanceToLeftFoot)
        {
            mirrorIddle = true;
        }
        else
        {
            mirrorIddle = false;
        }
    }
}
