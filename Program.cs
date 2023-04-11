using System.Reflection;
using System.IO;
namespace convert16
{
    internal class Program
    {
        static void Main(string[] args)
        {
            

            string inputFolderName = "input";
            var curFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string inputFolderFullPath = Path.Combine(curFolder, inputFolderName);

            string outputFolderName = "output";
            var curoutputFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string outputFolderFullPath = Path.Combine(curoutputFolder, outputFolderName);
            string namefileoutput;
            string[] files = Directory.GetFiles(inputFolderFullPath);
            string[] filesmassiv= new string[files.Length];
            
            for (int i = 0; i < files.Length; i++)
            {
                namefileoutput = Path.GetFileName(files[i]);
                var outputFileFullPathend = outputFolderFullPath +"\\" +namefileoutput;
                

                var lines = File.ReadAllLines(files[i]);
                
                foreach (string line in lines)
                {
                    string convertedLine = ConvertLine(line);
                    convertedLine = convertedLine + Environment.NewLine;
                    File.AppendAllText(outputFileFullPathend, convertedLine);
                    
                }
                Console.WriteLine("filesmassiv[number] "+ outputFileFullPathend);
            }
           
        }


        static void outputFileFromDirecrory(string outputFileFullPath,string convertedLine)
        {

            string[] filesoutput = Directory.GetFiles(outputFileFullPath);
            var files=Directory.GetFiles(outputFileFullPath);
            /**
            foreach (string file in files)
            {
                if (File.Exists(file))
                {

                    // Проверяем, пуст ли файл
                    if (new FileInfo(file).Length == 0)
                    {

                        // Файл пустой, записываем в него текст
                        using (StreamWriter writer = new StreamWriter(file))
                        {
                            writer.WriteLine("Привет, мир!");
                        }
                    }
                    else
                    {
                        // Файл не пустой, переходим к следующему файлу
                        Console.WriteLine("Файл не пустой, переходим к следующему файлу.");
                    }
                }
                else
                {
                    // Файл не существует, создаем его и записываем в него текст
                    using (StreamWriter writer = new StreamWriter(file))
                    {
                        writer.WriteLine("Привет, мир!");
                    }
                    Console.WriteLine("ошибка");
                }
            }
            **/
            /**
            for (int a=0; a<filesoutput.Length; a++)
            {
                // Проверяем, существует ли файл
                if (File.Exists(filesoutput[a]))
                {
                    
                    // Проверяем, пуст ли файл
                    if (new FileInfo(filesoutput[a]).Length == 0)
                    {
                        
                        // Файл пустой, записываем в него текст
                        using (StreamWriter writer = new StreamWriter(filesoutput[a]))
                        {
                            writer.WriteLine("Привет, мир!");
                        }
                    }
                    else
                    {
                        // Файл не пустой, переходим к следующему файлу
                        Console.WriteLine("Файл не пустой, переходим к следующему файлу.");
                    }
                }
                else
                {
                    // Файл не существует, создаем его и записываем в него текст
                    using (StreamWriter writer = new StreamWriter(filesoutput[a]))
                    {
                        writer.WriteLine("Привет, мир!");
                    }
                    Console.WriteLine("ошибка");
                }
            }
            **/
        }


        static string ConvertLine(string line)
        {
            long unixTimeSecondsNew = ((DateTimeOffset) DateTime.Now.AddMonths(2)).ToUnixTimeSeconds();
            
            var cookiesStr = string.Empty;
            var singleAccSplit = line.Split(new char[] { '|' });
            for (int j = 0; j < singleAccSplit.Length; j++)
            {
                if (singleAccSplit[j].Contains("c_user"))
                {
                    cookiesStr = singleAccSplit[j];
                    break;
                }
            }
            var finalCookiesString = "[";
            var cookiesArray = cookiesStr.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            for (int j = 0; j < cookiesArray.Length; j++)
            {
                var cookie = cookiesArray[j];
                var nameValue = cookie.Split(new char[] { '=' });
                if (nameValue.Contains("wd"))
                {
                    nameValue[1] = "1920x1080";
                }        
                finalCookiesString += "{ \"domain\":\".facebook.com\",\"expirationDate\":" + unixTimeSecondsNew + ",\"httpOnly\":true,\"name\":\"" + nameValue[0] + "\",\"path\":\"/\",\"secure\":false,\"value\":\"" + nameValue[1] + "\"},";
            }
            finalCookiesString = finalCookiesString.TrimEnd(',') + "]";                
            return line.Replace(cookiesStr, finalCookiesString);
        }
    }
}