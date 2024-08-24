using System;
using UnityEngine;
using System.Threading.Tasks;
using AmarilisLib;
using System.Collections.Generic;
using System.Linq;

public class IEnumerableExtensionsExample : MonoBehaviour
{
    class Person : IComparable<Person>
    {
        public string Name { get; set; }
        public int Age { get; set; }

        int IComparable<Person>.CompareTo(Person other)
        {
            if(other == null) return -1;
            return Name.CompareTo(other.Name);
        }
    }

    // IEnumerableExtensions usage example 1 : Various sorts
    public void Example1()
    {
        var people = new List<Person>
        {
            new Person { Name = "Bob", Age = 25 },
            new Person { Name = "Charlie", Age = 35 },
            new Person { Name = "Alice", Age = 30 },
        };

        var quickSorted = people.ToQuickSortedList();
        Debug.Log("Quick Sort by Name: " + string.Join(", ", quickSorted.Select(p => p.Name)));

        var mergeSorted = people.ToMergeSortedList();
        Debug.Log("Merge Sort by Name: " + string.Join(", ", mergeSorted.Select(p => p.Name)));

        var heapSorted = people.ToHeapSortedList();
        Debug.Log("Heap Sort by Name: " + string.Join(", ", heapSorted.Select(p => p.Name)));

        var shellSorted = people.ToShellSortedList();
        Debug.Log("Shell Sort by Name: " + string.Join(", ", shellSorted.Select(p => p.Name)));

        var insertionSorted = people.ToInsertionSortedList();
        Debug.Log("Insertion Sort by Name: " + string.Join(", ", insertionSorted.Select(p => p.Name)));

        var selectionSorted = people.ToSelectionSortedList();
        Debug.Log("Selection Sort by Name: " + string.Join(", ", quickSorted.Select(p => p.Name)));

        var bubbleSorted = people.ToBubbleSortedList();
        Debug.Log("Bubble Sort by Name: " + string.Join(", ", bubbleSorted.Select(p => p.Name)));

        var countingSorted = people.ToCountingSortedList(p => p.Age);
        Debug.Log("Counting Sort by Age: " + string.Join(", ", countingSorted.Select(p => p.Name)));

        var radixSorted = people.ToRadixSortedList(p => p.Age);
        Debug.Log("Radix Sort by Age: " + string.Join(", ", radixSorted.Select(p => p.Name)));
    }

    // IEnumerableExtensions usage example 2 : Shuffle
    public void Example2()
    {
        var numbers = new List<int> { 1, 2, 3, 4, 5 };

        var shuffledNumbers = numbers.Shuffle();
        Debug.Log("Shuffled: " + string.Join(", ", shuffledNumbers));
    }

    // IEnumerableExtensions usage example 3 : SkipTake
    public void Example3()
    {
        var numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

        var result = numbers.SkipTake(3, 4);
        Debug.Log(string.Join(", ", result));
    }

    // IEnumerableExtensions usage example 4 : Retry
    public void Example4()
    {
        var numbers = new List<int> { 1, 2, 3 };

        var result = numbers.Retry(2);
        Debug.Log(string.Join(", ", result));
    }

    // IEnumerableExtensions usage example 5 : Indexed
    public void Example5()
    {
        var numbers = new List<int> { 10, 20, 30, 40, 50 };

        foreach(var indexedItem in numbers.Indexed())
            Debug.Log($"Index: {indexedItem.Index}, Value: {indexedItem.Value}");
    }

    // IEnumerableExtensions usage example 6 : DuplicateGroup
    public void Example6()
    {
        var words = new List<string> { "apple", "banana", "cherry", "apple", "banana", "date" };

        var duplicateGroups = words.DuplicateGroup(word => word.Length);

        foreach(var group in duplicateGroups)
        {
            Debug.Log($"Key: {group.Key}");
            foreach(var word in group)
                Debug.Log($"  {word}");
        }
    }

    // IEnumerableExtensions usage example 7 : ChunkBy
    public void Example7()
    {
        var numbers = new List<int> { 1, 2, 2, 3, 4, 5, 6, 7, 8, 8, 9 };

        var chunks = numbers.ChunkBy(n => n % 2 == 0);

        foreach(var chunk in chunks)
        {
            Debug.Log($"Is Even: {chunk.Key}");
            foreach(var number in chunk)
                Debug.Log($"  {number}");
        }
    }

    // IEnumerableExtensions usage example 8 : SelectSynthetic
    public void Example8()
    {
        var words = new List<string> { "apple", "banana", "cherry" };

        var syntheticItems = words.SelectSynthetic(word => word.Length);

        foreach(var item in syntheticItems)
            Debug.Log($"Word: {item.SourceItem}, Length: {item.SelectItem}");
    }
    private void OnGUI()
    {
        if(GUILayout.Button("IEnumerableExtensions usage example 1"))
        {
            Example1();
        }
        if(GUILayout.Button("IEnumerableExtensions usage example 2"))
        {
            Example2();
        }
        if(GUILayout.Button("IEnumerableExtensions usage example 3"))
        {
            Example3();
        }
        if(GUILayout.Button("IEnumerableExtensions usage example 4"))
        {
            Example4();
        }
        if(GUILayout.Button("IEnumerableExtensions usage example 5"))
        {
            Example5();
        }
        if(GUILayout.Button("IEnumerableExtensions usage example 6"))
        {
            Example6();
        }
        if(GUILayout.Button("IEnumerableExtensions usage example 7"))
        {
            Example7();
        }
        if(GUILayout.Button("IEnumerableExtensions usage example 8"))
        {
            Example8();
        }
    }
}