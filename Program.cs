using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 核心4_列表
{
    class Student
    {
        public int score;
        public string name;
        public Student(string name,int score)
        {
            this.name = name;
            this.score = score;
        }

        public override string ToString()
        {
            return $"{name}:{score}";
        }
    }
    class MyList<T>
    {

        // 私有成员
        T[] data; // 实际保存列表数据的数组
        int count;

        public MyList()
        {
            data = new T[8];
            count = 0;
        }

        // 公开的属性和方法
        public int Count
        {
            get 
            {
                return count;
            }
        }

        // 在后面添加元素value
        public void Add(T value)
        {
            // count == 8 如何处理？
            if(count == data.Length)
            {
                // 拓容
                T[] temp = new T[data.Length * 2];
                // 拷贝原始的数据 到temp里
                data.CopyTo(temp, 0);
                data = temp;   // data指向temp数组，原来较短的数组被GC，原来对象失去引用
            }

            data[count] = value;
            count++;
        }
        
        // 删除元素（按下标i）
        public void RemoveAt(int index)
        {
            if (index < 0 || index >= count)
            {
                throw new IndexOutOfRangeException($"MyList RemoveAt越界 i={index} count={count}");
            }
            for (int i = index;i <count-1;i++)
            {
                data[i] = data[i + 1];
            }
            count--;
        }

        //public T Get(int i)
        //{
        //    if(i <0 || i >=count)
        //    {
        //        throw new IndexOutOfRangeException($"MyList Get越界 i={i} count={count}");
        //    }
        //    return data[i];
        //}
        //public void Set(int i,T v)
        //{
        //    if (i < 0 || i >= count)
        //    {
        //        throw new IndexOutOfRangeException($"MyList Get越界 i={i} count={count}");
        //    }
        //    data[i] = v;
        //}

        public T this[int i]
        {
            get
            {
                if (i < 0 || i >= count)
                {
                    throw new IndexOutOfRangeException($"MyList Get越界 i={i} count={count}");
                }
                return data[i];
            }
            set
            {
                if (i < 0 || i >= count)
                {
                    throw new IndexOutOfRangeException($"MyList Get越界 i={i} count={count}");
                }
                data[i] = value;
            }
        }

        public void Sort()
        {
            // Array.Sort(data);
            Array.Sort(data, 0, count);
        }

        public void Sort(Comparison<T> comp)
        {
            T[] temp = new T[count];
            Array.Copy(data, temp, count);
            Array.Sort(temp, comp);

            temp.CopyTo(data, 0);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for(int i = 0;i < count;i++)
            {
                sb.Append(data[i]);
                sb.Append(" ");
            }
            return sb.ToString();
        }


    }
    class Program
    {
        static void Main(string[] args)
        {
            MyList<int> list = new MyList<int>();
            //list.Add(1);
            //list.Add(2);
            //list.Add(3);    // [1,2,3]
            //list.RemoveAt(1);  // [1,3]
            //for(int i = 0;i <list.Count;i++)
            //{
            //    Console.WriteLine(list.Get(i));
            //}

            // ---------------------
            for (int i = 0; i < 60; i++)
            {
                list.Add(100-i);
            }

            for(int i = 0;i < 10;i++)
            {
                list.RemoveAt(1);
            }
            Console.WriteLine(list);

            //list.Sort();
            list.Sort((a, b) => { return -a.CompareTo(b); });

            Console.WriteLine(list);

            MyList<string> list_string = new MyList<string>();
            list_string.Add("小明");
            list_string.Add("你好");
            list_string.Add("!");

            list_string[0] = "小红";

            Console.WriteLine(list_string);

            list_string.Sort();

            Console.WriteLine(list_string);

            MyList<Student> students = new MyList<Student>();
            Student s1 = new Student("小明", 80);
            Student s2 = new Student("小红", 100);
            Student s3 = new Student("小军", 90);
            students.Add(s1);
            students.Add(s2);
            students.Add(s3);

            students.Sort((a,b) => { 
                return a.score.CompareTo(b.score); 
            });

            Console.WriteLine(students);
            Console.ReadKey();
        }
    }
}
