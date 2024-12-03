using System;
using System.IO;
using DotLiquid;

class Program
{
    static void Main(string[] args)
    {
        /* В Main методе мы создаем экземпляр Doctor,
         * загружаем шаблон и используем его для генерации XML,
         * который затем выводим на консоль.*/

        /*Регистрируем класс доктор, чтобы он мог использоваться в шаблоне.
         * Мы указываем, какие свойства доступны для использования в шаблоне. */

        Template.RegisterSafeType(typeof(Doctor), new[] { "Family", "Given", "Patronymic" });


        // Папка для сохранения XML файлов
        string outputDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        Directory.CreateDirectory(outputDirectory); // Создание папки, если она не существует

        while (true)
        {
            Console.Write("Введите ФИО врача (или нажмите Enter для выхода): ");
            string doctorFullName = Console.ReadLine();
            try
            {
                // Проверка на выход
                if (string.IsNullOrWhiteSpace(doctorFullName))
                {
                    break; // Выход из цикла
                }
                Doctor doctor = new Doctor(doctorFullName);

                // Загрузка шаблона
                /*Я бы использовал префикс identity для всех полей и
                 * иначе бы их назвал, но делал чтобы получилось по шаблону*/
                string templatePath = Path.Combine(Directory.GetCurrentDirectory(), "doctor_template.liquid");
                string templateContent = File.ReadAllText(templatePath);

                // Генерация XML
                var template = Template.Parse(templateContent);
                var result = template.Render(Hash.FromAnonymousObject(new { doctor }));

                // Генерация имени файла
                string time = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");
                string fileName = $"{time}_{doctor.Family}_{doctor.Given}.xml";
                string filePath = Path.Combine(outputDirectory, fileName);

                // Запись в файл
                File.WriteAllText(filePath, result, System.Text.Encoding.UTF8);
                Console.WriteLine($"XML файл сохранен: {filePath}");

                Console.WriteLine(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }
    }
}