using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleUnit : MonoBehaviour
{
    [SerializeField] bool isPlayer;
    [SerializeField] BattleHud hud;

    public bool IsPlayer
    {
        get
        {
            return isPlayer;
        }
    }

    public BattleHud Hud
    {
        get
        {
            return hud;
        }
    }

    public Enemy Enemy
    {
        get; set;
    }

    public void Setup(Enemy enemy)
    {
        Enemy = enemy;
        if (isPlayer)
        {
            GetComponent<Image>().sprite = Enemy.Base.PlayerSprite;
        }
        else
        {
            GetComponent<Image>().sprite = Enemy.Base.EnemySprite;
        }

        hud.SetData(enemy);
    }

}
