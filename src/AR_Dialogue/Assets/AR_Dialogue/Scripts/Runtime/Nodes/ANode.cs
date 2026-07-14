using System;
using System.Collections;
using System.Collections.Generic;
using AR_Dialogue.Scripts.Runtime;
using UnityEngine;
using XNode;
using Object = System.Object;

	public abstract class ANode : Node
	{
		protected DialogueMemory _dialogueMemory;
		protected Actor _actor;

		// Use this for initialization
		public virtual void Init(DialogueMemory dialogueMemory , Actor actor)
		{
			_dialogueMemory = dialogueMemory;
			_actor = actor;
		}

		//What the node does
		public abstract void Execute();

		public virtual void OnNextNode()
		{
			
		}

		public virtual Type GetNodeType()
		{
			return GetType();
		}

		// Return the correct value of an output port when requested
		public override object GetValue(NodePort port)
		{
			return port.node; // Replace this
		}

	}


	#region Attributes

	[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
	public class HideInNodeEditorAttribute : Attribute
	{
		public bool HideInEditor { get; }

		public HideInNodeEditorAttribute(bool hideInEditor)
		{
			HideInEditor = hideInEditor;
		}
	}

	[AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
	public class UsesTextFieldAttribute : Attribute
	{
		public string Text { get; }

		public UsesTextFieldAttribute(string text)
		{
			Text = text;
		}
	}

	[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
	public class HasDynamicPortsAttribute : Attribute
	{
		public bool HasDynamicPorts { get; }

		public HasDynamicPortsAttribute(bool hasDynamicPorts)
		{
			HasDynamicPorts = hasDynamicPorts;
		}
	}

	#endregion