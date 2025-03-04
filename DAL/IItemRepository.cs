
using Rent_A_House_Amin_Mecha.Models;

namespace Rent_A_House_Amin_Mecha.DAL;

public interface IItemRepository
{
    Task<IEnumerable<Item>> GetAll();
    Task<Item?> GetItemById(int id);
    Task Create(Item item);
    Task Update(Item item);
    Task<bool> Delete(int id);
}
