using System;
using System.Collections.Generic;
using System.Text;

namespace VueStore.Repository.Interfaces
{
    //This would contain audit fields like LastUpdated, UpdatedBy
    interface IBaseDBResource
    {
        int Id { get; set; }
    }
}
