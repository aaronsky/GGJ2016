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

    public float moveSpeed = 3;

    public bool hasBrokenOut = false;

    // Use this for initialization
    void Start() {
        animator = GetComponent<Animator>();
        var spriteRenderer = GetComponent<SpriteRenderer>();
        Texture2D tex = (Texture2D)Resources.Load<Texture2D>("player");
        frameSize.x = tex.width / 2;
        frameSize.y = tex.height / 4;
        spriteRenderer.sprite = Sprite.Create(tex, new Rect(0, 0, frameSize.x, frameSize.y), new Vector2(0.5f, 0));
    }

    public void MoveLeft()
    {
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

	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            hasBrokenOut = true;
            MoveRight();
        }
        else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
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
            if (!blinking) { 
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
    }
}
