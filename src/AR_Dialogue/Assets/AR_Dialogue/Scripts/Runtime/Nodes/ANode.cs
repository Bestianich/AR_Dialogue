using System;
using System.Collections;
using System.Collections.Generic;
using AR_Dialogue.Scripts.Runtime;
using UnityEngine;
using UnityEngine.Serialization;
using XNode;
using Object = System.Object;

	[WaitForPlayerInput(false)]
	public abstract class ANode : Node
	{
		protected DialogueMemory _dialogueMemory;
		protected Actor _actor;
		protected bool _isInitialized = false;
		public bool HasBeenExecuted { get; protected set; }

		// Use this for initialization
		public virtual void Init(DialogueMemory dialogueMemory , Actor actor)
		{
			if(_isInitialized)
				return;
			_dialogueMemory = dialogueMemory;
			_actor = actor;
			_isInitialized = true;
			HasBeenExecuted = false;
		}

		//What the node does
		public virtual void Execute()
		{
			HasBeenExecuted = true;
		}

		public virtual void OnNextNode()
		{
			HasBeenExecuted = false;
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

	[AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
	public class WaitForPlayerInputAttribute : Attribute
	{
		public bool WaitForPlayerInput { get; }

		public WaitForPlayerInputAttribute(bool waitForPlayerInput)
		{
			WaitForPlayerInput = waitForPlayerInput;
		}
	}

	#endregion