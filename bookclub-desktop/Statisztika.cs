using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace bookclub_desktop;

public static class Statisztika
{
    static List<Member> members;

    public static void Run()
    {
        try
        {
            ReadMembers();
            Console.WriteLine($"Kitiltott tagod szama: {GetBanned()}");
            if ( IsYoungerThan() == true )
            {
                Console.WriteLine("Van a tagok között 18 évnél fiatalabb személy.");
            }
            else
            {
                Console.WriteLine("Nincs a tagok között 18 évnél fiatalabb személy.");
            }
            WhatGender();
            var oldest = GetOldest();
            Console.WriteLine($"A legidosebb klubtag: {oldest.Name} ({oldest.Birth_date})");
            Console.WriteLine("Adjon meg egz nevet:");
            var member = FindMember(Console.ReadLine());
            if (member != null)
            {
                if (member.Banned)
                {
                    Console.WriteLine("A megadott személy kivan tiltva");
                }
                else
                {
                    Console.WriteLine("A megadott személy nincs kitiltva");
                }
            }
            else
            {
                Console.WriteLine("Nincs ilyen tagja a klubnak");
            }

        }
        catch (MySqlException ex)
        {

            Console.WriteLine("Hiba tortent");
            Console.WriteLine(ex);
        }

    }

    private static void ReadMembers()
    {
        DBHelper db = new DBHelper();
        members = db.ReadMembers();
    }

    private static int GetBanned()
    {
        return members.Where(m => m.Banned == true).Count();
    }

    private static bool IsYoungerThan()
    {
        if (members.Count(m => (DateTime.Now - m.Birth_date).TotalDays < 18 * 365) == 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    private static Member? GetOldest()
    {
       return members.OrderBy(m => m.Birth_date).FirstOrDefault();
    }

    private static void WhatGender()
    {
       var genders = members.GroupBy(m => m.GenderDisplay).ToDictionary(g => g.Key, g => g.Count());
        foreach(var gender in genders)
        {
            Console.WriteLine($"{gender.Key} , {gender.Value}");
        }

    }

    static Member? FindMember(string? name)
    {
       return members.Where(m => m.Name == name).FirstOrDefault();
    }


}
