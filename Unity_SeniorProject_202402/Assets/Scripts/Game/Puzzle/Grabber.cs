using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MUI
{
    public class Grabber : MonoBehaviour
    {
        private GameObject seletedObject;
        public float snapThreshold; // 吸附閾值

        private PuzzlePiece[] allPieces;

        private void Start()
        {
            allPieces = FindObjectsOfType<PuzzlePiece>();
        }

        private void Update()
        {          
            if (Input.GetMouseButtonDown(0))
            {
                if (seletedObject == null)
                {
                    RaycastHit hit = CastRay();
                    if (hit.collider != null)
                    {
                        if (!hit.collider.CompareTag("Drag"))
                        {
                            return;
                        }

                        seletedObject = hit.collider.gameObject;
                        if (seletedObject.gameObject.GetComponent<PuzzlePiece>().isLocked)
                        {
                            seletedObject = null;
                            return;
                        }
                        Cursor.visible = false;
                    }
                }
                else
                {  //放下
                    Vector3 pos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.WorldToScreenPoint(seletedObject.transform.position).z);
                    Vector3 worldPos = Camera.main.ScreenToWorldPoint(pos);
                    seletedObject.transform.position = new Vector3(worldPos.x, worldPos.y, 0f);

                    PuzzleSlot[] slots = FindObjectsOfType<PuzzleSlot>();

                    foreach (PuzzleSlot slot in slots)
                    {
                        if (slot.slotNumber == seletedObject.gameObject.GetComponent<PuzzlePiece>().puzzleID)
                        {
                            float distance = Vector2.Distance(seletedObject.gameObject.transform.position, slot.transform.position);

                            if (distance < snapThreshold)
                            {
                                seletedObject.gameObject.transform.position = slot.transform.position;
                                seletedObject.gameObject.GetComponent<PuzzlePiece>().isLocked = true;
                                CheckWinCondition();
                            }
                        }
                        //slot.slotNumber == seletedObject.transform.rotation.eulerAngles.x;
                    }

                    seletedObject = null;
                    Cursor.visible = true;

                }
            }

            if (seletedObject != null)
            {
                Vector3 pos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.WorldToScreenPoint(seletedObject.transform.position).z);
                Vector3 worldPos = Camera.main.ScreenToWorldPoint(pos);
                seletedObject.transform.position = new Vector3(worldPos.x, worldPos.y,-0.25f);

                if (Input.GetMouseButtonDown(1))
                {
                    seletedObject.transform.rotation = Quaternion.Euler(new Vector3(
                        seletedObject.transform.rotation.eulerAngles.x + 90f,
                        seletedObject.transform.rotation.eulerAngles.y ,
                        seletedObject.transform.rotation.eulerAngles.z));
                }
            }

            if (CheckWinCondition())
            {
                Debug.Log("恭喜！你完成了拼圖！");
                StartCoroutine("WinGO");
            }
        }

        private RaycastHit CastRay()
        {
            Vector3 screemMousePosFar = new Vector3(
                Input.mousePosition.x,
                Input.mousePosition.y,
                Camera.main.farClipPlane);
            Vector3 screemMousePosNear = new Vector3(
               Input.mousePosition.x,
               Input.mousePosition.y,
               Camera.main.nearClipPlane);
            Vector3 worldMousePosFar = Camera.main.ScreenToWorldPoint(screemMousePosFar);
            Vector3 worldMousePosNear = Camera.main.ScreenToWorldPoint(screemMousePosNear);
            RaycastHit hit;
            Physics.Raycast(worldMousePosNear, worldMousePosFar - worldMousePosNear,out hit);
            
            return hit;
        }

        bool CheckWinCondition()
        {
            // 使用 LINQ 來檢查所有拼圖片段是否都被鎖定
            return allPieces.All(piece => piece.isLocked);
        }

        IEnumerator WinGO()
        {
            yield return new WaitForSeconds(1f);
            SceneManager.LoadScene("Scene01");
        }
    }
}

