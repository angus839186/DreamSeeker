using UnityEngine;

namespace MUI
{
    public class SokobanBox : MonoBehaviour
    {

        public GameObject PhotoHint1;
        public GameObject PhotoHint3;
        public float pushForce = 5f;
        void OnCollisionStay2D(Collision2D collision)
        {
            if (collision != null)
            {
                Rigidbody2D objectRb = this.gameObject.GetComponent<Rigidbody2D>();
                Vector2 pushDirection = (this.transform.position - transform.position).normalized;

                objectRb.AddForce(pushDirection * pushForce);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Target"))
            {
                transform.position = collision.transform.position;
                GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                GetComponent<Rigidbody2D>().isKinematic = true;
                GameManager.Instance.CompleteMiniGame("Sokobanbox");
                PhotoHint1.SetActive(true);
                PhotoHint3.SetActive(true);
            }
        }
    }
}

