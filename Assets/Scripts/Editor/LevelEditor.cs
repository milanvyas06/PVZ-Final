using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static PlantsVsZombies.Enums;
using Newtonsoft.Json;
using System.IO;

namespace PlantsVsZombies
{
    public class LevelEditor : EditorWindow
    {
        #region PrivateVariables

        private List<LevelItem> levelItems = new List<LevelItem>();

        private readonly int rows = GameConstants.rows;
        private readonly int columns = GameConstants.columns;
        private readonly int skipColumn = GameConstants.skipColumn;

        private Vector2 scrollPosition = Vector2.zero;
        private GUIStyle customStyle;

        #endregion /PrivateVariables

        #region Initialize

        [MenuItem("LevelEditor/Show")]
        static void Init()
        {
            // Get existing open window or if none, make a new one:
            LevelEditor window = (LevelEditor)GetWindow(typeof(LevelEditor), false, "Level Editor", true);
            window.minSize = new Vector2(950, 500);
            window.Show();
        }

        #endregion /Initialize

        #region MonobehaviourCallbacks

        private void OnEnable()
        {
            LoadDataFromFile();
        }

        private void OnDisable()
        {
            for (int i = 0; i < levelItems.Count; i++)
            {
                levelItems[i].foldOut = false;
            }
        }

        private void OnGUI()
        {
            if (customStyle == null)
            {
                customStyle = new GUIStyle();
            }

            EditorGUILayout.BeginVertical();
            scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition, true, true);
            DrawLevelList();
            DrawAddButton();
            DrawSaveButton();
            EditorGUILayout.EndScrollView();
            EditorGUILayout.EndVertical();
        }

        #endregion /MonobehaviourCallbacks

        #region PrivateMethods

        private void DrawLevelList()
        {
            EditorGUILayout.BeginVertical();
            List<LevelItem> myLevelItems = new List<LevelItem>(levelItems);

            for (int i = 0; i < myLevelItems.Count; i++)
            {
                myLevelItems[i].foldOut = EditorGUILayout.Foldout(myLevelItems[i].foldOut, $"Level {(i + 1)}");

                if (myLevelItems[i].foldOut)
                {
                    if (GUILayout.Button("Delete Level", GUILayout.Width(100)))
                    {
                        if (levelItems.Contains(myLevelItems[i]))
                        {
                            levelItems.Remove(myLevelItems[i]);
                        }
                    }

                    DrawLevel(myLevelItems[i].levelGrid);
                }

                //EditorGUILayout.EndFoldoutHeaderGroup();
            }

            EditorGUILayout.EndVertical();
        }

        private void DrawLevel(LevelGrid levelGrid)
        {
            EditorGUILayout.BeginVertical();
            GUILayout.Space(5f);

            for (int r = 0; r < rows; r++)
            {
                EditorGUILayout.BeginHorizontal();
                GUILayout.Space(5f);

                for (int c = 0; c < columns; c++)
                {
                    customStyle.normal.background = (r + c) % 2 == 0 ? MakeTexure(1, 1, Color.gray * 0.1f) : MakeTexure(1, 1, Color.gray * 0.5f);
                    EditorGUILayout.BeginVertical(customStyle);
                    GUILayout.Space(10f);

                    if (c < skipColumn)
                    {
                        EditorGUILayout.LabelField($"Item {r}x{c}", GUILayout.MinWidth(100), GUILayout.MinHeight(50));
                    }
                    else
                    {
                        if (levelGrid != null && levelGrid.gridItems[r][c] != null)
                        {
                            var items = levelGrid.gridItems[r][c];
                            EditorGUILayout.LabelField($"Item {r}x{c}", GUILayout.MinWidth(100));
                            items.itemType = (ItemType)EditorGUILayout.EnumPopup("", items.itemType, GUILayout.Width(100));

                            if (items.itemType == ItemType.Weapon)
                            {
                                items.weaponType = (WeaponType)EditorGUILayout.EnumPopup("", items.weaponType, GUILayout.Width(100));
                            }
                            else
                            {
                                items.enemieType = (EnemyType)EditorGUILayout.EnumPopup("", items.enemieType, GUILayout.Width(100));
                            }
                        }
                        else
                        {
                            EditorGUILayout.LabelField($"Item {r}x{c}", GUILayout.MinWidth(100), GUILayout.MinHeight(50));
                        }
                    }

                    GUILayout.Space(10f);
                    EditorGUILayout.EndVertical();
                }

                GUILayout.Space(5f);
                EditorGUILayout.EndHorizontal();
            }

            GUILayout.Space(5f);
            EditorGUILayout.EndVertical();
        }

        private void DrawAddButton()
        {
            EditorGUILayout.BeginVertical();

            if (GUILayout.Button("Add New Level"))
            {
                levelItems.Add(new LevelItem(rows, columns, skipColumn));
            }

            EditorGUILayout.EndVertical();
        }

        private void DrawSaveButton()
        {
            EditorGUILayout.BeginVertical();

            if (GUILayout.Button("Save Levels"))
            {
                SaveDataToFile();
            }

            EditorGUILayout.EndVertical();
        }

        #endregion /PrivateMethods

        #region Utilities

        private Texture2D MakeTexure(int width, int height, Color col)
        {
            Color[] pix = new Color[width * height];

            for (int i = 0; i < pix.Length; i++)
                pix[i] = col;

            Texture2D result = new Texture2D(width, height);
            result.SetPixels(pix);
            result.Apply();

            return result;
        }

        private void LoadDataFromFile()
        {
            TextAsset levelsAsset = Resources.Load<TextAsset>("Levels");

            if (levelsAsset != null)
            {
                string data = levelsAsset.text;

                if (!string.IsNullOrEmpty(data))
                {
                    levelItems = JsonConvert.DeserializeObject<List<LevelItem>>(data);
                }
            }
        }

        private void SaveDataToFile()
        {
            TextAsset levelsAsset = Resources.Load<TextAsset>("Levels");

            if (levelsAsset != null)
            {
                string data = JsonConvert.SerializeObject(levelItems);
                File.WriteAllText(AssetDatabase.GetAssetPath(levelsAsset), data);
            }
        }

        #endregion /Utilities
    }
}
