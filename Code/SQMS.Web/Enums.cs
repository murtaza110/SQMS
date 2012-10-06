using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SQMS.Web
{
    public enum ENRole
    {
        Admin = 1,
        SubAdmin,
        Moallim,
        Member = 5,
    }

    public enum ENRegionType
    {
        Region = 1,
        Country,
        State,
        City,
        Mohalla,
        Location,
    }

    public enum ENRequestType
    {
        SabaqGroup = 1,
        SabaqRegistration,
    }

    public enum ENSabaqStatus
    {
        Requested = 1,
        Approved,
        Rejected,
        InProgress,
        Suspended,
        Completed,
        Cancelled,
    }
}