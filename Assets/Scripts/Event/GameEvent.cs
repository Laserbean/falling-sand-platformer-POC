using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameEvent : ScriptableObject
{
	private List<GameEventListener> listeners = 
		new List<GameEventListener>();

	public void Raise()
	{
		for(int i = listeners.Count -1; i >= 0; i--)
			listeners[i].OnEventRaised();
	}

	public void Raise2Int(int num)
	{
		for(int i = listeners.Count -1; i >= 0; i--)
			listeners[i].OnEventRaised(num);
	}

	public void Raise2Bool(bool boo)
	{
		for(int i = listeners.Count -1; i >= 0; i--)
			listeners[i].OnEventRaised(boo);
	}

	public void Raise2Vector3(Vector3 vec)
	{
		for(int i = listeners.Count -1; i >= 0; i--)
			listeners[i].OnEventRaised(vec);
	}

	public void Raise2String(string str1)
	{
		for(int i = listeners.Count -1; i >= 0; i--)
			listeners[i].OnEventRaised(str1);
	}



	public void RegisterListener(GameEventListener listener)
	{ listeners.Add(listener); }

	public void UnregisterListener(GameEventListener listener)
	{ listeners.Remove(listener); }
}