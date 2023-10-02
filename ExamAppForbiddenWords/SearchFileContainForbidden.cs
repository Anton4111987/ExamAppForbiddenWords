using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Permissions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace ExamAppForbiddenWords
{
    public class SearchFileContainForbidden
    {
        Task[] tasks;
        public readonly string fileExtension = ".txt"; // какие файлы будем искать
        public List<string>[] filesArray;  
        public List<string> files; // массив найденных файлов
        public string? exeptions; // для исключений

        public int countAllFiles; // общее количество файлов
        public int countFilesContainForbidden; // количество файлов содержащих запрещенные слова
        public int countCopyFileContainsForbidden; // количество копированных файлов содержащих запрещенные слова

        public List<string> filesContainForbiddenWords;
       
        string? pathSaveFile; // путь для сохранения измененных файлов
        List<string>? forbiddenWords; // список с запрещенными словами

        PauseDelegate pauseDelegate; // делегат 
        bool stopWork;
        readonly int pause = 100;
        ProgressBar progressBar;
       
        

        public SearchFileContainForbidden(PauseDelegate pauseDelegate)
        {
            files = new List<string>();
            // SearchFilesOnDrives(); // поиск всех файлов на всех дисках
            countFilesContainForbidden = 0;
            countAllFiles = 0;
            filesContainForbiddenWords = new List<string>();
            countCopyFileContainsForbidden = 0;
            stopWork = false;
            this.pauseDelegate = pauseDelegate;
            this.forbiddenWords = new List<string>();
        }
       
        public void SearchFilesOnDrives(ProgressBar progressBar, Label label) // поиск файлов на всех дисках
        {
           // this.progressBar = progressBar;
           
           
            countAllFiles = 0;
            DriveInfo[] drives = DriveInfo.GetDrives(); // Получаем все доступные диски
            tasks = new Task[drives.Length]; // иницилизации длины массива тасков
            progressBar.Invoke((MethodInvoker)(() => progressBar.Minimum = 0)); // вызов прогрессбара из другого потока через делегат action 
            progressBar.Invoke((MethodInvoker)(() => progressBar.Maximum = drives.Length));// вызов прогрессбара из другого потока через делегат action 
            filesArray = new List<string>[drives.Length];
            int countDrives = 0;
            while (!stopWork && countDrives<drives.Length)
            {                       
                for(int i =0; i < drives.Length; i++)
                {                   
                    label.Text =$"Поиск всех файлов {fileExtension} на диске {drives[i]}";
                    if (!pauseDelegate())
                    {
                        if (drives[i].DriveType == DriveType.Fixed) // Проверяем, является ли диск жестким диском
                        {                           
                            filesArray[i] = new List<string>();
                            tasks[i] =  Task.Run(() => FindTextFilesAsync(drives[i].RootDirectory, i));// ищем текстовые файлы в указанной директории                       
                            Thread.Sleep(1000);
                                                       
                        }
                    }
                    else
                    {
                        Thread.Sleep(pause);
                        break;
                    }
                   
                    countDrives++;
                    progressBar.Invoke((MethodInvoker)(() => progressBar.Value = i + 1)); // вызов прогрессбара из другого потока через делегат action
                                                                 
                }
                
                Task.WaitAll(tasks);
                
                for (int j = 0; j < drives.Length; j++) // соединение всех списков файлов полученных с каждого диска
                {                              
                    files.AddRange(filesArray[j]);                   
                }
                label.Text=$"Количество файлов = {files.Count} ";
            }
            
        }


        private async void FindTextFilesAsync(DirectoryInfo directory, int index)
        {           
            await Task.Run(() =>
            {
                try
                {
                    FileInfo[] allFiles = directory.GetFiles();  // Ищем все файлы в текущей директории
                    foreach (FileInfo file in allFiles) // Проходимся по каждому файлу
                    {
                        if (file.Extension == fileExtension) // Проверяем, является ли файл текстовым
                        {
                            filesArray[index].Add(file.FullName); // добавляем в список все файлы
                            countAllFiles++; // считаем общее количество файлов 
                        }
                    }
                    foreach (DirectoryInfo subdirectory in directory.GetDirectories())  // Рекурсивно вызываем метод для каждой поддиректории
                    {
                        FindTextFilesAsync(subdirectory, index);
                    }
                }
                catch
                {
                    return;
                }
            });

        }

        public void SearchFilesContainsForbiffenWordsAsync(List<string> forbiddenWords, string pathSaveFile, ProgressBar progressBar, Label labelContainsForbidden) // добавление файлов содержащих запрещенные слова в отдельный список
        {            
            countFilesContainForbidden = 0;            
            this.pathSaveFile = pathSaveFile;
           
            this.forbiddenWords = forbiddenWords;
            this.progressBar = progressBar;
            progressBar.Invoke((MethodInvoker)(() => progressBar.Minimum = 0)); // вызов прогрессбара из другого потока через делегат action 
            progressBar.Invoke((MethodInvoker)(() => progressBar.Maximum = files.Count));// вызов прогрессбара из другого потока через делегат action 

            for (int x = 0; x < files.Count; x++)
            {
                labelContainsForbidden.Text = $"Поиск файлов содержащих запрещенные слова\n{files[x]}";
                bool found = false;
                try
                {
                    string textFile = File.ReadAllText(files[x]);
                    string fileContents = textFile; // копирование содержимого файла   
                    for (int i = 0; i < forbiddenWords.Count; i++) //foreach (string word in forbiddenWords) // цикл для запрещенных слов чтобы проверить имеются ли в файлах слова
                    {
                        if (textFile.Contains(forbiddenWords[i]))
                        {
                            found = true;
                            countFilesContainForbidden++;
                            break;
                        }
                    }
                }
                catch
                { continue; }
                if (found)
                    filesContainForbiddenWords.Add(files[x]);
                progressBar.Invoke((MethodInvoker)(() => progressBar.Value = x + 1)); // вызов прогрессбара из другого потока через делегат action 
            }
            labelContainsForbidden.Text = $"Закончился метод поиска файлов .txt, содержащих запрещенные слова\n" +
                $"Файлов с запрещенными словами - {filesContainForbiddenWords.Count}";
            
        }

        public void ChangeFileAndCopyAsync(ProgressBar progressBar, Label label)
        {          
                StringBuilder reportFile = new();// файл отчет
                string fileContents;
                countCopyFileContainsForbidden = 0;
                progressBar.Invoke((MethodInvoker)(() => progressBar.Minimum = 0)); // вызов прогрессбара из другого потока через делегат action 
                progressBar.Invoke((MethodInvoker)(() => progressBar.Maximum = filesContainForbiddenWords.Count));// вызов прогрессбара из другого потока через делегат action 
                for (int x = 0; x < filesContainForbiddenWords.Count; x++) // цикл по файлам содержащим запрещенные слова
                {
                    label.Text = $"Редактирование и копирование файлов\n{filesContainForbiddenWords[x]}"; // передача названия копируемого файла в label
                    int countWords = 0;
                    int count = 0;// счетчик для того чтобы сохранение файла происходило толлько один раз, также количество замен
                                 
                    try
                    {
                        string textFile = File.ReadAllText(filesContainForbiddenWords[x]);
                        fileContents = textFile; // копирование содержимого файла                                                

                        for (int i = 0; i < forbiddenWords?.Count; i++) //foreach (string word in forbiddenWords) // цикл для запрещенных слов чтобы проверить имеются ли в файлах слова
                        {
                            Regex regex = new Regex(forbiddenWords[i]); // класс для подсчета замененных слов
                            MatchCollection matchs = regex.Matches(fileContents); // подсчет замененных слов
                            fileContents = fileContents.Replace(forbiddenWords[i], "*******"); // замена в файле всех запрещенных слов word на *******
                            countWords += matchs.Count;
                            string fileName = Path.GetFileName(filesContainForbiddenWords[x]);// получение имени файла с расширением
                            if (count == 0)
                            {
                                FileInfo file = new FileInfo(filesContainForbiddenWords[x]);
                                reportFile.Append("Название файла: " + fileName + "\n" + "Путь к файлу: " +
                                Path.GetDirectoryName(filesContainForbiddenWords[x]) + "\n" + "Размер файла(байт): " + file.Length + "\n");

                            }
                            reportFile.Append($"количество замененных слов ({forbiddenWords[i]}) в файле ({fileName}) = " + matchs.Count + "\n");

                            File.WriteAllText(pathSaveFile + "\\" + Path.GetFileName(filesContainForbiddenWords[x]), fileContents, Encoding.Default);// сохранение измененного файла
                            count++;

                        }

                    }
                    catch { continue; }

                    if (countWords != 0) // если количество замененных слов не равно нулю то добавляем в файл-отчет
                        reportFile.Append("Общее количество замененных слов - " + countWords + "\n\n");

                    countCopyFileContainsForbidden++;
                    progressBar.Invoke((MethodInvoker)(() => progressBar.Value = x + 1)); // вызов прогрессбара из другого потока через делегат action 
                }
                File.WriteAllText(pathSaveFile + "\\ФАЙЛ - ОТЧЕТ.txt ", reportFile.ToString(), Encoding.Default); // сохранение файла-отчета

            label.Text = $"Закончился метод редактирования и копирования файлов, всего скопировано файлов {filesContainForbiddenWords.Count} ";
        }


    }
}
