using System;
using System.Collections.Generic;
using System.Linq;


namespace internship_3_oop_intro
{
    class Program
    {
        enum Option
        {
            AddEvent = 1,

        }
        static void Main(string[] args)
        {
            DateTime date1 = new DateTime(2015, 12, 24);
            DateTime date2 = new DateTime(2015, 12, 25);
            var event1_Info = new Event("Coffee break", 1, date1, date2);
            var event1_Attendes = new List<Person>();
            event1_Attendes.Add(new Person("Anna", "Jo", "1234", 098));
            event1_Attendes.Add(new Person("John", "Jo", "5678", 091));

            var eventsInfo = new Dictionary<Event, List<Person>>()
            {
                { event1_Info, event1_Attendes}
            };

            foreach (var happen in eventsInfo)
            {
                Console.WriteLine(happen.Key.Name + " " + happen.Value[1].FirstName);
            }

            var a = -1;
            while (a != 0)
            {

                var choice = int.Parse(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        AddEvent(eventsInfo);
                        break;
                    case 2:
                        DeleteOrEditEvent(eventsInfo, "izbrisati");
                        break;
                    case 3:
                        DeleteOrEditEvent(eventsInfo, "editirati");
                        break;
                    case 4:
                        AddPersonToEvent(eventsInfo);
                        break;
                    case 5:
                        RemovePersonFromEvent(eventsInfo);
                        break;
                    case 6:
                        a = 0;
                        break;

                    default:

                        Console.WriteLine("Krivi unos");
                        break;


                }
            }

            foreach (var item in eventsInfo)
                Console.WriteLine(item.Key.Name);



            /*

            var myEvent = new Event("me", 1,5,6);
            Console.WriteLine(myEvent.TypeOfEvent);
            */


            //}
        }
        //
        static DateTime setDateTime()
        {
            var a = -1;
            DateTime validTime = new DateTime(2017, 1, 18);
            while (a != 0)
            {
                var inputTime = Console.ReadLine();
                if (DateTime.TryParse(inputTime, out validTime))
                {
                    a = 0;
                }
                else
                {
                    Console.WriteLine("Vaš unos trajanja nije ispravan. Ponovite unos:");
                }
            }
            return validTime;
        }

        static bool isValidStringInput(string text)
        {
            if (text.Length == 0)
            {
                Console.WriteLine("Unijeli ste prazan string. Ponovite unos.");
                return false;
            }
            else
            {
                return true;
            }
        }

        //
        static void AddEvent(Dictionary<Event, List<Person>> dictionary)
        {
            var a = -1;
            var name = "";
            Console.WriteLine("Odabrali ste: unos novog event-a.");


            while (a != 0)
            {
                Console.WriteLine("Unesite ime  event-a: ");
                name = Console.ReadLine();
                if (name.Length == 0)
                {
                    Console.WriteLine("Unijeli ste prazan string. Trebate ponoviti unos.");
                }
                else
                    a = 0;
            }


            a = -1;
            int typeOfEvent = -1;
            while (a != 0)
            {
                Console.WriteLine("Odaberite tip novog event-a unosom odgovarajućeg broja:");
                Console.WriteLine(
                " 0 - Coffee\n" +
                " 1 - Lecture\n" +
                " 2 - Concert\n" +
                " 3 - StudySession\n"
                );
                var input = Console.ReadLine();
                var number = -1;
                bool isNumber = Int32.TryParse(input, out number);
                if (isNumber)
                {
                    if (number >= 0 && number <= 4)
                    {
                        typeOfEvent = number;
                        a = 0;
                    }
                    else
                    {
                        Console.WriteLine("Unijeli ste broj koji nije ponuđen. Ponovite unos.");
                    }
                }
                else
                {
                    Console.WriteLine("Niste unijeli broj. Ponovite unos");
                }
            }


            a = -1;
            DateTime startTime = new DateTime();
            DateTime endTime = new DateTime();
            while (a != 0)
            {
                Console.WriteLine("Unesite početak trajanja event-a u formatu dd/mm/yyyy hh:mm");
                startTime = setDateTime();
                Console.WriteLine("Unesite završetak trajanja event-a u formatu dd/mm/yyyy hh:mm");
                endTime = setDateTime();
                int result = DateTime.Compare(startTime, endTime);
                if (result > 0)
                {
                    Console.WriteLine("Kraj event-a ne može biti prije početka. Ponovite unos.");
                }
                else if (result == 0)
                {
                    Console.WriteLine("Vrijeme početka i kraja event-a ne mogu biti isti. Ponovite unos.");
                }
                else
                {
                    a = 0;
                }
            }

            var newEvent = new Event(name, typeOfEvent, startTime, endTime);
            var eventAttendes = new List<Person>();
            dictionary.Add(newEvent, eventAttendes);
        }


