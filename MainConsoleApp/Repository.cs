using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;

namespace MainConsoleApp
{
    /// <summary>
    /// Class for storaging workers repository
    /// </summary>
    class Repository
    {
        /// <summary>
        /// Contains path to storage file
        /// </summary>
        private readonly string path;
        
        /// <summary>
        /// Contains array of all workers
        /// </summary>
        private Worker[] workers;

        /// <summary>
        /// Constructor for creating repository
        /// </summary>
        /// <param name="path">Path to storage file</param>
        public Repository(string path)
        {
            this.path = path;
            if (!File.Exists(path))
            {
                File.Create(path);
            }

            FileInfo file = new FileInfo(path);
            int arrayLength = File.ReadLines(file.FullName).Count();
            workers = new Worker[arrayLength];
            int i = 0;

            using (StreamReader sr = new StreamReader(path))
            {

                while (!sr.EndOfStream)
                {
                    string stringToRead = sr.ReadLine();
                    string[] stringArray = stringToRead.Split('#');

                    workers[i] = new Worker(int.Parse(stringArray[0]), DateTime.Parse(stringArray[1]), stringArray[2], int.Parse(stringArray[3]), int.Parse(stringArray[4]), DateTime.Parse(stringArray[5]), stringArray[6]);
                    i++;
                }

            }
        }

        /// <summary>
        /// Returns all workers
        /// </summary>
        /// <returns>Array with all workers</returns>
        public Worker[] GetAllWorkers()
        {
            return workers;
        }

        /// <summary>
        /// Return worker by given id
        /// </summary>
        /// <param name="id">ID of worker</param>
        /// <returns>Worker with given id</returns>
        public Worker GetWorkerById(int id)
        {
            foreach(Worker worker in workers)
            {
                if (worker.Id == id)
                {
                    return worker;
                }
            }

            Console.WriteLine("!!! Введеный id отсутствует в базе !!!");
            return new Worker(id);
        }

        /// <summary>
        /// Returns all workers who's enry was created between given dates
        /// </summary>
        /// <param name="dateFrom">Date from</param>
        /// <param name="dateTo">Date to</param>
        /// <returns>Array of workers who's enry was created between given dates</returns>
        public Worker[] GetWorkersBetweenTwoDates(DateTime dateFrom, DateTime dateTo)
        {
            Worker[] result = new Worker[workers.Length];
            int i = 0;

            foreach (Worker worker in workers)
            {
                if (worker.EntryCreationDate >= dateFrom && worker.EntryCreationDate <= dateTo)
                {
                    result[i] = new Worker(worker.Id, worker.EntryCreationDate, worker.Name, worker.Age, worker.Height, worker.BirthDate, worker.BirthPlace);
                    i++;
                }                    
            }           

            Array.Resize(ref result, i);

            return result;
        }

        /// <summary>
        /// Adds new worker to array of workers
        /// </summary>
        /// <param name="worker">New worker</param>
        public void AddWorker(Worker worker)
        {
            int newIndex = workers.Length;
            Array.Resize(ref workers, workers.Length + 1);
            workers[newIndex] = worker;
        }

