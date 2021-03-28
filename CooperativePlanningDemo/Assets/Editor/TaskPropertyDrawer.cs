using System;
using System.IO;
using System.Linq;
using Planning;
using Unity.Collections;
using UnityEditor;
using UnityEngine;
using Utilities;
using Object = UnityEngine.Object;

[CustomPropertyDrawer(typeof(Task))]
public class TaskEditor : PropertyDrawer
{
  private readonly Type[] types;
  private readonly string[] typenames;
  private int height;
  
  private readonly float propertyFieldHeight = 18f;
  private readonly float verticalPadding = 2f;

  public TaskEditor()
  {
    types = ReflectiveEnumerator.DerivedTypes<Task>().ToArray();
    typenames = new string[types.Length];
    for (int i = 0; i < types.Length; ++i)
    {
      typenames[i] = types[i].ToString();//.Split('.').Last();
    }
  }

  void GUIHelper(ref Rect position, SerializedProperty sp)
  {
    bool enterChildren = true;
    int prevDepth = sp.depth;
    while (sp.NextVisible(enterChildren))
    {
      if(sp.depth > prevDepth)
        Indent(ref position);
      if (sp.depth < prevDepth)
        UnIndent(ref position);
      enterChildren = EditorGUI.PropertyField(position, sp, new GUIContent(sp.displayName));

      prevDepth = sp.depth;
      NewLine(ref position);
    }
  }
  
  void GuiLine( int thickness = 1 )
  {

    Rect rect = EditorGUILayout.GetControlRect(false, thickness );

    rect.height = thickness;

    EditorGUI.DrawRect(rect, new Color ( 0.5f,0.5f,0.5f, 1 ) );

  }

  void Indent(ref Rect position)
  {
    position.x += 10f;
    position.width -= 10f;
  }
  void UnIndent(ref Rect position)
  {
    position.x -= 10f;
    position.width += 10f;
  }

  void NewLine(ref Rect position)
  {
    position.y += propertyFieldHeight;
    position.y += verticalPadding;
    ++height;
  }
  
  public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
  {
    EditorGUI.BeginProperty(position, label, property);
    
    height = 0;
    position.height = propertyFieldHeight;
    
    // popup selection to pick Task type
    int selectedIndex = 
      property.objectReferenceValue ?
        Array.IndexOf(types, property.objectReferenceValue.GetType()) : 0;
    selectedIndex = EditorGUI.Popup(position, selectedIndex, typenames);
    NewLine(ref position);

    // get the Task and check that it's inherriting correct type
    Object task = property.objectReferenceValue;
    if (!task || task.GetType() != types[selectedIndex])
    {
      // if the type is wrong, reconstruct the task with the correct type
      task = ScriptableObject.CreateInstance(types[selectedIndex]);
      property.objectReferenceValue = task;
      property.serializedObject.ApplyModifiedProperties();
    }

    // display the inerrited task's serialized fields
    SerializedObject so = new SerializedObject(task);
    SerializedProperty sp = so.GetIterator();
    Indent(ref position);
    sp.NextVisible(true); // skip readonly script property
    
    GUIHelper(ref position, sp);
    
    so.ApplyModifiedProperties();

    EditorGUI.EndProperty();
  }

  public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
  {
    return (propertyFieldHeight + verticalPadding) * height + 10f;
  }
}