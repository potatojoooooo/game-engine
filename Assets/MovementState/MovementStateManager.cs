using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementStateManager : MonoBehaviour
{
    public float currentMoveSpeed; // Movement speed of the character
    public float walkSpeed = 3, walkBackSpeed = 2; // Movement speed of the character
    public float runSpeed = 7, runBackSpeed = 5; // Movement speed of the character
    public float crouchSpeed = 2, crouchBackSpeed = 1; // Movement speed of the character

    [HideInInspector] public Vector3 dir; // Direction vector for movement
    [HideInInspector]public float hzInput, vInput; // Input values for horizontal and vertical movement
    CharacterController controller; // Reference to the CharacterController component

    [SerializeField] float groundYOffset;
    [SerializeField] LayerMask groundMask;
    Vector3 spherePos;

    [SerializeField] float gravity = -9.81f;
    Vector3 velocity;

    MovementBaseState currentState;

    public IdleState Idle = new IdleState();
    public WalkState Walk = new WalkState();
    public CrouchState Crouch = new CrouchState();
    public RunState Run = new RunState();

    [HideInInspector] public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        controller = GetComponent<CharacterController>(); // Get the CharacterController component attached to this GameObject
        SwitchState(Idle);
    }

    public void SwitchState(MovementBaseState state)
    {
        currentState = state;
        currentState.EnterState(this);
    }

    // Update is called once per frame
    void Update()
    {
        GetDirectionAndMove(); // Call the function to get input and move the character
        Gravity();

        anim.SetFloat("hzinput", hzInput);
        anim.SetFloat("vinput", vInput);

        currentState.UpdateState(this);
    }

    void GetDirectionAndMove()
    {
        hzInput = Input.GetAxis("Horizontal"); // Get horizontal input (A/D or Left/Right keys)
        vInput = Input.GetAxis("Vertical"); // Get vertical input (W/S or Up/Down keys)

        dir = transform.forward * vInput + transform.right * hzInput; // Calculate the movement direction vector

        controller.Move(dir.normalized * currentMoveSpeed * Time.deltaTime); // Move the character based on the calculated direction and speed
    }

    bool IsGrounded()
    {
        spherePos = new Vector3(transform.position.x, transform.position.y - groundYOffset, transform.position.z);
        if (Physics.CheckSphere(spherePos, controller.radius - 0.05f, groundMask)) return true;
        return false;
    }

    void Gravity()
    {
        if (!IsGrounded()) velocity.y += gravity * Time.deltaTime;
        else if (velocity.y < 0) velocity.y = -2;

        controller.Move(velocity * Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(spherePos, controller.radius - 0.05f);
    }
}
