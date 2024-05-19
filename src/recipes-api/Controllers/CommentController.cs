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
[Route("comment")]
public class CommentController : ControllerBase
{  
    public readonly ICommentService _service;
    
    public CommentController(ICommentService service)
    {
        this._service = service;        
    }

    // 10 - Sua aplicação deve ter o endpoint POST /comment
    [HttpPost]
    public IActionResult Create([FromBody]Comment comment)
    {
        try
        {
            if (comment == null)
                return BadRequest();
            
            _service.AddComment(comment);

            return StatusCode(201, comment);
        }
         catch (Exception ex)
        {
            return StatusCode(500, $"Ocorreu um Erro ao processar a solicitação: " + ex.Message);
        }
    }

    // 11 - Sua aplicação deve ter o endpoint GET /comment/:recipeName
    [HttpGet("{name}", Name = "GetComment")]
    public IActionResult Get(string name)
    {                
       try
       {
            if (name == null)
              return BadRequest();

            _service.GetComments(name);

            return StatusCode(200);
       }
        catch (Exception ex)
        {
            return StatusCode(500, $"Ocorreu um Erro ao processar a solicitação: " + ex.Message);
        }                 
    }
}