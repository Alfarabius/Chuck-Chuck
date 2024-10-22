using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private PlayerMovement mov;
    private Animator anim;
    private SpriteRenderer spriteRend;

    [Header("Movement Tilt")]
    [SerializeField] private float maxTilt;
    [SerializeField] [Range(0, 1)] private float tiltSpeed;

    public bool startedJumping {  private get; set; }
    public bool justLanded { private get; set; }

    public float currentVelY;

    [SerializeField] GameObject LandEffect;

    private void Start()
    {
        mov = GetComponent<PlayerMovement>();
        spriteRend = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    private void LateUpdate()
    {
        #region Tilt
        float tiltProgress;

        int mult = -1;

        if (mov.IsSliding)
        {
            tiltProgress = 0.25f;
        }
        else
        {
            tiltProgress = Mathf.InverseLerp(-mov.Data.runMaxSpeed, mov.Data.runMaxSpeed, mov.RB.velocity.x);
            mult = mov.IsFacingRight ? 1 : -1;
        }

        float newRot = tiltProgress * maxTilt * 2 - maxTilt;
        float rot = Mathf.LerpAngle(spriteRend.transform.localRotation.eulerAngles.z * mult, newRot, tiltSpeed);
        spriteRend.transform.localRotation = Quaternion.Euler(0, 0, rot * mult);
        #endregion

        CheckAnimationState();
    }

    private void CheckAnimationState()
    {
        if (startedJumping)
        {
            anim.SetBool("IsJumping", true);
            LandEffect.SetActive(true);
            startedJumping = false;
            return;
        }

        if (justLanded)
        {
            anim.SetBool("IsJumping", false);
            LandEffect.SetActive(true);
            justLanded = false;
            return;
        }

        anim.SetBool("IsRunning", Mathf.Abs(mov.RB.velocity.x) > 0.01f);
    }
}
