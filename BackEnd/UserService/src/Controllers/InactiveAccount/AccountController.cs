using Microsoft.AspNetCore.Mvc;
using UserService.Repositories;

namespace UserService.Controllers;

[Route("[controller]")]
[ApiController]
public class AccountController(InactiveAccountRepository repository): ControllerBase
{
    private readonly InactiveAccountRepository _repository = repository;


}