using BenchmarkDotNet.Attributes;

[MemoryDiagnoser]
public partial class BenchmarkLinQPerformanceBase
{
    int[] array = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
    int[] unsortedArray = new int[] { 8, 2, 1, 6, 5, 10, 4, 9, 3, 7 };
    Person[] persons = new Person[]
    {
        new Person { Id = 1, Name = "Delores" },
        new Person { Id = 2, Name = "Sharlay" },
        new Person { Id = 3, Name = "Devashish " },
        new Person { Id = 4, Name = "Niramisa " },
        new Person { Id = 5, Name = "Vaughan " },
        new Person { Id = 6, Name = "Minx" },
        new Person { Id = 7, Name = "Drew" },
        new Person { Id = 8, Name = "Walt" },
        new Person { Id = 9, Name = "Stieg" },
        new Person { Id = 10, Name = "Leon" }
    };

    Person[] unsortedPersons = new Person[]
   {
        new Person { Id = 8, Name = "Walt" },
        new Person { Id = 2, Name = "Sharlay" },
        new Person { Id = 1, Name = "Delores" },
        new Person { Id = 6, Name = "Minx" },
        new Person { Id = 5, Name = "Vaughan " },
        new Person { Id = 10, Name = "Leon" },
        new Person { Id = 4, Name = "Niramisa " },
        new Person { Id = 9, Name = "Stieg" },
        new Person { Id = 3, Name = "Devashish " },
        new Person { Id = 7, Name = "Drew" }
   };

    [Benchmark]
    public void GetElementFromArrayWithLinQ()
    {
        var result = array.Where(x => x == 5)
                          .FirstOrDefault();
    }

    [Benchmark]
    public void GetElementFromArrayWithLinqFirst()
    {
        var result = array.Where(x => x == 5)
                          .First();
    }

    [Benchmark]
    public void GetElementFromArray()
    {
        int result = 0;
        int i = 0;

        while (i < array.Length && array[i] != 5)
        {
            i++;
        }

        if (array[i] == 5)
        {
            result = array[i];
        }
    }


    [Benchmark]
    public void GetElementFromUnsortedArrayWithLinQ()
    {
        var result = unsortedArray.OrderBy(x => x)
                          .Where(x => x == 5)
                          .FirstOrDefault();
    }

    [Benchmark]
    public void GetElementFromUnsortedArray()
    {
        int result = 0;
        int i = 0;

        Array.Sort(unsortedArray);

        while (i < unsortedArray.Length && unsortedArray[i] != 5)
        {
            i++;
        }

        if (unsortedArray[i] == 5)
        {
            result = unsortedArray[i];
        }
    }

    [Benchmark]
    public void GetPersonFromArrayWithLinQ()
    {
        var result = persons.Where(x => x.Id == 5)
                            .Select(x => x.Name)
                            .FirstOrDefault();
    }

    [Benchmark]
    public void GetPersonFromArray()
    {
        string result = "";
        int i = 0;

        while (i < persons.Length && persons[i].Id != 5)
        {
            i++;
        }

        if (persons[i].Id == 5)
        {
            result = persons[i].Name;
        }
    }

    [Benchmark]
    public void GetPersonFromUnsortedArrayWithLinQ()
    {
        var result = unsortedPersons.OrderBy(x => x.Id)
                                    .Where(x => x.Id == 5)
                                    .Select(x => x.Name)
                                    .FirstOrDefault();
    }

    [Benchmark]
    public void GetPersonFromUnsortedArray()
    {
        string result = "";
        int i = 0;

        Array.Sort(unsortedPersons, (x, y) =>
        {
            return x.Id > y.Id ? 1 : -1;
        });

        while (i < unsortedPersons.Length && unsortedPersons[i].Id != 5)
        {
            i++;
        }

        if (unsortedPersons[i].Id == 5)
        {
            result = unsortedPersons[i].Name;
        }
    }
}