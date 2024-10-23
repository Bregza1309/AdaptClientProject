using System.ComponentModel.DataAnnotations;
namespace AdaptClientProjectApi.Models
{
	public class Client
	{
		public int Id { get; set; }
		[Required(ErrorMessage = "Client Name is Required")]
		[MaxLength(100 , ErrorMessage = "Client Name should be no more than 50 characters")]
		[MinLength(6 , ErrorMessage = "Client Name should be  more than 6 characters")]
		public string ClientName { get; set; } = string.Empty;
		public DateTime DateRegistered { get; set; } = DateTime.Now;
		[Required(ErrorMessage = "Client Location is Required")]
        [MaxLength(100, ErrorMessage = "Client Location should be no more than 50 characters")]
        
        public string Location { get; set; } = string.Empty;
        [Required(ErrorMessage = "Number of user is required is Required")]
        public int NumberOfUsers {  get; set; }

	}
}
