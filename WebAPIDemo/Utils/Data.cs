using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebAPIDemo.Common;
using WebAPIDemo.Models;

namespace WebAPIDemo.Utils
{
    public  class Data
    {
        public static List<TeamMember> GetStaticMembers()
        {
            try
            {
                string data = "";

                using (var reader = new StreamReader(Constants.Path))
                {
                    data = reader.ReadToEnd();

                }
                return JsonConvert.DeserializeObject<List<TeamMember>>(data);

            }
            catch (Exception)
            {
                throw;
            }
        }

        public static bool AddData(TeamMember teamMember)
        {
            try
            {
                string data = "";

                using (var reader = new StreamReader(Constants.Path))
                {
                    data = reader.ReadToEnd();

                }
               var listOfMembers= JsonConvert.DeserializeObject<List<TeamMember>>(data);

                listOfMembers.Add(teamMember);
            
                var jsonData = JsonConvert.SerializeObject(listOfMembers);

                using (var stream = new StreamWriter(Constants.Path))
                {
                    stream.Write(jsonData);
                }
                return true;

            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
