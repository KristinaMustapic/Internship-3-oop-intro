using System;
using System.Collections.Generic;


namespace internship_3_oop_intro
{
    class Program
    { 
        static void Main(string[] args)
        {  
            var eventsInfo = new Dictionary<Event, List<Person>>()
            {
                { new Event("Coffee time", 0, new DateTime(2015, 12, 24), new DateTime(2015, 12, 27)), new List<Person>(){new Person("Anna", "Jo", 1234, 098), new Person("John", "Jo", 5678, 0981)} },
                { new Event("Music festival", 0, new DateTime(2019, 07, 14), new DateTime(2019, 07, 17)), new List<Person>(){} }
            };

            var a = -1;
            while (a != 0)
            {
                Menu();
                var input = Console.ReadLine();
                var choice = -1;
                isValidNumberInput(input, ref choice);
                var option = (Option)choice;

                switch (option)
                {
                    case Option.AddEvent:
                        AddEvent(eventsInfo);
                        break;

                    case Option.DeleteEvent:
                        DeleteOrEditEvent(eventsInfo, "izbrisati");
                        break;

                    case Option.EditEvent:
                        DeleteOrEditEvent(eventsInfo, "editirati");
                        break;

                    case Option.AddPersonToEvent:
                        AddPersonToEvent(eventsInfo);
                        break;

                    case Option.RemovePersonFromEvent:
                        RemovePersonFromEvent(eventsInfo);
                        break;

                    case Option.PrintEventInfo:
                        PrintEventInfo(eventsInfo);
                        break;

                    case Option.StopApp:
                        StopApp(ref a); 
                        break;

                    default:
                        Console.WriteLine("Krivi unos. Ponovite unos odabira.");
                        break;
                }
            }
        }


        enum Option
        {
            AddEvent = 1,
            DeleteEvent = 2,
            EditEvent = 3,
            AddPersonToEvent = 4,
            RemovePersonFromEvent = 5,
            PrintEventInfo = 6,
            StopApp = 7
        }


