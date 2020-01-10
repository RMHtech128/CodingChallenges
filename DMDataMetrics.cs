using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using ExtensionMethods;


namespace ActorNameMetrics
{


    /// <summary>
    /* This is where the data parsing, calculations, etc occur.
Input:
Name Validation
-----------------------

Here are some rules about what you should consider a name:

1. Names start at the beginning of the line.
2. Names follow these rules:
     * Formatted "Lastname, Firstname"
     * Contain only uppercase and lowercase letters - alpha only no numeric's no punctuations
     


Names that don't follow those rules should be ignored.

For your test, use the attached example file.Your
program will be tested against much(e.g. 1000x) larger files.

   A. Requirments: Output
    1. The unique count of full names (i.e.duplicates are counted only once)
    2. The unique count of last names
    3. The unique count of first names
    4. The ten most common last names(the names and number of occurrences)
    5. The ten most common first names(the names and number of occurrences)
    6. Note A list of 25 specially unique names(see below for details)
    7. A list of 25 modified names(see below for details)
    
   B. Requirements: Specially Unique Functionality
    1. Name Validation - perform name validation
        i. Names start at the beginning of the line.
        ii. Names follow these rules:
            * Formatted "Lastname, Firstname"
            * Contain only uppercase and lowercase letters
        iii. Ignore Names that don't follow those rules.
    2. Lists of Specially Unique & Modified Names
        i. Take the first N names from the file where both of the following are true:
            * No previous name has the same first name
            * No previous name has the same last name
    

    C. Data sample:
        Graham, Mckenna -- ut
        Voluptatem ipsam et at.
        Marvin, Garfield -- non
        Facere et necessitatibus animi.
        McLaughlin, Mariah -- consequatur
        Eveniet temporibus ducimus amet eaque.
        Lang, Agustina -- pariatur
        Unde voluptas sit fugit.
        Bradtke, Nikko -- et
        Maiores ab officia sed.
    */
    /// </summary>
    internal class DMDataMetrics
    {
        //private string[] lines;

        /// <summary>
        /// The list at this point contains only all lines in the source
        /// </summary>
        /// <param name="lines"></param>
        public DMDataMetrics(string[] lines)
        {
            try
            {
                Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name);
                IsolateFullNames(ref lines); //isolate on the names.

                Console.WriteLine(@"Req 1: Count of Unique Full Names:" + UniqueCountOfNameParts(lines).ToString());
                
                string[] linesSurnames = new string[lines.Count()];
                string[] linesFirstNames = new string[lines.Count()];
                IsolateFirstAndSurnames(lines, ref linesSurnames, ref linesFirstNames);
                
                Console.WriteLine(@"Req 2: Count of Unique Last Names:" + UniqueCountOfNameParts(linesSurnames).ToString());
                
                Console.WriteLine(@"Req 3: Count of Unique First Names:" + UniqueCountOfNameParts(linesFirstNames).ToString());

                Console.WriteLine("Req 4: Ten most common Last Names");
                TenMostCommonNames(linesSurnames);
                Console.WriteLine("Req 5: Ten most common First Names");
                TenMostCommonNames(linesFirstNames);
                Console.WriteLine(@"Req 6: List 25 Specially Unique Names");
                int maxUniquesQty = 25; // the arrays are defined without limit. Set to 25 to meet the requirements of limited testing. The 25 is for testing.
                string[] speciallyUniqueNames = new string[maxUniquesQty];
                List25SpeciallyUniqueNames( lines, ref speciallyUniqueNames, ref linesSurnames, ref linesFirstNames);
                for(int i = 0; i < speciallyUniqueNames.Count(s => s != null); i++ )
                    Console.WriteLine(speciallyUniqueNames[i]);
                Console.WriteLine(@"Req 7: List 25 Specially Modified Names");
                List25SpeciallyModifiedNames(ref speciallyUniqueNames);
                for (int i = 0; i < speciallyUniqueNames.Count(s => s != null); i++)
                    Console.WriteLine(speciallyUniqueNames[i]);
                Console.Read();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }            
        }



