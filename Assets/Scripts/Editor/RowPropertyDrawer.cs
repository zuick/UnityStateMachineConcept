using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;

public class RowPropertyDrawer : PropertyDrawer
{
	public virtual List<string> GetColumns(){ return null; }
	public virtual int rowBoolWidth { get { return 40; }}
	public virtual int rowWidth { get { return 120; }}
	public virtual int rowHeight { get { return 16; }}
	public virtual int spacing { get { return 10; }}
	public virtual bool showLabel { get { return true; }}

	private float firstElementHeightScale = 2f;

	public bool isFirst(GUIContent label)
	{
		return label.text == "Element 0";
	}
	public override float GetPropertyHeight(SerializedProperty property, GUIContent label){
		if(isFirst(label))
			return firstElementHeightScale * rowHeight;

		return rowHeight;
	}

	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
	{
		EditorGUI.BeginProperty(position, label, property);

		// Draw label
		if(showLabel)
			position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

		// Don't make child fields be indented
		var indent = EditorGUI.indentLevel;
		EditorGUI.indentLevel = 0;
		var first = isFirst(label);
		var columns = GetColumns();
		var xPosition = position.x;
		if(columns != null)
		{
			if(first)
			{
				for(var i = 0; i < columns.Count; i++)
				{
					var rowProperty = property.FindPropertyRelative(columns[i]);
					var width = rowProperty.type == "bool" ? rowBoolWidth : rowWidth;
					Rect rect = new Rect(xPosition, position.y, width, position.height / firstElementHeightScale);
					EditorGUI.LabelField(rect, columns[i]);
					xPosition += width + spacing;
				}
				xPosition = position.x;
				EditorGUI.indentLevel = 0;
			}

			for(var i = 0; i < columns.Count; i++)
			{
				var rowProperty = property.FindPropertyRelative(columns[i]);
				var width = rowProperty.type == "bool" ? rowBoolWidth : rowWidth;
				var yPosition = first ? rowHeight + position.y : position.y;
				
				Rect rect = new Rect(xPosition, yPosition, width, rowHeight);
				EditorGUI.PropertyField(rect, rowProperty, GUIContent.none);
				xPosition += width + spacing;
			}
		}
		// Set indent back to what it was
		EditorGUI.indentLevel = indent;

		EditorGUI.EndProperty();
	}
}