        static void Menu()
        {
            Console.WriteLine(
                
                "\nOdaberite akciju:\n"
                + " 1 - Dodavanje event-a\n" 
                + " 2 - Brisanje event-a\n"
                + " 3 - Editiranje event-a\n"
                + " 4 - Dodavanje osobe na event\n"
                + " 5 - Uklanjanje osobe s event-a\n"
                + " 6 - Ispis detalja event-a\n"
                + " 7 - Prekid rada\n"

                );
        }

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
                    Console.WriteLine("Niste unijeli broj. Ponovite unos.");
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
                    DateTime existingStartTime = new DateTime();
                    DateTime existingEndTime = new DateTime();
                    foreach (var existingEvent in dictionary.Keys)
                    {
                        existingStartTime = existingEvent.StartTime;
                        existingEndTime = existingEvent.EndTime;
                        if (IsTimeIntervalFree(startTime, endTime, existingStartTime, existingEndTime))
                        {
                            a = 0;
                        }
                        else
                        {
                            Console.WriteLine("Uneseno vrijeme se preklapa s vremenom već postojećeg event-a. Unesite novo vrijeme za održavanje event-a.");
                        }
                    }
                }
            }
                   
                    var newEvent = new Event(name, typeOfEvent, startTime, endTime);
                    var eventAttendes = new List<Person>();
                    dictionary.Add(newEvent, eventAttendes);
                    var i = 0;
                    Console.WriteLine("Ovo je Vaš trenutni popis event-ova:");
                    foreach (var item in dictionary)
                    {
                        Console.WriteLine(" " + i + " - " + item.Key.Name + " (" + item.Key.TypeOfEvent + ") (Početak event-a: " + item.Key.StartTime + " , kraj event-a: " + item.Key.EndTime + ") ");
                        i++;
                    }
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
                    Console.WriteLine("Nemate unesenih event-ova.");
                    a = 0;
                }
                else
                {
                    Console.WriteLine("Ovo je Vaš trenutni popis event-ova:");
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
                        Console.WriteLine("Niste unijeli broj. Ponovite unos.");
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
                        Console.WriteLine("Nemate unesenih event-ova.");
                    }
                    else
                    {
                        Console.WriteLine("Ovo ja Vaš trenutni popis event-ova:");
                        if(dictionary.Count != 0)
                        {
                            foreach (var item in dictionary)
                            {
                                Console.WriteLine(" " + i + " - " + item.Key.Name + " (Početak event-a: " + item.Key.StartTime + " , kraj event-a: " + item.Key.EndTime + ") ");
                                i++;
                            }
                        }
                        else
                        {
                            Console.WriteLine("Nemate unesenih event-ova.");
                        }
                    }
                }
                else
                {
                    //by default only left option is editing an event
                    a = -1;
                    var name = "";


                    while (a != 0)
                    {
                        Console.WriteLine("Unesite ime event-a: ");
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
                            Console.WriteLine("Niste unijeli broj. Ponovite unos.");
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
                            DateTime existingStartTime = new DateTime();
                            DateTime existingEndTime = new DateTime();
                            foreach (var existingEvent in dictionary.Keys)
                            {
                                existingStartTime = existingEvent.StartTime;
                                existingEndTime = existingEvent.EndTime;
                                if (IsTimeIntervalFree(startTime, endTime, existingStartTime, existingEndTime))
                                {
                                    a = 0;
                                }
                                else
                                {
                                    Console.WriteLine("Uneseno vrijeme se preklapa s vremenom već postojećeg event-a. Unesite novo vrijeme za održavanje event-a.");
                                }
                            }
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
                        if (number >= 0 && number < dictionary.Count)
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
                        Console.WriteLine("Niste unijeli broj. Ponovite unos.");
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
                Console.WriteLine("Nemate unesenih event-ova.");
            }
        }


        static Person AddPerson(Dictionary<Event, List<Person>> dictionary)
        {
            var firstName = "";
            var lastName = "";
            var oib = -1;
            var phoneNumber = -1;
            var a = -1;
            var newPerson = new Person("", "", 123, 091);


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
                Console.WriteLine("Unesite ispravan OIB broj osobe: ");
                var input = Console.ReadLine();
                var number = -1;
                bool isNumber = Int32.TryParse(input, out number);

                if (isNumber)
                {
                    oib = number;
                    var doesOIBExist = false;
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
                        Console.WriteLine("Osoba s unesenim OIB-om već postoji.");
                    }
                    else
                    {
                        a = 0;
                    }
                }
                else
                {
                    Console.WriteLine("Niste unijeli broj. Ponovite unos.");
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
            DateTime date1 = new DateTime(2015, 12, 24);
            DateTime date2 = new DateTime(2015, 12, 25);
            var chosenEvent = new Event("test", 0, date1, date2);
            var chosenPerson = new Person("","",1,1);
            var a = -1;
            int oib = -1;
            Console.WriteLine("Odabrali ste: brisanje osobe u event-u.");
            if (dictionary.Count != 0)
            { 
            Console.WriteLine("Odaberite u kojem event-u želite ukloniti osobu odabirom odgovarajućeg broja event-a:");
            var i = 0;
            int eventIndex = -1;
                foreach (var item in dictionary)
                {
                    Console.WriteLine(i + " - " + item.Key.Name);
                    i++;
                }
            
            while (a != 0)
            {
                var input = Console.ReadLine();
                var number = -1;
                bool isNumber = Int32.TryParse(input, out number);
                if (isNumber)
                {
                    if (number >= 0 && number < dictionary.Count)
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
                    Console.WriteLine("Niste unijeli broj. Ponovite unos.");
                }
            }
            i = 0;
            foreach (var item in dictionary)
            {
                if (eventIndex == i)
                {
                    chosenEvent = item.Key;
                }
                i++;
            }
                if (dictionary[chosenEvent].Count != 0)
                {
                    //


                    Console.WriteLine("Event " + chosenEvent.Name + " trenutno ima sudionike:");
                    foreach (var person in dictionary[chosenEvent])
                    {
                        Console.WriteLine(person.FirstName + " " + person.LastName + " ( OIB : " + person.OIB + " )");
                    }
                    Console.WriteLine("Unesite OIB osobe koju želite ukloniti s event-a " + chosenEvent.Name);
                    a = -1;
                    while (a != 0)
                    {
                        Console.WriteLine("Unesite ispravan OIB broj osobe: ");
                        var input = Console.ReadLine();
                        var number = -1;
                        bool isNumber = Int32.TryParse(input, out number);

                        if (isNumber)
                        {
                            oib = number;
                            var doesOIBExist = false;
                            foreach (var existingEvent in dictionary.Keys)
                            {
                                foreach (var person in dictionary[existingEvent])
                                {
                                    if (person.OIB == oib)
                                    {
                                        doesOIBExist = true;
                                        chosenPerson = person;
                                    }
                                }
                            }

                            if (!doesOIBExist)
                            {
                                Console.WriteLine("Osoba s unesenim OIB-om ne postoji.");
                            }
                            else
                            {
                                a = 0;
                            }
                        }
                        else
                        {
                            Console.WriteLine("Niste unijeli broj. Ponovite unos.");
                        }
                    }
                    dictionary[chosenEvent].Remove(chosenPerson);
                }
                
                if (dictionary[chosenEvent].Count != 0)
                {
                    Console.WriteLine("Event " + chosenEvent.Name + " trenutno ima sudionike:");
                    foreach (var person in dictionary[chosenEvent])
                    {
                        Console.WriteLine(person.FirstName + " " + person.LastName + " ( OIB : " + person.OIB + " )");
                    }
                }
                else
                {
                    Console.WriteLine("Odabrani event nema unesenih sudionika.");
                }
            }
            else
            {
                Console.WriteLine("Nemate unesenih event-ova.");
            }   
        }


        static int EventInfoSubMenu()
        {
            var a = -1;
            int typeOfPrintingEventInfo = -1;
            while (a != 0)
            {
                Console.WriteLine("\nOdaberite vrstu ispisa detalja event-a unosom odgovarajućeg broja:");
                Console.WriteLine(
                "\n"+
                " 1 - Ispis detalja eventa u formatu:( name – event type – start time – end time – trajanje u satima – ispis broja ljudi na eventu )\n" +
                " 2 - Ispis svih osoba na eventu u formatu:( [Redni broj u listi]. name – last name – broj mobitela )\n" +
                " 3 - Ispis svih detalja (i opcija 1 i opcija 2)\n" +
                " 4 - Izlazak iz podmenija\n"
                );
                var input = Console.ReadLine();
                var number = -1;
                bool isNumber = Int32.TryParse(input, out number);
                if (isNumber)
                {
                    if (number >= 1 && number <= 4)
                    {
                        typeOfPrintingEventInfo = number;
                        a = 0;
                    }
                    else
                    {
                        Console.WriteLine("Unijeli ste broj koji nije ponuđen. Ponovite unos.");
                    }
                }
                else
                {
                    Console.WriteLine("Niste unijeli broj. Ponovite unos.");
                }
            }
            return typeOfPrintingEventInfo;
        }


        static void printEventInfo1(Dictionary<Event, List<Person>> dictionary)
        {
            if(dictionary.Keys.Count != 0)
            {
                Console.WriteLine("Izabrali ste: ispis detalja event-ova u 1. formatu.");
                foreach (var existingEvent in dictionary.Keys)
                {
                    var duration = (existingEvent.EndTime - existingEvent.StartTime).TotalHours;
                    Console.WriteLine(
                        "( "
                        + existingEvent.Name
                        + " - " + existingEvent.TypeOfEvent
                        + " - " + existingEvent.StartTime
                        + " - " + existingEvent.EndTime
                        + " - " + duration
                        + " - " + dictionary[existingEvent].Count
                        + " )"
                        );
                }
            }
            else
            {
                Console.WriteLine("Nemate unesenih event-ova.");
            }

        }


        static void printEventInfo2(Dictionary<Event, List<Person>> dictionary)
        {
            if (dictionary.Keys.Count != 0)
            {
                Console.WriteLine("Izabrali ste: ispis detalja event-ova u 2. formatu.");
                foreach (var existingEventKey in dictionary.Keys)
                {
                    if (dictionary[existingEventKey].Count != 0)
                    {
                        var i = 0;
                        Console.WriteLine("Event " + existingEventKey.Name);
                        foreach (var person in dictionary[existingEventKey])
                        {
                            Console.WriteLine(
                            "( "
                            + "[" + i + "] "
                            + person.FirstName
                            + " - " + person.LastName
                            + " - " + person.PhoneNumber
                            + " )"
                            );
                            i++;
                        }
                        Console.WriteLine();
                    }
                    else
                    {
                        Console.WriteLine("Za event " + existingEventKey.Name + " nemate unesenih osoba.");
                    }
                }
            }
            else
            {
                Console.WriteLine("Nemate unesenih event-ova.");
            }
        }

        static void PrintEventInfo(Dictionary<Event, List<Person>> dictionary)
        {
            Console.WriteLine("Odabrali ste: ispis svih detalja event-ova.");
            
            if(dictionary.Count != 0)
            {
                var a = -1;
                while (a != 0)
                {
                    var typeOfPrintingEventInfo = EventInfoSubMenu();

                    if (typeOfPrintingEventInfo == 1)
                        printEventInfo1(dictionary);

                    else if (typeOfPrintingEventInfo == 2)
                        printEventInfo2(dictionary);

                    else if (typeOfPrintingEventInfo == 3)
                    {
                        Console.WriteLine("Odabrali ste: ispis detalja i u 1. i 2. formatu.");
                        Console.WriteLine();
                        printEventInfo1(dictionary);
                        Console.WriteLine();
                        printEventInfo2(dictionary);
                    }
                    else
                    {
                        a = 0;
                        Console.WriteLine("Izašli ste iz podmenija.");
                    }
                }
            }

            else
            {
                Console.WriteLine("Nemate unesenih event-ova.");
            }
        }


        static void StopApp(ref int a)
        {
            Console.WriteLine("Prekinuli ste rad aplikacije.");
            a = 0;
        }


        static bool IsTimeIntervalFree(DateTime startTimeOfNewEvent, DateTime endTimeOfNewEvent, DateTime startTimeOfExistingEvent, DateTime endTimeOfExistingEvent)
        {
            if((startTimeOfNewEvent < endTimeOfExistingEvent) && (endTimeOfNewEvent > startTimeOfExistingEvent))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
     
         
        static void isValidNumberInput(string input, ref int inputNumber)
        {
            inputNumber = -1;
            var number = -1;
            bool isNumber = Int32.TryParse(input, out number);
            if (isNumber)
            {
                if (number >= 1 && number <=7 )
                {
                    inputNumber = number;
                }
                else
                {
                    Console.WriteLine("Unijeli ste broj koji nije ponuđen.");
                }
            }
            else
            {
                Console.WriteLine("Niste unijeli broj.");
            }
        }
    }
}

