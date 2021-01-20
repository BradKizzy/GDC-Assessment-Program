﻿using System;
using System.IO;
using System.Text;
using System.Linq;
using System.Collections.Generic;
//Author: Bradley Kisner
//Date: 01/19/2021

//This is an assessment file parser program for the GDC. The function of this program is to parse a .csv file of the users choice that exists within the current directory
//and print out two separate lists of emails for which ones are valid and which ones aren't

namespace GDCTestProg
{
    class Program
    {
        static void Main()
{

//The two lines below prompt the user to input one of the following file names. After this, the console will read the file name to determine if it is valid.
Console.WriteLine("Input your file name below(testfile.csv, valids.csv, invalids.csv): ");
var name = Console.ReadLine();
//After the console reads the file name, this if else statement will determine whether or not the file exists or not. If the file does exist,
//the program will proceed with parsing the file and printing the emails.
if(File.Exists(name)){
//Reads the lines of the parsed csv file.
var lines = File.ReadLines(name, Encoding.UTF8);
//This variable represents each user with an email. Since all we need to print are the emails, Array space 2 will be selected since it corresponds
//to the email addresses in column 3 of the csv files
var users = from line in lines
            let fields = line.Replace(", ", ",").Split(",")
            select new User(fields[2]);
//This variable is created to sort the email addresses in descending order.
var sorted = from user in users
             orderby user.EmailAddress descending
             select user;
//The two lists below are new empty lists to be filled by valid and invalid emails.
List<String> valid = new List<String>();
List<String> invalid = new List<String>();
//This loop is responsible for checking each email to see if they are valid or not. In this case, emails are determined to be valid if they end with the following domains.
foreach(var user in sorted)
{
    var str = user.EmailAddress;
    if(str.EndsWith(".com" )|| str.EndsWith(".net") || str.EndsWith(".gov") || str.EndsWith(".edu"))
    {
        valid.Add(str);
    }else{
        invalid.Add(str);
    }
}
//This loop notifies the user with the number of valid email addresses within their selected file, along with the addresses listed in descending order.
Console.WriteLine("Valid Emails total number: {0}. List of valid emails in descending order:", valid.Count);
foreach (var email in valid)
{
    Console.WriteLine(email);
}
//This loop notifies the user with the number of invalid email addresses within their selected file, along with the addresses listed in descending order. 
Console.WriteLine("\nInvalid Emails total number: {0}. List of invalid emails in descending order:", invalid.Count);
foreach (var email in invalid)
{
    Console.WriteLine(email);
}
//If the file that was typed in doesn't exist, the user will receive the error message below.
}else
{
    Console.WriteLine("Oops! The file you were searching for doesn't exist!");
}



}
    //This is meant to consider the email addresses as users.
    public record User(string EmailAddress);
}
}