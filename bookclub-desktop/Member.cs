using System;

namespace bookclub_desktop;

public class Member
{

    private int id;
    private string name;
    private string gender;
    private DateTime birth_date;
    private bool banned;

    public Member(int id, string name, string gender, DateTime birth_date, bool banned)
    {
        this.id = id;
        this.name = name;
        this.gender = gender;
        this.birth_date = birth_date;
        this.banned = banned;
    }

    public int Id { get => id; set => id = value; }
    public string Name { get => name; set => name = value; }
    public string Gender { get => gender; set => gender = value; }
    public DateTime Birth_date { get => birth_date; set => birth_date = value; }
    public bool Banned { get => banned; set => banned = value; }

    public string FormattedBirthDate
    {
        get
        {
            return birth_date.ToString("yyyy-MM-dd");
        }
    }

    public string BannedDisplay
    {
        get
        {
            return banned ? "X" : "";
        }
    }

    public string GenderDisplay
    {
        get
        {
            switch (gender)
            {
                case "F":
                    return "No";
                case "M":
                    return "Ferfi";
                default:
                    return "Ismeretlen";
            }
        }
    }
}
