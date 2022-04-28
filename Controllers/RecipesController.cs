#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Dieter.Data;
using Dieter.Models;

namespace Dieter.Controllers
{
    public class RecipesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RecipesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Recipes
        public async Task<IActionResult> Index()
        {

            RecipeViewModel ViewModel = new RecipeViewModel();

            ViewModel.Recipes = await _context.Recipes.ToListAsync();
            ViewModel.Categories = await _context.Categories.ToListAsync();
            ViewModel.CategoriesRecipes = await _context.CategoryRecipe.ToListAsync();

            return View(ViewModel);
        }

        // GET: Recipes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recipe = await _context.Recipes
                .FirstOrDefaultAsync(m => m.Id == id);

            if (recipe == null)
            {
                return NotFound();
            }

            DetailsRecipeViewModel DetailsVM = new DetailsRecipeViewModel();

            DetailsVM.Recipe = recipe; //obiekt przepis zawierający informacje o przepisie
            DetailsVM.RecipeProducts = await _context.RecipeProducts.Where(p => p.RecipeId == recipe.Id).ToListAsync(); //do listy produktów zapisujemy wszystkie informacje dotyczące konkretnego przepisu
            List<Product> products = _context.Products.ToList();
            List<Category> categories = _context.Categories.ToList();

            foreach (CategoryRecipe categoriesRecipe in _context.CategoryRecipe.Where(c => c.RecipeId == DetailsVM.Recipe.Id))
            {
                foreach (Category category in categories)
                {
                    if (category.Id == categoriesRecipe.CategoryId)
                    {
                        DetailsVM.Categories.Add(category);
                    }
                }
            }

            foreach (RecipeProduct productInfo in DetailsVM.RecipeProducts)
            {
                foreach (Product product in products)
                {
                    if (productInfo.ProductId == product.Id)
                    {
                        DetailsVM.Products.Add(product); //powstaje lista produktów powiązanych z przepisem
                        DetailsVM.kcalSum = DetailsVM.kcalSum + (product.Kcal * productInfo.Ammount);
                    }
                }
            }
            return View(DetailsVM);
        }

        // GET: Recipes/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            return View();
        }

        // POST: Recipes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,ImgUrl,Description,CategoriesIds,Ammounts")] Recipe recipe, int[] searchIds)
        {
            if (ModelState.IsValid)
            {
                List<int> AmmountsList = recipe.Ammounts.Split(',').Select(Int32.Parse).ToList();

                //Sprawdzenie czy podano tyle samo produktów i ilości produktów
                if (searchIds.Length != AmmountsList.Count)
                {
                    TempData["ErrorInfo"] = "Wprowadź tyle samo elementów w <b>polu produktów</b> i <b>ich ilości</b>.";
                    ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
                    return View(recipe);
                }

                _context.Add(recipe);
                await _context.SaveChangesAsync();

                //Łączenie tablic id produktów i podanej ilości produktów
                var productsIdsAndAmmounts = searchIds.Zip(AmmountsList, (s, a) => new { productId = s, Ammount = a });

                foreach (var sa in productsIdsAndAmmounts)
                {
                    RecipeProduct productModel = new RecipeProduct();
                    productModel.ProductId = sa.productId;
                    productModel.Ammount = sa.Ammount;
                    productModel.RecipeId = recipe.Id;

                    _context.RecipeProducts.Add(productModel);
                    await _context.SaveChangesAsync();
                }

                foreach (var cat in recipe.CategoriesIds)
                {
                    CategoryRecipe CR = new CategoryRecipe();
                    CR.CategoryId = cat;
                    CR.RecipeId = recipe.Id;

                    _context.CategoryRecipe.Add(CR);
                    await _context.SaveChangesAsync();
                }

                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            return View(recipe);
        }

        // GET: Recipes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recipe = await _context.Recipes.FindAsync(id);
            if (recipe == null)
            {
                return NotFound();
            }
            return View(recipe);
        }

        // POST: Recipes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,ImgUrl,Description")] Recipe recipe)
        {
            if (id != recipe.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(recipe);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RecipeExists(recipe.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(recipe);
        }

        // GET: Recipes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recipe = await _context.Recipes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (recipe == null)
            {
                return NotFound();
            }

            return View(recipe);
        }

        // POST: Recipes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var recipe = await _context.Recipes.FindAsync(id);
            _context.Recipes.Remove(recipe);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RecipeExists(int id)
        {
            return _context.Recipes.Any(e => e.Id == id);
        }

        public List<Product> Func_FindAllProducts()
        {
            return _context.Products.ToList();
        }

        public List<Product> Func_SearchProduct(string text)
        {
            return _context.Products.Where(p => p.Name.Contains(text)).ToList();
        }

        [ActionName("Search")]
        public IActionResult SearchProduct(string text)
        {
            if (text == null)
            {
                return new JsonResult(Func_FindAllProducts());
            }
            else
            {
                return new JsonResult(Func_SearchProduct(text));
            }
        }

        [HttpGet]
        [ActionName("searchtest")]
        public async Task<IActionResult> Searching(string term)
        {
            if (!string.IsNullOrEmpty(term))
            {
                var products = await _context.Products.ToListAsync();
                var data = products.Where(a => a.Name.Contains(term, StringComparison.OrdinalIgnoreCase)).ToList().AsReadOnly();
                return Ok(data);
            }
            else
            {
                return Ok();
            }
        }

        [HttpGet]
        public async Task<IActionResult> FindRecipe(string term)
        {
            if(!string.IsNullOrEmpty(term))
            {
                var recipes = await _context.Recipes.ToListAsync();
                var data = recipes.Where(r => r.Name.ToUpper().Contains(term.ToUpper())).ToList().AsReadOnly();
                return Ok(data);
            } 
            else
            {
                return Ok();
            }
        }
    }
}
