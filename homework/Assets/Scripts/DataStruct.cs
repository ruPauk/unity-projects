using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Unity.VisualScripting;
using UnityEngine;

public class DataStruct
{
    private List<string> _list;
    private LinkedList<string> _linkedList;
    private Queue<string> _queue;
    private Stack<string> _stack;
    private HashSet<string> _hashSet;
    private Dictionary<int, string> _dictionary;
    private SortedList<int, string> _sortedList;
    //private BinaryTree
    //ArrayList<string> 
    
    public DataStruct() 
    { 
        _list = new List<string>();
        _linkedList = new LinkedList<string>();
        _queue = new Queue<string>();
        _stack = new Stack<string>();
        _hashSet = new HashSet<string>();
        _dictionary = new Dictionary<int, string>();
        _sortedList = new SortedList<int, string>();
    }

    public void AddElements(string element)
    {
        _list.Add(element);
        _linkedList.AddLast(element);
        _queue.Enqueue(element);
        _stack.Push(element);
        _hashSet.Add(element);
        _dictionary.Add(_dictionary.Count, element);
        _sortedList.Add(100, element);
    }

    public void DeleteElement()
    {
        string element = _list[0];
        _list.Remove(element);
        //_list.RemoveAt(0);

        _linkedList.RemoveLast();
        //_linkedList.RemoveFirst();
        //_linkedList.Remove(_linkedList.First);

        _queue.Dequeue();
        _stack.Pop();

        _hashSet.Remove(element);
        //_hashSet.RemoveWhere(x => x != element);

        _dictionary.Remove(0);

        _sortedList.Remove(0);
        //_sortedList.RemoveAt(0);
    }

    public void GettingElements()
    {
        var resultList = _list[0];
        _list.Contains(resultList); // везде есть Contains, так что не буду дублировать
        var resultLinkedList = _linkedList.Last.Value;
        var resultQueue = _queue.Peek();
        var resultStack = _stack.Peek();
        var resultHashSet = _hashSet.Contains(resultList); // здесь смысла нет получать элемент вроде?
        var resultDictionary = _dictionary[0];
        var resultSortedList = _sortedList[0];
    }
}
