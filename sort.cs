using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GentlemanSet
{
    public class Algorithm<T> where T:IComparable<T>
    {
        //-------------------//
        //Sorting algorithm
        //classic swap two values
        private static void _swap(ref T[] oItems, int nLeft, int nRight)
        {
            if (nLeft != nRight)
            {
                T oTemp = oItems[nLeft];
                oItems[nLeft] = oItems[nRight];
                oItems[nRight] = oTemp;
            }
        }
        //classic swap two values
        public static void BubbleSort(ref T[] oItems)
        {
            bool bSwaped;
            do
            {
                bSwaped = false;
                for (int i = 0; i < oItems.Length - 1; i++)
                {
                    if (oItems[i].CompareTo(oItems[i+1]) > 0)
                    {
                        _swap(ref oItems, i, i + 1);
                        bSwaped = true;
                    }
                }
            } while (bSwaped != false);
        }
        //insertion sort
        public static void InsertionSort(ref T[] items) 
        { 
            int sortedRangeEndIndex = 1; 
 
            while (sortedRangeEndIndex < items.Length) 
            { 
                if (items[sortedRangeEndIndex].CompareTo(items[sortedRangeEndIndex - 1]) < 0)
                {
                    int insertIndex = FindInsertionIndex(ref items, items[sortedRangeEndIndex]);
                    Insert(ref items, insertIndex, sortedRangeEndIndex); 
                } 
 
                sortedRangeEndIndex++; 
            } 
        }
        private static int FindInsertionIndex(ref T[] items, T valueToInsert)
        {
            for (int index = 0; index < items.Length; index++)
            {
                if (items[index].CompareTo(valueToInsert) > 0)
                {
                    return index;
                }
            }

            throw new InvalidOperationException("The insertion index was not found");
        }
        private static void Insert(ref T[] itemArray, int indexInsertingAt, int indexInsertingFrom) 
        { 
            // itemArray =       0 1 2 4 5 6 3 7 
            // insertingAt =     3 
            // insertingFrom =   6 
            // actions 
            //  1: Store index at in temp     temp = 4 
            //  2: Set index at to index from  -> 0 1 2 3 5 6 3 7   temp = 4 
            //  3: Walking backward from index from to index at + 1. 
            //     Shift values from left to right once. 
            //     0 1 2 3 5 6 6 7   temp = 4 
            //     0 1 2 3 5 5 6 7   temp = 4 
            //  4: Write temp value to index at + 1. 
            //     0 1 2 3 4 5 6 7   temp = 4 

            // Step 1. 
            T temp = itemArray[indexInsertingAt];

            // Step 2. 

            itemArray[indexInsertingAt] = itemArray[indexInsertingFrom];

            // Step 3. 
            for (int current = indexInsertingFrom; current > indexInsertingAt; current--)
            {
                itemArray[current] = itemArray[current - 1];
            }

            // Step 4. 
            itemArray[indexInsertingAt + 1] = temp;
        }
        //selection sort
        public static void SelectionSort(ref T[] items)
        {
            int sortedRangeEnd = 0;

            while (sortedRangeEnd < items.Length)
            {
                int nextIndex = FindIndexOfSmallestFromIndex(ref items, sortedRangeEnd);
                _swap(ref items, sortedRangeEnd, nextIndex);

                sortedRangeEnd++;
            }
        }
        private static int FindIndexOfSmallestFromIndex(ref T[] items, int sortedRangeEnd)
        {
            T currentSmallest = items[sortedRangeEnd];
            int currentSmallestIndex = sortedRangeEnd;

            for (int i = sortedRangeEnd + 1; i < items.Length; i++)
            {
                if (currentSmallest.CompareTo(items[i]) > 0)
                {
                    currentSmallest = items[i];
                    currentSmallestIndex = i;
                }
            }
            return currentSmallestIndex;
        }
        //merge sort
        public static void MergeSort(ref T[] items) 
        { 
            if (items.Length <= 1) 
            { 
                return; 
            } 
 
            int leftSize = items.Length / 2; 
            int rightSize = items.Length - leftSize;

            T[] left = new T[leftSize];
            T[] right = new T[rightSize]; 
 
            Array.Copy(items, 0, left, 0, leftSize); 
            Array.Copy(items, leftSize, right, 0, rightSize);
            MergeSort(ref left);
            MergeSort(ref right);
            Merge(ref items, left, right);
        }

        private static void Merge(ref T[] items, T[] left, T[] right)
        {
            int leftIndex = 0;
            int rightIndex = 0;
            int targetIndex = 0;

            int remaining = left.Length + right.Length;

            while (remaining > 0)
            {
                if (leftIndex >= left.Length)
                {
                    items[targetIndex] = right[rightIndex++];
                }
                else if (rightIndex >= right.Length)
                {
                    items[targetIndex] = left[leftIndex++];
                }
                else if (left[leftIndex].CompareTo(right[rightIndex]) < 0)
                {
                    items[targetIndex] = left[leftIndex++];
                }
                else
                {
                    items[targetIndex] = right[rightIndex++];
                }

                targetIndex++;
                remaining--;
            }
        }

        //quick sorting
        private static Random _pivotRng = new Random();

        public static void QuickSort(ref T[] items)
        {
            quicksort(ref items, 0, items.Length - 1);
        }

        private static void quicksort(ref T[] items, int left, int right)
        {
            if (left < right)
            {
                int pivotIndex = _pivotRng.Next(left, right);
                int newPivot = partition(ref items, left, right, pivotIndex);

                quicksort(ref items, left, newPivot - 1);
                quicksort(ref items, newPivot + 1, right);
            }
        }

        private static int partition(ref T[] items, int left, int right, int pivotIndex)
        {
            T pivotValue = items[pivotIndex];
            _swap(ref items, pivotIndex, right);
            int storeIndex = left;
            for (int i = left; i < right; i++)
            {
                if (items[i].CompareTo(pivotValue) < 0)
                {
                    _swap(ref items, i, storeIndex);
                    storeIndex += 1;
                }
            }
            _swap(ref items, storeIndex, right);
            return storeIndex;
        }
    }
}