        /// <summary>
        /// Removes worker from array of workers
        /// </summary>
        /// <param name="id">ID of worker to remove</param>
        public void RemoveWorker(int id)
        {
            bool needToRemove = false;

            for (int i = 0; i < workers.Length; i++)
            {
                if (workers[i].Id == id)
                {
                    (workers[workers.Length - 1], workers[i]) = (workers[i], workers[workers.Length - 1]);
                    needToRemove = true;
                }
            }

            if (needToRemove)
            {
                Array.Resize(ref workers, workers.Length - 1);
                Console.WriteLine("Сотрудник с ID " + id + " был удален");
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("Сотрудника с введенным ID не существует");
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Changes worker properties
        /// </summary>
        /// <param name="id">ID of worker to change</param>
        public void ChangeWorker(int id)
        {
            string readLine;

            for (int i = 0; i < workers.Length; i++)
            {
                if (workers[i].Id != id)
                {
                    continue;
                }
                
                Console.Write("Введите новое ФИО (пустая строка для пропуска): ");
                readLine = Console.ReadLine();
                if (readLine != "")
                {
                    workers[i].Name = readLine;
                }

                readLine = AddInfo("Введите новый возраст (пустая строка для пропуска): ", true, false, true);
                if (readLine != "")
                {
                    workers[i].Age = int.Parse(readLine);
                }

                readLine = AddInfo("Введите новый рост (пустая строка для пропуска): ", true, false, true);
                if (readLine != "")
                {
                    workers[i].Height = int.Parse(readLine);
                }

                readLine = AddInfo("Введите новую дату рождения (пустая строка для пропуска): ", false, true, true);
                if (readLine != "")
                {
                    workers[i].BirthDate = DateTime.Parse(readLine);
                }

                Console.Write("Введите новое место рождения (пустая строка для пропуска): ");
                readLine = Console.ReadLine();
                if (readLine != "")
                {
                    workers[i].BirthPlace = readLine;
                }
            }

            Console.WriteLine("Изменения сохранены");
        }

        /// <summary>
        /// Creates a worker using dialog in command line
        /// </summary>
        /// <returns>Created worker</returns>
        public Worker CreateWorker()
        {
            int id = AddId();
            DateTime entryDate = DateTime.Now;
            string name = AddInfo("Ф. И. О.: ", false, false, false);
            int age = int.Parse(AddInfo("Возраст: ", true, false, false));
            int height = int.Parse(AddInfo("Рост: ", true, false, false));
            DateTime birthDate = DateTime.Parse(AddInfo("Дата рождения: ", false, true, false));
            string birthPlace = AddInfo("Место рождения: ", false, false, false);

            return new Worker(id, entryDate, name, age, height, birthDate, birthPlace);
        }

        /// <summary>
        /// Writes all changes into file
        /// </summary>
        public void CommitChanges()
        {
            using (StreamWriter sw = new StreamWriter(path, false))
            {
                foreach (Worker worker in workers)
                {
                    sw.WriteLine(worker.Id + "#" + worker.EntryCreationDate.ToShortDateString() + "#" + worker.Name + "#" + worker.Age + "#" + worker.Height + "#" + worker.BirthDate.ToShortDateString() + "#" + worker.BirthPlace);
                }
            }
        }

        /// <summary>
        /// Generates info string
        /// </summary>
        /// <param name="infoLabel">Label to show on screen</param>
        /// <param name="isNumeric">Is parametr numeric</param>
        /// <param name="isDate">Is parametr date type</param>
        /// <param name="validEmpty">Is empty valid</param>
        /// <returns>Info string</returns>
        private string AddInfo(string infoLabel, bool isNumeric, bool isDate, bool validEmpty)
        {
            string info;

            while (true)
            {
                Console.Write(infoLabel);
                info = Console.ReadLine();

                if (validEmpty && info == "")
                {
                    break;
                }

                if (isNumeric && !int.TryParse(info, out _))
                {
                    Console.WriteLine("Только числовые значения !");
                    continue;
                }

                if (isDate && !DateTime.TryParse(info, out _))
                {
                    Console.WriteLine("Введите дату в формате дд.мм.гггг");
                    continue;
                }

                break;
            }
            return info;
        }

        /// <summary>
        /// Returns list of used id
        /// </summary>
        /// <param name="path">Path to file</param>
        /// <returns>Used ID list</returns>
        public List<int> GetIdList()
        {
            List<int> result = new List<int>();

            if (!File.Exists(path))
            {
                return result;
            }

            foreach (Worker worker in workers)
            {
                result.Add(worker.Id);
            }

            return result;
        }

        /// <summary>
        /// Returns id for new worker
        /// </summary>
        /// <param name="idList">list of used id</param>
        /// <returns>id for usage</returns>
        private int AddId()
        {
            int id;
            List<int> idList = GetIdList();

            while (true)
            {
                Console.Write("ID: ");
                string idString = Console.ReadLine();

                if (!int.TryParse(idString, out int _))
                {
                    Console.WriteLine("Только числовые значения !");
                    continue;
                }
                id = int.Parse(idString);
                if (idList.Contains(id))
                {
                    Console.WriteLine("Этот id уже существует, введите другой!");
                    continue;
                }
                

                break;
            }

            return id;
        }

        /// <summary>
        /// Method for reading date from command line with check
        /// </summary>
        /// <param name="caption">Caption to show</param>
        /// <returns>Entered Date</returns>
        public DateTime EnterDateWithCheck(string caption)
        {
            string stringDate;
            
            while (true)
            {
                Console.Write(caption);
                stringDate = Console.ReadLine();

                if (!DateTime.TryParse(stringDate, out DateTime _))
                {
                    Console.WriteLine("Введите дату в формате дд.мм.гггг !");
                    continue;
                }

                break;
            }

            return DateTime.Parse(stringDate);
        }

        /// <summary>
        /// Prints array of workers on screen
        /// </summary>
        /// <param name="workers">Array of workers</param>
        public void Show(Worker[] workers)
        {
            foreach (Worker worker in workers)
            {
                worker.Print();
            }

            Console.WriteLine();
            Console.ReadKey();
        }

        /// <summary>
        /// Prints single worker on screen
        /// </summary>
        /// <param name="worker">Worker</param>
        public void Show(Worker worker)
        {
            worker.Print();            

            Console.WriteLine();
            Console.ReadKey();
        }

        /// <summary>
        /// Sorts workers with selected sort type
        /// </summary>
        /// <param name="typeOfSort">Type of sort</param>
        public void SortWorkers(int typeOfSort)
        {
            switch (typeOfSort)
            {
                case 1:
                    workers = workers.OrderBy(w => w.Id).ToArray();
                    break;
                case 2:
                    workers = workers.OrderBy(w => w.EntryCreationDate).ToArray();
                    break;
                case 3:
                    workers = workers.OrderBy(w => w.Name).ToArray();
                    break;
                case 4:
                    workers = workers.OrderBy(w => w.Age).ToArray();
                    break;
                case 5:
                    workers = workers.OrderBy(w => w.Height).ToArray();
                    break;
                case 6:
                    workers = workers.OrderBy(w => w.BirthDate).ToArray();
                    break;
                case 7:
                    workers = workers.OrderBy(w => w.BirthPlace).ToArray();
                    break;
                default:
                    break;
            }
        }
        
    }
}
