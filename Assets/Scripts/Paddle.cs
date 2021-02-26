using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
  #region Singleton

  private static Paddle _instance;

  public static Paddle Instance => _instance;

  void Awake()
  {
    if (_instance != null)
    {
      Destroy(gameObject);
    }
    else
    {
      _instance = this;
    }
  }

  #endregion

  private Camera mainCamera;
  private float paddleInitialY;
  private float defaultPaddleWidthInPixels = 200;
  private float defaultLeftClamp = 135;
  private float defaultRightClamp = 410;
  private SpriteRenderer sr;

  void Start()
  {
    this.mainCamera = FindObjectOfType<Camera>();
    this.paddleInitialY = this.transform.position.y;
    this.sr = GetComponent<SpriteRenderer>();
  }

  void Update()
  {
    PaddleMovement();
  }

  void PaddleMovement()
  {
    float paddleShift = (this.defaultPaddleWidthInPixels - ((this.defaultPaddleWidthInPixels / 2) * this.sr.size.x)) / 2;
    float leftClamp = this.defaultLeftClamp - paddleShift;
    float rightClamp = this.defaultRightClamp + paddleShift;
    float mousePositionPixels = Mathf.Clamp(Input.mousePosition.x, leftClamp, rightClamp);
    float mousePositionWorldX = mainCamera.ScreenToWorldPoint(new Vector3(mousePositionPixels, 0, 0)).x;
    this.transform.position = new Vector3(mousePositionWorldX, paddleInitialY, 0);
  }

  void OnCollisionEnter2D(Collision2D collision)
  {
    if (collision.gameObject.tag == "Ball")
    {
      Rigidbody2D ballRb = collision.gameObject.GetComponent<Rigidbody2D>();
      Vector3 hitPoint = collision.contacts[0].point;
      Vector3 paddleCenter = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y);

      ballRb.velocity = Vector2.zero;

      float difference = paddleCenter.x - hitPoint.x;

      if (hitPoint.x < paddleCenter.x)
      {
        ballRb.AddForce(new Vector3(-(Mathf.Abs(difference * 200)), BallsManager.Instance.initialBallSpeed));
      }
      else
      {
        ballRb.AddForce(new Vector2((Mathf.Abs(difference * 200)), BallsManager.Instance.initialBallSpeed));
      }
    }
  }
}
