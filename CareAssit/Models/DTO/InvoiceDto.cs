using System.ComponentModel.DataAnnotations;
public enum Status
{
    Success,
    Unsuccessful
}
namespace CareAssit.Models.DTO
{
    
    public class InvoiceDto
    {
        [Key]
        public Guid Invoice_Id { get; set; }
        public Guid? Request_Id { get; set; }
        [Required]
        public string InvoiceNumber { get; set; }
        [Required]
        public string InvoiceDate { get; set; }
        [Required]
        public string DueDate { get; set; }

        public int Consultation_Fee { get; set; }
        public int Diag_Tests_Fee { get; set; }
        public int Diag_Scan_Fee { get; set; }
        public int Presc_Medication { get; set; }
        public int Tax { get; set; }

        public int Total_Amount { get; set; }
        public bool Payment { get; set; }=false;
        public Status status { get; set; } = Status.Unsuccessful;

        /*public enum Status
        {
            Success,
            Unsuccessful
        }*/
    }
    public class AddInvoiceDto
    {
        public Guid? Request_Id { get; set; }
        [Required]
        public string InvoiceNumber { get; set; }
        [Required]
        public string InvoiceDate { get; set; }
        [Required]
        public string DueDate { get; set; }

        public int Consultation_Fee { get; set; }
        public int Diag_Tests_Fee { get; set; }
        public int Diag_Scan_Fee { get; set; }
        public int Presc_Medication { get; set; }
        public int Tax { get; set; }

        public int Total_Amount { get; set; }
        public bool Payment { get; set; } = false;
        public Status status { get; set; } = Status.Unsuccessful;

        /*public enum Status
        {
            Success,
            Unsuccessful
        }*/
    }
    public class UpdateInvoiceDto
    {
        public Guid? Request_Id { get; set; }
        [Required]
        public string InvoiceNumber { get; set; }
        [Required]
        public string InvoiceDate { get; set; }
        [Required]
        public string DueDate { get; set; }

        public int Consultation_Fee { get; set; }
        public int Diag_Tests_Fee { get; set; }
        public int Diag_Scan_Fee { get; set; }
        public int Presc_Medication { get; set; }
        public int Tax { get; set; }

        public int Total_Amount { get; set; }
        public bool Payment { get; set; } = false;
        public Status status { get; set; } = Status.Unsuccessful;

        /*public enum Status
        {
            Success,
            Unsuccessful
        }*/
    }
    public class UpdateUserInvoiceDto
    {
        public Guid Invoice_Id { get; set; }
        /*public Guid Invoice_Id { get; set; }
        public Guid? Request_Id { get; set; }
        [Required]
        public string InvoiceNumber { get; set; }
        [Required]
        public string InvoiceDate { get; set; }
        [Required]
        public string DueDate { get; set; }

        public int Consultation_Fee { get; set; }
        public int Diag_Tests_Fee { get; set; }
        public int Diag_Scan_Fee { get; set; }
        public int Presc_Medication { get; set; }
        public int Tax { get; set; }

        public int Total_Amount { get; set; }
        public bool Payment { get; set; }*/
        //public Status status { get; set; } = Status.Unsuccessful;

        /*public enum Status
        {
            Success,
            Unsuccessful
        }*/
    }
}
