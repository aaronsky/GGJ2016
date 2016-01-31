using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {

    private Vector2 frameSize = new Vector2();
    public Light spotLight;
    public Animator animator;
    public bool FacingRight = false;
    bool walking = false;
    bool blinking = false;
    bool facingBack = false;
    public bool sitting = false;

    public float moveSpeed;
    public float walkHeight;

    public bool inputEnabled = true;
    public bool hasBrokenOut = false;
    private bool cleanButtonSeparation = false;
    private bool firstFrame = true;

    // Use this for initialization
    void Start() {
        animator = GetComponent<Animator>();
        var spriteRenderer = GetComponent<SpriteRenderer>();
        Texture2D tex = (Texture2D)Resources.Load<Texture2D>("player");
        frameSize.x = tex.width / 2;
        frameSize.y = tex.height / 4;
        spriteRenderer.sprite = Sprite.Create(tex, new Rect(0, 0, frameSize.x, frameSize.y), new Vector2(0.5f, 0));
        Stand();
    }

    public void Flip()
    {
        FacingRight = !FacingRight;
        var scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    public void MoveLeft()
    {
        Stand();
        TurnAround(false);
        gameObject.transform.Translate(-moveSpeed * Time.deltaTime, 0, 0);
        if (!walking)
        {
            walking = true;
            animator.SetBool("Walking", true);
        }
        if (FacingRight)
        {
            FacingRight = false;
            var scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }

    public void MoveRight()
    {
        Stand();
        TurnAround(false);
        gameObject.transform.Translate(moveSpeed * Time.deltaTime, 0, 0);
        if (!walking)
        {
            walking = true;
            animator.SetBool("Walking", true);
        }
        if (!FacingRight)
        {
            FacingRight = true;
            var scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }

    public void TurnAround(bool? value = null)
    {
        facingBack = value ?? !facingBack;
        animator.SetBool("FacingBack", facingBack);
    }

    public void Sit(float x, float y)
    {
        var pos = transform.position;
        pos.x = x;
        pos.y = y;
        transform.position = pos;
        sitting = true;
        animator.SetBool("Walking", false);
        animator.SetBool("FacingBack", false);
        animator.SetBool("Sitting", true);
    }

    public void Stand()
    {
        var pos = transform.position;
        pos.y = walkHeight;
        transform.position = pos;
        sitting = false;
        animator.SetBool("Sitting", false);
    }

	// Update is called once per frame
	void Update () {
        if (!firstFrame && !Input.anyKey && !cleanButtonSeparation)
        {
            cleanButtonSeparation = true;
        }
        else if ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) && cleanButtonSeparation && inputEnabled && !StateManager.textOnScreen)
        {
            hasBrokenOut = true;
            MoveRight();
        }
		else if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) && cleanButtonSeparation && inputEnabled && !StateManager.textOnScreen)
        {
            hasBrokenOut = true;
            MoveLeft();
        }
        else
        {
            if (walking)
            {
                walking = false;
                animator.SetBool("Walking", false);
            }
            if (!blinking)
            {
                if (Random.Range(0.0f, 1.0f) > 0.99f)
                {
                    blinking = true;
                    animator.SetBool("Blinking", true);
                }
            }
            else {
                blinking = false;
                animator.SetBool("Blinking", false);
            }
        }
        if (gameObject.transform.position.x > 10)
            gameObject.transform.position = new Vector3(10, gameObject.transform.position.y);
        if (gameObject.transform.position.x < -10)
            gameObject.transform.position = new Vector3(-10, gameObject.transform.position.y);
        var spotX = spotLight.transform.position.x;
        var playerX = gameObject.transform.position.x;
        if (spotX > playerX + 1)
        {
            spotLight.transform.Translate(-moveSpeed * Time.deltaTime, 0, 0);
        }
        else if (spotX < playerX - 1)
        {
            spotLight.transform.Translate(moveSpeed * Time.deltaTime, 0, 0);
        }
        var pos = spotLight.transform.position;
        pos.y = transform.position.y;
        spotLight.transform.position = pos;
        firstFrame = false;
    }

    public void IncreaseView()
    {
        Debug.Log("increased view");
    }
}


