﻿using MP_Garcia_GeneJoseph_BMIS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MP_Garcia_GeneJoseph_BMIS
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Resident> residents = new Resident().Residents();

            if (residents.Count < 1 || residents == null)
                Console.WriteLine("System records does not have resident records.");
            else
                foreach (var resident in residents)
                {
                    Console.WriteLine("Id: {0}", resident.ResidentId);
                    Console.WriteLine("Name: {0}", resident.FirstName + " " + resident.LastName);
                    Console.WriteLine("Sex: {0}", resident.Sex);
                    Console.WriteLine("Birthday: {0}", resident.Birthdate.ToLongDateString());
                    Console.WriteLine("Address: {0}", resident.Address);
                    Console.WriteLine("Status: {0}\n", resident.Status);
                }

            new Resident().SaveResidents(residents);

            Console.WriteLine("------------------------------------");

            List<Account> accounts = new Account().Accounts();

            if (accounts.Count < 1)
                Console.WriteLine("Empty");
            else
                foreach (var account in accounts)
                {
                    Console.WriteLine("Id: {0}", account.AccountId);
                    Console.WriteLine("Username: {0}", account.Username);
                    Console.WriteLine("Password: {0}", account.Password);
                    Console.WriteLine("Resident Id: {0}", account.ResidentId);
                    Console.WriteLine("Date: {0}\n", account.RegisteredDate.ToLongDateString());
                }

            new Account().SaveAccounts(accounts);

            Console.WriteLine("------------------------------------");

            Console.ReadKey();
        }
    }
}
