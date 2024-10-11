using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace RazorPagesLab.Pages.AddressBook;

public class UpdateAddressHandler
	: IRequestHandler<UpdateAddressRequest, Guid>
{
	private readonly IRepo<AddressBookEntry> _repo;

	public UpdateAddressHandler(IRepo<AddressBookEntry> repo)
	{
		_repo = repo;
	}

	public async Task<Guid> Handle(UpdateAddressRequest request, CancellationToken cancellationToken)
	{
        var entry = _repo.Find(new EntryByIdSpecification(request.Id)).FirstOrDefault();
        
		entry.Update(request.Line1, request.Line2, request.City, request.State,
			request.PostalCode);
		return await Task.FromResult(entry.Id);
	}
}