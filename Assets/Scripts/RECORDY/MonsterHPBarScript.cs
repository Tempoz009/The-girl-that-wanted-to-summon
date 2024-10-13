using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class MonsterHPBarScript : MonoBehaviour
{
    private GameObject _target;
    private GameObject square;
    [SerializeField] private float _maxWidth;
    [SerializeField] private float YHpBarTransformDelay;
    [SerializeField] private float sizeHpBarY;
    private enemy enemy1;

 
    public void CreateHPBar(GameObject target)
    {
        square = new GameObject("hpbar");
        square.tag = "Aboba";
        SpriteRenderer spriteRenderer = square.AddComponent<SpriteRenderer>();

        Texture2D texture = new Texture2D(1, 1);
        texture.SetPixel(0, 0, Color.white);
        texture.Apply();
        

        Rect rect = new Rect(0, 0, 1, 1);
        spriteRenderer.sprite = Sprite.Create(texture, rect, new Vector2(0.5f, 0.5f));
        
        square.GetComponent<Renderer>().material.color = Color.red;
        square.transform.localScale = new Vector2(_maxWidth, sizeHpBarY);
        square.transform.position= new Vector2(target.transform.position.x,target.transform.position.y+YHpBarTransformDelay);
    }
    
    private void Start()
    {
        enemy1 = GetComponent<enemy>();
        _target=gameObject;
        if(gameObject!=null)
        CreateHPBar(gameObject);
    }
    private void Update()
    {
        if(enemy1.gameObject.tag == "Summon")
        {
            square.GetComponent<Renderer>().material.color = Color.green;
        }
        if(square!=null)
        {
            _maxWidth=(enemy1.EnemyMaxHp*10)-((enemy1.EnemyMaxHp-enemy1.EnemyCurrentHp)*10); //поменяешь когда вставишь свое EnemyMaxHP и EnemyCurrentHP для моба
            square.transform.localScale = new Vector2(_maxWidth, sizeHpBarY);
            square.transform.position= new Vector2(_target.transform.position.x,_target.transform.position.y+YHpBarTransformDelay);
        }
    }

    private void OnDestroy()//юзай метод когда враг умрет
    {
        Destroy(square);
    }

}
