using System;
using System.Text;
using System.Threading;

Console.OutputEncoding = Encoding.UTF8;

// Верхня межа Random.Next не включається: отримуємо числа від 1 до 12.
int[] numbers = new int[20];
Random random = new Random();
for (int i = 0; i < numbers.Length; i++)
    numbers[i] = random.Next(1, 13);

Console.WriteLine("Масив: " + string.Join(", ", numbers));

Thread[] threads = new Thread[numbers.Length];
for (int i = 0; i < numbers.Length; i++)
{
    // Окрема локальна змінна для кожного потоку, без захоплення індексу i.
    int num = numbers[i];
    threads[i] = new Thread(() =>
    {
        long numF = 1;
        for (int factor = 2; factor <= num; factor++)
            numF *= factor;

        Console.WriteLine($"факторіал числа: {num}! = {numF}");
    });
}

// Спочатку запускаємо всі потоки, щоб вони могли працювати паралельно.
foreach (Thread thread in threads)
    thread.Start();

// Join очікує і завершення розрахунку, і виведення результату кожного потоку.
foreach (Thread thread in threads)
    thread.Join();

Console.WriteLine("Розрахунок закінчено.");
