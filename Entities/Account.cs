using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Api.Entities
{

    public class Account( string accountName, string bankName, string bankCode, string accountType)
    {
    
        public int Id { get; set; }
        public int AccountNumber { get; set;}
        public required string AccountName { get; set;} = accountName;
        public required string BankName { get; set;} = bankName;
        public required string BankCode { get; set;} = bankCode;
        public required string AccountType { get; set;} = accountType;
        public decimal Balance { get; set;} = 0;
        
        
        
    }
}