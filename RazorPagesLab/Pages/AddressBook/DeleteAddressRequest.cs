using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using MediatR;

namespace RazorPagesLab.Pages.AddressBook;

public class DeleteAddressRequest
    : IRequest<Guid>
{
    [Required(ErrorMessage = "Id is required.")]
    public Guid Id { get; set; }

    [DisplayName("Address Line 1")]
    public string Line1 { get; set; }

    [DisplayName("Address Line 2")]
    public string Line2 { get; set; }

    [DisplayName("City")]
    public string City { get; set; }

    [DisplayName("State")]
    public string State { get; set; }

    [DisplayName("Postal Code")]
    public string PostalCode { get; set; }

}