using KillingMachine.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KillingMachine.Controllers;

public abstract class CrudController<TEntity> : Controller where TEntity : class
{
    protected readonly AppDbContext Db;
    protected CrudController(AppDbContext db) => Db = db;
    protected DbSet<TEntity> Set => Db.Set<TEntity>();

    public virtual async Task<IActionResult> Index()
    {
        return View(await Set.AsNoTracking().ToListAsync());
    }

    public virtual async Task<IActionResult> Details(int? id)
    {
        if (id is null) return NotFound();
        var entity = await Set.FindAsync(id.Value);
        return entity is null ? NotFound() : View(entity);
    }

    public virtual IActionResult Create() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public virtual async Task<IActionResult> Create(TEntity entity)
    {
        if (!ModelState.IsValid) return View(entity);
        try
        {
            Set.Add(entity);
            await Db.SaveChangesAsync();
            TempData["Success"] = "Registro creado correctamente.";
            return RedirectToAction(nameof(Index));
        }
        catch (DbUpdateException ex)
        {
            ModelState.AddModelError(string.Empty, "No se pudo guardar. Verifique datos duplicados o relaciones existentes.");
            ViewData["TechnicalError"] = ex.GetBaseException().Message;
            return View(entity);
        }
    }

    public virtual async Task<IActionResult> Edit(int? id)
    {
        if (id is null) return NotFound();
        var entity = await Set.FindAsync(id.Value);
        return entity is null ? NotFound() : View(entity);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public virtual async Task<IActionResult> Edit(int id, TEntity entity)
    {
        var idProperty = typeof(TEntity).GetProperty("Id");
        if (idProperty is null || Convert.ToInt32(idProperty.GetValue(entity)) != id) return NotFound();
        if (!ModelState.IsValid) return View(entity);

        try
        {
            Set.Update(entity);
            await Db.SaveChangesAsync();
            TempData["Success"] = "Registro actualizado correctamente.";
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await Set.AnyAsync(x => EF.Property<int>(x, "Id") == id)) return NotFound();
            throw;
        }
        catch (DbUpdateException ex)
        {
            ModelState.AddModelError(string.Empty, "No se pudo guardar. Verifique datos duplicados o relaciones existentes.");
            ViewData["TechnicalError"] = ex.GetBaseException().Message;
            return View(entity);
        }

        return RedirectToAction(nameof(Index));
    }

    public virtual async Task<IActionResult> Delete(int? id)
    {
        if (id is null) return NotFound();
        var entity = await Set.FindAsync(id.Value);
        return entity is null ? NotFound() : View(entity);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public virtual async Task<IActionResult> DeleteConfirmed(int id)
    {
        var entity = await Set.FindAsync(id);
        if (entity is null) return RedirectToAction(nameof(Index));

        try
        {
            Set.Remove(entity);
            await Db.SaveChangesAsync();
            TempData["Success"] = "Registro eliminado correctamente.";
        }
        catch (DbUpdateException)
        {
            TempData["Error"] = "No se puede eliminar porque el registro tiene informacion relacionada.";
        }

        return RedirectToAction(nameof(Index));
    }
}
