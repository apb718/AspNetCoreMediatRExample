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
		Console.WriteLine($"Onget triggered with {id}");
		// Todo: Use repo to get address book entry, set UpdateAddressRequest fields.
		var entries = _repo.Find(new EntryByIdSpecification(id)).FirstOrDefault();
		UpdateAddressRequest.Id = id;
		UpdateAddressRequest.Line1 = entries.Line1;
		UpdateAddressRequest.Line2 = entries.Line2;
		UpdateAddressRequest.City = entries.City;
		UpdateAddressRequest.State = entries.State;
		UpdateAddressRequest.PostalCode = entries.PostalCode;
		
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