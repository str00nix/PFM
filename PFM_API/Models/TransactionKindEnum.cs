using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace PFM_API.Models
{
    public enum TransactionKindEnum
    {
        dep, wdw, pmt, fee, inc, rev, adj, lnd, lnr, fcx, aop, acl, spl, sal
    }
}