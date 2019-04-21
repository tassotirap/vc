namespace Verizon.Connect.QueryService.ViewModel
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class PlotQueryRequest
    {
        [Required]
        public DateTime? EndDateTime { get; set; }

        [Required]
        public DateTime? StartDateTime { get; set; }

        [Required]
        public int? VId { get; set; }
    }
}