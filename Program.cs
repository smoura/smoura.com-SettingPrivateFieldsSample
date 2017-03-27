namespace SettingPrivateField
{
    using System;
    using System.Reflection;

    class Program
    {
        public class Person
        {
            private string name;

            public string Name
            {
                get { return name; }
                private set { name = value; }
            }

            public Person(string name)
            {
                this.name = name;
            }
        }

        public static void SetField<T>(T instance, string fieldName, object obj)
        {
            var f = typeof(T).GetField(fieldName, BindingFlags.Instance | BindingFlags.NonPublic);
            if (obj.GetType() != f.FieldType)
                throw new InvalidOperationException("Types do not match!");
            f.SetValue(instance, obj);
        }

        static void Main(string[] args)
        {
            var person = new Person("John");
            Console.WriteLine(string.Format("My name is {0}!", person.Name));
            SetField(person, "name", "Jason");
            Console.WriteLine(string.Format("My name is {0}!", person.Name));

            Console.ReadKey();
        }
    }
}
