using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Lab4
{
    public class List
    {
        public static int Size { get; set; }
        public int Count { get; set; }
        public string Value { get; set; }
        public List Next { get; set; }
        public static List Tail { get; set; }
        class Owner
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Org { get; set; }
        }
        class Date
        {
            public string CreationDate { get; set; }
        }
        List.Owner owner = new Owner() { Id = 0, Name = "Алексей", Org = "БГТУ" };
        List.Date date = new Date() { CreationDate = "31.10.2020" };
        public List()
        {
            Size++;
            Count = 0;
            Value = null;
            Next = null;
            Tail = this;
        }

        public List(string data)
        {
            Size++;
            Count++;
            Value = data;
            Next = null;
            Tail = this;
        }
        public void Append(string data)
        {
            Tail = new List(data);
            if ((object)Next == null)
            {
                Next = Tail;
            }
            else
            {
                this[Size - 1].Next = Tail;
                Tail.Count = Size-1;
            }
        }

        public List this[int index]
        {
            get
            {
                List temp = this;
                if (index < List.Size)
                {
                    while (temp.Count < index && (object)temp.Next != null)
                        temp = temp.Next;
                    return temp;
                }
                else
                {
                    throw new OverflowException();
                }
            }
            set { }
        }

        public static List operator +( List leftValue, int index)
        {
            if ((object)leftValue[index] != null)
            {
                var NewList = leftValue;
                var newEl = new List();
                newEl.Count = index;
                newEl.Next = NewList[index];
                NewList[index].Count++;
                if (index>0)
                    NewList[index - 1].Next = newEl;
                return NewList;
            }
            else throw new ArgumentNullException();

        }
        public static List operator >>(List leftValue, int index)
        {
            if ((object)leftValue[index] != null)
            {
                if (index > 0)
                {
                    leftValue[index - 1] = leftValue[index].Next;
                    leftValue[index] = null;
                    Size--;
                    for (int i = index + 1; i < Tail.Count; i++)
                        leftValue[i].Count--;
                }
                else leftValue = null;
                return leftValue;
            }
            else throw new ArgumentNullException();

        }
        public static bool operator !=(List leftValue, List rightValue)
        {
            for (int i =0; i<List.Size;i++)
            {
                if (leftValue[i].Value != rightValue[i].Value)
                    return true;
            }
            return false;
        }
        public static bool operator ==(List leftValue, List rightValue)
        {
            for (int i = 0; i < List.Size; i++)
            {
                if (leftValue[i].Value != rightValue[i].Value)
                    return false;
            }
            return true;
        }
    }
    public static class StatisticOperation
    {
        static int Sum()
        {
            var last = List.Tail;
            int sum = 0;
            while (last.Count != 0)
            {
                if (last.Value != null)
                    sum += last.Value.Length;
            }
            return sum;
        }
        public static class Difference
        {
            static int MaxDiff()
            {
                var last = List.Tail;
                int maxSize = 0;
                if (last.Value != null)
                {
                    int minSize = last.Value.Length;
                    while (last.Count != 0)
                    {
                        if (maxSize < last.Value.Length)
                            maxSize = last.Value.Length;
                        if (minSize > last.Value.Length)
                            minSize = last.Value.Length;
                    }
                    return maxSize - minSize;
                }
                return maxSize;
            }
        }
        public static class ListSize
        {
            static int Size()
            {
                return List.Size;
            }
        }
        public static int MaxLength(this List catalogue)
        {
            int catSize = List.Tail.Count;
            int length = 0;
            int maxLength = 0;
            for (int i = 0; i < catSize; i++)
            {
                if ((object)catalogue[i] == null || catalogue[i].Value == null)
                    continue;
                else
                {
                    length = catalogue[i].Value.Length;
                    if (maxLength < length)
                        maxLength = length;
                }
            }
            return maxLength;
        }

        public static void Pop(this List catalogue)
        {
            int newEnd = List.Tail.Count - 1;
            catalogue[newEnd].Next = null;
            List.Tail = catalogue[newEnd];

        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            List item = new List();

            item.Append("item2");
            item.Append("item3");
            List item2 = new List("newitem");
            var f = item + 2;
            var t = item >> 1;
            item.Pop();
            Console.WriteLine(item.MaxLength());
        }
    }
}
