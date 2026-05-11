using System.Globalization;
using System.Xml.Serialization;

namespace ConsoleApp2
{
    public class Movie
    {
        private string _name;
        private int _duration;
        private int[] _rating;
        public string Name => _name;
        public int Duration => _duration;
        public int[] Rating => _rating.ToArray();
        public Movie(string name, int duration)
        {
            _name = name;
            _duration = duration;
            _rating = new int[0];
        }
        public void Add(int stars)
        {
            Array.Resize(ref _rating, _rating.Length + 1);
            _rating[_rating.Length - 1] = stars;
        }
    }
    
    public class MovieDTO
    {
        public string Name { get; set; }
        public int Duration { get; set; }
        public MovieDTO() { }
        public MovieDTO(string name, int duration)
        {
            Name = name;
            Duration = duration;
        }
        public MovieDTO(Movie movie)
        {
            Name = movie.Name;
            Duration = movie.Duration;
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            // Создаём путь до файла
            string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string filePath = Path.Combine(folderPath, "movie.xml");

            // Создаём XML - сериализатор
            // - Класс должен иметь конструктор без параметров
            // - Класс должен быть публичным
            // - В классе все свойства должны быть с публичным get и set

            // Оригинальный объект -> ТДО объект -> отдать его в сериализатор
            // Объект для сериализации
            Movie movie1 = new Movie("The Devil's Advocate", 143);
            movie1.Add(5);
            movie1.Add(5);
            movie1.Add(2);
            MovieDTO movieDTO1 = new MovieDTO(movie1);
            var serializer = new XmlSerializer(typeof(MovieDTO));
            using (var writer = new StreamWriter(filePath))
            {
                serializer.Serialize(writer, movieDTO1);
            }

            // Десериализуем объект из файла -> получаем ДТО объект -> оригинальный объект
            MovieDTO movieDTO2;
            using(var reader = new StreamReader(filePath))
            {
                movieDTO2 = (MovieDTO)serializer.Deserialize(reader);
            }
            Movie movie2 = new Movie(movieDTO2.Name, movieDTO2.Duration);

            if (CompareMovies(movie1, movie2))
                Console.WriteLine("Success");
            else
                Console.WriteLine("Wrong");

        }
        private static bool CompareMovies(Movie m1, Movie m2)
        {
            if (m1.Name != m2.Name) return false;
            if (m1.Duration != m2.Duration) return false;
            if (m1.Rating.Length != m2.Rating.Length) return false;
            for (int i = 0; i < m1.Rating.Length; i++)
            {
                if (m1.Rating[i] != m2.Rating[i]) return false;
            }
            return true;
        }
    }
}
