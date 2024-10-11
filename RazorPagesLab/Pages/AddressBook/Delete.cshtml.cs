using System;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorPagesLab.Pages.AddressBook;

public class DeleteModel : PageModel
{
    private readonly IMediator _mediator;
    private readonly IRepo<AddressBookEntry> _repo;

    public DeleteModel(IRepo<AddressBookEntry> repo, IMediator mediator)
    {
        _repo = repo;
        _mediator = mediator;
    }

    [BindProperty]
    public DeleteAddressRequest DeleteAddressRequest { get; set; }

    public void OnGet(Guid id)
    {
        // Todo: Use repo to get address book entry, set UpdateAddressRequest fields.
        var entry = _repo.Find(new EntryByIdSpecification(id)).FirstOrDefault();
        Console.WriteLine(id);

        if (entry == null)
        {
            Console.WriteLine("Address entry not found");
            return;
        }

        DeleteAddressRequest = new DeleteAddressRequest
        {
            Id = id,
            Line1 = entry.Line1,
            Line2 = entry.Line2,
            City = entry.City,
            State = entry.State,
            PostalCode = entry.PostalCode
        };

    }

    public async Task<IActionResult> OnPost()
    {
        // Todo: Use mediator to send a "command" to update the address book entry, redirect to entry list.i
        if (ModelState.IsValid)
        {
           await _mediator.Send(DeleteAddressRequest);
           return RedirectToPage("Index");
        }

        return NotFound();
    }
}