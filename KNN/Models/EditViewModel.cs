using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace KNN.Models
{
    public class EditViewModel
    {
        [Display(Name="Header Image")]
        public IFormFile BlogHeaderImage { get; set; }

        public Blog blog { get; set; }
    }
}