        static void DeleteOrEditEvent(Dictionary<Event, List<Person>> dictionary, string option)
        {
            Console.WriteLine($"Odabrali ste: {option} event.", option);
            var a = -1;
            var i = 0;
            int eventIndex = -1;


            while (a != 0)
            {
                i = 0;
                if (dictionary.Count == 0)
                {
                    Console.WriteLine("Vaš popis je već prazan");
                    a = 0;
                }
                else
                {
                    Console.WriteLine("Ovo je vaš trenutni popis event-ova:");
                    foreach (var item in dictionary)
                    {
                        Console.WriteLine(" " + i + " - " + item.Key.Name + " (Početak event-a: " + item.Key.StartTime + " , kraj event-a: " + item.Key.EndTime + ") ");
                        i++;
                    }
                    Console.WriteLine($"Unesite odgovarajući broj event-a kojeg želite {option}:");
                    var input = Console.ReadLine();
                    var number = -1;
                    bool isNumber = Int32.TryParse(input, out number);
                    if (isNumber)
                    {
                        if (number >= 0 && number <= dictionary.Count)
                        {
                            eventIndex = number;
                            a = 0;
                        }
                        else
                        {
                            Console.WriteLine("Unijeli ste broj koji nije ponuđen. Ponovite unos.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Niste unijeli broj. Ponovite unos");
                    }
                }
            }

            if (dictionary.Count != 0)
            {
                i = 0;
                DateTime date1 = new DateTime(2015, 12, 24);
                DateTime date2 = new DateTime(2015, 12, 25);
                var chosenEvent = new Event("test", 0, date1, date2);
                foreach (var item in dictionary)
                {
                    if (eventIndex == i)
                    {
                        chosenEvent = item.Key;
                    }

                    i++;
                }
                if (option == "izbrisati")
                { 
                    dictionary.Remove(chosenEvent);
                    i = 0;
                    if (dictionary.Count == 0)
                    {
                        Console.WriteLine("Popis event-ova je prazan");
                    }
                    else
                    {
                        Console.WriteLine("Ovo ja vaš trenutni popis event-ova:");
                        foreach (var item in dictionary)
                        {
                            Console.WriteLine(" " + i + " - " + item.Key.Name + " (Početak event-a: " + item.Key.StartTime + " , kraj event-a: " + item.Key.EndTime + ") ");
                            i++;
                        }
                    }
                }
                else
                {
                    //by default only left option is editing an event
                    var b = -1;
                    var name = "";


                    while (b != 0)
                    {
                        Console.WriteLine("Unesite ime  event-a: ");
                        name = Console.ReadLine();
                        if (name.Length == 0)
                        {
                            Console.WriteLine("Unijeli ste prazan string. Trebate ponoviti unos.");

                        }
                        else
                            b = 0;
                    }


                    b = -1;
                    int typeOfEvent = -1;
                    while (b != 0)
                    {
                        Console.WriteLine("Odaberite tip novog event-a unosom odgovarajućeg broja:");
                        Console.WriteLine(
                        " 0 - Coffee\n" +
                        " 1 - Lecture\n" +
                        " 2 - Concert\n" +
                        " 3 - StudySession\n"
                        );
                        var input = Console.ReadLine();
                        var number = -1;
                        bool isNumber = Int32.TryParse(input, out number);

                        if (isNumber)
                        {
                            if (number >= 0 && number <= 4)
                            {
                                typeOfEvent = number;
                                b = 0;
                            }
                            else
                            {
                                Console.WriteLine("Unijeli ste broj koji nije ponuđen. Ponovite unos.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Niste unijeli broj. Ponovite unos");
                        }
                    }


                    b = -1; 
                    DateTime startTime = new DateTime(2017, 1, 18);
                    DateTime endTime = new DateTime(2017, 1, 18);
                    while (b != 0)
                    {
                        Console.WriteLine("Unesite početak trajanja event-a u formatu dd/mm/yyyy hh:mm");
                        startTime = setDateTime();
                        Console.WriteLine("Unesite završetak trajanja event-a u formatu dd/mm/yyyy hh:mm");
                        endTime = setDateTime();
                        int result = DateTime.Compare(startTime, endTime);
                        if (result > 0)
                        {
                            Console.WriteLine("Kraj event-a ne može biti prije početka. Ponovite unos.");
                        }
                        else if (result == 0)
                        {
                            Console.WriteLine("Vrijeme početka i kraja event-a ne mogu biti isti. Ponovite unos.");
                        }
                        else
                        {
                            b = 0;
                        }
                    }
                    chosenEvent.Name = name;
                    chosenEvent.TypeOfEvent = (EventType)typeOfEvent;
                    chosenEvent.StartTime = startTime;
                    chosenEvent.EndTime = endTime;
                    Console.WriteLine("Ovo je Vaš trenutni popis event-ova:");
                    foreach (var item in dictionary)
                    {
                        Console.WriteLine(" " + i + " - " + item.Key.Name + " (" + item.Key.TypeOfEvent + ") (Početak event-a: " + item.Key.StartTime + " , kraj event-a: " + item.Key.EndTime + ") ");
                        i++;
                    }
                }
            }
        }


        static void AddPersonToEvent(Dictionary<Event, List<Person>> dictionary)
        {
            Console.WriteLine("Odabrali ste: dodavanje osobe event-u");
            if (dictionary.Count != 0)
            {
                var newPerson = AddPerson(dictionary);
                Console.WriteLine("Odaberite kojem event-u želite dodati osobu odabirom odgovarajućeg broja event-a:");
                var i = 0;
                int eventIndex = -1;
                foreach (var item in dictionary)
                {
                    Console.WriteLine(i + " - " + item.Key.Name);
                    i++;
                }


                var a = -1;
                while (a != 0)
                {
                    var input = Console.ReadLine();
                    var number = -1;
                    bool isNumber = Int32.TryParse(input, out number);
                    if (isNumber)
                    {
                        if (number >= 0 && number <= dictionary.Count)
                        {
                            eventIndex = number;
                            a = 0;
                        }
                        else
                        {
                            Console.WriteLine("Unijeli ste broj koji nije ponuđen. Ponovite unos.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Niste unijeli broj. Ponovite unos");
                    }
                }
                i = 0;
                DateTime date1 = new DateTime(2015, 12, 24);
                DateTime date2 = new DateTime(2015, 12, 25);
                var chosenEvent = new Event("test", 0, date1, date2);
                foreach (var item in dictionary)
                {
                    if (eventIndex == i)
                    {
                        chosenEvent = item.Key;
                    }
                    i++;
                }
                dictionary[chosenEvent].Add(newPerson);
                Console.WriteLine("Event "+chosenEvent.Name+" trenutno ima sudionike:");
                foreach (var person in dictionary[chosenEvent])
                    {
                    Console.WriteLine(person.FirstName+" "+person.LastName+" ( OIB : "+person.OIB+" )");
                    }
            }
            else
            {
                Console.WriteLine("Popis event-ova je prazan.");
            }
        }


        static Person AddPerson(Dictionary<Event, List<Person>> dictionary)
        {
            var firstName = "";
            var lastName = "";
            var oib = "";
            var phoneNumber = -1;
            var a = -1;
            var newPerson = new Person("", "", "", 091);


            a = -1;
            while (a != 0)
            {
                Console.WriteLine("Unesite ime osobe: ");
                firstName = Console.ReadLine();
                if (isValidStringInput(firstName))
                {
                    a = 0;
                }
            }


            a = -1;
            while (a != 0)
            {
                Console.WriteLine("Unesite prezime osobe: ");
                lastName = Console.ReadLine();
                if (isValidStringInput(lastName))
                {
                    a = 0;
                }
            }


            a = -1;
            while (a != 0)
            {
                Console.WriteLine("Unesite broj mobitela osobe: ");
                var input = Console.ReadLine();
                var number = -1;
                bool isNumber = Int32.TryParse(input, out number);

                if (isNumber)
                {
                    phoneNumber = number;
                    a = 0;
                }
                else
                {
                    Console.WriteLine("Niste unijeli broj. Ponovite unos.");
                }
            }


            a = -1;
            while (a != 0)
            {
                var doesOIBExist = false;
                Console.WriteLine("Unesite broj OIB osobe: ");
                oib = Console.ReadLine();
                if (isValidStringInput(oib))
                {
                    foreach (var existingEvent in dictionary.Keys)
                    {
                        foreach (var person in dictionary[existingEvent])
                        {
                            if (person.OIB == oib)
                            {
                                doesOIBExist = true;
                            }
                        }
                    }

                    if (doesOIBExist)
                    {
                        Console.WriteLine("Osoba s unesenim OIB-om već postoji. Unesite ispravan OIB.");
                    }
                    else
                    {
                        a = 0;
                    }
                }
            }
            newPerson.FirstName = firstName;
            newPerson.LastName = lastName;
            newPerson.PhoneNumber = phoneNumber;
            newPerson.OIB = oib;
            return newPerson;
        }


        static void RemovePersonFromEvent(Dictionary<Event, List<Person>> dictionary)
        {

        }

    }
}

/*
 * dodatni features:
 -funkcija za provjeru unosa broja
-popravi za dodavanj eventa ne smiju imat preklapanje u vrimenu / overlaping
- funkc kod unosa i edita eventa
 */