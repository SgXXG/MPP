using System;
using System.Collections;
using System.Collections.Generic;

namespace MPP_Lab5
{
    class DynamicList<T> : IEnumerable<T>
    {
        private T[] _container;
        private int _nextPosition = 0;

        public void Add(T value)
        {
            EnsureCapacity();
            _container[_nextPosition] = value;
            _nextPosition++;
        }

        public void Remove(T value)
        {
            RemoveAt(Array.IndexOf(_container, value));
        }

        public void RemoveAt(int index)
        {
            var newContainer = new T[_container.Length - 1];
            Array.Copy(_container, newContainer, index);
            Array.Copy(_container, index + 1, newContainer, index, _container.Length - index - 1);
            _container = newContainer;
            _nextPosition--;
        }

        public void Clear()
        {
            _container = null;
            _nextPosition = 0;
        }

        public int Count
        {
            get { return _nextPosition; }
        }

        public T this[int index]
        {
            get { return _container[index]; }
            set { _container[index] = value; }
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (var i = 0; i < _nextPosition; i++)
            {
                yield return _container[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private void EnsureCapacity()
        {
            if (_container == null)
            {
                _container = new T[16];
            }
            if (_nextPosition == _container.Length)
            {
                var newContainer = new T[_container.Length + 16];
                Array.Copy(_container, newContainer, _container.Length);
                _container = newContainer;
            }
        }
    }

    static class Program
    {
        static void Main(string[] args)
        {
            var dynamicList = new DynamicList<int>();
            for (var i = 0; i <= 128; i++)
            {
                dynamicList.Add(i);
            }
            foreach (var element in dynamicList)
            {
                Console.Write(element + " ");
            }
            Console.WriteLine("\n");

            for (var i = 32; i <= 64; i++)
            {
                dynamicList.Remove(i);
            }
            foreach (var element in dynamicList)
            {
                Console.Write(element + " ");
            }
            Console.WriteLine("\n");
        }
    }
}