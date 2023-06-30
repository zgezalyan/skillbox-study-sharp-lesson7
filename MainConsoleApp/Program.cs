using System;
using System.Collections.Generic;

namespace MainConsoleApp
{
    internal class Program
    {

        /// <summary>
        /// Action to show all workers
        /// </summary>
        /// <param name="repository">Repository</param>
        private static void ShowAllWorkersAction(Repository repository)
        {
            repository.Show(repository.GetAllWorkers());
        }

        /// <summary>
        /// Action to show one worker
        /// </summary>
        /// <param name="repository">Repository</param>
        private static void ShowWorkerByIdAction(Repository repository)
        {
            int id;
            List<int> idList = repository.GetIdList();

            while (true)
            {
                Console.Write("Введите id записи: ");
                string idString = Console.ReadLine();

                if (!int.TryParse(idString, out int _))
                {
                    Console.WriteLine("Только числовые значения !");
                    continue;
                }
                id = int.Parse(idString);
                if (!idList.Contains(id))
                {
                    Console.WriteLine("Такого id нет, введите другой!");
                    continue;
                }

                break;
            }

            repository.Show(repository.GetWorkerById(id));
        }

        /// <summary>
        /// Action to show workers in date range
        /// </summary>
        /// <param name="repository">Repository</param>
        private static void ShowWorkersByDateAction(Repository repository)
        {
            DateTime startDate = repository.EnterDateWithCheck("Введите стартовую дату: ");
            DateTime endDate = repository.EnterDateWithCheck("Введите конечную дату: ");

            repository.Show(repository.GetWorkersBetweenTwoDates(startDate, endDate));
        }

        /// <summary>
        /// Action to add new worker
        /// </summary>
        /// <param name="repository">Repository</param>
        private static void AddNewWorkerAction(Repository repository)
        {
            Worker worker = repository.CreateWorker();
            repository.AddWorker(worker);
            repository.CommitChanges();
            Console.WriteLine();
        }

        /// <summary>
        /// Action to remove worker
        /// </summary>
        /// <param name="repository">Repository</param>
        private static void RemoveWorkerAction(Repository repository)
        {
            int id;

            while (true)
            {
                Console.Write("Введите id записи: ");
                string idString = Console.ReadLine();

                if (!int.TryParse(idString, out int _))
                {
                    Console.WriteLine("Только числовые значения !");
                    continue;
                }
                id = int.Parse(idString);
                break;
            }

            repository.RemoveWorker(id);
            repository.CommitChanges();
            Console.WriteLine();
        }

        /// <summary>
        /// Action to change worker
        /// </summary>
        /// <param name="repository">Repository</param>
        private static void ChangeWorkerAction(Repository repository)
        {
            int id;
            List<int> idList = repository.GetIdList();

            while (true)
            {
                Console.Write("Введите id записи: ");
                string idString = Console.ReadLine();

                if (!int.TryParse(idString, out int _))
                {
                    Console.WriteLine("Только числовые значения !");
                    continue;
                }
                id = int.Parse(idString);
                if (!idList.Contains(id))
                {
                    Console.WriteLine("Такого id нет, введите другой!");
                    continue;
                }

                break;
            }

            repository.ChangeWorker(id);
            repository.CommitChanges();
            Console.WriteLine();
        }

        /// <summary>
        /// ACtion to sort workers
        /// </summary>
        /// <param name="repository">Repository</param>
        private static void SortWorkersAction(Repository repository)
        {
            Console.WriteLine();
            while (true)
            {
                Console.WriteLine("Выберите тип сортировки: ");
                Console.WriteLine("1 - по ID (клавиша 1)");
                Console.WriteLine("2 - по дате создания записи (клавиша 2)");
                Console.WriteLine("3 - по имени (клавиша 3)");
                Console.WriteLine("4 - по возрасту (клавиша 4)");
                Console.WriteLine("5 - по росту (клавиша 5)");
                Console.WriteLine("6 - по дню рождения (клавиша 6)");
                Console.WriteLine("7 - по городу рождения (клавиша 7)");
                string action = Console.ReadLine();

                if (int.TryParse(action, out int intAction))
                {
                    repository.SortWorkers(intAction);
                    break;
                }
                
            }
            Console.WriteLine();
        }

        static void Main(string[] args)
        {
            Repository repository = new Repository("../../employees.txt");
            
            while (true)
            {
                Console.WriteLine("Выберите действие: ");
                Console.WriteLine("1 - просмотреть все записи (клавиша 1)");
                Console.WriteLine("2 - просмотреть одну запись (клавиша 2)");
                Console.WriteLine("3 - просмотреть записи в диапозоне дат (клавиша 3)");
                Console.WriteLine("4 - добавить новую запись (клавиша 4)");
                Console.WriteLine("5 - удалить запись (клавиша 5)");
                Console.WriteLine("6 - редактировать запись (клавиша 6)");
                Console.WriteLine("7 - Отсортировать записи (клавиша 7)");
                Console.WriteLine("0 - выход (клавиша 0)");
                string action = Console.ReadLine();

                switch(action)
                {
                    case "1":
                        ShowAllWorkersAction(repository);
                        break;
                    case "2":                                               
                        ShowWorkerByIdAction(repository);
                        break;
                    case "3":                        
                        ShowWorkersByDateAction(repository);
                        break;
                    case "4":
                        AddNewWorkerAction(repository);
                        break;
                    case "5":
                        RemoveWorkerAction(repository);
                        break;
                    case "6":
                        ChangeWorkerAction(repository);
                        break;
                    case "7":
                        SortWorkersAction(repository);
                        break;
                    case "0":
                        break;
                    default:                        
                        break;
                }

                if (action == "0")
                {
                    break;
                }
            }
            
        }
    }
}
