using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab4v2
{
    delegate KeyValuePair<TKey, TValue> GenerateElement<TKey, TValue>(int j);

    class TestCollections<TKey, TValue>
    {
        private List<TKey> list = new List<TKey>();
        private List<string> stringList = new List<string>();
        private Dictionary<TKey, TValue> dictionary = new Dictionary<TKey, TValue>();
        private Dictionary<string, TValue> stringDictionary = new Dictionary<string, TValue>();

        public TestCollections(int size, GenerateElement<TKey, TValue> generator)
        {
            KeyValuePair<TKey, TValue> current;
            for (int i = 0; i < size; i++)
            {
                current = generator(i);
                list.Add(current.Key);
                stringList.Add(current.Key.ToString());
                dictionary.Add(current.Key, current.Value);
                stringDictionary.Add(current.Key.ToString(), current.Value);
            }
        }

        public static KeyValuePair<Person, Student> GenerateElement(int id)
        {
            Student student = new Student();
            student.Name += id;
            Person person = new Person();
            person.Surname += id;

            return new KeyValuePair<Person, Student>(person, student);
        }

        public bool FindInList(TKey key)
        {
            return list.Contains(key);
        }

        public bool FindInStringList(string str)
        {
            return stringList.Contains(str);
        }

        public bool FindKeyInDictionary(TKey key)
        {
            return dictionary.ContainsKey(key);
        }

        public bool FindKeyInStringDictionary(string str)
        {
            return stringDictionary.ContainsKey(str);
        }

        public bool FindValueInDictionary(TValue value)
        {
            return dictionary.ContainsValue(value);
        }

        public bool FindValueInStringDictionary(TValue value)
        {
            return stringDictionary.ContainsValue(value);
        }
    }
}
