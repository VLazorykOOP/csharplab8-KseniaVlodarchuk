using System.Text.RegularExpressions;

class Program
{
    static void ProcessText(string inputText)
    {
        Regex regex = new Regex(@"\[(.*?)\]");

        string replacedText = regex.Replace(inputText, match => {
            return new string('X', match.Length);
        });

        Console.WriteLine("Замінений текст:");
        Console.WriteLine(replacedText);

        string outputPath = "output5.txt";
        File.WriteAllText(outputPath, replacedText);

        Console.WriteLine($"Створено файл: {outputPath}");
    }
    public static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("Номер завдання:");
            int n = int.Parse(Console.ReadLine()); 
            switch (n)
            {
                case 1:
                    string inputFileName = "input.txt";
                    string outputFileName = "output.txt";
                    string editedOutputFileName = "edited_output.txt";

                    string text = File.ReadAllText(inputFileName);

                    // Пошук адрес web-сайтів домена com
                    MatchCollection matches = Regex.Matches(text, @"\b\w+\.com\b");

                    // Запис підтекстів заданого формату у новий файл та підрахунок їх кількості
                    int count = matches.Count;
                    using (StreamWriter writer = new StreamWriter(outputFileName))
                    {
                        foreach (Match match in matches)
                        {
                            writer.WriteLine(match.Value);
                        }
                    }

                    // Вилучення та заміна деяких підтекстів за вказаними параметрами користувача
                    // Наприклад, заміна 'com' на 'org'
                    string editedText = Regex.Replace(text, @"\bcom\b", "org");

                    // Запис відредагованого тексту у новий файл
                    File.WriteAllText(editedOutputFileName, editedText);

                    Console.WriteLine($"Found and saved {count} URLs.");
                    break;
                case 2:
                    string inputFileName1 = "input2.txt";
                    string outputFileName1 = "output2.txt";

                    string textt = File.ReadAllText(inputFileName1);

                    // Видалення українських слів, які починаються на голосну літеру
                    string editedText1 = Regex.Replace(textt, @"\b[АаЕеЄєИиІіЇїОоУуЮюЯя]\w*\b", "");

                    // Запис результату у новий файл
                    File.WriteAllText(outputFileName1, editedText1);

                    Console.WriteLine("Ukrainian words starting with a vowel removed and saved to output2.txt");
                    break;
                case 3:
                    // Читання текстів з файлів
                    string text1 = File.ReadAllText("text1.txt");
                    string text2 = File.ReadAllText("text2.txt");

                    // Розділення текстів на слова
                    string[] words1 = text1.Split(new char[] { ' ', ',', '.', '!', '?', ':', ';' }, StringSplitOptions.RemoveEmptyEntries);
                    string[] words2 = text2.Split(new char[] { ' ', ',', '.', '!', '?', ':', ';' }, StringSplitOptions.RemoveEmptyEntries);

                    // Вибір спільних слів
                    string[] commonWords = words1.Intersect(words2).ToArray();

                    // Запис спільних слів у новий файл
                    File.WriteAllText("output3.txt", string.Join(" ", commonWords));

                    Console.WriteLine("Common words from text1 and text2 saved to output3.txt");
                    break;
                case 4:
                    string fileName = "numbers.bin";

                    // Створення файлу та запис степенів числа 3
                    using (BinaryWriter writer = new BinaryWriter(File.Open(fileName, FileMode.Create)))
                    {
                        for (int i = 0; i < 10; i++) // Перших 10 степенів числа 3
                        {
                            int powerOfThree = (int)Math.Pow(3, i);
                            writer.Write(powerOfThree);
                        }
                    }

                    // Читання файлу та виведення на екран компонентів з парним порядковим номером
                    using (BinaryReader reader = new BinaryReader(File.Open(fileName, FileMode.Open)))
                    {
                        int count1 = 0;
                        while (reader.BaseStream.Position < reader.BaseStream.Length)
                        {
                            int number = reader.ReadInt32();
                            if (count1 % 2 == 0)
                            {
                                Console.WriteLine($"Component {count1}: {number}");
                            }
                            count1++;
                        }
                    }
                    break;
                case 5:
                    try
                    {
                        string inputText5 = "Це [текст] з кутовими дужками.";

                        ProcessText(inputText5);
                        // Створення папок <прізвище_студента>1 і <прізвище_студента>2
                        string directoryPath1 = @"D:\temp\прізвище_студента1";
                        string directoryPath2 = @"D:\temp\прізвище_студента2";
                        Directory.CreateDirectory(directoryPath1);
                        Directory.CreateDirectory(directoryPath2);

                        // Створення файлу t1.txt з вказаним текстом
                        string filePath1 = Path.Combine(directoryPath1, "t1.txt");
                        string t1 = "<Шевченко Степан Іванович, 2001> року народження, місце проживання <м. Суми>";
                        File.WriteAllText(filePath1, t1);

                        // Створення файлу t2.txt з вказаним текстом
                        string filePath2 = Path.Combine(directoryPath2, "t2.txt");
                        string t2 = "<Комар Сергій Федорович, 2000 > року народження, місце проживання <м. Київ>";
                        File.WriteAllText(filePath2, t2);

                        // Зчитування тексту з файлів t1.txt і t2.txt
                        string textFromFile1 = File.ReadAllText(filePath1);
                        string textFromFile2 = File.ReadAllText(filePath2);

                        // Запис тексту з файлів t1.txt і t2.txt в файл t3.txt у папці <прізвище_студента>2
                        string filePath3 = Path.Combine(directoryPath2, "t3.txt");
                        File.WriteAllText(filePath3, textFromFile1 + Environment.NewLine + textFromFile2);

                        // Переміщення файлу t2.txt у папку <прізвище_студента>2
                        string newPathT2 = Path.Combine(directoryPath2, "t2.txt");
                        File.Move(filePath2, newPathT2);
                        string[] filesInAll = Directory.GetFiles(directoryPath2);
                        foreach (string file in filesInAll)
                        {
                            FileInfo fileInfo = new FileInfo(file);
                            Console.WriteLine($"Ім'я файлу: {fileInfo.Name}");
                            Console.WriteLine($"Шлях: {fileInfo.FullName}");
                            Console.WriteLine($"Дата створення: {fileInfo.CreationTime}");
                            Console.WriteLine($"Розмір: {fileInfo.Length} байт");
                            Console.WriteLine();
                        }
                        // Копіювання файлу t1.txt в папку <прізвище_студента>2
                        string copyPathT1 = Path.Combine(directoryPath2, "t1_copy.txt");
                        File.Copy(filePath1, copyPathT1);

                        
                        // Перейменування папки K2 в ALL
                        string oldDirectoryName = directoryPath2;
                        string newDirectoryName = @"D:\temp\ALL";
                        Directory.Move(oldDirectoryName, newDirectoryName);

                        // Видалення всіх файлів у директорії
                        string[] files = Directory.GetFiles(directoryPath1);
                        foreach (string file in files)
                        {
                            File.Delete(file);
                        }
                        // Вилучення папки <прізвище_студента>1
                        Directory.Delete(directoryPath1);

                        // Виведення повної інформації про файли папки All
                        string[] filesInAlll = Directory.GetFiles(newDirectoryName);
                        foreach (string file in filesInAlll)
                        {
                            FileInfo fileInfo = new FileInfo(file);
                            Console.WriteLine($"Ім'я файлу: {fileInfo.Name}");
                            Console.WriteLine($"Шлях: {fileInfo.FullName}");
                            Console.WriteLine($"Дата створення: {fileInfo.CreationTime}");
                            Console.WriteLine($"Розмір: {fileInfo.Length} байт");
                            Console.WriteLine();
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Сталася помилка: {ex.Message}");
                    }
                    Console.ReadLine();
                    break;
                default:
                    Console.WriteLine("No");
                    break;
            }
        }
    }
}