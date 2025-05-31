using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using GenerateAndCreateMap.Classes;

[CustomEditor(typeof(FloorMap))]
public class FloorMapEditor : Editor
{
    private const float defaultElementWidth = 120f;
    private bool showGrid = true;
    private int elementsToAdd = 1;
    private int maxElementsPerRow = 9; // Максимальное количество элементов в ряду (можно менять)

    public override void OnInspectorGUI()
    {
        FloorMap floorMap = (FloorMap)target;
        SerializedObject serializedObject = new SerializedObject(floorMap);
        
        serializedObject.Update();
        EditorGUI.BeginChangeCheck();
        
        showGrid = EditorGUILayout.Foldout(showGrid, "Points Grid", true);
        
        if (showGrid)
        {
            // Настройки отображения
            EditorGUILayout.BeginHorizontal();
            maxElementsPerRow = EditorGUILayout.IntSlider("Max per Row", maxElementsPerRow, 1, 20);
            EditorGUILayout.EndHorizontal();

            if (floorMap.points == null)
            {
                floorMap.points = new List<Point>();
            }

            SerializedProperty pointsProp = serializedObject.FindProperty("points");
            
            // Расчет сетки
            float availableWidth = EditorGUIUtility.currentViewWidth - 30;
            int elementsPerRow = Mathf.Min(
                maxElementsPerRow, 
                Mathf.Max(1, Mathf.FloorToInt(availableWidth / defaultElementWidth))
            );
            
            int totalElements = pointsProp.arraySize;
            int totalRows = Mathf.CeilToInt((float)totalElements / elementsPerRow);
            
            // Отрисовка элементов
            for (int row = 0; row < totalRows; row++)
            {
                EditorGUILayout.BeginHorizontal();
                
                int startIndex = row * elementsPerRow;
                int endIndex = Mathf.Min(startIndex + elementsPerRow, totalElements);
                
                for (int i = startIndex; i < endIndex; i++)
                {
                    EditorGUILayout.BeginVertical(GUI.skin.box, GUILayout.Width(defaultElementWidth));
                    
                    SerializedProperty pointProp = pointsProp.GetArrayElementAtIndex(i);
                    if (pointProp != null)
                    {
                        EditorGUILayout.PropertyField(pointProp.FindPropertyRelative("x"), GUIContent.none);
                        EditorGUILayout.PropertyField(pointProp.FindPropertyRelative("y"), GUIContent.none);
                        EditorGUILayout.PropertyField(pointProp.FindPropertyRelative("z"), GUIContent.none);
                        EditorGUILayout.PropertyField(pointProp.FindPropertyRelative("floorType"), GUIContent.none);
                    }
                    
                    if (GUILayout.Button("×", GUILayout.Width(20)))
                    {
                        pointsProp.DeleteArrayElementAtIndex(i);
                        serializedObject.ApplyModifiedProperties();
                        return;
                    }
                    
                    EditorGUILayout.EndVertical();
                }
                
                EditorGUILayout.EndHorizontal();
            }
            
            // Панель добавления
            EditorGUILayout.BeginHorizontal();
            
            // Поле для ввода количества с защитой от некорректных значений
            int newElementsToAdd = EditorGUILayout.IntField("Add Count", elementsToAdd);
            elementsToAdd = Mathf.Clamp(newElementsToAdd, 1, 1000);
            
            if (GUILayout.Button($"+ Add {elementsToAdd} Points", GUILayout.Height(30)))
            {
                int startSize = pointsProp.arraySize;
                pointsProp.arraySize += elementsToAdd;
                
                for (int i = startSize; i < pointsProp.arraySize; i++)
                {
                    var newPoint = pointsProp.GetArrayElementAtIndex(i);
                    newPoint.FindPropertyRelative("x").intValue = 0;
                    newPoint.FindPropertyRelative("y").intValue = 0;
                    newPoint.FindPropertyRelative("z").intValue = 0;
                }
            }
            EditorGUILayout.EndHorizontal();
        }
        
        if (EditorGUI.EndChangeCheck())
        {
            serializedObject.ApplyModifiedProperties();
            EditorUtility.SetDirty(floorMap);
        }
    }
}