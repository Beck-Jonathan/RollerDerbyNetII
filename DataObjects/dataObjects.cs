using System;

namespace DataObjects
{



    public class Team
    {
        public string TeamId { set; get; }
        public string LeagueID { set; get; }
        public string TeamName { set; get; }
        public decimal MonthlyDue { set; get; }
        public string City { set; get; }
        public string State { set; get; }
        public string Zip { set; get; }

    }

    public class TeamVM : Team
    {
        public League league { set; get; }

    }
    public class Game
    {
        public int GameID { set; get; }
        public string LocationID { set; get; }
        public DateTime Date { set; get; }

    }
    public class Game_Team_Line
    {
        public int GameID { set; get; }
        public string TeamId { set; get; }
        public int Points { set; get; }

    }

    public class Invoice
    {
        public int InvoiceID { set; get; }
        public string SkaterID { set; get; }
        public float InvoiceAmount { set; get; }
        public DateTime IssueDate { set; get; }

    }
    public class Receipts
    {
        public int ReceiptID { set; get; }
        public int InvoiceID { set; get; }
        public DateTime RecipttDate { set; get; }
        public float ReceiptAmount { set; get; }

    }
    public class Role
    {
        public string RoleID { set; get; }

    }
    public class Skater_Role_Line
    {
        public string RoleID { set; get; }
        public string SkaterID { set; get; }

    }
    public class Exercise
    {
        public string ExerciseID { set; get; }
        public int Exercise_count { set; get; }
        public string Exercise_units { set; get; }

    }
    public class Practice
    {
        public int PracticeID { set; get; }
        public string LocationID { set; get; }
        public DateTime DateTime { set; get; }

    }
    public class exercise_practice_line
    {
        public string ExerciseID { set; get; }
        public int PracticeID { set; get; }
        public int Count { set; get; }

    }
    public class Skater_practice_Line
    {
        public string SkaterID { set; get; }
        public int PracticeID { set; get; }

    }

}

