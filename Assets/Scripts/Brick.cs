using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class Brick : MonoBehaviour
{
  private SpriteRenderer sr;

  public int HitPoints = 1;

  public ParticleSystem DestroyEffect;

  public static event Action<Brick> OnBrickDestruction;

  void Awake()
  {
    this.sr = this.GetComponent<SpriteRenderer>();
  }

  void OnCollisionEnter2D(Collision2D collision)
  {
    Ball ball = collision.gameObject.GetComponent<Ball>();
    ApplyCollisionLogic(ball);
  }

  void ApplyCollisionLogic(Ball ball)
  {
    this.HitPoints--;

    if (this.HitPoints <= 0)
    {
      OnBrickDestruction?.Invoke(this);
      SpawnDestroyEffect();
      Destroy(this.gameObject);
    }
    else
    {
      this.sr.sprite = BricksManager.Instance.Sprites[this.HitPoints - 1];
    }
  }

  void SpawnDestroyEffect()
  {
    Vector3 brickPosition = gameObject.transform.position;
    Vector3 spawnPosition = new Vector3(brickPosition.x, brickPosition.y - 0.2f);
    GameObject effect = Instantiate(DestroyEffect.gameObject, spawnPosition, Quaternion.identity);
    MainModule mm = effect.GetComponent<ParticleSystem>().main;
    mm.startColor = this.sr.color;
    Destroy(effect, DestroyEffect.main.startLifetime.constant);
  }

  public void Init(Transform containerTransform, Sprite sprite, Color color, int hitPoints)
  {
    this.transform.SetParent(containerTransform);
    this.sr.sprite = sprite;
    this.sr.color = color;
    this.HitPoints = hitPoints;
  }
}
