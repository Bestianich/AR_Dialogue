using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
using Object = System.Object;

public abstract class ANode<T> : Node
{

	// Use this for initialization
	protected override void Init() {
		base.Init();
		
	}

	//What the node does
	public abstract T Execute();

	public virtual Type GetNodeType()
	{
		return GetType();
	}
	
	// Return the correct value of an output port when requested
	public override object GetValue(NodePort port) {
		return port.node; // Replace this
	}
}