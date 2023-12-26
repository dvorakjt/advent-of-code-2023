class MinHeap<T> where T : IComparable<T> 
{
  private readonly List<T> Heap = [];
  private Dictionary<T, int> HeapIndices = new();
  
  public int Count { get {
    return Heap.Count;
  }}

  public void Insert(T item)
  {
    Heap.Add(item);
    HeapIndices.Add(item, Heap.Count - 1);
    HeapifyUp(Heap.Count - 1);
  }

  public T Extract()
  {
    if(Count == 0) throw new IndexOutOfRangeException("Cannot extract an element from an empty heap.");

    T minValue = Heap[0];

    if(Count == 1)
    {
      Heap.Clear();
      HeapIndices.Clear();
      return minValue;
    }

    Heap[0] = Heap.Last();
    HeapIndices[Heap[0]] = 0;
    Heap.RemoveAt(Heap.Count - 1);
    HeapIndices.Remove(Heap[0]);

    HeapifyDown(0);

    return minValue;
  }

  private int HeapifyUp(int i)
  {
    while(i > 0 && Heap[i].CompareTo(Heap[Parent(i)]) < 0)
    {
      (Heap[i], Heap[Parent(i)]) = (Heap[Parent(i)], Heap[i]);
     
      HeapIndices[Heap[i]] = i;
      HeapIndices[Heap[Parent(i)]] = Parent(i);

      i = Parent(i);
    }

    //returns the final index of i
    return i;
  }

  public void DecreaseKey(T item)
  {
    int i = HeapIndices[item];

    HeapifyUp(i);
  }

  private void HeapifyDown(int i)
  {
    int leftChild = LeftChild(i);
    int rightChild  = RightChild(i);
    int min = i;
  
    if(leftChild < Count && Heap[leftChild].CompareTo(Heap[min]) < 0)
    {
      min = leftChild;
    }
    if(rightChild < Count && Heap[rightChild].CompareTo(Heap[min]) < 0)
    {
      min = rightChild;
    }

    if(min != i)
    {
      (Heap[i], Heap[min]) = (Heap[min], Heap[i]);
      HeapIndices[Heap[i]] = i;
      HeapIndices[Heap[min]] = min;
      HeapifyDown(min);
    }
  }

  private int Parent(int i)
  {
    return (i - 1) / 2;
  }

  private int LeftChild(int i)
  {
    return i * 2 + 1;
  }

  private int RightChild(int i)
  {
    return i * 2 + 2;
  }
}