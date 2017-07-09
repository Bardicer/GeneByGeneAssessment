using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GeneByGeneAssessment.Models
{
    public static class DatabaseSetup
    {
        public static void Initialize(GBGContext context)
        {
            context.Database.EnsureCreated();

            if (context.Users.Any())
            {
                return;
            }

            context.Users.AddRange(ReadUsersFromFile());
            context.Statuses.AddRange(ReadStatusesFromFile());
            context.Samples.AddRange(ReadSamplesFromFile());
            context.SaveChanges();            
        }
        
        private static List<User> ReadUsersFromFile()
        {
            var userList = new List<User>();
            var userStrings = File.ReadLines(".\\Misc\\Users.txt").ToList();
            userStrings.RemoveAt(0);
            foreach (var userString in userStrings)
            {
                var userArray = userString.Split(',');
                userList.Add(new User
                {
                    UserId = int.Parse(userArray[0]),
                    FirstName = userArray[1],
                    LastName = userArray[2]
                });
            }
            return userList;
        }

        private static List<Status> ReadStatusesFromFile()
        {
            var statusList = new List<Status>();
            var statusStrings = File.ReadLines(".\\Misc\\Statuses.txt").ToList();
            statusStrings.RemoveAt(0);
            foreach (var statusString in statusStrings)
            {
                var statusArray = statusString.Split(',');
                statusList.Add(new Status
                {
                    StatusId = int.Parse(statusArray[0]),
                    StatusName = statusArray[1]
                });
            }
            return statusList;
        }

        private static List<Sample> ReadSamplesFromFile()
        {
            var sampleList = new List<Sample>();
            var sampleStrings = File.ReadLines(".\\Misc\\Samples.txt").ToList();
            sampleStrings.RemoveAt(0);
            foreach (var sampleString in sampleStrings)
            {
                var sampleArray = sampleString.Split(',');
                sampleList.Add(new Sample
                {
                    SampleId = int.Parse(sampleArray[0]),
                    Barcode = sampleArray[1],
                    CreatedAt = Convert.ToDateTime(sampleArray[2]),
                    CreatedBy = int.Parse(sampleArray[3]),
                    StatusId = int.Parse(sampleArray[4])
                });
            }
            return sampleList;
        }
    }
}
