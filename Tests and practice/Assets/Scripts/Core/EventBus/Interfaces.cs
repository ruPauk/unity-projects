using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEvent { }

public interface IBaseEventReceiver { }

//��������� ��� ������������������� ���������� �������
public interface IEventReceiver<T> : IBaseEventReceiver where T : struct, IEvent
{
    void OnEvent(T @event);
}