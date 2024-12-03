public class Doctor
{
    public string Family { get; set; }
    public string Given { get; set; }
    public string Patronymic { get; set; }
    /* Конструктор принимает полное имя,
     * разбивает его на части и заполняет свойства Family, Given и Patronymic.
     * Если отчество отсутствует, оно будет равно null.*/

    public Doctor(string fullName)
    {
        var nameParts = fullName.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        if (nameParts.Length < 2)
        {
            throw new ArgumentException("ФИО должно содержать хотя бы фамилию и имя.");
        }

        Family = nameParts[0];
        Given = nameParts[1];
        Patronymic = nameParts.Length > 2 ? nameParts[2] : null;
    }
}
