using System;

namespace MainConsoleApp
{
    /// <summary>
    /// Structure contains info about worker
    /// </summary>
    struct Worker
    {
        /// <summary>
        /// ID of entry
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// Date of entry creation
        /// </summary>
        public DateTime EntryCreationDate { get; set; }
        
        /// <summary>
        /// Name of worker
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Age of worker
        /// </summary>
        public int Age { get; set; }
        
        /// <summary>
        /// Height of worker
        /// </summary>
        public int Height { get; set; }
        
        /// <summary>
        /// Birth date of worker
        /// </summary>
        public DateTime BirthDate { get; set; }
        
        /// <summary>
        /// Birth place of worker
        /// </summary>
        public string BirthPlace { get; set; }

        /// <summary>
        /// Full constructor
        /// </summary>
        /// <param name="id">ID of entry</param>
        /// <param name="entryCreationDate">Date of entry creation</param>
        /// <param name="name">Name of worker</param>
        /// <param name="age">Age of worker</param>
        /// <param name="height">Height of worker</param>
        /// <param name="birthDate">Birth date of worker</param>
        /// <param name="birthPlace">Birth place of worker</param>
        public Worker (int id, DateTime entryCreationDate, string name, int age, int height, DateTime birthDate, string birthPlace)
        {
            this.Id = id;
            this.EntryCreationDate = entryCreationDate;
            this.Name = name;
            this.Age = age;
            this.Height = height;
            this.BirthDate = birthDate;
            this.BirthPlace = birthPlace;
        }

        /// <summary>
        /// Constructor with default Birth place of worker
        /// </summary>
        /// <param name="id">ID of entry</param>
        /// <param name="entryCreationDate">Date of entry creation</param>
        /// <param name="name">Name of worker</param>
        /// <param name="age">Age of worker</param>
        /// <param name="height">Height of worker</param>
        /// <param name="birthDate">Birth date of worker</param>        
        public Worker(int id, DateTime entryCreationDate, string name, int age, int height, DateTime birthDate) :
            this(id, entryCreationDate, name, age, height, birthDate, "DefaultCity")
        {

        }

        /// <summary>
        /// Constructor with default Birth place of worker, Birth date of worker
        /// </summary>
        /// <param name="id">ID of entry</param>
        /// <param name="entryCreationDate">Date of entry creation</param>
        /// <param name="name">Name of worker</param>
        /// <param name="age">Age of worker</param>
        /// <param name="height">Height of worker</param>        
        public Worker(int id, DateTime entryCreationDate, string name, int age, int height) :
            this(id, entryCreationDate, name, age, height, new DateTime(1990, 1, 1), "DefaultCity")
        {

        }

        /// <summary>
        /// Constructor with default Birth place of worker, Birth date of worker, Height of worker
        /// </summary>
        /// <param name="id">ID of entry</param>
        /// <param name="entryCreationDate">Date of entry creation</param>
        /// <param name="name">Name of worker</param>
        /// <param name="age">Age of worker</param>        
        public Worker(int id, DateTime entryCreationDate, string name, int age) :
            this(id, entryCreationDate, name, age, 180, new DateTime(1990, 1, 1), "DefaultCity")
        {

        }

        /// <summary>
        /// Constructor with default Birth place of worker, Birth date of worker, Height of worker, Age of worker
        /// </summary>
        /// <param name="id">ID of entry</param>
        /// <param name="entryCreationDate">Date of entry creation</param>
        /// <param name="name">Name of worker</param>        
        public Worker(int id, DateTime entryCreationDate, string name) :
            this(id, entryCreationDate, name, 30, 180, new DateTime(1990, 1, 1), "DefaultCity")
        {

        }

        /// <summary>
        /// Constructor with default Birth place of worker, Birth date of worker, Height of worker, Age of worker, Name of worker
        /// </summary>
        /// <param name="id">ID of entry</param>
        /// <param name="entryCreationDate">Date of entry creation</param>        
        public Worker(int id, DateTime entryCreationDate) :
            this(id, entryCreationDate, "Lastname Firstname Midname", 30, 180, new DateTime(1990, 1, 1), "DefaultCity")
        {

        }

        /// <summary>
        /// Constructor with default Birth place of worker, Birth date of worker, Height of worker, Age of worker, Name of worker, Date of entry creation
        /// </summary>
        /// <param name="id">ID of entry</param>        
        public Worker(int id) :
            this(id, DateTime.Today, "Lastname Firstname Midname", 30, 180, new DateTime(1990, 1, 1), "DefaultCity")
        {

        }

        /// <summary>
        /// Prints all workers info
        /// </summary>
        public void Print()
        {
            Console.WriteLine();
            Console.WriteLine("ID: " + Id);
            Console.WriteLine("Дата добавления записи: " + EntryCreationDate);
            Console.WriteLine("Ф. И. О.: " + Name);
            Console.WriteLine("Возраст: " + Age);
            Console.WriteLine("Рост: " + Height);
            Console.WriteLine("Дата рождения: " + BirthDate);
            Console.WriteLine("Место рождения: " + BirthPlace);
        }

    }
}
