using UnityEngine;

namespace MUI
{
    public class GroundSprite : MonoBehaviour
    {
        private Player0Movement player0;
        public Rigidbody2D[] rb;
        private float MoveH;
        [SerializeField] private float Movespeed;

        public GameObject[] sprite;

        private void Start()
        {
            player0 = GameObject.FindWithTag("Player").GetComponent<Player0Movement>();
            for (int i = 0; i < sprite.Length; i++)
            {
                rb[i] = sprite[i].GetComponent<Rigidbody2D>();
            }
        }

        private void Update()
        {
            if (player0.isMoved == true)
            { 
                MoveH = Input.GetAxis("Horizontal") * Movespeed;

                //«e´º
                rb[0].velocity = new Vector2(MoveH * 1.2f, rb[0].velocity.y);
                //¾ð
                rb[1].velocity = new Vector2(MoveH * 0.8f, rb[1].velocity.y);
                //¤s
                rb[2].velocity = new Vector2(MoveH * 0.5f, rb[2].velocity.y);
                //¶³¼h
                rb[3].velocity = new Vector2(MoveH * 1.1f, rb[3].velocity.y);
                rb[4].velocity = new Vector2(MoveH * 0.7f, rb[4].velocity.y);
                rb[5].velocity = new Vector2(MoveH * 0.5f, rb[5].velocity.y);
                rb[6].velocity = new Vector2(MoveH * 0.1f, rb[6].velocity.y);      
            }
            else
            {
                MoveH = 0f;
                for (int i = 0; i < rb.Length; i++)
                {
                    rb[i].velocity = new Vector2(0, 0);
                }
            }
        }
    }
}

