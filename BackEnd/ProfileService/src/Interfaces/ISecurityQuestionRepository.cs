using ProfileService.Models;
using ProfileService.Models.DTOs;

namespace ProfileService.Interfaces;

public interface ISecurityQuestionRepository
{
    public Task<SecurityQuestion> CreateSecurityQuestionAsync(SecurityQuestionDTO securityQuestionDTO);

    public Task<List<SecurityQuestion>> GetSecurityQuestionsByIdAsync(Guid userUuid);

    public Task<List<SecurityQuestion>> UpdateSecurityQuestionsAsync(List<SecurityQuestion> updatedSecurityQuestions);

    public Task DeleteSecurityQuestionAsync(Guid uuid);
}