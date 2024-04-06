using System;
using System.IO;
using System.Runtime.Serialization;

namespace BinaryFileReadWrite
{
    [Serializable]
    public class UserData
    {
        public string? Name { get; set; }
        public int Age { get; set; }
        public string? Address { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Wybierz opcję:");
            Console.WriteLine("1. Zapisz dane do pliku binarnego");
            Console.WriteLine("2. Odczytaj dane z pliku binarnego");
            int choice;
            if (!int.TryParse(Console.ReadLine(), out choice))
            {
                Console.WriteLine("Nieprawidłowa opcja.");
                return;
            }

            switch (choice)
            {
                case 1:
                    SaveData();
                    break;
                case 2:
                    ReadData();
                    break;
                default:
                    Console.WriteLine("Nieprawidłowa opcja.");
                    break;
            }
        }

        static void SaveData()
        {
            Console.WriteLine("Podaj imię:");
            string name = Console.ReadLine();
            Console.WriteLine("Podaj wiek:");
            if (!int.TryParse(Console.ReadLine(), out int age))
            {
                Console.WriteLine("Nieprawidłowy wiek.");
                return;
            }
            Console.WriteLine("Podaj adres:");
            string address = Console.ReadLine();

            UserData userData = new UserData
            {
                Name = name,
                Age = age,
                Address = address
            };

            using (FileStream stream = new FileStream("userData.bin", FileMode.Create, FileAccess.Write))
            {
                var serializer = new System.Runtime.Serialization.Json.DataContractJsonSerializer(typeof(UserData));
                serializer.WriteObject(stream, userData);
            }

            Console.WriteLine("Dane zostały zapisane do pliku.");
        }

        static void ReadData()
        {
            if (File.Exists("userData.bin"))
            {
                using (FileStream stream = new FileStream("userData.bin", FileMode.Open, FileAccess.Read))
                {
                    var serializer = new System.Runtime.Serialization.Json.DataContractJsonSerializer(typeof(UserData));
                    UserData userData = (UserData)serializer.ReadObject(stream);
                    Console.WriteLine($"Imię: {userData.Name}");
                    Console.WriteLine($"Wiek: {userData.Age}");
                    Console.WriteLine($"Adres: {userData.Address}");
                }
            }
            else
            {
                Console.WriteLine("Plik z danymi nie istnieje.");
            }
        }
    }
}