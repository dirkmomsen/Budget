using API.Data;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/budget/{budgetId}/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IBudgetItemRepository _budgetItemRepository;
        private readonly IMapper _mapper;

        public ItemController(IBudgetItemRepository budgetItemRepository, IMapper mapper)
        {
            _budgetItemRepository = budgetItemRepository;
            _mapper = mapper;
        }
    }
}
