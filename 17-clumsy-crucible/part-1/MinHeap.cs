class MinHeap<T> where T : IComparable<T> 
{
  private readonly List<T> Heap = [];
  private readonly Dictionary<T, int> HeapIndices = [];
  
  public int Count { get {
    return Heap.Count;
  }}

  public void InsertOrUpdate(T item)
  {
    if(!HeapIndices.ContainsKey(item))
    {
      Heap.Add(item);
      HeapIndices[item] = Count - 1;
    }

    int i = HeapIndices[item];

    HeapifyUp(i);
  }

  public T ExtractMin()
  {
    if(Count == 0) throw new IndexOutOfRangeException("Cannot extract an element from an empty heap.");

    T minValue = Heap[0];

    if(Count == 1)
    {
      Heap.Clear();
      HeapIndices.Clear();
      return minValue;
    }

    HeapIndices.Remove(Heap[0]);
    Heap[0] = Heap.Last();
    HeapIndices[Heap[0]] = 0;
    Heap.RemoveAt(Count - 1);

    HeapifyDown(0);

    return minValue;
  }

  private void HeapifyUp(int i)
  {
    while(i > 0 && Heap[i].CompareTo(Heap[MinHeap<T>.Parent(i)]) < 0)
    {
      (Heap[i], Heap[MinHeap<T>.Parent(i)]) = (Heap[MinHeap<T>.Parent(i)], Heap[i]);
     
      HeapIndices[Heap[i]] = i;
      HeapIndices[Heap[MinHeap<T>.Parent(i)]] = MinHeap<T>.Parent(i);

      i = MinHeap<T>.Parent(i);
    }
  }

  private void HeapifyDown(int i)
  {
    int leftChild = MinHeap<T>.LeftChild(i);
    int rightChild  = MinHeap<T>.RightChild(i);
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

  private static int Parent(int i)
  {
    return (i - 1) / 2;
  }

  private static int LeftChild(int i)
  {
    return i * 2 + 1;
  }

  private static int RightChild(int i)
  {
    return i * 2 + 2;
  }
}