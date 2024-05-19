using Microsoft.AspNetCore.Mvc;
using recipes_api.Services;
using recipes_api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace recipes_api.Controllers;

[ApiController]
[Route("recipe")]
public class RecipesController : ControllerBase
{    
    public readonly IRecipeService _service;
    
    public RecipesController(IRecipeService service)
    {
        this._service = service;        
    }

    // 1 - Sua aplicação deve ter o endpoint GET /recipe
    //Read
    [HttpGet]
    public IActionResult Get()
    {
        try
        {
            var recipes = _service.GetRecipes();
            if (recipes == null || recipes.Count == 0)
            {
                return NotFound("Nenhuma receita encontrada");
            }

            return Ok(recipes);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Ocorreu um Erro ao processar a solicitação: " + ex.Message);
        }
    }

    // 2 - Sua aplicação deve ter o endpoint GET /recipe/:name
    //Read
    [HttpGet("{name}", Name = "GetRecipe")]
    public IActionResult Get(string name)
    {                
       try
        {
            var recipes = _service.GetRecipe(name);
            if (recipes == null)
            {
                return NotFound("Nenhuma receita encontrada");
            }

            return Ok(recipes);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Ocorreu um Erro ao processar a solicitação: " + ex.Message);
        }
    }

    // 3 - Sua aplicação deve ter o endpoint POST /recipe
    [HttpPost]
    public IActionResult Create([FromBody]Recipe recipe)
    {
        try
        {
            if (recipe == null)
                return BadRequest("Os dados da receita estão ausentes.");

            _service.AddRecipe(recipe);

            return StatusCode(201, recipe);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Ocorreu um Erro ao processar a solicitação: " + ex.Message);
        }
    }

    // 4 - Sua aplicação deve ter o endpoint PUT /recipe
    [HttpPut("{name}")]
    public IActionResult Update(string name, [FromBody]Recipe recipe)
    {
        try
        {
            if (recipe == null)
                return BadRequest("Dados ausentes.");

            var recipeForUpdate = _service.RecipeExists(recipe.Name);

            if (!recipeForUpdate)
                return BadRequest("Dados inválidos. Receita nao encontrada");
            
            _service.UpdateRecipe(recipe);
            
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Ocorreu um Erro ao processar a solicitação: " + ex.Message);
        }
    }

    // 5 - Sua aplicação deve ter o endpoint DEL /recipe
    [HttpDelete("{name}")]
    public IActionResult Delete(string name)
    {
        if (!_service.RecipeExists(name))
            return BadRequest();
        
        return StatusCode(204);
    }    
}
