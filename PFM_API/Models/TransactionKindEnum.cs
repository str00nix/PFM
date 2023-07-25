using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace PFM_API.Models
{
    public enum TransactionKindEnum
    {
        [Description("Deposit")]
        dep,

        [Description("Withdrawal")]
        wdw,

        [Description("Payment")]
        pmt,

        [Description("Fee")]
        fee,

        [Description("Interest credit")]
        inc,

        [Description("Reversal")]
        rev,

        [Description("Adjustment")]
        adj,

        [Description("Loan disbursement")]
        lnd,

        [Description("Loan repayment")]
        lnr,

        [Description("Foreign currency exchange")]
        fcx,

        [Description("Account openning")]
        aop,

        [Description("Account closing")]
        acl,

        [Description("Split payment")]
        spl,

        [Description("Salary")]
        sal
    }
}