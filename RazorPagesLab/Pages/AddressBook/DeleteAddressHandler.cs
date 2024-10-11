using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace RazorPagesLab.Pages.AddressBook;

public class DeleteAddressHandler 
    : IRequestHandler<DeleteAddressRequest, Guid>
{
    private readonly IRepo<AddressBookEntry> _repo;

    public DeleteAddressHandler(IRepo<AddressBookEntry> repo)
    {
        _repo = repo;
    }

    public async Task<Guid> Handle(DeleteAddressRequest request, CancellationToken cancellationToken)
    {
        var entry = _repo.Find(new EntryByIdSpecification(request.Id)).FirstOrDefault();
        Console.WriteLine(entry.Line1);
        Console.WriteLine($"Entry ID: {request.Id}");
        Console.WriteLine($"Entry found: {entry != null}");

        if (entry == null)
        {
            Console.WriteLine("Entry not found in repo");
            return await Task.FromResult(Guid.Empty);
        }
       
        _repo.Remove(entry);
        return await Task.FromResult(request.Id);
    }
}