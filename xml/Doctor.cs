public class Doctor
{
    public string Family { get; set; }
    public string Given { get; set; }
    public string Patronymic { get; set; }

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