        private void IsolateFirstAndSurnames(string[] lines, ref string[] linesLast, ref string[] linesFirst)
        {
            try
            {
                Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name);
         
                int j = 0;
                foreach (string line in lines)
                {
                    if (!String.IsNullOrEmpty(line) && line.Contains(','))
                    {
                        linesLast[j] = line.Substring(0, line.IndexOf(",")).Trim();
                        linesFirst[j] = line.Substring(line.IndexOf(",") + 1).Trim();
                        j++;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// 1. Names start at the beginning of the line.
        /// 2. Names follow these rules:
        /// * Formatted "Lastname, Firstname"
        /// * Contain only uppercase and lowercase letters - alpha only no numeric's no punctuations
        /// </summary>
        /// <param name="lines"></param>
        private void IsolateFullNames(ref string[] lines)
        {
            try
            {
                Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name);
                int i = 0;
                int count = 0;
                for (; i < lines.Length; i++)
                {
                    if (lines[i].Contains(","))
                    {
                        count++;
                    }
                }
                string[] arrayFullNames = new string[count + 1];

                i = 0;
                foreach (string line in lines)
                {
                    // remove double-dash first.
                    string tempStr = line;
                    if (tempStr.Contains("--")) // substring throws exception if string doesn't exist.
                        tempStr = tempStr.Substring(0, line.IndexOf("--")).Trim();
                    // remove any extra white spaces because of full name searches later.
                    tempStr = tempStr.Replace(" ", string.Empty);
                    if (
                        !String.IsNullOrEmpty(tempStr)
                        && tempStr.Contains(',')
                        && !tempStr.Contains('\'')
                        && !tempStr.Contains('-')
                        && !tempStr.Any(c => char.IsDigit(c))
                        )
                    {
                        // now check for chars before the comma and after.
                        string[] temp = tempStr.Split(',');
                        if (!string.IsNullOrEmpty(temp[0]) && !string.IsNullOrEmpty(temp[1]))
                        {
                            arrayFullNames[i] = tempStr;
                            i++;
                        }
                    }
                }
                lines = new string[count + 1];
                lines = arrayFullNames; // Be careful here that the old array length... never mind, just reinitialize the array.
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Not used.
        /// </summary>
        /// <param name="lines"></param>
        /// <param name="listActorNames"></param>
        private void ParseFirstSurnamesList(string[] lines, ref List<ActorClass> listActorNames)
        {
            try
            {
                Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name);
                foreach (string line in lines)
                {
                    if (!String.IsNullOrEmpty(line) && line.Contains(','))
                    {
                        string[] names = line.Split(',');
                        if (names[0].Contains(" ") && names[0].Length > 0)
                            names[0] = names[0].Substring(0, names[0].IndexOf(" ")).Trim();
                        if (names[1].Contains("--") && names[1].Length > 0)
                            names[1] = names[1].Substring(0, names[1].IndexOf("--")).Trim();
                        ActorClass ac = new ActorClass(names[0], names[1]);
                        listActorNames.Add(ac);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// It's possible that the list contains a firstname without a lastname
        /// and viceversa.      
        /// </summary>
        /// <param name="lines"></param>
        /// <returns>int</returns>
        //private int UniqueCountOfFullNames(string[] lines)
        //{
        //    try
        //    {
        //        Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name);
        //        if (lines.Length != 0)
        //        {
        //            int countUnique = 0;
        //            // a few ways to do this: array sort and count, linq, iterations.
        //            foreach (var name in lines)
        //            {
        //                int dup = lines.Count(a => a == name);
        //                if (dup == 1)
        //                    countUnique++;
        //            }
        //            return countUnique;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //    }
        //    return 0;
        //}


        private int UniqueCountOfNameParts(string[] lines) // refactrot to use for both first and suranmes.
        {
            int countUnique = 0;
            try
            {
                string[] foundNames = new string[lines.Length];
                Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name);
                for (int i = 0; i <= lines.Length-1; i++)
                {
                    // if I already search for this name, then skip it.
                    bool found = false;
                    for (int k = 0; k < countUnique; k++)
                    {
                        if (foundNames[k] == lines[i])
                        { // skip this one.
                          //Skip the search
                            found = true;
                            break;
                        }
                    }
                    if (found)
                        continue;

                    int dupCount = 0;
                    for (int j = 0; j <= lines.Length-1; j++)
                    {
                        if (lines[i] == lines[j]) // i can = j for the initial count of unique, then all other duplicates are  ignored
                        {
                            dupCount++;
                            if (dupCount > 1) // ski[ this name
                                break;
                            else if (dupCount == 1)
                            {
                                // make certain I don't double count (you never count the next "i++" of "Graham" as unique!)
                                foundNames[countUnique] = lines[i];
                                countUnique++;
                            }
                        }
                    }
                    
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return countUnique-1;
        }



        private int UniqueCountOfFirstNames(string[] lines)
        {
            int countUnique = 0;
            try
            {
                Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name);
                if (lines.Length != 0)
                {
                    // a few ways to do this: sort and count, linq, iterations.
                    foreach (var name in lines)
                    {
                        //int dup = lines.Count(a => a.Substring(a.IndexOf(",") + 1) == name.Substring(a.IndexOf(",") + 1));
                        int dup = lines.Count(a => a == name);
                        if (dup == 1)
                            countUnique++;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return countUnique;
        }

        /// <summary>
        /// Count all duplicates and make a list of the most common names
        /// or 
        /// </summary>
        /// <param name="lines"></param>
        /// <returns></returns>
        private void TenMostCommonNames(string[] lines)
        {
            try
            {
                Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name);
                var listCommonNames = new List<DMNameCountClass>();
                
                //string[,] commonNames = new string[lines.Count() + 1,2];

                // a few ways to do this: sort and count, linq, iterations.
                //int i = 0;
                foreach (var name in lines)
                {
                    if (name != null)
                    {
                        //int dup = lines.Count(a => a.Substring(a.IndexOf(",") + 1) == name.Substring(a.IndexOf(",") + 1));
                        int dup = lines.Count(a => a == name);
                        if (dup > 0)
                        {
                            DMNameCountClass nCc = new DMNameCountClass(name, dup);
                            if (!listCommonNames.Exists(x => x._name.Contains(name))) // if name doesn;t exist in the list.
                            {
                                if (!string.IsNullOrEmpty(nCc._name))
                                    listCommonNames.Add(nCc);
                            }

                        }
                    }
                }
                //now sort on the count and pull the top ten.
                var listSorted = listCommonNames.OrderByDescending(x => x._count);
                int k = 0;
                foreach( var ordered in listSorted)
                {
                    Console.WriteLine(ordered._name + @" occured " + ordered._count + @" times.");
                    if (++k >= 10)
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// 6. A list of 25 specially unique names (see below for details)
        /// take the first 25 of these:
        /// * No previous first name has the same first name
        /// * No previous last name has the same last name
        /// </summary>
        /// <param name="lines"></param>
        private void List25SpeciallyUniqueNames(string [] lines, ref string[] speciallyUniqueNames, ref string[] linesSurnames, ref string[]linesFirstNames)
        {
            try
            {
                Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name);

                int numNames = linesFirstNames.Count();
                List<string> listUniqueFirst = new List<string>(); //RMH:  lists are slower than arrays so I can refactor this to be faster.
                List<string> listUniqueLast = new List<string>(); // RMH list latency may not include the time taken by coding a "found" flag for array searches.
                for (int j = 0, i = 0; i < numNames; i++)
                {
                    // get a first name, 
                    //check the current list and see if it already is in the results array
                    // repeat for last name.
                    // if both conditions are met, then put it in the results array.
                    string searchFirstName = linesFirstNames[i];

                    string foundName = listUniqueFirst.FirstOrDefault(x => x == searchFirstName); // RMH: can refactor this to remove local variables: leaving for clarity
                    if (foundName == null)
                    {
                        string searchSurname = linesSurnames[i];
                        foundName = string.Empty;
                        foundName = listUniqueLast.FirstOrDefault(x => x == searchSurname); // RMH: can refactor this to remove local variables: leaving for clarity
                        if (foundName == null)
                        {
                            // then add it  
                            listUniqueFirst.Add(searchFirstName);
                            listUniqueLast.Add(searchSurname);
                            speciallyUniqueNames[j] = searchFirstName + @"," + searchSurname;
                            j++;
                            if (j == 25)
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// 7. A list of 25 modified names(see below for details)
        /// Once you have this initial list of 25 names, print it. Then print a new list
        /// that contains 25 modified names.These modified names should only use first
        /// names and last names from the initial 25 names.However, the modified list and
        /// the initial list should not share any full names, and no first or last name may
        /// be used more than once.
        /// 
        /// so basically, that's saying to move the first name to somewhere else so it no longer is associated with
        /// the last name, It is obsfucating the full names. Good practice when copying production to QA and Dev Test
        /// so just move down one index and put the last surname one at the top.
        /// // then incemt down the list
        /// </summary>
        /// <param name="lines"></param>
        private void List25SpeciallyModifiedNames(ref string[] speciallyUniqueNames)
        {
            try
            {
                Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name);
                int numNames = speciallyUniqueNames.Count(s => s != null); ;
                string[] linesSurnames = new string[numNames];
                string[] linesFirstNames = new string[numNames];
                IsolateFirstAndSurnames(speciallyUniqueNames, ref linesSurnames, ref linesFirstNames);
                speciallyUniqueNames = new string[numNames]; // RMH: don't need to do this in production, but helps with dev testing. - improvement here

                // set the first occurance
                speciallyUniqueNames[0] = linesFirstNames[0] + @"," + linesSurnames[numNames - 1];

                int i = 1; 
                // now pull the next first name
                for (; i < numNames-1; i++)
                {
                    speciallyUniqueNames[i] = linesFirstNames[i + 1] + @"," + linesSurnames[i];
                }
                // set the last name occurance
                speciallyUniqueNames[i] = linesFirstNames[i] + @"," + linesSurnames[0];
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}