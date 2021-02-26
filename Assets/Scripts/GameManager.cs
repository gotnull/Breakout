using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
  #region Singleton

  private static GameManager _instance;

  public static GameManager Instance => _instance;

  void Start()
  {
    Screen.SetResolution(540, 960, false);
  }

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

  public bool IsGameStarted { get; set; }
}
