using System;
using System.Linq;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorPagesLab.Pages.AddressBook;

public class EditModel : PageModel
{
	private readonly IMediator _mediator;
	private readonly IRepo<AddressBookEntry> _repo;

	public EditModel(IRepo<AddressBookEntry> repo, IMediator mediator)
	{
		_repo = repo;
		_mediator = mediator;
	}

	[BindProperty]
	public UpdateAddressRequest UpdateAddressRequest { get; set; }

	public void OnGet(Guid id)
	{
		// Todo: Use repo to get address book entry, set UpdateAddressRequest fields.
		var entry = _repo.Find(new EntryByIdSpecification(id)).FirstOrDefault();
		Console.WriteLine(entry.City);
		if (entry == null)
		{
			Console.WriteLine("Address entry not found");
			return;
		}
		
		// Console.WriteLine($"{UpdateAddressRequest.Id.GetType} {entry.Id.GetType}\n");
		UpdateAddressRequest = new UpdateAddressRequest
		{
			Id = entry.Id,
			Line1 = entry.Line1,
			Line2 = entry.Line2,
			City = entry.City,
			State = entry.State,
			PostalCode = entry.PostalCode
		};
		
		
	}

	public ActionResult OnPost()
	{
		// Todo: Use mediator to send a "command" to update the address book entry, redirect to entry list.i
		if (ModelState.IsValid)
		{
			_mediator.Send(UpdateAddressRequest);
			return RedirectToPage("Index");
		}

		return Page();
	}
}