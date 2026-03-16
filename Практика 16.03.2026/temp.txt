namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Cat cat1 = new Cat(1);
            cat1.Method1(); cat1.Method2(); cat1.Method3();

            Animal cat2 = new Cat(2);
            cat2.Method1(); cat2.Method2(); cat2.Method3();
        }
    }

    public abstract class Animal
    {
        private int _number;
        public abstract string Word { get; }
        public Animal(int number)
        {
            _number = number;
        }

        public void Method1()
        {
            Console.WriteLine("Animal method 1");
        }
        public virtual void Method2()
        {
            Console.WriteLine("Animal method 2");
        }

        public abstract void Method3();
    }

    public class Cat : Animal
    {
        //public override string Word => "Bye!";
        public override string Word
        {
            get
            {
                return "Bye!";
            }
        }
        public Cat(int number) : base(number) { }

        public new void Method1() // Скрыли обычный метод
        {
            Console.WriteLine("Cat method 1");
        }
        public override void Method2() // Переопределили виртуальный метод
        {
            Console.WriteLine("Cat method 2");
        }
        public override void Method3() // Переопределили абстрактный метод
        {
            Console.WriteLine("Cat method 3");
        }
    }
}
