using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallsManager : MonoBehaviour
{
  #region Singleton

  private static BallsManager _instance;

  public static BallsManager Instance => _instance;

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

  [SerializeField]
  private Ball ballPrefab;

  private Ball initialBall;

  private Rigidbody2D initialBallRb;

  public float initialBallSpeed = 250;

  public List<Ball> Balls { get; set; }

  void Start()
  {
    InitBall();
  }

  void Update()
  {
    if (!GameManager.Instance.IsGameStarted)
    {
      // Align ball position to the paddle position
      Vector3 paddlePosition = Paddle.Instance.gameObject.transform.position;
      Vector3 ballPosition = new Vector3(paddlePosition.x, paddlePosition.y + .27f, 0);
      this.initialBall.transform.position = ballPosition;

      // Left mouse button
      if (Input.GetMouseButtonDown(0))
      {
        this.initialBallRb.isKinematic = false;
        this.initialBallRb.AddForce(new Vector2(0, this.initialBallSpeed));
        GameManager.Instance.IsGameStarted = true;
      }
    }
  }

  void InitBall()
  {
    Vector3 paddlePosition = Paddle.Instance.gameObject.transform.position;
    Vector3 startingPosition = new Vector3(paddlePosition.x, paddlePosition.y + .27f, 0);
    this.initialBall = Instantiate(ballPrefab, startingPosition, Quaternion.identity);
    this.initialBallRb = this.initialBall.GetComponent<Rigidbody2D>();

    this.Balls = new List<Ball>
    {
        initialBall
    };
  }
}
