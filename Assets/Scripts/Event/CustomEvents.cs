using UnityEngine.Events;
using UnityEngine;

[System.Serializable]
public class MyIntEvent : UnityEvent<int>
{
}

[System.Serializable]
public class MyBoolEvent : UnityEvent<bool>
{
}

[System.Serializable]
public class MyVector3Event : UnityEvent<Vector3>
{
}
[System.Serializable]
public class MyStringEvent : UnityEvent<string>
{
}



