using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class playermovemente : MonoBehaviour
{
	public float speed = 5f;
	public float reverseGravitySpeed = 1f;
	public float juice = 2f;
	public float constraintLength = 8f;
	public float juiceDiminishSpeed = 1f;
	public float angleXInfluence = 3f;
	public float startJuice = 0f;
	public Rigidbody2D rb;
	public GameObject jetpackParticle;
	public Slider jetpackFuelSlider;

	bool hasLost = false;
	bool isReversing = false;
    float axisX;
	float startGrav;
	float particleX;
	Vector2 pivotVelocity;
	SpriteRenderer spriteRenderer;
	private void Start()
	{
		jetpackFuelSlider = FindObjectOfType<Slider>();
		particleX = jetpackParticle.transform.localPosition.x;
		spriteRenderer = GetComponent<SpriteRenderer>();
		startGrav = rb.gravityScale;
		if (startJuice == 0) startJuice = juice;
		jetpackFuelSlider.maxValue = juice;
		jetpackFuelSlider.value = startJuice;
		juice = startJuice;
	}
	private void Update()
	{
		axisX = Input.GetAxisRaw("Horizontal");
		if (Input.GetKey(KeyCode.Space) && !hasLost) 
		{
			if (juice <= 0f)
			{
				hasLost = true;
				StopCoroutine(RechargeSlider());
				FindObjectOfType<gameManager>().OnGameLose();
				isReversing = false;
				return;
			}
			juice -= juiceDiminishSpeed * Time.deltaTime;
			jetpackFuelSlider.value = juice;
			isReversing = true;
		}
		else isReversing = false;

		pivotVelocity.x = Input.GetAxis("Horizontal") * angleXInfluence;
		pivotVelocity.y = rb.velocity.y;
		float ang;
		if (pivotVelocity.magnitude != 0f)
		{
			float angSin = Mathf.Asin(pivotVelocity.y / pivotVelocity.magnitude) * Mathf.Rad2Deg;
			float angCos = Mathf.Acos(pivotVelocity.x / pivotVelocity.magnitude) * Mathf.Rad2Deg;
			/*if (pivotVelocity.y == 0f)
				angSin = 0f;*/
			if (Mathf.Abs(angCos) <= 90)
			{
				spriteRenderer.flipX = true;
				jetpackParticle.transform.localPosition = new Vector2(-particleX, jetpackParticle.transform.localPosition.y);
			}
			else
			{
				angSin = -angSin;
				spriteRenderer.flipX = false;
				jetpackParticle.transform.localPosition = new Vector2(particleX, jetpackParticle.transform.localPosition.y);
			}
			ang = angSin;
		}
		else
		{
			ang = 0f;
		}
		transform.rotation = Quaternion.Euler(0f, 0f, ang);
	}
	private void FixedUpdate()
	{
		/* 
		float moveX = axisX * speed * Time.deltaTime;
		transform.position += new Vector3(moveX, 0f, 0f); */
		HorizontalMove();
		if (isReversing)
		{
			jetpackParticle.GetComponent<ParticleSystem>().Play();
			//jetpackParticle.transform.position = transform.position;
			rb.gravityScale = reverseGravitySpeed;
		}
		else
		{
			jetpackParticle.GetComponent<ParticleSystem>().Stop(true, ParticleSystemStopBehavior.StopEmitting);
			rb.gravityScale = startGrav;
		}
	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Finish"))
		{
			if (hasLost) return;
			GameObject copy = GetComponentInChildren<Animator>(true).gameObject;
			transform.rotation = Quaternion.Euler(0f, 0f, 0f);
			copy.SetActive(true);
			spriteRenderer.enabled = false;
			rb.isKinematic = true;
			//copy.GetComponent<Animator>().SetTrigger("finish");
			FindObjectOfType<gameManager>().OnGameWin();
		}
		else if (collision.gameObject.CompareTag("fuel"))
		{
			GameObject dieEffect = FindObjectOfType<gameManager>().dieEffect;
			GameObject effect = Instantiate(dieEffect, collision.transform.position, Quaternion.identity);
			Destroy(effect, 2f);
			Destroy(collision.gameObject);
			StartCoroutine(RechargeSlider());
		}
		else if (collision.gameObject.CompareTag("key"))
		{
			GameObject dieEffect = FindObjectOfType<gameManager>().shrimpParticle;
			GameObject effect = Instantiate(dieEffect, collision.transform.position, Quaternion.identity);
			Destroy(effect, 2f);
			collision.gameObject.GetComponent<key>().UnlockDoor();
			Destroy(collision.gameObject);			
		}
	}
	void HorizontalMove()
	{
		float moveX = axisX * speed * Time.deltaTime;
		Vector3 move = new Vector3();
		if (Input.GetAxisRaw("Horizontal") < 0) move = new Vector3(moveX, 0f, 0f);
		else if (Input.GetAxisRaw("Horizontal") > 0) move = new Vector3(moveX, 0f, 0f);
		if (transform.position.x >= constraintLength && move.x > 0f) move = Vector3.zero;
		else if (transform.position.x <= -constraintLength && move.x < 0f) move = Vector3.zero;
		transform.position += move;
		if (move.x == 0f) rb.constraints = RigidbodyConstraints2D.FreezePositionX;
		else rb.constraints = RigidbodyConstraints2D.None;
		//Debug.Log(move);
	}
	IEnumerator RechargeSlider()
	{
		if (hasLost) yield break;
		float chargetofill = jetpackFuelSlider.maxValue - jetpackFuelSlider.value;
		float iterations = 60f;
		float increment = chargetofill / iterations;
		audiomanager.instance.play("recharge");
		for (int i = 0; i < iterations; i++)
		{
			juice += increment;
			jetpackFuelSlider.value = juice;
			yield return new WaitForSeconds(1 / iterations);
		}
	}
}
