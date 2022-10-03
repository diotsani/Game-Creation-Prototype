using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal static class Constants
{
    public static class Status
    {
        public const string Skill = "Skill: ";
        public const string Stress = "Stress: ";
        public const string Health = "Health: ";
        public const string Money = "Money: ";
        public const string Book = "Book: ";
        public const string Food = "Food: ";
        public const string Action = "Action: ";
        public const string Day = "Day: ";
    }
    public static class Requirments
    {
        public const string ApplyJob = "Apply Job";
        public const string TakeCourse = "Take Course";
        public const string PlayGame = "Play Game";
        public const string CheckMail = "Check Mail";
        
        public const string SocialMedia = "Social Media";
        
        public const string ReadBook = "Read Book";
        public const string CheckBookShelf = "Check Book Shelf";
        
        public const string Jogging = "Jogging";
        public const string BuyFood = "Buy Food";
        
        public const string Eat = "Eat";
        public const string ThrowFood = "Throw Food";
        public const string CheckFoodStock = "Check Food Stock";
        
        public const string Sell = "Sell";
        public const string Repair = "Repair";
        
    }
    public static class Path
    {
        public const string Laptop ="ScriptableObjects/Laptop";
        public const string Handphone = "ScriptableObjects/Handphone";
        public const string Book = "ScriptableObjects/Book";
        public const string Door = "ScriptableObjects/Door";
        public const string Refrigerator = "ScriptableObjects/Refrigerator";
        
    }
    public static class Monologue
    {
        public const string ApplyJobMonolog = "I have to get a job or I will suffer for the rest of my life. Wish me luck!";
        public const string TakeCourseMonolog = "That’s make my head hurt.";
        public const string CheckMailMonolog = "There’s no email. Sorry, you did not pass the test.";
        public const string PlayGameMonolog = "I feel refreshed, I can release my stress.";
        public const string RepairLaptopMonolog = "I have to spend a lot of money to fix it, come on!";
        
        public const string SocialMediaMonolog = "We are looking for a talented designer who will be responsible for the design of our mobile application.” / “No job info.";
        public const string RepairHandphoneMonolog = "I have to change my phone, this phone is too old. I want a new model of iPhone, but I'm broke.";
        public const string SellHandphoneMonolog = "This is the only thing I can do to survive, I really need money";
        
        public const string ReadBookMonolog = "I guess reading a book is not a bad thing.";
        //public const string CheckBookMonolog = "I have {jumlah buku} right now.";
        public const string SellBookMonolog = "Forgive me. Books are a window to the world, but I need money.";
        
        public const string JoggingMonolog = "I need to stretch my muscles, staying at home all the time is not good for my health.";
        public const string BuyFoodMonolog = "I need food to life.";
        
        public const string EatMonolog = "Don’t forget to eat for your own sake.";
        public const string ThrowFoodMonolog = "It's too bad if you have to throw it away.";
        //public const string CheckFoodStockMonolog = "I have [jumlah makanan] right now.";
        
        public static string FoodStockMonolog(int value)
        {
            return $"I have {value} food right now";
        }
        
        public static string BookStockMonolog(int value)
        {
            return $"I have {value} book right now";
        }
    }
    
}
