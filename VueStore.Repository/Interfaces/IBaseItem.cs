using System;
using System.Collections.Generic;
using System.Text;

namespace VueStore.Repository.Interfaces
{
    //This would contain audit fields like LastUpdated, UpdatedBy
    interface IBaseItem : IBaseDBResource
    {
        string ItemName { get; set; }
        int Cost { get; set; }
    }
}
