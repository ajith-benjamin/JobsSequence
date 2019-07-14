//Author         : Ajith Benjamin
//Created Date   : 13/Jul/2019
//Purpose        : To sequence the jobs and check for dependencies


using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace JobsExecutionOrder
{
    public class JobsOrder
    {
        static void Main()
        {
            Console.WriteLine("Enter the jobs with or without dependencies(with comma separation), for example if job c dependents on job a give c-a. Give it in a single string like a,b-c,c-f,d-a,e-b,f");
            string jobs = Console.ReadLine();
            string output = Sequence(jobs);
            Console.WriteLine("The correct jobs execution sequence would be: " + output);           
        }
        
        public static string Sequence(string jobs)
        {
            List<string> jobsList = jobs.Split(',').ToList();
            ArrayList jobsSequence = new ArrayList();
            foreach(string job in jobsList)
            {
                //Check for dependency
                if (job.Contains("-"))
                {
                    //Split and move the job and dependant into list
                    List<string> lst = job.Split("-").Take(2).ToList();
                    //Check for self dependency
                    if (lst[0] == lst[1])
                    {
                        throw new Exception("Jobs cannot depend on themselves");
                    }
                    else
                    {
                        if (jobsSequence.Contains(lst[1]))
                        {
                            int ind = jobsSequence.IndexOf(lst[1]);
                            if (!jobsSequence.Contains(lst[0]))
                                jobsSequence.Insert(ind + 1, lst[0]);
                            else
                            {
                                //Check for circular dependencies
                                int ind1 = jobsSequence.IndexOf(lst[0]);
                                int ind2 = jobsSequence.IndexOf(lst[1]);
                                if (ind2 > ind1)
                                    throw new Exception("Jobs can't have circular dependencies");
                            }
                        }
                        else
                        {
                            if (!jobsSequence.Contains(lst[0]))
                            {
                                jobsSequence.Add(lst[1]);
                                jobsSequence.Add(lst[0]);
                            }
                            else
                            {
                                int ind3 = jobsSequence.IndexOf(lst[0]);
                                jobsSequence.Insert(ind3, lst[1]);
                            }
                        }

                    }
                }
                else
                    if (!jobsSequence.Contains(job))
                    jobsSequence.Add(job);
            }

            var strings = jobsSequence.ToArray();
            return string.Join(" ", strings);
        }

      
    }
}
