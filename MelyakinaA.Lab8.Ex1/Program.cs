using System;
using System.Text;
using System.IO;
using System.Linq;

string path = @"Text.txt";
string pathDir = @"C:\Users\melya\source\repos\Lab8\Melyakina.Lab8.Ex1\obj\Debug\net6.0";
DirectoryInfo dirInfo = new DirectoryInfo(pathDir);
FileInfo info = new FileInfo(path);
Console.WriteLine(info.FullName); //абсолютный путь файла
Console.WriteLine(info.Name); // относительный путь файла
Console.WriteLine(info.CreationTime); // дата создания файла
Console.WriteLine($"{info.Length} байт"); // размер файла в байтах
                                          //Console.WriteLine(info.);//есть ли кириллица,латиница,цифры,кол-во строк и слов
Console.WriteLine();
//Console.WriteLine(info.);


Console.WriteLine(Path.GetFileNameWithoutExtension(path));
Console.WriteLine(Environment.CurrentDirectory);

Console.WriteLine();

if (File.Exists(path))
{
    string text = File.ReadAllText(path);


    int i = 0;
    var words = text.Split(' ', StringSplitOptions.RemoveEmptyEntries);
    for (i = 0; i < words.Length; i++)
    {
        i++;//количество слов



    }

    Console.WriteLine($"Всего слов:{i.ToString()}");

    bool Flag1 = false;
    bool Flag2 = false;
    bool Flag3 = false;

    for (int k = 0; k < text.Length; k++)
    {

        if (text[k] >= 'a' && text[k] <= 'z' || text[k] >= 'A' && text[k] <= 'Z')
        {
            Flag1 = true;


        }
        if (text[k] >= 'а' && text[k] <= 'я' || text[k] >= 'А' && text[k] <= 'Я')
        {
            Flag2 = true;


        }
        if (Char.IsDigit(text[k]))
        {
            Flag3 = true;

        }


    }
    if (Flag3)
    {
        Console.WriteLine("В тексте есть цифры");
    }
    if (Flag2)
    {
        Console.WriteLine("В тексте есть кириллица");
    }
    if (Flag1)
    {
        Console.WriteLine("В тексте есть латиница");
    }

    var stat = words.Distinct()
            .ToDictionary(word => word, word => words.Count(x => x == word))
            .OrderByDescending(x => x.Value);
    Console.WriteLine();

    StringBuilder sb = new StringBuilder();
    foreach (var item in stat)
    {
        Console.WriteLine(item);
        sb.AppendLine($"{item.Key} {item.Value}");

    }

    StreamReader sr = new StreamReader("Text.txt");

    int j = 0;
    while ((text = sr.ReadLine()) != null) //читаем по одной линии(строке) пока не вычитаем все из потока (пока не достигнем конца файла)
    {
        j++;
    }
    Console.WriteLine("Всего строк:" + j.ToString());//количество строк

    //СОХРАНЕНИЕ РЕЗУЛЬТАТОВ
    sb.AppendLine($"{info.FullName}\n{info.Name}\n{info.CreationTime}\n{info.Length} байт\nВсего строк: {j.ToString()}\nВсего слов: {i.ToString()}");
    //string result = $"{info.FullName}\n{info.Name}\n{info.CreationTime}\n{info.Length} байт\nВсего строк: {j.ToString()}\nВсего слов: {i.ToString()}";
    //File.WriteAllText("result1.txt", result);
    //Console.WriteLine(result);
    File.WriteAllText("result1.txt", sb.ToString());

}
else
{
    Console.WriteLine("Такого файла не существует!");
}
//       Абсолютный путь.
//    // string path = @"D:\Repo\Education\Files\Files\text1.txt";
//    // Относительный путь.
//    // string path = @"..\..\..\text1.txt";