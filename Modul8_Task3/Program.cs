/*
 * ЗАДАНИЕ 3
Доработайте программу из задания 1, используя ваш метод из задания 2.

При запуске программа должна:

Показать, сколько весит папка до очистки. Использовать метод из задания 2. 
Выполнить очистку.
Показать сколько файлов удалено и сколько места освобождено.
Показать, сколько папка весит после очистки.
 */

DirectoryInfo directory = new DirectoryInfo(@"C:\Users\мвидео\Desktop\Kawaiki");
try
{
    if (directory.Exists)
    {
        long size = 0;
        long fileCount = 0;
        long totalsize = EvaluateSpace(size, directory);
        Console.WriteLine($"Исходный размер папки {directory}: {totalsize} байт");
        long deletedfiles = DeleteAndCount(fileCount, directory);
        Console.WriteLine($"Удалено файлов: {deletedfiles}");
        long newtotalsize = EvaluateSpace(size, directory);
        Console.WriteLine($"Освобождено: {totalsize - newtotalsize}");
        Console.WriteLine($"Данный размер папки: {newtotalsize}");
    }
    else
    {
        Console.WriteLine("Такой папки не существует.");
    }
}
catch (Exception ex)
{
    if(ex is UnauthorizedAccessException)
    {
        Console.WriteLine("Отказ в правах доступа.");
    }
}

 static long EvaluateSpace(long size, DirectoryInfo dirSpace)
 {
     foreach (FileInfo file in dirSpace.GetFiles())
     {
         size += file.Length;
     }
     foreach (DirectoryInfo folder in dirSpace.GetDirectories())
     {
         size += EvaluateSpace(0, folder);
     }
     return size;
 }

static long DeleteAndCount(long fileCount, DirectoryInfo dirSpace)
{
    DateTime thirty = DateTime.Now - TimeSpan.FromMinutes(30);
    foreach (FileInfo file in dirSpace.GetFiles())
    {
        if(file.LastAccessTime < thirty)
        {
            fileCount++;
            file.Delete();
        }
    }
    foreach(DirectoryInfo folder in dirSpace.GetDirectories())
    {
        DeleteAndCount(0, folder);
    }
    return fileCount;
}