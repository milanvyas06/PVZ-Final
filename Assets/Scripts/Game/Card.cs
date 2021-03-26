using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlantsVsZombies.Enums;

namespace PlantsVsZombies
{
    public class Card : MonoBehaviour
    {
        #region PrivateVariables

        private WeaponType weaponType = WeaponType.None;

        #endregion /PrivateVariables

        #region PublicVariables

        public SpriteRenderer weaponSprite;

        public bool IsSpawn { get; set; }
        public GameObject DragableObject { get; set; }

        #endregion /PublicVariables

        #region MonobehaviourCallbacks

        private void OnMouseDown()
        {
            if (!IsSpawn)
            {
                //Debug.Log($"Instantiate Zombie");
                GameObject dragableWeapon = LevelManager.Instance.GetDragableWeaponOfType(weaponType);
                DragableObject = (GameObject)Instantiate(dragableWeapon, transform.position, Quaternion.identity);
                IsSpawn = true;
            }
        }

        private void OnMouseDrag()
        {
            if (DragableObject != null)
            {
                Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                DragableObject.transform.position = SnapToGridPosition(pos);
            }
        }

        private void OnMouseUp()
        {
            //Debug.Log("Mouse Up");

            if (DragableObject != null)
            {
                Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                Tile tile = GetTileAtPosition(pos);

                if (tile != null)
                {
                    if (!tile.IsOccupied)
                    {
                        //Debug.Log("Snapping Done Successfully");
                        GameObject spawnableWeapon = LevelManager.Instance.GetSpawableWeaponOfType(weaponType);
                        Vector3 spawnableWeaponPos = new Vector3(DragableObject.transform.position.x,
                            DragableObject.transform.position.y, 0f);
                        Instantiate(spawnableWeapon, spawnableWeaponPos, Quaternion.identity);

                        //Destoy the dragableObject
                        Destroy(DragableObject);
                        //Destroy the card, after weapon placement
                        //Destroy(gameObject);
                        Destroy(transform.parent.gameObject);
                    }
                    else
                    {
                        Destroy(DragableObject);
                        DragableObject = null;
                        IsSpawn = false;
                    }
                }
                else
                {
                    Destroy(DragableObject);
                    DragableObject = null;
                    IsSpawn = false;
                }
            }
        }

        #endregion /MonobehaviourCallbacks

        #region PrivateMethods

        private Vector2 SnapToGridPosition(Vector2 pos)
        {
            Vector2 gridPosition = new Vector2(Mathf.Clamp(pos.x, GridManager.XMin, -GridManager.XMin + 2), Mathf.Clamp(pos.y, GridManager.YMin, -GridManager.YMin)); // +2 is center to right

            Tile tile = GetTileAtPosition(pos);

            if (tile != null)
            {
                if (!tile.IsOccupied)
                {
                    gridPosition = tile.gameObject.transform.position;
                }
            }

            return gridPosition;
        }

        private Tile GetTileAtPosition(Vector2 pos)
        {
            Tile tile = null;

            RaycastHit2D hit2D = Physics2D.Raycast(pos, Vector2.down);

            if (hit2D.collider != null)
            {
                if (hit2D.collider.gameObject.CompareTag(GameConstants.TILE_TAG))
                {
                    tile = hit2D.collider.gameObject.GetComponent<Tile>();

                    if (tile != null)
                    {
                        return tile;
                    }
                }
            }

            return tile;
        }

        public void SetCardItem(WeaponType weaponType)
        {
            this.weaponType = weaponType;
            weaponSprite.sprite = LevelManager.Instance.GetIdleSpriteOfWeaponType(weaponType);
        }

        #endregion /PrivateMethods
    }
}
