using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MvvmLightDemo.Model;

namespace MvvmLightDemo.Design
{
    public class DesignDataService : IDataService
    {
        //public void GetData(Action<DataItem, Exception> callback)
        //{
        //    // Use this to create design time data

        //    var item = new DataItem("Welcome to MVVM Light [design]");
        //    callback(item, null);
        //}

        public async Task<IList<Friend>> GetFriends()
        {
            var result = new List<Friend>();
            for (var index = 0; index < 42; index++)
            {
                result.Add(
                  new Friend
                  {
                      DateOfBirth = (DateTime.Now - TimeSpan.FromDays(index)),
                      FirstName = "FirstName" + index,
                      LastName = "LastName" + index,
                      ImageUrl = "http://www.ipac.caltech.edu/2mass/gallery/hydrasm.jpg"
                  });
            }
            return result;
        }
    }
}